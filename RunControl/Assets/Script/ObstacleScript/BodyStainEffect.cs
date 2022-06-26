using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyStainEffect : MonoBehaviour
{
  
    IEnumerator Start()
    {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }

 
}
