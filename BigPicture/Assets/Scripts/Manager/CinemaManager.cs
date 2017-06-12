using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemaManager : Singleton<CinemaManager>
{
    public GameObject CinemaPos;

    private void Awake()
    {
        if (CinemaPos == null)
            CinemaPos = GameObject.Find("CinemaPoses");
    }
}