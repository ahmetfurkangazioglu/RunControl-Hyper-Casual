using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [SerializeField] Animator anim;
    public void DisableAnim()
    {
        anim.SetBool("Play", false);
    }
}
