using System.Xml.Serialization;

public class MonsterData
{
    /// <summary>
    /// 개체 타입
    /// </summary>
    public readonly eTRIBE_TYPE Type;

    public readonly eJOB_TYPE Job;

    /// <summary>
    /// 사정거리
    /// </summary>
    public int Range;

    /// <summary>
    /// 인식범위
    /// </summary>
    public int EyeSight;

    public readonly StatusData StatusData;

    public MonsterData(eTRIBE_TYPE tribe, eJOB_TYPE job, int str, int sp, int ag, int av, int def, int rec, int lck , int rg , int es)
    {
        Type = tribe;
        Job = job;
        Range = rg;
        EyeSight = es;
        StatusData = new StatusData(str, sp, ag, av, def, rec, lck);
    }

    public MonsterData(eTRIBE_TYPE tribe, eJOB_TYPE job, int rg,int es, StatusData stat)
    {
        Type = tribe;
        Job = job;
        Range = rg;
        EyeSight = es;
        StatusData = stat;
    }
}