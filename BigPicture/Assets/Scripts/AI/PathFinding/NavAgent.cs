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
        //if(Vector3.zero != destination)
        //    m_agent.SetDestination(destination);
    }

    public void SetSpeed(float _speed)
    {
        m_agent.speed = _speed;
    }
    public Vector3 GetDestination()
    {
        return destination;
    }

    public void SetDestination(Vector3 _destiantion)
    {
        destination = _destiantion;
    }

    public float GetDistance()
    {
        return Vector3.Distance(this.transform.position, this.GetDestination());
    }

    public void Clear()
    {
        destination = this.gameObject.transform.position;
        m_agent.SetDestination(destination);
    }

    public bool IsArrive()
    {
        if (this.GetDestination() == Vector3.zero)
            return true;

        if (0.5f > Vector3.Distance(this.transform.position, this.GetDestination()))
            return true;

        return false;
    }

    public IEnumerator MoveToTarget()
    {
        while (false == this.IsArrive())
        {
            if (Vector3.zero != destination)
                m_agent.SetDestination(destination);

            Debug.DrawLine(transform.position, this.GetDestination(), Color.red);
            yield return null;
        }
    }
}