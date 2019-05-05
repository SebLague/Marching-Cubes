Shader "Custom/Sea World"
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
        float4 params;


        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        sampler2D ramp;

        void surf (Input IN, inout SurfaceOutputStandard o)
        {

            float h = ((IN.worldPos.y + pow(IN.worldNormal.y*.5+.5,params.z) * params.x) / params.y)%1;
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
