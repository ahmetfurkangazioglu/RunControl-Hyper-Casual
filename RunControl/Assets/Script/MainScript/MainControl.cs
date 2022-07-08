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
    MemoryManager memoryManager = new MemoryManager();

    [Header("EffectsSettings")]
    public List<GameObject> DeadEffectPooling;
    public List<GameObject> CreatEffectPooling;
    public List<GameObject> BodyStainPooling;

    [Header("EnemySettings")]
    public List<GameObject> EnemyPooling;
    public int HowManyhEnemies;

    [Header("Hat Operation")]
    public GameObject[] Hatitems;
    public GameObject[] Weaponitems;
    public SkinnedMeshRenderer _meshRender;
    public Material[] NinjaMat;
    void Start()
    {
        SetItem();
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

    private void SetItem()
    {
        if (memoryManager.Get_int("MaterialIndex") != -1)
        {
            Material[] mats = _meshRender.materials;
            mats[0] = NinjaMat[memoryManager.Get_int("MaterialIndex")];
            _meshRender.material = mats[0];
        }
        if (memoryManager.Get_int("HatIndex") != -1)
            Hatitems[(memoryManager.Get_int("HatIndex"))].SetActive(true);
        if (memoryManager.Get_int("WeaponIndex") != -1)
            Weaponitems[(memoryManager.Get_int("WeaponIndex"))].SetActive(true);

    }
}
