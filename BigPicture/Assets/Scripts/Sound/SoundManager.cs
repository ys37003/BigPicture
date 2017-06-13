using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField]
    List<AudioSource> townNoise;

    [SerializeField]
    GameObject playerPos;
    [SerializeField]
    private float value;

    [SerializeField]
    float Distance;

    // Update is called once per frame
    void Update()
    {
        VolumeHandle();
    }

    void VolumeHandle()
    {
        for (int i = 0; i < townNoise.Count; ++i)
        {
            float distance = Vector3.Distance(playerPos.transform.position, townNoise[i].transform.position);

            Distance = distance;
            if (60 < distance)
            {
                townNoise[i].volume = 0;
            }
            else
            {
                townNoise[i].volume = (100 - distance) * value;
            }
        }
    }
}