using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NpcCharacter : MonoBehaviour
{
    private NavMeshAgent _navMesh;
    GameObject Target;
    void Start()
    {
        _navMesh = gameObject.GetComponent<NavMeshAgent>();
        Target = GameObject.FindWithTag("MainControl").GetComponent<MainControl>().Target;
    }

    void LateUpdate()
    {
        _navMesh.SetDestination(Target.transform.position);
    }


}
