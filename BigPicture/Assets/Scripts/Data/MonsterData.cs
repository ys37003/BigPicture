public class MonsterData
{
    /// <summary>
    /// 개체 타입
    /// </summary>
    public readonly eENTITY_TYPE Type;

    /// <summary>
    /// 고유 번호 
    /// </summary>
    public readonly int No;

    /// <summary>
    /// 이름
    /// </summary>
    public readonly string Name;

    /// <summary>
    /// 능력치
    /// </summary>
    public readonly StatusData StatusData;

    public MonsterData(eENTITY_TYPE type, int no, string name, int str, int sp, int ag, int av, int def, int rec, int lck)
    {
        Type = type;
        No = no;
        Name = name;
        StatusData = new StatusData(str, sp, ag, av, def, rec, lck);
    }

    public MonsterData(eENTITY_TYPE type, int no, string name, StatusData stat)
    {
        Type = type;
        No = no;
        Name = name;
        StatusData = stat;
    }
}