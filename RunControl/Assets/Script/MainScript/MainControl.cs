using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyLibrary;


public class MainControl : MonoBehaviour
{
    public GameObject Target;
    public List<GameObject> NpcPooling;
    public static int NpcAmount;
    public bool isStartFight;

    [Header("EffectsSettings")]
    public List<GameObject> DeadEffectPooling;
    public List<GameObject> CreatEffectPooling;
    public List<GameObject> BodyStainPooling;


    [Header("EnemySettings")]
    public List<GameObject> EnemyPooling;
    public int HowManyhEnemies;
    void Start()
    {
        NpcAmount = 1;
        CreateEnemy();
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

   
    public void DeadEffects(GameObject item,bool BodyStain,bool isDeadNpc)
    { 
        MathOperations.EffectPoolingManager(DeadEffectPooling, item);
        if (BodyStain)
        {
            MathOperations.BodyStain(BodyStainPooling, item);
        }
        FinalFightControl(isDeadNpc);


    }
    private void CreateEnemy()
    {
        for (int i = 0; i < HowManyhEnemies; i++)
        {
            EnemyPooling[i].SetActive(true);                                         
        }
    }

    public void StartFinalFight()
    {
        FightResult();
        isStartFight = true;
        foreach (var item in EnemyPooling)
        {    
          if (item.activeInHierarchy)
         {
           item.GetComponent<EnemyNpc>().AnimationTrriger();
         }          
        }
    }

    private void FightResult()
    {
        if (NpcAmount==1 || HowManyhEnemies==0)
        {
            isStartFight = false;
            Debug.Log(HowManyhEnemies);
            Debug.Log(NpcAmount);
            if (HowManyhEnemies == 0)
            {
                Debug.Log("Win");
                //win
            }
            else if (NpcAmount==1 && HowManyhEnemies>=1)
            {
                Debug.Log("Lose");                
                //lose
            }
        }
    }

    private void FinalFightControl(bool isDeadNpc)
    {
        if (isDeadNpc)
            NpcAmount--;
        else
            HowManyhEnemies--;

        if (isStartFight)
            FightResult();
    }
}
