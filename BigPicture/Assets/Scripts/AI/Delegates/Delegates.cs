using UnityEngine;
using UnityEngine.AI;

public delegate Vector3 DGSetDestination(BaseGameEntity _entity, Group _group);
public delegate Vector3 DGSetFomation(BaseGameEntity _entity, Transform _transform );
public delegate bool DGApproach(float _distance);
public class Delegates
{
    private static Delegates instance;
    int dealerFomationNum = 1;
    private Delegates()
    {

    }

    public static Delegates Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Delegates();
            }

            return instance;
        }
        private set { }
    }

    public Vector3 SetDestination_Nomal(BaseGameEntity _entity, Group _group)
    {
        BaseGameEntity foword = _group.JobToEntity(eJOB_TYPE.FORWARD);

        NavMeshHit hit;
        Vector3 destination = MathAssist.Instance().RandomVector3(foword.transform.position, 2.0f);
        if (false == NavMesh.SamplePosition(destination, out hit, 1, NavMesh.AllAreas))
        {
            Debug.Log("false");
            return Vector3.zero;
        }

        return hit.position;
    }

    public Vector3 SetDestination_Boss(BaseGameEntity _entity, Group _group)
    {
        Vector3 destination;
        destination = MathAssist.Instance().RandomVector3(_entity.transform.position, 10.0f);

        return destination;
    }

    public Vector3 SetDestination_Foword(BaseGameEntity _entity, Group _group)
    {
        Vector3 destination;
        NavMeshHit hit;

        BaseGameEntity foword = _group.JobToEntity(eJOB_TYPE.FORWARD);

        if ( 0 == (int)Random.Range(0,2))
        {
            destination = MathAssist.Instance().RandomVector3(foword.transform.position, 10.0f);
            NavMesh.SamplePosition(destination, out hit, 1, NavMesh.AllAreas);

            _group.DispatchMessageGroup(1.5f, _entity.ID, (int)eMESSAGE_TYPE.FLLOW_ME, destination);
            return hit.position;
        }

        destination = MathAssist.Instance().RandomVector3(foword.transform.position, 2.0f);
        if( false == NavMesh.SamplePosition(destination, out hit, 1, NavMesh.AllAreas))
        {
            Debug.Log("false");
            return Vector3.zero;
        }

        return hit.position;
    }

    public Vector3 SetDestination_Partner(BaseGameEntity _entity, Group _group)
    {
        BaseGameEntity player = _group.TypeToEntity(eENTITY_TYPE.PLAYER);
        Vector3 destination = MathAssist.Instance().RandomVector3(player.transform.position, 5.0f);
        NavMeshHit hit;

        if (false == NavMesh.SamplePosition(destination, out hit, 1, NavMesh.AllAreas))
        {
            Debug.Log("false");
            return Vector3.zero;
        }

        return hit.position;
    }


    public Vector3 SetFomation_Foword(BaseGameEntity _entity , Transform _transform)
    {
        Vector3 destination = _entity.transform.position + (_transform.forward * 3);

        NavMeshHit hit;
        if (false == NavMesh.SamplePosition(destination, out hit, 1, NavMesh.AllAreas))
        {
            Debug.Log("false");
            return Vector3.zero;
        }

        return hit.position;
    }

    public Vector3 SetFomation_Dealer(BaseGameEntity _entity, Transform _transform)
    {
        dealerFomationNum *= -1;

        Vector3 destination = _entity.transform.position + (_transform.right * 3 * dealerFomationNum);

        NavMeshHit hit;
        if (false == NavMesh.SamplePosition(destination, out hit, 1, NavMesh.AllAreas))
        {
            Debug.Log("false");
            return Vector3.zero;
        }
        return hit.position;

    }

    public Vector3 SetFomation_Partner(BaseGameEntity _entity, Transform _transform)
    {
        return Vector3.zero;
    }

    public Vector3 SetFomation_Support(BaseGameEntity _entity, Transform _transform)
    {
        Vector3 destination = _entity.transform.position + (_transform.forward * -3);

        NavMeshHit hit;
        if (false == NavMesh.SamplePosition(destination, out hit, 1, NavMesh.AllAreas))
        {
            Debug.Log("false");
            return Vector3.zero;
        }
        return hit.position;
    }

    public bool Approach_Foword(float _distance)
    {
        if(10.0f > _distance)
            return true;
        else
            return false;
    }

    public bool Approach_Dealer(float _distance)
    {
        if (10.0f > _distance)
            return true;
        else
            return false;
    }

    public bool Approach_Support(float _distance)
    {
        if (20.0f > _distance)
            return true;
        else
            return false;
    }
}