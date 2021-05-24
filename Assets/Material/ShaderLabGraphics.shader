// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/ShaderLabGraphics"
{
    Properties 
    {
      _ColorStart ("ColorStart",Color) = (0,0,0,0)
      _ColorEnd ("ColorEnd",Color) = (1,1,1,0)
      _Gloss ("Gloss", Range(0,1)) = 1
    }

    SubShader
     {
        Tags{"RenderType"="Opaque"}
        Pass
        {
          CGPROGRAM 
          #pragma vertex vert // говорим имя у вертексного шейдера 
          #pragma fragment frag  // говорим имя пиксельного шейдера
          
          #include "UnityCG.cginc"
          #include "Lighting.cginc"
          #include "AutoLight.cginc"
          
           struct vertexInput 
           {
             float4 vertex:POSITION;
             float2 uv0:TEXCOORD0;
             float3 norm:NORMAL;
           };
           
           struct vertexOutput
           {
            float4 clipSpaceposition:SV_POSITION;
            float2 uv0:TEXCOORD0;
            float3 worldNormal:TEXCOORD1;
            float3 worldPos:TEXCOORD2;
            float3 normal:TEXCOORD3;
           };
           
           vertexOutput vert(vertexInput v) 
           {
             vertexOutput o; // возвращаемая структура 
             o.clipSpaceposition = UnityObjectToClipPos(v.vertex); // переводим координаты из пространства модели в проекционное 
             o.uv0 = v.uv0; // просто передаем uv координаты
             o.normal = UnityObjectToWorldNormal(v.norm);
             o.worldNormal = mul(unity_WorldToObject,v.norm);
             o.worldPos = mul(unity_ObjectToWorld,v.vertex); // переводим координаты из пространства модели в мировое 
             return o;
           }
           
           
           
          uniform float4 _ColorStart;
          uniform float4 _ColorEnd;
          uniform float _Gloss;
          
           fixed4 frag(vertexOutput v):SV_Target
           {
            vertexOutput o;
            float3 N = normalize(v.normal);
            float3 L = _WorldSpaceLightPos0.xyz; //Направление освещения
            
            //Освещение
            //Прямое освещение (DirectLight)
            float3 lambert = saturate(dot(N,L));
            float3 diffuseLight = lambert * _LightColor0.xyz;
            
            //Отраженный свет (Specular lighting)
            float3 V = normalize(_WorldSpaceCameraPos - v.worldPos);
            //float3 R = reflect(-L, N);
            float H = normalize(L + V);
            
            float3 specularLight = saturate(dot(H, N)) * (lambert > 0); //Blinn-Phong
            
            float specularExponent = exp2(_Gloss * 11) + 2;
            specularLight = pow(specularLight, specularExponent);
            specularLight *= _LightColor0.xyz;
            
            float4 color = lerp(_ColorStart, _ColorEnd, v.uv0.x);
            
            /*//Рассеянный свет (Ambient Light)
            float3 ambientLight = float3(0.1, 0.1, 0.1);
            
            //Объединение цветов
            float3 diffuseLight = ambientLight + diffuseLight;
            float3 finalSurfaceColor = diffuseLight * _Color.rgb;*/
            
            return fixed4(diffuseLight * color + specularLight ,1.0); 
           } 
          ENDCG  
    
          }
     }
        Fallback "Diffuse"
}