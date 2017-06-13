using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestData
{
    public readonly int Number;
    public readonly string Description;

    public QuestData(int number, string description)
    {
        Number = number;
        Description = description;
    }
}