using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReCruitManager : Singleton<ReCruitManager> {

    private List<int> answer = new List<int>();
    private List<QuestionStruct> questionList = new List<QuestionStruct>();
    // Use this for initialization
    private ePARTNER_NAME partnerName;
    public Image backGroundImage;
    public Text question;
    public Button yes;
    public Button no;
    private bool isRun = false;
    private int questionIndex = -1;
    void Start () {
        StartRequist(null);

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void UIAction(bool _value)
    {
        backGroundImage.gameObject.SetActive(_value);
        question.gameObject.SetActive(_value);
        yes.gameObject.SetActive(_value);
        no.gameObject.SetActive(_value);
        questionIndex = 0;
    }

    public void StartRequist(BaseGameEntity _entity )
    {
        UIAction(true);
        //partnerName = _name;
        questionList = DataManager.Instance().GetRecruitData(partnerName);
        question.text = questionList[questionIndex].question;
        yes.GetComponentInChildren<Text>().text = questionList[questionIndex].yes;
        no.GetComponentInChildren<Text>().text = questionList[questionIndex].no;
    }

    
    public void Answer(int _num)
    {
        answer.Add(_num);

        questionIndex++;
        if (questionIndex == questionList.Count)
        {
            UIAction(false);
            if (CompareAnswer(answer))
            {
                Debug.Log("Clear");
            }
            else
            {
                Debug.Log("Fail");
            }
        }
        else
        {
            SetQuestion(questionIndex);
        }
    }

    bool CompareAnswer(List<int> _answer)
    {
        for(int i = 0; i < _answer.Count; ++i )
        {
            if(_answer[i] != questionList[i].anwser)
            {
                return false;
            }
        }
        return true;
    }

    void SetQuestion(int _num)
    {
        question.text = questionList[_num].question;
        yes.GetComponentInChildren<Text>().text = questionList[_num].yes;
        no.GetComponentInChildren<Text>().text = questionList[_num].no;
    }
}
