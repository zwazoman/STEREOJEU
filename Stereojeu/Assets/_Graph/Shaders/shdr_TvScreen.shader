Shader "Custom/shdr_TvScreen"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _NoiseTexture ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _Emission ("Exposure",Float) = 1.0
        _OnOffRatio ("On Off Ratio",Range(0,1)) = 1.0
        _whiteNoiseTiling ("White Noise Tiling",Float) = 1.0
        _StripesTiling ("Stripes Tiling",Vector) = (1.0,1.0,1.0,1.0)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _NoiseTexture;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_NoiseTexture;
        };

        half _Glossiness;
        half _OnOffRatio;
        half _Metallic;
        half _Emission;
        float _whiteNoiseTiling;
        float2 _StripesTiling;

        /*
        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)*/

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // screen
            float truncatedTime = trunc(_Time.x*400)*.0025;
            float stripes = saturate(tex2D (_NoiseTexture, IN.uv_NoiseTexture * _StripesTiling + -40.1245867849* truncatedTime).y);
            float spripesNP = (stripes*2-1);
            spripesNP = lerp(spripesNP, spripesNP*spripesNP*spripesNP, .5 );
            fixed4 screen = tex2D (_MainTex, IN.uv_MainTex+float2(spripesNP*.01,0));
            screen = screen*(stripes*.2+.8) + stripes*stripes*.05;
            
            //noise
            float whiteNoise = tex2D (_NoiseTexture, IN.uv_NoiseTexture*_whiteNoiseTiling + -40.1245867849* truncatedTime).r;
            fixed4 noise =  whiteNoise*whiteNoise*1.5;

            //mix
            float grad = abs(IN.uv_MainTex.y-.5)*2;
            bool mask = grad<_OnOffRatio;
            float exposureMultiplier = (1.0-abs(grad-_OnOffRatio));
            exposureMultiplier*=exposureMultiplier*sign(exposureMultiplier);
            exposureMultiplier = exposureMultiplier*.5+.5;
            exposureMultiplier = exposureMultiplier * (1-abs(_OnOffRatio*2-1));
            fixed4 c = lerp(noise ,screen,mask) * (1+exposureMultiplier*.5)+exposureMultiplier*.5;
            
            o.Albedo = c.rgb;
            o.Emission = c.rgb * _Emission;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
