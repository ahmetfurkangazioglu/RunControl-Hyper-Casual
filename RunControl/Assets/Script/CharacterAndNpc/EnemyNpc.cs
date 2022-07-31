using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNpc : MonoBehaviour
{
    bool isStartFight;
    NavMeshAgent navMeshAgent;
    Animator Anim;
    [SerializeField] GameObject Target;
    void Start()
    {
        Anim = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    void LateUpdate()
    {
        if(isStartFight)
        navMeshAgent.SetDestination(Target.transform.position);
    }
    public void AnimationTrriger()
    {
        Anim.SetBool("Fight", true);
        isStartFight = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Npc"))
        {
            GameObject.FindWithTag("MainControl").GetComponent<MainControl>().DeadEffects(gameObject, false, false);
            gameObject.SetActive(false);
        }
    }
}
