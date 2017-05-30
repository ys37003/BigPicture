using System.Xml.Serialization;
using System.Collections.Generic;

public class TalkResultData
{
    public readonly int  Talk;
    public readonly int  Order;
    public readonly int  Like;
    public readonly int  Quest;
    public readonly bool RepeatEnd;

    public TalkResultData(string result)
    {
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

            TalkResultData data = new TalkResultData(str);
            ResultList.Add(data);
        }
    }
}