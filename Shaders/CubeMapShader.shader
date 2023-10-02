Shader "Unlit/lalala"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Gloss ("Gloss", range(0,1)) = 1
        _maper ("Cube", Cube ) = "" {}
        amount ("amount", float ) = 1
        amount1 ("amount1", float ) = 1
        _FresnelNormalStrength ("FresnelNormalStrength", float ) = 1
        _FresnelShininess ("Shininess", float ) = 1
        _FresnelBias ("Bias", float ) = 1
        _FresnelStrength ("Strength", float ) = 1
        
        _amb ("amb", color ) = (1,0,0,1)
        
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

       
        
        
        
        
        Pass
        {
            Tags{"LightMode" = "ForwardBase"}
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
          
            #include "CubeMapDoc.cginc"
            ENDCG
        }
        
        Pass
        {
            Tags{"LightMode" = "ForwardAdd"}
            Blend One One
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fwadd
            // make fog work
          
            #include "CubeMapDoc.cginc"
            ENDCG
        }
        
        
        
        
        
    }
}
