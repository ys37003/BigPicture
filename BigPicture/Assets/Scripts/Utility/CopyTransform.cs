using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyTransform : MonoBehaviour
{
    public Transform Target;
    public bool CopyPostion;
    public bool CopyRotation;
    public bool CopyScale;

    private void LateUpdate()
    {
        if (CopyPostion)
        {
            transform.position = Target.position;
        }

        if(CopyRotation)
        {
            transform.rotation = Target.rotation;
        }

        if(CopyScale)
        {
            transform.localScale = Target.localScale;
        }
    }
}