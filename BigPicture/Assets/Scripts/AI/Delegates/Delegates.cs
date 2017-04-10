using UnityEngine;

public delegate Vector3 DGSetDestination(Vector3 _position);
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

    public Vector3 SetDestination_Nomal(Vector3 _position)
    {
        return MathAssist.Instance().RandomVector3(_position, 5.0f);
    }

    public Vector3 SetDestination_Foword(Vector3 _position)
    {
        return MathAssist.Instance().RandomVector3(_position, 20.0f);
    }

    public void ToWalk_Nomal(BaseGameEntity _entity)
    {
        MessageDispatcher.Instance.DispatchMessage(Random.Range(4, 6), _entity.ID, _entity.ID, (int)eMESSAGE_TYPE.TO_WALK, null);
    }
    public void ToWalk_Foword(BaseGameEntity _entity)
    {
        MessageDispatcher.Instance.DispatchMessage(Random.Range(3, 4), _entity.ID, _entity.ID, (int)eMESSAGE_TYPE.TO_WALK, null);
    }
}