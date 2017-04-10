using UnityEngine;

public delegate Vector3 DGSetDestination(BaseGameEntity _entity, Group group, Vector3 _position);
public delegate void DGToWalk(BaseGameEntity _entity);
public class Delegates
{
    private static Delegates instance;

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
        if( 0 == (int)Random.Range(0,3))
        {
            Vector3 position = MathAssist.Instance().RandomVector3(_position, 20.0f);
            group.DispatchMessageGroup(1, _entity.ID, (int)eMESSAGE_TYPE.FLLOWME, position);
            return position;
        }
        return MathAssist.Instance().RandomVector3(_position, 2.0f);
    }
}