Shader "Unlit/OutLine"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _OutLineColor("OutLine Color", Color) = (1,1,1,0)
        _OutLineWidth("OutLine Width", Range(0.01, 0.01)) = 0.01
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue" = "Transparent"}
        LOD 200

        cull front
        zwrite off
        CGPROGRAM
        #pragma surface surf NoLight vertex:vert noshadow noambient
        #pragma target 3.0

        float4 _OutLineColor;
        float _OutLineWidth;

        void vert(inout appdata_full v) {
            v.vertex.xyz += v.normal.xyz * _OutLineWidth;
        }

        struct Input
        {
            float4 color;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {

        }

        float4 LightingNoLight(SurfaceOutput s, float3 lightDir, float atten) {
            return float4(_OutLineColor.rgb, 1);
        }
        ENDCG

    }
    FallBack "Diffuse"
}