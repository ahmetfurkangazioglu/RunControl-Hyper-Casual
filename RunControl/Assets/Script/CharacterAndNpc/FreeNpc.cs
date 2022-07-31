using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FreeNpc : MonoBehaviour
{
    bool Collected;
    [Header("General Operation")]
    [SerializeField] MainControl mainControl;
    [SerializeField] GameObject Target;
    [SerializeField] Animator anim;
    [Header("Material Operation")]
    [SerializeField] SkinnedMeshRenderer _meshRender;
    [SerializeField] Material NewMatarial;
    [SerializeField] GameObject[] ChamgeTag;

    NavMeshAgent _meshAgent;
    void Start()
    {
        _meshAgent = gameObject.GetComponent<NavMeshAgent>();
    }
    void LateUpdate()
    {
        if(Collected)
        _meshAgent.SetDestination(Target.transform.position);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Npc")||other.CompareTag("Player"))
        {       
            if (!Collected)
            FreeNpcSettings();      
        }
        if (other.CompareTag("NeedleBox"))
        {
            mainControl.DeadEffects(gameObject, false, true);
            gameObject.SetActive(false);
        }
        if (other.CompareTag("Hamer"))
        {
            mainControl.DeadEffects(gameObject, true, true);
            gameObject.SetActive(false);
        }
        if (other.CompareTag("EnemyNpc"))
        {
            mainControl.DeadEffects(gameObject, false, true);
            gameObject.SetActive(false);
        }
    }
    void FreeNpcSettings() {
        Material[] mats = _meshRender.materials;
        mats[0] = NewMatarial;
        _meshRender.material = mats[0];
        Collected = true;
        gameObject.GetComponent<AudioSource>().Play();
        anim.SetBool("Run", true);
        foreach (var item in ChamgeTag)
        {
            item.tag = "Npc";
        }
        mainControl.NpcPooling.Add(gameObject);
        MainControl.NpcAmount++;     
    }

}
