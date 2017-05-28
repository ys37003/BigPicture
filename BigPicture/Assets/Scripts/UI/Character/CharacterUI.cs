using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUI : UIBase<CharacterUI>
{
    [SerializeField] private HPbar hpBar;

    protected override void OverrideStart()
    {
        hpBar.SetData(TeamManager.Instance.GetCharacter(0).Status);
    }
}