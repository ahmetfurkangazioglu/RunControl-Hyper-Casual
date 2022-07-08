using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public Animator anim;

    public void DisableAnim()
    {
        anim.SetBool("Play", false);
    }
}
