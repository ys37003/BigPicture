using System.Xml.Serialization;

public class StatusData
{
    #region 능력치
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

    private float Hp;
    public  float HP
    {
        get { return Hp; }
        set
        {
            Hp = value;

                 if (Hp > 100) Hp = 100;
            else if (Hp < 0)   Hp = 0;
        }
    }
    #endregion

    #region 계산값
    /// <summary>
    /// 물리 공격력
    /// </summary>
    public float PhysicsPower { get { return Strength + (Luck + Spell) * 0.1f * Luck * 0.1f; } }

    /// <summary>
    /// 마법 공격력
    /// </summary>
    public float SpellPower { get { return Spell + (Luck + Spell) * 0.1f * Luck * 0.1f; } }

    /// <summary>
    /// 이동 속도
    /// </summary>
    public float MoveSpeed { get { return Agility + (Spell + Luck) * 0.1f; } }

    /// <summary>
    /// 회피율
    /// </summary>
    public float EvasionRate { get { return Avoid * (Agility + Luck) * 0.01f; } }

    /// <summary>
    /// 방어력
    /// </summary>
    public float Armor { get { return Strength * 0.01f * Agility + Avoid * (Defense + Luck) * 0.1f; } }

    /// <summary>
    /// 초당 회복력
    /// </summary>
    public float RecoveryRPS { get { return Recovery + Spell + Luck * 0.1f; } }

    /// <summary>
    /// 최대 체력(100으로 고정)
    /// </summary>
    static public readonly float MAX_HP = 100;
    #endregion

    /// <summary>
    /// 능력치
    /// </summary>
    public StatusData(int str, int sp, int ag, int av, int def, int rec, int lck, float hp)
    {
        Strength = str;
        Spell    = sp;
        Agility  = ag;
        Avoid    = av;
        Defense  = def;
        Recovery = rec;
        Luck     = lck;
        Hp       = hp;
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
        Hp       = stat.Hp;
    }

    public static StatusData operator +(StatusData stat1, StatusData stat2)
    {
        return new StatusData(stat1.Strength + stat2.Strength,
                              stat1.Spell    + stat2.Spell,
                              stat1.Agility  + stat2.Agility,
                              stat1.Avoid    + stat2.Avoid,
                              stat1.Defense  + stat2.Defense,
                              stat1.Recovery + stat2.Recovery,
                              stat1.Luck     + stat2.Luck,
                              stat1.Hp       + stat2.Hp);
    }
}