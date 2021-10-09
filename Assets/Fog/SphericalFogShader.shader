Shader "Unlit/SphericalFogShader"
{
    Properties
    {
        _FogCenter("Fog Center", Vector) = (0,0,0,0.5)
        _FogColor("Fog Color", Color) = (1,1,1,1)
        _Density("Density", Range(0.0, 1.0)) = 0.5
    }
    SubShader
    {
        // Tags { "RenderType"="Transparent" }
        // Tags { "RenderType"="Opaque" }
        Tags { "Queue"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off 
        Lighting Off 
        ZWrite Off
        ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct vertexInput
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct vertexOutput
            {
                float3 view : TEXCOORD0;
                float4 pos : SV_POSITION;
            };

            float4 _FogCenter;
            fixed4 _FogColor;
            float _Density;
            sampler2D _CamDepthTexture;

            float3 raySphereIntersect(float3 sphereCenter, float3 sphereRadius, 
                                      float3 cameraPosition, float3 viewDirection) {
                float3 L = cameraPosition - sphereCenter;
                float a = dot(viewDirection, viewDirection);
                float b = 2 * dot(viewDirection, L);
                float c = dot(L, L) - sphereRadius * sphereRadius;
                float delta = b * b - 4 * a * c;
                if (delta <= 0.0f) {
                    return 0;
                }
                float deltaSqrt = sqrt(delta);
                float t0 = (-b + deltaSqrt) / (2 * a);
                float t1 = (-b - deltaSqrt) / (2 * a);

                if (t0 > t1) {
                    float temp = t0;
                    t0 = t1;
                    t1 = temp;
                }

                if (t0 < 0) {
                    t0 = 0;
                    if (t0 < 0) {
                        return 0;
                    }
                }
                float3 position = cameraPosition + viewDirection * t0;
                return position;
            }

            float CalculateFogIntensity(float3 sphereCenter, 
                                        float3 sphereRadius, 
                                        float density,
                                        float3 cameraPosition,
                                        float3 viewDirection) {
                float centerValue = 1; // the value of the most thickness fog
                float clarity = 1; // how clear the fog is, clarity = 1 => fully clear 
                float3 position = raySphereIntersect(sphereCenter, sphereRadius, 
                                                     cameraPosition, viewDirection);
                // centerValue * 1 - how far we are inside the fog / radius
                // saturate make the value between 0-1
                float val = saturate(centerValue * (1 - length(position)/sphereRadius));
                float fog_amount = saturate(val * density);
                clarity *= (1 - fog_amount);
                return 1 - clarity;
            }

            vertexOutput vert (vertexInput v)
            {
                vertexOutput o;
                float4 wPos = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos(v.vertex);
                o.view = wPos.xyz - _WorldSpaceCameraPos; // view direction
                return o;
            }

            fixed4 frag (vertexOutput o) : SV_Target
            {
                half4 color = half4(1,1,1,1);
                float3 viewDir = normalize(o.view);

                float centerValue = 1; // the value of the most thickness fog
                float clarity = 1; // how clear the fog is, clarity = 1 => fully clear 
                float3 position = raySphereIntersect(_FogCenter.xyz, _FogCenter.w, 
                                                     _WorldSpaceCameraPos, viewDir);
                // centerValue * 1 - how far we are inside the fog / radius
                // saturate make the value between 0-1
                float val = saturate(centerValue * (1 - length(position)/_FogCenter.w));
                float fog_amount = saturate(val * _Density);
                clarity *= (1 - fog_amount);

                color.rgb = _FogColor.rgb;
                color.a = 1 - clarity;
                return color;
            }
            ENDCG
        }
    }
}

