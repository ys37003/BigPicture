using UnityEngine;

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
        return MathAssist.Instance().RandomVector3(foword.transform.position, 2.0f);
    }

    public Vector3 SetDestination_Foword(BaseGameEntity _entity, Group _group)
    {
        BaseGameEntity foword = _group.JobToEntity(eJOB_TYPE.FORWARD);
        if ( 0 == (int)Random.Range(0,2))
        {
            Vector3 position = MathAssist.Instance().RandomVector3(foword.transform.position, 20.0f);
            _group.DispatchMessageGroup(1.5f, _entity.ID, (int)eMESSAGE_TYPE.FLLOW_ME, position);
            return position;
        }
        return MathAssist.Instance().RandomVector3(foword.transform.position , 2.0f);
    }

    public Vector3 SetDestination_Partner(BaseGameEntity _entity, Group _group)
    {
        BaseGameEntity player = _group.TypeToEntity(eENTITY_TYPE.PLAYER);
        return MathAssist.Instance().RandomVector3(player.transform.position, 5.0f);
    }


    public Vector3 SetFomation_Foword(BaseGameEntity _entity , Transform _transform)
    {
        return _entity.transform.position + (_transform.forward * 3);
    }

    public Vector3 SetFomation_Dealer(BaseGameEntity _entity, Transform _transform)
    {
        dealerFomationNum *= -1;

        return _entity.transform.position + (_transform.right * 3 * dealerFomationNum );
    }

    public Vector3 SetFomation_Support(BaseGameEntity _entity, Transform _transform)
    {
        return _entity.transform.position + (_transform.forward * -3);
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