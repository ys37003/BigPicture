using System.Collections.Generic;
using System.Xml;
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
    public readonly string monsterData_path = "DataSheets/Monster/MonsterElement";

    private Dictionary<ePARTNER_NAME, List<TalkBaseData>> talkBaseDataDic = new Dictionary<ePARTNER_NAME, List<TalkBaseData>>();
    public readonly string talkBaseData_path = "DataSheets/TalkBase/TalkBase";

    private Dictionary<int, List<TalkDetailData>> talkDetailDataDic = new Dictionary<int, List<TalkDetailData>>();
    public readonly string talkDetailData_path = "DataSheets/TalkDetail/TalkDetail";

    private Dictionary<ePARTNER_NAME, int> likeAbilityDataDic = new Dictionary<ePARTNER_NAME, int>();
    public readonly string likeAbilityData_path = "DataSheets/Likeability/Likeability";

    private List<List<QuestionStruct>> reCruitDatas = new List<List<QuestionStruct>>();
    public readonly string reCruitData_path = "DataSheets/Recruit/RecruitElement";

    private XmlNodeList[] xmlTable;
    private LoadXML loadXml = new LoadXML();

    const int JOB_NUM = 4;
    public void DataLoad()
    {
        MonsterDataInit();
        TalkBaseDataInit();
        TalkDetailDataInit();
        Likeability();

        ReCruitDataInit();
    }

    void MonsterDataInit()
    {
        xmlTable = loadXml.LoadXml_MonsterData(monsterData_path);
        // 각 종족별 직업의 수 : 4
        int TribeNum = xmlTable[(int)eMONSTER_DATASHEET.TRIBE].Count / JOB_NUM;

        for (int i = 0; i < TribeNum; ++i)
        {
            monsterDatas.Add(new List<MonsterData>());
        }

        for (int i = 0; i < TribeNum; ++i)
        {
            for (int j = i * JOB_NUM; j < (i + 1) * JOB_NUM; ++j)
            {
                MonsterData monsterData = new MonsterData
                (
                    (eTRIBE_TYPE)int.Parse(xmlTable[(int)eMONSTER_DATASHEET.TRIBE].Item(j).InnerText),
                    (eJOB_TYPE)int.Parse(xmlTable[(int)eMONSTER_DATASHEET.JOB].Item(j).InnerText),
                    int.Parse(xmlTable[(int)eMONSTER_DATASHEET.STRENGTH].Item(j).InnerText),
                    int.Parse(xmlTable[(int)eMONSTER_DATASHEET.SPELL].Item(j).InnerText),
                    int.Parse(xmlTable[(int)eMONSTER_DATASHEET.AGILITY].Item(j).InnerText),
                    int.Parse(xmlTable[(int)eMONSTER_DATASHEET.AVOID].Item(j).InnerText),
                    int.Parse(xmlTable[(int)eMONSTER_DATASHEET.DEFENSE].Item(j).InnerText),
                    int.Parse(xmlTable[(int)eMONSTER_DATASHEET.RECOVERY].Item(j).InnerText),
                    int.Parse(xmlTable[(int)eMONSTER_DATASHEET.LUCK].Item(j).InnerText),
                    StatusData.MAX_HP,
                    int.Parse(xmlTable[(int)eMONSTER_DATASHEET.RANGE].Item(j).InnerText),
                    int.Parse(xmlTable[(int)eMONSTER_DATASHEET.EYESIGHT].Item(j).InnerText)
                );

                monsterDatas[i].Add(monsterData);
            }
        }
    }

    void TalkBaseDataInit()
    {
        xmlTable = loadXml.LoadXml_TalkBaseData(talkBaseData_path);

        for(ePARTNER_NAME name = ePARTNER_NAME.DONUT; name < ePARTNER_NAME.END; ++name)
        {
            talkBaseDataDic.Add(name, new List<TalkBaseData>());
        }

        for (int i = 0; i < xmlTable[(int)eTALK_BASE.NAME].Count; ++i)
        {
            TalkBaseData data = new TalkBaseData
            (
                (ePARTNER_NAME)int.Parse(xmlTable[(int)eTALK_BASE.NAME].Item(i).InnerText),
                int.Parse(xmlTable[(int)eTALK_BASE.TALK_TYPE].Item(i).InnerText),
                int.Parse(xmlTable[(int)eTALK_BASE.TALK_NUMBER].Item(i).InnerText),
                int.Parse(xmlTable[(int)eTALK_BASE.REPEAT].Item(i).InnerText) == 1
            );

            talkBaseDataDic[data.Name].Add(data);
        }
    }

    void TalkDetailDataInit()
    {
        TextAsset xml = (TextAsset)Resources.Load(talkDetailData_path);

        XmlDocument xmldoc = new XmlDocument();
        xmldoc.LoadXml(xml.text);

        XmlNodeList xnList = xmldoc.GetElementsByTagName("Talk_Detail");

        foreach (XmlNode xn in xnList)
        {
            TalkDetailData data = new TalkDetailData
            (
                int.Parse(xn["TalkNumber"].InnerText),
                int.Parse(xn["Order"].InnerText),
                (ePARTNER_NAME)int.Parse(xn["Name"].InnerText),
                xn["Description"].InnerText,
                xn["Choice"] != null ? xn["Choice"].InnerText : string.Empty,
                xn["Result"] != null ? xn["Result"].InnerText : string.Empty
            );

            if (!talkDetailDataDic.ContainsKey(data.TalkNumber))
                talkDetailDataDic.Add(data.TalkNumber, new List<TalkDetailData>());

            talkDetailDataDic[data.TalkNumber].Add(data);
        }
    }

    void Likeability()
    {
        xmlTable = loadXml.LoadXml_TalkBaseData(talkBaseData_path);

        for (int i = 0; i < xmlTable[(int)eLIKEABILITY.NAME].Count; ++i)
        {
            ePARTNER_NAME name = (ePARTNER_NAME)int.Parse(xmlTable[(int)eTALK_BASE.NAME].Item(i).InnerText);
            int like = int.Parse(xmlTable[(int)eTALK_BASE.TALK_TYPE].Item(i).InnerText);

            likeAbilityDataDic.Add(name, like);
        }
    }

    void ReCruitDataInit()
    {
        xmlTable = loadXml.LoadXml_ReCruitData(reCruitData_path);

        int oldName = -1;

        for(int i = 0; i < xmlTable[(int)eRECRUIT_DATASHEET.NAME].Count; ++i )
        {
            if(oldName != int.Parse(xmlTable[(int)eRECRUIT_DATASHEET.NAME].Item(i).InnerText))
            {
                oldName = int.Parse(xmlTable[(int)eRECRUIT_DATASHEET.NAME].Item(i).InnerText);
                reCruitDatas.Add(new List<QuestionStruct>());
            }
            QuestionStruct questionStruct = new QuestionStruct();
            questionStruct.question = xmlTable[(int)eRECRUIT_DATASHEET.QUESTION].Item(i).InnerText;
            questionStruct.yes = xmlTable[(int)eRECRUIT_DATASHEET.YES].Item(i).InnerText;
            questionStruct.no = xmlTable[(int)eRECRUIT_DATASHEET.NO].Item(i).InnerText;
            questionStruct.anwser = int.Parse(xmlTable[(int)eRECRUIT_DATASHEET.ANSWER].Item(i).InnerText);

            reCruitDatas[reCruitDatas.Count - 1].Add(questionStruct);
        }
    }

    public MonsterData GetMonsterData(eTRIBE_TYPE _entityTribe, eJOB_TYPE _entityJob)
    {
        return monsterDatas[(int)_entityTribe][(int)_entityJob];
    }

    public List<QuestionStruct> GetRecruitData(ePARTNER_NAME _name)
    {
        return reCruitDatas[(int)_name];
    }
}