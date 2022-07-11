using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MyLibrary;
using UnityEngine.SceneManagement;
using TMPro;


public class MainControl : MonoBehaviour
{
    Scene scene;
    public GameObject Target;
    public List<GameObject> NpcPooling;
    public static int NpcAmount;
    public bool isStartFight;
    public AudioSource[] GeneralSound;
    public GameObject[] GeneralPanel;
    public Slider SoundSlider;
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
    [Header("LanguageSettings")]
    public TextMeshProUGUI[] AllText;
    public List<LanguageSet> languageMainData = new List<LanguageSet>();
    List<LanguageSet> languageText = new List<LanguageSet>();
    DataManager dataManager = new DataManager();
    void Start()
    {
        scene = SceneManager.GetActiveScene();
        NpcAmount = 1;
        Destroy(GameObject.FindWithTag("SoundManager"));
        LanguageManager();
        SetItem();
        CreateEnemy();
        SetVolume();
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
    public void SoundSettings(string Settings)
    {
        switch (Settings)
        {
            case "Settings":
                Time.timeScale = 0;
                GeneralSound[0].Play();
                GeneralPanel[0].SetActive(true);
                break;
            case "Sound":
                GeneralSound[1].volume = SoundSlider.value;
               memoryManager.Save_float("GameSound", SoundSlider.value);
                break;
            case "Close":
                GeneralSound[0].Play();
                GeneralPanel[0].SetActive(false);
                Time.timeScale = 1;
                break;
        }
    }
    public void Quit(string exit)
    {
        GeneralSound[0].Play();
        Time.timeScale = 0;
        switch (exit)
        {          
            case "Stop":
                GeneralPanel[1].SetActive(true);
                break;
            case "Restart":
                SceneManager.LoadScene(scene.buildIndex);
                Time.timeScale = 1;
                break;
            case "Continue":
                GeneralPanel[1].SetActive(false);
                Time.timeScale = 1;
                break;
            case "BackMenu":
                SceneManager.LoadScene(0);
                Time.timeScale = 1;
                break;
        }
    }
    private void SetLanguage(string Value)
    {
        if (Value == "TR")
        {
            for (int i = 0; i < AllText.Length; i++)
            {
                AllText[i].text = languageText[0].Language_TR[i].Text;
            }
        }
        else if (Value == "EN")
        {
            for (int i = 0; i < AllText.Length; i++)
            {
                AllText[i].text = languageText[0].Language_EN[i].Text;
            }
        }
    }
    void SetVolume()
    {
        GeneralSound[0].volume = memoryManager.Get_Float("FxSound");
        GeneralSound[1].volume = memoryManager.Get_Float("GameSound");
        SoundSlider.value = memoryManager.Get_Float("GameSound");
    }
    void LanguageManager()
    {
        languageMainData = dataManager.LoadLanguageList();
        languageText.Add(languageMainData[1]);
        SetLanguage(memoryManager.Get_String("Language"));
    }
}
