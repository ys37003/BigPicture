using UnityEngine;

public delegate Vector3 DGSetDestination(BaseGameEntity _entity, Group group, Vector3 _position);
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

    public Vector3 SetDestination_Nomal(BaseGameEntity _entity, Group group, Vector3 _position)
    {
        return MathAssist.Instance().RandomVector3(_position, 2.0f);
    }

    public Vector3 SetDestination_Foword(BaseGameEntity _entity, Group group, Vector3 _position)
    {
        if( 0 == (int)Random.Range(0,2))
        {
            Vector3 position = MathAssist.Instance().RandomVector3(_position, 20.0f);
            group.DispatchMessageGroup(1, _entity.ID, (int)eMESSAGE_TYPE.FLLOW_ME, position);
            return position;
        }
        return MathAssist.Instance().RandomVector3(_position, 2.0f);
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