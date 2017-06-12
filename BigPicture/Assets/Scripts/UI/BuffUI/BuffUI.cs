using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffUI : MonoBehaviour {
    StatusData buffStatus;
    MonsterHUDUI HUDUI;

    AI owner;
    public void Init(AI _owner)
    {
        owner = _owner;
        buffStatus = owner.AddStatus;
        HUDUI = owner.HUDUI;
    }

    public void AddBuff(eBuff _buff)
    {
        HUDUI.AddBuff(_buff);
    }

    public void RemoveBuff(eBuff _buff)
    {
        HUDUI.RemoveBuff(_buff);
    }
}
