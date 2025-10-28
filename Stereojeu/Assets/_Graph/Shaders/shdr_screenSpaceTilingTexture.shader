Shader "Unlit/shdr_screenSpaceTilingTexture"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _exposure("Exposure", Float) = 1
        _color("Color", Color) = (0,0,0,0)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
        Cull Front   
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
            float4 _color;
            float _exposure;
            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                
                o.scrPos =  v.vertex;
                o.scrPos =  UnityObjectToViewPos(v.vertex).xyz;
                o.scrPos.xy/=o.scrPos.z;
                //o.viewPos = UnityObjectToViewPos(v.vertex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // fragment shader
                //fixed4 col = tex2D(_MainTex, i.viewPos.xy);
                
                // sample the texture
                
                fixed4 col = float4(i.scrPos,1);//tex2D(_MainTex, i.scrPos.xy*5) * _exposure;
                col = tex2D(_MainTex, i.scrPos.xy) * float4(_exposure.xxx,1) * _color; 
                return col;
            }
            ENDCG
        }
    }
}
