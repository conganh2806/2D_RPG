using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundCameraPos : CinemachineExtension
{
    [SerializeField] private float pixelsPerUnit;


    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if(stage == CinemachineCore.Stage.Body)
        {
            
            Vector3 pos = state.FinalPosition;
            // 7
            Vector3 pos2 = new Vector3(Round(pos.x) , Round(pos.y), pos.z);

            state.PositionCorrection -= pos2 - pos;
            
        }
    }


    private float Round(float x)
    {
        return Mathf.Round(x * pixelsPerUnit) / pixelsPerUnit;
    }
}
