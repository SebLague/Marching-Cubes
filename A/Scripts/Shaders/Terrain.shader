Shader "Custom/Terrain"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
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

        struct Input
        {
            float2 uv_MainTex;
            float3 worldPos;
            float3 worldNormal;
        };

        float boundsY;
        float normalOffsetWeight;

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        sampler2D ramp;

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            float h = smoothstep(-boundsY/2, boundsY/2, IN.worldPos.y + IN.worldNormal.y * normalOffsetWeight);
            float3 tex = tex2D(ramp, float2(h,.5));
            o.Albedo = tex;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
           
        }
        ENDCG
    }
    FallBack "Diffuse"
}
