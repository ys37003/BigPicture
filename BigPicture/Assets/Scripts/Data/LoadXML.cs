using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class LoadXML
{
    public XmlNodeList[] LoadXml_MonsterData(string _address)
    {
        TextAsset xml = (TextAsset)Resources.Load(_address);

        XmlDocument xmldoc = new XmlDocument();
        Debug.Log(xml);
        xmldoc.LoadXml(xml.text);

        XmlNodeList tribeTable    = xmldoc.GetElementsByTagName("Tribe");
        XmlNodeList jobTable      = xmldoc.GetElementsByTagName("Job");
        XmlNodeList strengthTable = xmldoc.GetElementsByTagName("Strength");
        XmlNodeList spellTable    = xmldoc.GetElementsByTagName("Spell");
        XmlNodeList agilityTable  = xmldoc.GetElementsByTagName("Agility");
        XmlNodeList avoidTable    = xmldoc.GetElementsByTagName("Avoid");
        XmlNodeList defenseTable  = xmldoc.GetElementsByTagName("Defense");
        XmlNodeList recoveryTable = xmldoc.GetElementsByTagName("Recovery");
        XmlNodeList luckTable     = xmldoc.GetElementsByTagName("Luck");
        XmlNodeList rangeTable    = xmldoc.GetElementsByTagName("Range");
        XmlNodeList eyeSightTable = xmldoc.GetElementsByTagName("EyeSight");


        XmlNodeList[] xmlTable = { tribeTable, jobTable, strengthTable,
                                   spellTable , agilityTable , avoidTable,
                                   defenseTable, recoveryTable, luckTable,
                                   rangeTable, eyeSightTable };

        return xmlTable;
    }

    public XmlNodeList[] LoadXml_TalkBaseData(string _address)
    {
        TextAsset xml = (TextAsset)Resources.Load(_address);

        XmlDocument xmldoc = new XmlDocument();
        Debug.Log(xml);
        xmldoc.LoadXml(xml.text);

        XmlNodeList nameTable   = xmldoc.GetElementsByTagName("Name");
        XmlNodeList typeTable   = xmldoc.GetElementsByTagName("TalkType");
        XmlNodeList numberTable = xmldoc.GetElementsByTagName("TalkNumber");
        XmlNodeList repeatTable = xmldoc.GetElementsByTagName("Repeat");

        XmlNodeList[] xmlTable = { nameTable, typeTable, numberTable, repeatTable };

        return xmlTable;
    }

    public XmlNodeList[] LoadXml_LikeAbilityData(string _address)
    {
        TextAsset xml = (TextAsset)Resources.Load(_address);

        XmlDocument xmldoc = new XmlDocument();
        Debug.Log(xml);
        xmldoc.LoadXml(xml.text);

        XmlNodeList nameTable = xmldoc.GetElementsByTagName("Name");
        XmlNodeList likeTable = xmldoc.GetElementsByTagName("NeedLikeability");

        XmlNodeList[] xmlTable = { nameTable, likeTable };

        return xmlTable;
    }

    public XmlNodeList[] LoadXml_ReCruitData(string _address)
    {
        TextAsset xml = (TextAsset)Resources.Load(_address);

        XmlDocument xmldoc = new XmlDocument();
        Debug.Log(xml);
        xmldoc.LoadXml(xml.text);

        XmlNodeList nameTable     = xmldoc.GetElementsByTagName("Name");
        XmlNodeList questionTable = xmldoc.GetElementsByTagName("Question");
        XmlNodeList yesTable      = xmldoc.GetElementsByTagName("Yes");
        XmlNodeList noTable       = xmldoc.GetElementsByTagName("No");
        XmlNodeList answerTable   = xmldoc.GetElementsByTagName("Answer");

        XmlNodeList[] xmlTable = { nameTable, questionTable, yesTable, noTable, answerTable };

        return xmlTable;
    }
}