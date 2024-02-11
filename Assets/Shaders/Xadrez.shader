Shader "Custom/CheckerShader"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" { }
        _Color ("Main Color", Color) = (.5,.5,.5,1)
        _Scale ("Checker Scale", Range(1, 10)) = 2
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        CGPROGRAM
        #pragma surface surf Lambert

        struct Input
        {
            float2 uv_MainTex;
        };

        fixed _Scale;

        void surf(Input IN, inout SurfaceOutput o)
        {
            fixed checker = floor(fmod(IN.uv_MainTex.x / _Scale, 2)) != floor(fmod(IN.uv_MainTex.y / _Scale, 2));
            fixed3 c = checker ? fixed3(0, 0, 0) : fixed3(1, 1, 1);

            o.Albedo = c;
        }
        ENDCG
    }

    FallBack "Diffuse"
}
