Shader "Unlit/NebulaShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        [HDR]_TintOne ("Tint One", Color) = (1,1,1,1)
        [HDR]_TintTwo ("Tint Two", Color) = ( 1,1,1,1)
        [HDR]_TintThree ("Tint Three", Color) = ( 1,1,1,1)
        _Gradient ("Texture", 2D ) =  "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha // Blending
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _TintOne;
            float4 _TintTwo;
            float4 _TintThree;
            sampler2D _Gradient;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }


            
            float random (in float2 _st) {
                return frac(sin(dot(_st.xy,
                                    float2(12.9898f,78.233f)))*
                    43758.5453123);
            }

            // Based on Morgan McGuire @morgan3d
            // https://www.shadertoy.com/view/4dS3Wd
            float noise (in float2 _st) {
                float2 i = floor(_st);
                float2 f = frac(_st);

                // Four corners in 2D of a tile
                float a = random(i);
                float b = random(i + float2(1.0f, 0.0f));
                float c = random(i + float2(0.0f, 1.0f));
                float d = random(i + float2(1.0f, 1.0f));

                float2 u = f * f * (3.0f - 2.0f * f);

                return lerp(a, b, u.x) +
                        (c - a)* u.y * (1.0 - u.x) +
                        (d - b) * u.x * u.y;
            }

            #define NUM_OCTAVES 5

            float fbm ( in float2 _st) {
                float v = 0.0;
                float a = 0.5;
                float2 shift = float2(100.0f,100.0f);
                // Rotate to reduce axial bias
                float2x2 rot = float2x2(cos(0.5f), sin(0.5f),
                                -sin(0.5f), cos(0.5f));
                for (int i = 0; i < NUM_OCTAVES; ++i) {
                    v += a * noise(_st);
                    _st =  mul(_st,rot) * 2.0f  + shift;
                    a *= 0.5f;
                }
                return v;
            }

            

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                float4 col = tex2D(_MainTex, i.uv);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);

                float2 st = i.uv;
                st.y *=5;

                float2 q = float2(0.f,0.f);
                q.x = fbm( st + 0.00f*_Time.y);
                q.y = fbm( st + float2(1.0f,1.0f));

                float2 r = float2(0.f,0.f);
                r.x = fbm( st + 1.0f*q + float2(1.7f,9.2f)+ 0.15f*_Time.y );
                r.y = fbm( st + 1.0f*q + float2(8.3f,2.8f)+ 0.126f*_Time.y);

                float f = fbm(st+r);

                col.rgb = lerp(col.rgb,
                            _TintOne.rgb,
                            clamp((f*f)*4.0f,0.0f,1.0f));

                col.rgb = lerp(col.rgb,
                            _TintTwo.rgb,
                            clamp(length(q),0.0f,1.0f));

                col.rgb = lerp(col.rgb,
                            _TintThree.rgb,
                            clamp(length(r.x),0.0f,1.0f));

                return float4((f*f*f+.6f*f*f+.5f*f)*col.rgb,col.a) * tex2D(_Gradient, i.uv);
            }
            ENDCG
        }
    }
}
