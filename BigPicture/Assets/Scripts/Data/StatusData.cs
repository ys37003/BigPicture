public class StatusData
{
    /// <summary>
    /// 힘
    /// </summary>
    public int Strength;

    /// <summary>
    /// 마력
    /// </summary>
    public int Spell;

    /// <summary>
    /// 민첩(이동속도)
    /// </summary>
    public int Agility;

    /// <summary>
    /// 회피
    /// </summary>
    public int Avoid;

    /// <summary>
    /// 방어력
    /// </summary>
    public int Defense;

    /// <summary>
    /// 회복력
    /// </summary>
    public int Recovery;

    /// <summary>
    /// 행운(치명타, 패시브 강화)
    /// </summary>
    public int Luck;

    public StatusData(int str, int sp, int ag, int av, int def, int rec, int lck)
    {
        Strength = str;
        Spell    = sp;
        ag       = Agility;
        Avoid    = av;
        Defense  = def;
        Recovery = rec;
        Luck     = lck;
    }

    public StatusData(StatusData stat)
    {
        Strength = stat.Strength;
        Spell    = stat.Spell;
        Agility  = stat.Agility;
        Avoid    = stat.Avoid;
        Defense  = stat.Defense;
        Recovery = stat.Recovery;
        Luck     = stat.Luck;
    }

    public static StatusData operator +(StatusData stat1, StatusData stat2)
    {
        return new StatusData(stat1.Strength + stat2.Strength,
                              stat1.Spell + stat2.Spell,
                              stat1.Agility + stat2.Agility,
                              stat1.Avoid + stat2.Avoid,
                              stat1.Defense + stat2.Defense,
                              stat1.Recovery + stat2.Recovery,
                              stat1.Luck + stat2.Luck);
    }
}