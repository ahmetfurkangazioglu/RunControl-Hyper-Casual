using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyLibrary;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Other Operation")]
    [SerializeField] GameObject[] GeneralPanel;
    [SerializeField] AudioSource ButtonSound;
    [SerializeField] Slider LoadingSlider;

    [Header("ItemData")]
    [SerializeField] List<ItemInfo> _ItemInfo = new List<ItemInfo>();

    [Header("LanguageSettings")]
    [SerializeField] TextMeshProUGUI[] AllText;
    [SerializeField] List<LanguageSet> languageMainData = new List<LanguageSet>();

    MemoryManager memoryManager = new MemoryManager();
    DataManager dataManager = new DataManager();
    List<LanguageSet> languageText = new List<LanguageSet>();

    void Awake()
    {
        StartOperation();
    }
    void Start()
    {
        SetLanguage(memoryManager.Get_String("Language"));
    }
    public void LoadScenes(int index)
    {
        ButtonSound.Play();
        SceneManager.LoadScene(index);
    }
    public void PlayGame()
    {
        ButtonSound.Play();
        StartCoroutine(LoadingAsync());
    }
    void Quit(string exit)
    {
        ButtonSound.Play();
        switch (exit)
        {
            case "Sure":
                GeneralPanel[0].SetActive(true);
                break;
            case "No":
                GeneralPanel[0].SetActive(false);
                break;
            case "Yes":
                Application.Quit();
                break;
        }
    }
    void SetLanguage(string Value)
    {
        if (Value=="TR")
        {
            for (int i = 0; i < AllText.Length; i++)
            {
                AllText[i].text = languageText[0].Language_TR[i].Text;
            }
        }
        else if(Value=="EN")
        {
            for (int i = 0; i < AllText.Length; i++)
            {
                AllText[i].text = languageText[0].Language_EN[i].Text;
            }
        }
    }
    void StartOperation()
    {
        dataManager.FirstSave(_ItemInfo, languageMainData);
        memoryManager.PrefsControl();
        ButtonSound.volume = memoryManager.Get_Float("FxSound");
        languageText.Add(languageMainData[0]);
    }
    IEnumerator LoadingAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(memoryManager.Get_int("CurrentLevel"));
        GeneralPanel[1].SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            LoadingSlider.value = progress;
            yield return null;
        }
    }
}
