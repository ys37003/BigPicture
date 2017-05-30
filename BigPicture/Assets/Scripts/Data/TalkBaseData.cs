using System.Xml.Serialization;

public class TalkBaseData
{
    public readonly ePARTNER_NAME Name;
    public readonly int           TalkType;
    public readonly int           TalkNumber;
    public readonly bool          Repeat;

    public TalkBaseData(ePARTNER_NAME name, int type, int number, bool repeat)
    {
        Name       = name;
        TalkType   = type;
        TalkNumber = number;
        Repeat     = repeat;
    }

    public TalkBaseData(TalkBaseData data)
    {
        Name       = data.Name;
        TalkType   = data.TalkType;
        TalkNumber = data.TalkNumber;
        Repeat     = data.Repeat;
    }
}