using System.Xml.Serialization;
using System.Collections.Generic;

public class TalkResultData
{
    public readonly ePARTNER_NAME Name;
    public readonly int  Talk;
    public readonly int  Order;
    public readonly int  Like;
    public readonly int  Quest;
    public readonly int  SkillPoint;
    public readonly bool RepeatEnd;

    public TalkResultData(ePARTNER_NAME name, string result)
    {
        Name = name;
        Talk = -1;
        Order = -1;
        RepeatEnd = false;

        if (result.Length <= 0)
            return;

        foreach (string r in result.Split(','))
        {
            if (r.Substring(0, 1) == "T")
            {
                Talk = int.Parse(r.Substring(1));
            }
            else if (r.Substring(0, 1) == "O")
            {
                Order = int.Parse(r.Substring(1));
            }
            else if (r.Substring(0, 1) == "L")
            {
                Like = int.Parse(r.Substring(1));
            }
            else if (r.Substring(0, 1) == "Q")
            {
                Quest = int.Parse(r.Substring(1));
            }
            else if (r.Substring(0,1) == "S")
            {
                SkillPoint = int.Parse(r.Substring(1));
            }
            else if (r.Substring(0, 1) == "R")
            {
                RepeatEnd = true;
            }
        }
    }
}

public class TalkDetailData
{
    public readonly int             TalkNumber;
    public readonly int             Order;
    public readonly ePARTNER_NAME   Name;
    public readonly string          Description;
    public List<string>             ChoiceList;
    public List<TalkResultData>     ResultList;

    public TalkDetailData(int number, int order, ePARTNER_NAME name, string desc, string choice, string result)
    {
        TalkNumber  = number;
        Order       = order;
        Name        = name;
        Description = desc;

        ChoiceList = choice.Length > 0 
                   ? new List<string>(choice.Split('#'))
                   : new List<string>();

        ResultList = new List<TalkResultData>();

        foreach(string str in result.Split('#'))
        {
            if (str.Length <= 0)
                continue;

            TalkResultData data = new TalkResultData(name, str);
            ResultList.Add(data);
        }
    }
}

public class TalkDescription
{
    public readonly ePARTNER_NAME Name;
    public readonly string Description;
    public readonly TalkResultData Result;

    public TalkDescription(ePARTNER_NAME name, string desc, TalkResultData result)
    {
        Name = name;
        Description = desc;
        Result = result;
    }
}

public class TalkChoice
{
    public readonly ePARTNER_NAME        Name;
    public readonly List<string>         ChoiceList;
    public readonly List<TalkResultData> ResultList;

    public TalkChoice(ePARTNER_NAME name, List<string> choice, List<TalkResultData> result)
    {
        Name       = name;
        ChoiceList = choice;
        ResultList = result;
    }
}