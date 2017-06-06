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
    UI2D,
    UI3D,
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

        Debug.Log("d");
    }

    public Camera GetCamera(eCAMERA camera)
    {
        return cameraList[(int)camera];
    }
}