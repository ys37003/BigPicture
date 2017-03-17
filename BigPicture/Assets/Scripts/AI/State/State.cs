using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State<entity_type> {

    private static State<entity_type> instance;

    public State<entity_type> Instance
    {
        get
        {
            try
            {
                return instance;
            }
            catch
            {
                instance = new State<entity_type>();
                return instance;
            }
        }
    }

    public virtual void Exicute(entity_type _entity_type)
    {

    }
    public virtual void Enter(entity_type _entity_type)
    {

    }
    public virtual void Exit(entity_type _entity_type)
    {

    }
}
