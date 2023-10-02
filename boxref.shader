Shader "Unlit/boxref"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _maper("cuber", Cube) = "" {}
        _a("a", float) = 2
        _rough("rough", float) = 2
        _amb("ab", color) = (1,0,0,1)
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
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float3 vertWorld : TEXCOORD1;
                float3 normal : TEXCOORD2;
                float3 toDir : TEXCOORD3;
               
  
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST,_amb;
            float3 BBoxmin, BBoxmax;
            float3 BBoxcent;
            samplerCUBE _maper;
            float _a, _rough;
            

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                float4 world = mul(unity_ObjectToWorld,v.vertex);
                float3 dir = world - _WorldSpaceCameraPos;
                o.vertWorld = world;
                o.toDir= dir;
                o.normal= UnityObjectToWorldNormal(v.normal);
                
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                //getrelf from cube and then readd aabb
                float3 dir = normalize(i.toDir);
                float3 n = normalize(i.normal);
                float3 r = reflect(dir,n);
                
                float3 wrldpos =  i.vertWorld;
                float3 intersectmaxpoints = (BBoxmax - wrldpos )/ r;
                float3 intersectminpoints = (BBoxmin - wrldpos )/ r;
                float3 largestparm = max(intersectmaxpoints, intersectminpoints);
                float smalltparm = min(min(largestparm.x, largestparm.y), largestparm.z);
                float3 intersectws = wrldpos + smalltparm * r; 
                float3 reflcor = intersectws - BBoxcent; 
                
                fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
                float4 refl = texCUBE( _maper,reflcor);
                return _amb+(refl+_rough) * (col * _a);
            }
            ENDCG
        }
    }
}
