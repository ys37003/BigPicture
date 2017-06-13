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
    private eJOB_TYPE job;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Human" && other.GetComponent<Character>() != null)
        {
            partner.transform.LookAt(other.transform);
            if (!TalkUI.IsShow && Input.GetKeyDown(KeyCode.F))
            {
                TalkBaseData data = DataManager.Instance().GetTalkBaseData(name);

                if (data == null)
                    return;

                TalkUI.CreateUI
                (
                    TeamManager.Instance.GetPlayer().transform.Find("head_up_pivot"), 
                    hud_ui_pivot,
                    data
                );
            }
        }
    }

    public void SetComponent()
    {
        transform.SetParent(playerGroup.transform);
        partner.AddComponent<Partner>();
        partner.GetComponent<Partner>().Init(colEyeSight, colliderAttack, player, job , hud_ui_pivot );
        partner.GetComponentInChildren<HitCollider>().Init(partner.GetComponent<Partner>(), partner.GetComponent<Partner>());
        Destroy(this);
    }
}