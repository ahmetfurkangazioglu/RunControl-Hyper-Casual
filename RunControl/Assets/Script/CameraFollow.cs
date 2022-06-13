using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Target;
    public Vector3 Target_Offset;

    private void Start()
    {
        Target_Offset = transform.position - Target.position;
    }
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, Target.position + Target_Offset,.125f);
    }
}
