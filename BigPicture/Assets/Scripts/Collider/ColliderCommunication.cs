using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderCommunication : MonoBehaviour
{
    [SerializeField]
    private GameObject go;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Human" && other.GetComponent<Character>())
            go.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Human" && other.GetComponent<Character>())
            go.SetActive(false);
    }
}