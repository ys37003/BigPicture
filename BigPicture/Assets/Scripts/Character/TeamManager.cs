using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eDAMAGE_TYPE
{
    PHYSICS,
    SPELL,
}

public interface ICharacter
{
    eENTITY_TYPE Type { get; }
    eDAMAGE_TYPE DamageType { get; }
    StatusData Status { get; }
    int SkillPoint { get; set; }
}

public class TeamManager : Singleton<TeamManager>
{
    private List<ICharacter> characterList = new List<ICharacter>();

    private void LoadCharacter()
    {
        // 차후 구현
    }

    public int GetTeamSize()
    {
        return characterList.Count;
    }

    public void AddCharacter(ICharacter character)
    {
        if (character.Type != eENTITY_TYPE.PLAYER && character.Type != eENTITY_TYPE.NPC)
            return;

        if (characterList.Contains(character))
            return;

        characterList.Add(character);
    }

    public List<ICharacter> GetCharacterList()
    {
        return new List<ICharacter>(characterList);
    }

    public ICharacter GetCharacter(int index)
    {
        if (index >= characterList.Count)
            return null;

        return characterList[index];
    }
}