using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyLibrary;


public class MainControl : MonoBehaviour
{
    public GameObject Target;
    public GameObject Deneme;
    public List<GameObject> NpcPooling;
    public List<GameObject> DeadEffectPooling;
    public List<GameObject> CreatEffectPooling;
    public static int NpcAmount;
    void Start()
    {
        NpcAmount = 1;
    }
    public void NpcCharacterManager(string Value,int Number,Transform position)
    {
        switch (Value)
        {
            case "Multiply":
                MathOperations.Multiply(Number, NpcPooling, position, CreatEffectPooling);
                break;
            case "Plus":
                MathOperations.Plus(Number, NpcPooling, position, CreatEffectPooling);
                break;
            case "Minus":
                MathOperations.Minus(Number, NpcPooling,DeadEffectPooling);
                break;
            case "Divided":
                MathOperations.Divided(Number, NpcPooling,DeadEffectPooling);
                break;
        }
    }

   
    public void DeadEffects(GameObject item)
    {
      MathOperations.EffectPoolingManager(DeadEffectPooling,item);
        NpcAmount--;
    }
}
