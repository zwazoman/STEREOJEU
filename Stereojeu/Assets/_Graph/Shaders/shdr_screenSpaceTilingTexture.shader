Shader "Unlit/shdr_screenSpaceTilingTexture"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _exposure("Exposure", Float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
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
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float4 viewPos : TEXCOORD1;
                float3 scrPos : TEXCOORD2;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            
            float _exposure;
            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.scrPos =  ComputeScreenPos(v.vertex);
                //o.viewPos = UnityObjectToViewPos(v.vertex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {

                
                // fragment shader
                //fixed4 col = tex2D(_MainTex, i.viewPos.xy);
                
                // sample the texture
                
                fixed4 col = tex2D(_MainTex, i.scrPos.xz) * _exposure;
                return col;
            }
            ENDCG
        }
    }
}
