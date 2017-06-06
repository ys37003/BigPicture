using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDUIPoolManager : Singleton<HUDUIPoolManager>
{
    [SerializeField] private Queue<MonsterHUDUI> monsterHudUIqueue = new Queue<MonsterHUDUI>();
    [SerializeField] private Transform tfHUDUIRoot;
    [SerializeField] private Transform pool;

    private void Awake()
    {
        if (tfHUDUIRoot == null)
            tfHUDUIRoot = GameObject.Find("UI Root HUD").transform;

        pool = new GameObject().transform;
        pool.name = "HUDUI_Pool";
    }

    public MonsterHUDUI GetMonsterHUDUI(Transform target, StatusData status)
    {
        MonsterHUDUI ui = null;

        if (monsterHudUIqueue.Count > 0)
            monsterHudUIqueue.Dequeue();

        if (ui == null)
        {
            GameObject go = Resources.Load<GameObject>("UI/Prefabs/MonsterHUDUI");
            go = Instantiate(go, pool, false);
            ui = go.GetComponent<MonsterHUDUI>();
        }

        ui.transform.parent = tfHUDUIRoot;
        ui.transform.localScale = Vector3.one;
        ui.Init(target, status);
        ui.SetActive(true);
        return ui;
    }

    public void SetMonsterHUDUI(MonsterHUDUI ui)
    {
        monsterHudUIqueue.Enqueue(ui);
        ui.transform.parent = pool;
        ui.SetActive(false);
    }
}