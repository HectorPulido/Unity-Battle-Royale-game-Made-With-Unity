Shader "Yurowm/SkinShader" {
    Properties {
      _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader {
      Tags { "RenderType" = "Opaque" }
      Cull Back
      CGPROGRAM
      #pragma surface surf Lambert
      
      struct Input {
          float2 uv_MainTex;
          float3 viewDir;
      };
      sampler2D _MainTex;
      
      void surf (Input IN, inout SurfaceOutput o) {
          half rim = 1.0 - saturate(dot (normalize(IN.viewDir), o.Normal));
          float3 c = tex2D (_MainTex, IN.uv_MainTex);
          o.Albedo = c.rgb/2;
          o.Emission = lerp(0.2, 1.0, pow (rim, 2)) * c.rgb;
      }
      ENDCG
    } 
    Fallback "Diffuse"
  }