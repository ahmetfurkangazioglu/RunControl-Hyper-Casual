using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NpcCharacter : MonoBehaviour
{
    NavMeshAgent _navMesh;
    MainControl mainControl;
    public GameObject Target;
    void Start()
    {
        _navMesh = gameObject.GetComponent<NavMeshAgent>();
        mainControl = GameObject.FindWithTag("MainControl").GetComponent<MainControl>();
    }
    void LateUpdate()
    {
        _navMesh.SetDestination(Target.transform.position);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NeedleBox"))
        {
            mainControl.DeadEffects(gameObject,false,true);
            gameObject.SetActive(false);
        }
        if (other.CompareTag("Hamer"))
        {
            mainControl.DeadEffects(gameObject,true,true);
            gameObject.SetActive(false);
        }
        if (other.CompareTag("EnemyNpc"))
        {
            mainControl.DeadEffects(gameObject, false, true);
            gameObject.SetActive(false);
        }
    }
}
