Shader "Unlit/CrossShader"
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

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            uniform float _BlendFct;
            float4 _MainTex_ST;
            float _RotationSpeed;
            float _MovingSpeed;
            float _MovingAmplitude;
            float _FlashingSpeed;

            // https://www.youtube.com/watch?v=VzhxginBhdc
            // https://forum.unity.com/threads/transform-object-position-in-shader.591793/
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
                v.vertex = moveUpDown(v.vertex, _Time.y * _MovingSpeed, _MovingAmplitude);
                v.vertex = rotateAroundY(v.vertex, _Time * _RotationSpeed);
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f o) : SV_Target
            {
                float sinTime = sin(_Time.y * _FlashingSpeed);
                float gb = step(0.0, sinTime);
                fixed4 col = fixed4(1, gb, gb, 1);
                // fixed4 col = tex2D(_MainTex, o.uv);
                return col;
            }
            ENDCG
        }

        Pass
        {
            Tags {"LightMode"="ShadowCaster"}

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_shadowcaster
            #include "UnityCG.cginc"
            
            struct appdata {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f { 
                V2F_SHADOW_CASTER;
            };

            v2f vert(appdata v)
            {
                v2f o;
                TRANSFER_SHADOW_CASTER_NORMALOFFSET(o)
                return o;
            }

            float4 frag(v2f o) : SV_Target
            {
                SHADOW_CASTER_FRAGMENT(o)
            }
            ENDCG
        }
    }
}