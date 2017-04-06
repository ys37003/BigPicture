using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavAgent : MonoBehaviour
{
    public Vector3 destination;

    NavMeshAgent m_agent;
    // Use this for initialization
    void Start()
    {
        m_agent = this.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.zero != destination)
            m_agent.SetDestination(destination);
    }

    public Vector3 target
    {
        get { return destination; }
        set { destination = value; }
    }

    public void Clear()
    {
        destination = this.gameObject.transform.position;
        m_agent.SetDestination(destination);
    }
}