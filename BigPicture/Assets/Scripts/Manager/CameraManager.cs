using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 카메라 depth 순서
/// </summary>
public enum eCAMERA
{
    Main,
    Cinema,
    HUD,
    UI3D,
    UI2D,
}

public class CameraManager : Singleton<CameraManager>
{
    private List<Camera> cameraList;

    private void Awake()
    {
        cameraList = new List<Camera>(Camera.allCameras);
        cameraList.Sort((a, b) =>
        {
            return a.depth.CompareTo(b.depth);
        });
    }

    public Camera GetCamera(eCAMERA camera)
    {
        return cameraList[(int)camera];
    }
}