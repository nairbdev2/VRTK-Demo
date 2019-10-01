#ifndef POI_RANDOM
    #define POI_RANDOM
    
    float _EnableRandom;
    float m_start_Angle;
    uint _AngleType;
    float3 _AngleForwardDirection;
    float _CameraAngleMin, _CameraAngleMax;
    float _ModelAngleMin, _ModelAngleMax;
    float _AngleMinAlpha;
    
    float ApplyAngleBasedRendering(float3 modelPos)
    {
        half cameraAngleMin = _CameraAngleMin / 180;
        half cameraAngleMax = _CameraAngleMax / 180;
        half modelAngleMin = _ModelAngleMin / 180;
        half modelAngleMax = _ModelAngleMax / 180;
        half3 cameraToModelDirection = normalize(modelPos - getCameraPosition());
        half3 modelForwardDirection = normalize(mul(unity_ObjectToWorld, normalize(_AngleForwardDirection)));
        half cameraLookAtModel = remapClamped(.5 * dot(cameraToModelDirection, getCameraForward()) + .5, cameraAngleMax, cameraAngleMin, 0, 1);
        half modelLookAtCamera = remapClamped(.5 * dot(-cameraToModelDirection, modelForwardDirection) + .5, modelAngleMax, modelAngleMin, 0, 1);
        if (_AngleType == 0)
        {
            return max(cameraLookAtModel, _AngleMinAlpha);
        }
        else if(_AngleType == 1)
        {
            return max(modelLookAtCamera, _AngleMinAlpha);
        }
        else if(_AngleType == 2)
        {
            return max(cameraLookAtModel * modelLookAtCamera, _AngleMinAlpha);
        }
        return 1;
    }
    
#endif