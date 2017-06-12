using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEntity : BaseGameEntity
{
    public Group EntityGroup { get; set; }
    public StatusData AddStatus { get; set; }
    public MonsterHUDUI HUDUI { get; set; }
    public BuffUI buffUI { get; set; }

    protected void EntityInit(eENTITY_TYPE _type, eTRIBE_TYPE _tribe, eJOB_TYPE _job, Group _group)
    {
        EntityGroup = _group;
        base.EntityInit(_type, _tribe, _job);
    }
}
