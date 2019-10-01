#ifndef REFRACTION
    #define REFRACTION
    
    sampler2D _PoiGrab;
    float _RefractionIndex;
    float _RefractionOpacity;
    float _RefractionChromaticAberattion;
    UNITY_DECLARE_TEX2D_NOSAMPLER(_RefractionOpacityMask); float4 _RefractionOpacityMask_ST;

    float3 refraction;
    float refractionOpacityMask;
    
    inline float4 Refraction(v2f i, float indexOfRefraction, float chromaticAberration)
    {
        float4 screenPos = i.screenPos;
        #if UNITY_UV_STARTS_AT_TOP
            float scale = -1.0;
        #else
            float scale = 1.0;
        #endif
        float halfPosW = screenPos.w * 0.5;
        screenPos.y = (screenPos.y - halfPosW) * _ProjectionParams.x * scale + halfPosW;
        #if SHADER_API_D3D9 || SHADER_API_D3D11
            screenPos.w += 0.00000000001;
        #endif
        float2 projScreenPos = (screenPos / screenPos.w).xy;
        float3 worldViewDir = normalize(UnityWorldSpaceViewDir(i.worldPos));
        float3 refractionOffset = ((((indexOfRefraction - 1.0) * mul(UNITY_MATRIX_V, float4(poiMesh.fragmentNormal, 0.0))) * (1.0 / (screenPos.z + 1.0))) * (1.0 - dot(poiMesh.fragmentNormal, worldViewDir)));
        float2 cameraRefraction = float2(refractionOffset.x, - (refractionOffset.y * _ProjectionParams.x));
        //return tex2D(_PoiGrab, (projScreenPos + cameraRefraction));
        
        float4 redAlpha = tex2D(_PoiGrab, (projScreenPos + cameraRefraction));
        float green = tex2D(_PoiGrab, (projScreenPos + (cameraRefraction * (1.0 - chromaticAberration)))).g;
        float blue = tex2D(_PoiGrab, (projScreenPos + (cameraRefraction * (1.0 + chromaticAberration)))).b;
        return float4(redAlpha.r, green, blue, redAlpha.a);
    }
    
    void calculateRefraction(v2f i)
    {
        refraction = Refraction(i, _RefractionIndex, _RefractionChromaticAberattion).rgb;
        refractionOpacityMask = UNITY_SAMPLE_TEX2D_SAMPLER(_RefractionOpacityMask, _MainTex, TRANSFORM_TEX(i.uv, _RefractionOpacityMask));
    }
    
    void applyRefraction(inout float4 finalColor)
    {
        finalColor.rgb = lerp(refraction * finalColor, finalColor, finalColor.a * alphaMask);
        finalColor.a = 1;
    }
    
#endif