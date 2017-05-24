using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusUI : UIBase<StatusUI>
{
    [SerializeField] private List<StatusSlider> sliderList = new List<StatusSlider>();
    [SerializeField] private UILabel labelSkillPoint;
    [SerializeField] private UILabel labelPhysicsPower, labelSpellPower, labelMoveSpeed, labelEvasionRate, labelArmor, labelRecoveryRPS;
    [SerializeField] private UIButtonEx btnClose;
    [SerializeField] private List<UIToggleEx> toggleList = new List<UIToggleEx>();

    private ICharacter character;

    protected override void OverrideAwake()
    {
        foreach (StatusSlider slider in sliderList)
        {
            slider.onUpdateStat  += onUpdateStat;
            slider.GetSkillPoint += getSkillPoint;
        }

        EventDelegate.Add(btnClose.onClick, Destroy);

        for (int i = 0; i < toggleList.Count; ++i)
        {
            int count = i;

            toggleList[i].Number = count;
            if (i < TeamManager.Instance.GetTeamSize())
            {
                EventDelegate.Add(toggleList[i].onChange, () =>
                {
                    SetData(TeamManager.Instance.GetCharacter(toggleList[count].Number));
                });
            }
            else
            {
                toggleList[i].SetActive(false);
            }
        }
    }

    protected override void OverrideStart()
    {
        StartCoroutine("Init");
    }

    private IEnumerator Init()
    {
        yield return null;
        SetData(TeamManager.Instance.GetCharacter(0));
    }

    private void SetData(ICharacter character)
    {
        this.character = character;
        foreach (StatusSlider slider in sliderList)
        {
            switch (slider.stat)
            {
                case eSTAT.STRENGTH: slider.SetData(character.Status.Strength); break;
                case eSTAT.SPELL:    slider.SetData(character.Status.Spell);    break;
                case eSTAT.AGILITY:  slider.SetData(character.Status.Agility);  break;
                case eSTAT.AVOID:    slider.SetData(character.Status.Avoid);    break;
                case eSTAT.DEFENSE:  slider.SetData(character.Status.Defense);  break;
                case eSTAT.RECOVERY: slider.SetData(character.Status.Recovery); break;
                case eSTAT.LUCK:     slider.SetData(character.Status.Luck);     break;
            }
        }

        updateUI();
    }

    private void onUpdateStat(eSTAT stat, int value)
    {
        switch (stat)
        {
            case eSTAT.STRENGTH: character.Status.Strength += value; break;
            case eSTAT.SPELL:    character.Status.Spell    += value; break;
            case eSTAT.AGILITY:  character.Status.Agility  += value; break;
            case eSTAT.AVOID:    character.Status.Avoid    += value; break;
            case eSTAT.DEFENSE:  character.Status.Defense  += value; break;
            case eSTAT.RECOVERY: character.Status.Recovery += value; break;
            case eSTAT.LUCK:     character.Status.Luck     += value; break;
        }

        character.SkillPoint -= value;

        updateUI();
    }

    private void updateUI()
    {
        labelSkillPoint.text = string.Format("{0}", character.SkillPoint);

        labelPhysicsPower.text = string.Format("물리공격력 : {0}", character.Status.PhysicsPower);
        labelSpellPower.text   = string.Format("마법공격력 : {0}", character.Status.SpellPower);
        labelMoveSpeed.text    = string.Format("이동속도 : {0}", character.Status.MoveSpeed);
        labelEvasionRate.text  = string.Format("회피율 : {0}", character.Status.EvasionRate);
        labelArmor.text        = string.Format("방어력 : {0}", character.Status.Armor);
        labelRecoveryRPS.text  = string.Format("초당 회복력 : {0}", character.Status.RecoveryRPS);
    }

    private int getSkillPoint()
    {
        return character.SkillPoint;
    }
}