Shader "Unlit/RedHealthPickup"
{
    Properties
    {
        _MovingDistance ("Moving Distance", Range(0.0, 0.1)) = 0.05
        _MovingSpeed ("Moving Speed", Range(0.0, 10.0)) = 5
        _FlashingSpeed ("Flashing Speed", Range(0.0, 5.0)) = 2.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #include "UnityLightingCommon.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float4 normal : NORMAL;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float4 worldVertex : TEXCOORD0;
                float3 worldNormal : TEXCOORD1;
            };

            float _MovingSpeed;
            float _MovingDistance;
            float _FlashingSpeed;

            float4 moveUpDown(float4 vertex, float speed, float distance) 
            {
                float4 displacement = float4(0.0f, sin(_Time.y * speed) * distance, 0.0f, 0.0f);
                return vertex + displacement;
            }

            fixed4 flashing(float speed) 
            {
                float gb = step(0.0, sin(_Time.y * speed));
                fixed4 col = fixed4(1, gb, gb, 1);
                return col;
            }

            fixed4 phongShading(v2f o, fixed4 col) 
            {
                float3 interpNormal = normalize(o.worldNormal);

                // Calculate ambient RGB intensities
                float Ka = 1;
                float3 amb = col * UNITY_LIGHTMODEL_AMBIENT.rgb * Ka;

                // Calculate diffuse RGB reflections
                float fAtt = 1;
                float Kd = 1;
                float3 L = _WorldSpaceLightPos0; 
                float LdotN = dot(L, interpNormal);
                float3 dif = fAtt * _LightColor0 * Kd * col.rgb * saturate(LdotN);

                // Calculate specular reflections
                float Ks = 1;
                float specN = 25; 
                float3 V = normalize(_WorldSpaceCameraPos - o.worldVertex.xyz);
                // Using Blinn-Phong approximation:
                float3 H = normalize(V + L);
                float3 spe = fAtt * _LightColor0 * Ks * pow(saturate(dot(interpNormal, H)), specN);

                // Combine Phong illumination model components
                float4 returnColor = float4(0.0f, 0.0f, 0.0f, 0.0f);
                returnColor.rgb = amb.rgb + dif.rgb + spe.rgb;
                returnColor.a = col.a;

                return returnColor;
            }

            v2f vert (appdata v)
            {
                // Fix the position of the pickup
                v.vertex += float4(0, 0.3, 0, 0);

                // Apply up and down movement
                v.vertex = moveUpDown(v.vertex, _MovingSpeed, _MovingDistance);
                
                v2f o;

                // Convert Vertex position and corresponding normal into world coords.
                float4 worldVertex = mul(unity_ObjectToWorld, v.vertex);
                float3 worldNormal = normalize(mul(transpose((float3x3)unity_WorldToObject), v.normal.xyz));

                // Transform vertex in world coordinates to clip space coordinates
				o.vertex = UnityObjectToClipPos(v.vertex);

                o.worldVertex = worldVertex;
                o.worldNormal = worldNormal;
                return o;
            }

            fixed4 frag (v2f o) : SV_Target
            {
                fixed4 col = flashing(_FlashingSpeed);
                fixed4 returnColor = phongShading(o, col);
                return returnColor;
            }
            ENDCG
        }
    }
}
