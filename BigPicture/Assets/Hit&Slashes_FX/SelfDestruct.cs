using UnityEngine;
using System.Collections;


public class SelfDestruct : MonoBehaviour {

    public IEnumerator LifeTime()
    {
        float oldTime = Time.time;
        while (true)
        {
            if (oldTime + 1.0f < Time.time)
            {
                this.gameObject.SetActive(false);
                EffectPool.Instance.Push(this.gameObject);
                break;
            }
            yield return null;
        }
    }
}
