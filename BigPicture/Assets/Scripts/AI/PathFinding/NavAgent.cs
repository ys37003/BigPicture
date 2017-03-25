using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavAgent : MonoBehaviour {

    public Vector3 m_target;

    NavMeshAgent m_agent;
    // Use this for initialization
    void Start()
    {
        m_agent = this.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.zero != m_target)
            m_agent.SetDestination(m_target);
    }
    public Vector3 target
    {
        get { return m_target; }
        set { m_target = value; }
    }

    public void Clear()
    {
        m_target = this.gameObject.transform.position;
        m_agent.SetDestination(m_target);
    }
}
