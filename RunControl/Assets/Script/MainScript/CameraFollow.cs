using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Target;
    public Vector3 Target_Offset;
    public bool isStartFightCam;
    public GameObject NewPos;
    void Start()
    {
        Target_Offset = transform.position - Target.position;
    }
    void LateUpdate()
    {
        if(!isStartFightCam)
        transform.position = Vector3.Lerp(transform.position, Target.position + Target_Offset,.125f);
        else
        transform.position = Vector3.Lerp(transform.position, NewPos.transform.position, .005f);
    }
}
