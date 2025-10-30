Shader "Unlit/shdr_EmissiveGradient"
{
    Properties
    {
        _color("Color", Color) = (1,1,1,1)
        _exposure("Exposure", Float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Transparent"
            "Queue" = "Transparent"
        }
        LOD 100
        	
        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            
            float4 _color;
            float _exposure;
            
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
            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return _color * float4(_exposure,_exposure,_exposure,1.0-i.uv.x);
            }
            ENDCG
        }
    }
}
