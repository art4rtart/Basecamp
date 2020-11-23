Shader "Unlit/CloudFlowShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _FlowSpeed ("Flow Speed", Range(-1,1)) = 0
        _Tint ("Tint Color", Color) = (1,1,1,1)
        _Gradient ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        Blend OneMinusDstColor One // Soft Additive
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
            float _FlowSpeed;
            float4 _Tint;
            sampler2D _Gradient;
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                float2 newUV = i.uv;
                newUV.x += _Time.y*_FlowSpeed;
                fixed4 col = tex2D(_MainTex, newUV);
                col.rgb *= _Tint.rgb;
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col * tex2D(_Gradient, i.uv);
            }
            ENDCG
        }
    }
}
