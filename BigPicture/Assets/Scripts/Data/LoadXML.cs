using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class LoadXML  {

	public XmlNodeList[] LoadXml(string _address)
    {
        TextAsset xml = (TextAsset)Resources.Load(_address);

        XmlDocument xmldoc = new XmlDocument();
        Debug.Log(xml);
        xmldoc.LoadXml(xml.text);

        XmlNodeList tribeTable = xmldoc.GetElementsByTagName("Tribe");
        XmlNodeList jobTable = xmldoc.GetElementsByTagName("Job");
        XmlNodeList strengthTable = xmldoc.GetElementsByTagName("Strength");
        XmlNodeList spellTable = xmldoc.GetElementsByTagName("Spell");
        XmlNodeList agilityTable = xmldoc.GetElementsByTagName("Agility");
        XmlNodeList avoidTable = xmldoc.GetElementsByTagName("Avoid");
        XmlNodeList defenseTable = xmldoc.GetElementsByTagName("Defense");
        XmlNodeList recoveryTable = xmldoc.GetElementsByTagName("Recovery");
        XmlNodeList luckTable = xmldoc.GetElementsByTagName("Luck");
        XmlNodeList rangeTable = xmldoc.GetElementsByTagName("Range");
        XmlNodeList eyeSightTable = xmldoc.GetElementsByTagName("EyeSight");


        XmlNodeList[] xmlTable = { tribeTable, jobTable, strengthTable,
                                   spellTable , agilityTable , avoidTable,
                                   defenseTable, recoveryTable, luckTable,
                                   rangeTable, eyeSightTable };

        return xmlTable;
    }
}
