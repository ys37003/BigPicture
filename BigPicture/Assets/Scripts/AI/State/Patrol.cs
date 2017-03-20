using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol<entity_type> : State<entity_type> where entity_type : Ork
{

    private static Patrol<entity_type> instance;
    private Patrol()
    { }

    public static Patrol<entity_type> Instance()
    {
        if (instance == null)
        {
            instance = new Patrol<entity_type>();
        }
        return instance;
    }

    public override void Excute(entity_type _monster)
    {
        _monster.Patrol();
    }
    public override void Enter(entity_type _monster)
    {
        
    }
    public override void Exit(entity_type _monster)
    {

    }
}
