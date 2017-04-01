using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathAssist
{
    private static MathAssist instance;

    private MathAssist()
    {

    }

    public static MathAssist Instance()
    {
        if (instance == null)
        {
            instance = new MathAssist();
        }
        return instance;
    }

    public Vector3 RandomVector3(Vector3 _vector3, float _range)
    {
        Vector3 vector3 = new Vector3(Random.Range(_vector3.x - _range, _vector3.x + _range),
                                      _vector3.y,
                                      Random.Range(_vector3.z - _range, _vector3.z + _range));

        return vector3;
    }
}
