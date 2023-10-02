Shader "Unlit/pppp 1"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Ambient ("AmbTexture", 2D) = "white" {}
        _upoff ("float", float) = 2
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
    

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 wpos : TEXCOORD1;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex,aReflec1,_Reflec;
            float4 _MainTex_ST;
            

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.wpos = mul(unity_ObjectToWorld, v.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                
                
                fixed4 col = tex2D(_MainTex,i.uv );
                
               

                return col;
            }
             ENDCG
            }
        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha 
            
            
            //mult bu ambient color.
            CGPROGRAM
           
            #pragma vertex vert
            #pragma fragment frag
          

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float2 uv1 : TEXCOORD2;
                float4 wpos : TEXCOORD1;
                float4 vertex : SV_POSITION;
            };

            sampler2D _Reflec,aReflec1, _MainTex;
            float4 _Reflec_ST, _MainTex_ST; 
            float3 startviewport ;
            float  yoff;
            
            

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _Reflec);
                o.uv1 = TRANSFORM_TEX(v.uv, _MainTex);
                o.wpos = mul(unity_ObjectToWorld, v.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                
                
                //fixed4 col = tex2D(_MainTex,float2(i.uv.x,i.uv.y) );
                
                fixed4 col1 = float4(0,0,0,0);
                float4 col2  = tex2D(aReflec1,i.uv);
                float col2lum = dot(col2.xyz, float3(0.21 ,0.72, 0.07));
                float4 color = tex2D(_MainTex,i.uv1); 
                yoff = startviewport.y;
                //uv is vector
                if ( col2lum  > 0.1)
                {
                    if (yoff > i.uv.y)
                {
                    float distref = yoff - i.uv.y ;
                    float yoffset = distref + yoff;

                     col1 = tex2D(_Reflec,float2(i.uv.x,yoffset)); 
                    
                }
                }
                

                return col1*col2;
            }
            ENDCG




            
           
        }
    }
}
