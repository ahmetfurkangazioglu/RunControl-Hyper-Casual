using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainControl : MonoBehaviour
{
    public GameObject Target;
    public GameObject Deneme;
    public List<GameObject> NpcPooling;
    public int NpcAmount=1;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            foreach (var item in NpcPooling)
            {
                if (!item.activeInHierarchy)
                {
                    item.transform.position = Deneme.transform.position;
                    item.SetActive(true);
                    NpcAmount++;
                    break;
                }
            }
        }
    }

    public void NpcCharacterManager()
    {

    }
}
