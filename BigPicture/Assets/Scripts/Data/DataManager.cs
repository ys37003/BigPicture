using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

public class DataManager
{
    private static DataManager instance;

    private DataManager()
    {

    }

    public static DataManager Instance()
    {
        if (instance == null)
        {
            instance = new DataManager();
        }
        return instance;
    }

    /// <summary>
    /// List : 종족
    /// List<List<>> : 직업
    /// </summary>
    private List<List<MonsterData>> monsterDatas = new List<List<MonsterData>>();
    public string monsterData_path = "DataSheets/MonsterElement";

    private XmlNodeList[] xmlTable;
    private LoadXML loadXml = new LoadXML();

    const int JOB_NUM = 4;
    public void monsterDataLoad()
    {
        xmlTable = loadXml.LoadXml(monsterData_path);
        DataInit();
    }

    void DataInit()
    {
        // 각 종족별 직업의 수 : 4
        int TribeNum = xmlTable[(int)eDATASHEET.TRIBE].Count / JOB_NUM;

        for (int i = 0; i < TribeNum; ++i)
        {
            monsterDatas.Add(new List<MonsterData>());
        }

        for (int i = 0; i < TribeNum; ++i)
        {
            for (int j = i * JOB_NUM; j < (i + 1) * JOB_NUM; ++j)
            {
                MonsterData monsterData = new MonsterData(
                    (eTRIBE_TYPE)int.Parse(xmlTable[(int)eDATASHEET.TRIBE].Item(j).InnerText),
                    (eJOB_TYPE)int.Parse(xmlTable[(int)eDATASHEET.JOB].Item(j).InnerText),
                    int.Parse(xmlTable[(int)eDATASHEET.STRENGTH].Item(j).InnerText),
                    int.Parse(xmlTable[(int)eDATASHEET.SPELL].Item(j).InnerText),
                    int.Parse(xmlTable[(int)eDATASHEET.AGILITY].Item(j).InnerText),
                    int.Parse(xmlTable[(int)eDATASHEET.AVOID].Item(j).InnerText),
                    int.Parse(xmlTable[(int)eDATASHEET.DEFENSE].Item(j).InnerText),
                    int.Parse(xmlTable[(int)eDATASHEET.RECOVERY].Item(j).InnerText),
                    int.Parse(xmlTable[(int)eDATASHEET.LUCK].Item(j).InnerText),
                    StatusData.MAX_HP,
                    int.Parse(xmlTable[(int)eDATASHEET.RANGE].Item(j).InnerText),
                    int.Parse(xmlTable[(int)eDATASHEET.EYESIGHT].Item(j).InnerText)
                    );

                monsterDatas[i].Add(monsterData);
            }
        }
    }

    public MonsterData GetData(eTRIBE_TYPE _entityTribe, eJOB_TYPE _entityJob)
    {
        return monsterDatas[(int)_entityTribe][(int)_entityJob];
    }
}
