using System.Xml.Serialization;

public class MonsterData
{
    /// <summary>
    /// 개체 타입
    /// </summary>
    public readonly eTRIBE_TYPE Tribe;

    /// <summary>
    /// 무리 역할
    /// </summary>
    public readonly eJOB_TYPE Job;

    /// <summary>
    /// 능력치
    /// </summary>
    public readonly StatusData StatusData;

    /// <summary>
    /// 사정거리
    /// </summary>
    public readonly int Range;

    /// <summary>
    /// 인식범위
    /// </summary>
    public readonly int EyeSight;

    public MonsterData(eTRIBE_TYPE tribe, eJOB_TYPE job, int str, int sp, int ag, int av, int def, int rec, int lck, float hp, int rg, int es)
    {
        Tribe      = tribe;
        Job        = job;
        Range      = rg;
        EyeSight   = es;
        StatusData = new StatusData(str, sp, ag, av, def, rec, lck, hp);
    }

    public MonsterData(eTRIBE_TYPE tribe, eJOB_TYPE job, int rg, int es, StatusData stat)
    {
        Tribe      = tribe;
        Job        = job;
        Range      = rg;
        EyeSight   = es;
        StatusData = stat;
    }

    public MonsterData(MonsterData data)
    {
        Tribe      = data.Tribe;
        Job        = data.Job;
        Range      = data.Range;
        EyeSight   = data.EyeSight;
        StatusData = new StatusData(data.StatusData);
    }
}