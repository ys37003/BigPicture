using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparePartner : MonoBehaviour
{
    public ePARTNER_NAME name;

    [SerializeField]
    private GameObject partner;

    [SerializeField]
    private BoxCollider colEyeSight;

    [SerializeField]
    private Transform hud_ui_pivot;

    [SerializeField]
    private ColliderAttack colliderAttack = null;

    [SerializeField]
    private GameObject playerGroup;

    [SerializeField]
    private BaseGameEntity player;

    [SerializeField]
    private eJOB_TYPE _job;

    void Start()
    {
        name = ePARTNER_NAME.DONUT;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Human" && other.GetComponent<Character>() != null)
        {
            this.partner.transform.LookAt(other.transform);
            if (!TalkUI.IsShow && Input.GetKeyDown(KeyCode.F))
            {
                TalkUI.CreateUI
                (
                    (TeamManager.Instance.GetCharacter(0) as Character).transform.Find("head_up_pivot"), 
                    hud_ui_pivot,
                    DataManager.Instance().GetTalkBaseDataList(name)[0]
                );
            }
        }
    }

    public void SetComponent()
    {
        this.transform.SetParent(playerGroup.transform);
        this.partner.AddComponent<Partner>();
        this.partner.GetComponent<Partner>().Init(colEyeSight, colliderAttack, player, _job , hud_ui_pivot );
        this.partner.GetComponentInChildren<HitCollider>().Init(this.partner.GetComponent<Partner>(), this.partner.GetComponent<Partner>());
        Destroy(this);
    }
}