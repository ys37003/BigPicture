using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Partner : AI {

    private StateMachine<Partner> stateMachine;
    private Group group;
    public GameObject enemy;

    [SerializeField]
    eJOB_TYPE job_Type;

    [SerializeField]
    private BoxCollider colEyeSight;

    [SerializeField]
    private ColliderAttack colliderAttack = null;

    [SerializeField]
    private float attackRange = 1.0f;
    void Start()
    {
        EntityInit(eENTITY_TYPE.NPC, eTRIBE_TYPE.HUMAN, job_Type);

        Data = DataManager.Instance().GetData(this.Tribe, this.Job);
        Animator = this.GetComponent<Animator>();
        NavAgent = this.GetComponent<NavAgent>();
        group = this.GetComponentInParent<Group>();
        AttackAble = true;

        colEyeSight.center = new Vector3(0, this.transform.position.y, Data.EyeSight);
        colEyeSight.size = new Vector3(Data.EyeSight * 3, 1, Data.EyeSight * 2);

        colliderAttack.Init(eENTITY_TYPE.MONSTER, Animator, Data.StatusData);
        foreach (AnimationTrigger trigger in Animator.GetBehaviours<AnimationTrigger>())
        {
            trigger.ColliderAttack = colliderAttack;
        }
        //this.GetGroup().Add(this);
        //SetDelegate();


        //stateMachine = new StateMachine<HoodSkull>(this);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
