using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainControl : MonoBehaviour
{
    public GameObject Target;
    public GameObject Deneme;
    public List<GameObject> NpcPooling;
    public int NpcAmount;
    void Start()
    {
        NpcAmount = 1;
    }

    void Update()
    {
      
    }

    public void NpcCharacterManager(string Value, Transform position)
    {
        switch (Value)
        {
            case "2X":
                int Commit=0;
                foreach (var item in NpcPooling)
                {             
                    if (Commit<NpcAmount)
                    {
                        if (!item.activeInHierarchy)
                        {
                            item.transform.position = position.position;
                            item.SetActive(true);
                            Commit++;
                        }
                    }
                    else
                    {                     
                        NpcAmount =NpcAmount * 2;
                        break;
                    }
                
                }
                break;

        }
    }
}
