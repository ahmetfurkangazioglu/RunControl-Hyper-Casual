using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wild : MonoBehaviour
{
    public int FanPower;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Npc"))
        {
            other.GetComponent<Rigidbody>().AddForce(new Vector3(FanPower, 0, 0), ForceMode.Impulse);
        }  
    }
}
