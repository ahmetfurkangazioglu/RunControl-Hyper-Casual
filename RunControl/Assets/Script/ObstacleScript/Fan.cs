using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    [SerializeField] Animator Anim;
    [SerializeField] int FanTime;
    [SerializeField] BoxCollider boxCollider;
    public void StopFan()
    {
       Anim.SetBool("Fan", false);
       boxCollider.enabled = false;
       StartCoroutine(FanStart());   
    }
    IEnumerator FanStart()
    {
        yield return new WaitForSeconds(FanTime);
        Anim.SetBool("Fan", true);
        boxCollider.enabled = true;
    }
}
