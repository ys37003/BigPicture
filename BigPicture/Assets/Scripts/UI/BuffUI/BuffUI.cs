using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffUI : MonoBehaviour {

    [SerializeField]
    StatusData buffStatus;
    [SerializeField]
    MonsterHUDUI HUDUI;

    AI owner;
    // Use this for initialization
    void Start () {

    }

    public void Init(AI _owner)
    {
        owner = _owner;
        buffStatus = owner.AddStatus;
        HUDUI = owner.HUDUI;
    }

    public void SetBuff(StatusData _statusData)
    {
        Debug.Log("Power : " + _statusData.Strength);
        if ( 0 < _statusData.Strength )
        {
            HUDUI.AddBuff(eBuff.PowerUp);
        }
        else if(0 > _statusData.Strength)
        {
            Debug.Log("PowerDown");
            HUDUI.AddBuff(eBuff.PowerDown);
        }
        else if( 0 == _statusData.Strength )
        {
            HUDUI.RemoveBuff(eBuff.PowerUp);
            HUDUI.RemoveBuff(eBuff.PowerDown);
        }

        if (0 < _statusData.Agility)
        {
            HUDUI.AddBuff(eBuff.SpeedUp);
        }
        else if (0 > _statusData.Agility)
        {
            Debug.Log("SpeedDown");
            HUDUI.AddBuff(eBuff.SpeedDown);
        }
        else if (0 == _statusData.Agility)
        {
            HUDUI.RemoveBuff(eBuff.SpeedUp);
            HUDUI.RemoveBuff(eBuff.SpeedDown);
        }
    }
}
