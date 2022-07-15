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
    bool GameResult=true;
    [Header("General Operation")]
    public Slider LoadingSlider;
    public GameObject Target;
    public bool isStartFight;
    public GameObject[] GeneralPanel;
    [Header("Npc Operation")]
    public List<GameObject> NpcPooling;
    public static int NpcAmount;
    [Header("Effects Operation")]
    public List<GameObject> DeadEffectPooling;
    public List<GameObject> CreatEffectPooling;
    public List<GameObject> BodyStainPooling;
    [Header("Enemy Operation")]
    public List<GameObject> EnemyPooling;
    public int HowManyEnemies;
    [Header("Items Operation")]
    public GameObject[] Hatitems;
    public GameObject[] Weaponitems;
    public SkinnedMeshRenderer _meshRender;
    public Material[] NinjaMat;
    [Header("Sound Operation")]
    public AudioSource[] GeneralSound;
    public Slider SoundSlider;
    [Header("Language Operation")]
    public TextMeshProUGUI[] AllText;

    DataManager dataManager = new DataManager();
    MemoryManager memoryManager = new MemoryManager();
    List<LanguageSet> languageMainData = new List<LanguageSet>();
    List<LanguageSet> languageText = new List<LanguageSet>();
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
    public void NextLevel()
    {
        GeneralSound[0].Play();
        StartCoroutine(LoadingAsync(scene.buildIndex + 1));
    }
    void FightResult()
    {
        if (GameResult && (NpcAmount == 1 || HowManyEnemies == 0))
        {
            if (HowManyEnemies == 0)
            {
                GameResult = false;
                if (scene.buildIndex==memoryManager.Get_int("CurrentLevel"))
                {
                    int point = NpcAmount * 15;
                    memoryManager.Save_int("TotalPoint", memoryManager.Get_int("TotalPoint") + point);
                    memoryManager.Save_int("CurrentLevel", scene.buildIndex + 1);
                }
                else
                {
                    int point = NpcAmount * 5;
                    memoryManager.Save_int("TotalPoint", memoryManager.Get_int("TotalPoint") + point);
                }      
                GeneralPanel[3].SetActive(true);
                //win
            }
            else if (NpcAmount == 1 && HowManyEnemies >= 1)
            {
                GameResult = false;
                GeneralPanel[2].SetActive(true);
                //lose
            }
        }
    }
    void FinalFightControl(bool isDeadNpc)
    {
        if (isDeadNpc)
            NpcAmount--;
        else
            HowManyEnemies--;

        if (isStartFight)
            FightResult();
    }
    void CreateEnemy()
    {
        for (int i = 0; i < HowManyEnemies; i++)
        {
            EnemyPooling[i].SetActive(true);
        }
    }
    void SetItem()
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
    void SetLanguage(string Value)
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
    IEnumerator LoadingAsync(int Index)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(Index);
        GeneralPanel[4].SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            LoadingSlider.value = progress;
            yield return null;
        }
    }
}
