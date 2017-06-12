using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionManager : Singleton<ConditionManager> {

    public IEnumerator BleedingDelay(AI _ai)
    {
        float oldTime = Time.time;
        float endTIme = Time.time;
        _ai.buffUI.AddBuff(eBuff.Bleeding);
        while (true)
        {
            if (endTIme + 5.0f < Time.time)
            {
                break;
            }

            if (oldTime + 1.0f < Time.time)
            {
                oldTime = Time.time;
                _ai.Data.StatusData.HP -= 5;
            }
            yield return null;
        }
        _ai.buffUI.RemoveBuff(eBuff.Bleeding);
        //skillAble = true;
    }

    public IEnumerator PoisionDelay(AI _ai)
    {
        float oldTime = Time.time;
        float endTIme = Time.time;
        _ai.AddStatus = new StatusData(0, 0, -5, 0, 0, 0, 0, 0);
        _ai.buffUI.AddBuff(eBuff.SpeedDown);
        _ai.buffUI.AddBuff(eBuff.Poisoning);
        while (true)
        {
            if (endTIme + 5.0f < Time.time)
            {
                break;
            }

            if (oldTime + 1.0f < Time.time)
            {
                oldTime = Time.time;
                _ai.Data.StatusData.HP -= 3;
            }
            yield return null;
        }
        //skillAble = true;
        _ai.AddStatus = new StatusData(0, 0, 0, 0, 0, 0, 0, 0);
        _ai.buffUI.RemoveBuff(eBuff.SpeedDown);
        _ai.buffUI.RemoveBuff(eBuff.Poisoning);
    }

    public IEnumerator DebuffDelay(BattleEntity _ai)
    {
        float oldTime = Time.time;
        StatusData data = new StatusData(-5, 0, -5, 0, 0, 0, 0, 0);
        _ai.AddStatus = data;
        _ai.buffUI.AddBuff(eBuff.PowerDown);
        _ai.buffUI.AddBuff(eBuff.SpeedDown);

        while (true)
        {
            if (oldTime + 2.0f < Time.time)
            {
                break;
            }
            yield return null;
        }

        data = new StatusData(0, 0, 0, 0, 0, 0, 0, 0);
        _ai.AddStatus = data;

        _ai.buffUI.RemoveBuff(eBuff.PowerDown);
        _ai.buffUI.RemoveBuff(eBuff.SpeedDown);
    }

    public IEnumerator DamageAndDebuffDelay(BattleEntity _ai)
    {
        float oldTime = Time.time;
        StatusData data = new StatusData(-5, 0, -5, 0, 0, 0, 0, 0);
        _ai.AddStatus = data;
        _ai.buffUI.AddBuff(eBuff.PowerDown);
        _ai.buffUI.AddBuff(eBuff.SpeedDown);
        while (true)
        {
            if (oldTime + 2.0f < Time.time)
            {
                break;
            }
            yield return null;
        }

        data = new StatusData(0, 0, 0, 0, 0, 0, 0, 0);
        _ai.AddStatus = data;

        _ai.buffUI.RemoveBuff(eBuff.PowerDown);
        _ai.buffUI.RemoveBuff(eBuff.SpeedDown);
    }
}
