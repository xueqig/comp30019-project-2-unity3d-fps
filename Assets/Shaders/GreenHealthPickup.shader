Shader "Unlit/GreenHealthPickup"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _RotationSpeed ("Rotation Speed", Range(0.0, 100.0)) = 50.0
        _MovingAmplitude ("Moving Amplitude", Range(0.0, 0.1)) = 0.05
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
                float2 uv : TEXCOORD0;
                float4 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 worldVertex : TEXCOORD1;
                float3 worldNormal : TEXCOORD2;
            };

            sampler2D _MainTex;
            uniform float _BlendFct;
            float4 _MainTex_ST;
            float _RotationSpeed;
            float _MovingSpeed;
            float _MovingAmplitude;
            float _FlashingSpeed;

            float4 rotateAroundY(float4 vertex, float speed) {
                float4x4 rotationMatrix;
                rotationMatrix[0] = float4(cos(speed), 0, sin(speed), 0);
                rotationMatrix[1] = float4(0, 1, 0, 0);
                rotationMatrix[2] = float4(-sin(speed), 0, cos(speed), 0);
                rotationMatrix[3] = float4(0, 0, 0, 1);
                return mul(rotationMatrix, vertex);
            }

            float4 moveUpDown(float4 vertex, float speed, float amplitude) {
                float4 displacement = float4(0.0f, sin(speed) * amplitude, 0.0f, 0.0f);
                return vertex + displacement;
            }

            v2f vert (appdata v)
            {
                v.vertex += float4(0,0.3,0,0);
                v.vertex = moveUpDown(v.vertex, _Time.y * _MovingSpeed, _MovingAmplitude);
                // v.vertex = rotateAroundY(v.vertex, _Time.y * _RotationSpeed);
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                float4 worldVertex = mul(unity_ObjectToWorld, v.vertex);
                float3 worldNormal = normalize(mul(transpose((float3x3)unity_WorldToObject), v.normal.xyz));

                o.worldVertex = worldVertex;
                o.worldNormal = worldNormal;
                return o;
            }

            fixed4 frag (v2f v) : SV_Target
            {
                // float sinTime = sin(_Time.y * _FlashingSpeed);
                // float gb = step(0.0, sinTime);
                // fixed4 col = fixed4(1, gb, gb, 1);
                // // fixed4 col = tex2D(_MainTex, o.uv);
                // return col;

                float sinTime = sin(_Time.y * _FlashingSpeed);
                float rb = step(0.0, sinTime);
                fixed4 col = fixed4(rb, 1, rb, 1);

                // Our interpolated normal might not be of length 1
                float3 interpNormal = normalize(v.worldNormal);

                // Calculate ambient RGB intensities
                float Ka = 1;
                float3 amb = col * UNITY_LIGHTMODEL_AMBIENT.rgb * Ka;

                // Calculate diffuse RBG reflections, we save the results of L.N because we will use it again
                // (when calculating the reflected ray in our specular component)
                float fAtt = 1;
                float Kd = 1;
                float3 L = _WorldSpaceLightPos0; // Q6: Using built-in Unity light data: _WorldSpaceLightPos0.
                                                 // Note that we are using a *directional* light in this instance,
                                                 // so _WorldSpaceLightPos0 is actually a direction rather than
                                                 // a point. Therefore there is no need to subtract the world
                                                 // space vertex position like in our point-light shaders.
                float LdotN = dot(L, interpNormal);
                float3 dif = fAtt * _LightColor0 * Kd * col.rgb * saturate(LdotN); // Q6: Using built-in Unity light data: _LightColor0

                // Calculate specular reflections
                float Ks = 1;
                float specN = 5; // Values>>1 give tighter highlights
                float3 V = normalize(_WorldSpaceCameraPos - v.worldVertex.xyz);
                // Using Blinn-Phong approximation:
                specN = 25; // We usually need a higher specular power when using Blinn-Phong
                float3 H = normalize(V + L);
                float3 spe = fAtt * _LightColor0 * Ks * pow(saturate(dot(interpNormal, H)), specN); // Q6: Using built-in Unity light data: _LightColor0

                // Combine Phong illumination model components
                float4 returnColor = float4(0.0f, 0.0f, 0.0f, 0.0f);
                returnColor.rgb = amb.rgb + dif.rgb + spe.rgb;
                returnColor.a = col.a;

                return returnColor;
            }
            ENDCG
        }
    }
}
