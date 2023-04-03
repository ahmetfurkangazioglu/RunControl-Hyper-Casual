using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using MyLibrary;
using TMPro;


public class LevelManager : MonoBehaviour
{
    int CurrentLevel;
    [Header("Main Operation")]
    [SerializeField] Button[] Buttons;
    [SerializeField] Sprite LockSprite;
    [SerializeField] int LevelIndex =4;
    [SerializeField] AudioSource ButtonSound;

    [Header("Loading Operation")]
    [SerializeField] GameObject loadingPanel;
    [SerializeField] Slider LoadingSlider;

    [Header("Language Operation")]
    [SerializeField] TextMeshProUGUI[] AllText;

    DataManager dataManager = new DataManager();
    MemoryManager memory = new MemoryManager();
    List<LanguageSet> languageMainData = new List<LanguageSet>();
    List<LanguageSet> languageText = new List<LanguageSet>();
    void Start()
    {
        StartOperation();
        SetLanguage(memory.Get_String("Language"));
        SetLevel();
    } 
    public void LoadScene(int index)
    {
        ButtonSound.Play();
        StartCoroutine(LoadingAsync(index));
        SceneManager.LoadScene(index);
    }
    void SetLevel()
    {
        CurrentLevel = memory.Get_int("CurrentLevel") - LevelIndex;
        int index = 1;
        for (int i = 0; i < Buttons.Length; i++)
        {
            if (i < CurrentLevel)
            {
                Buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = index.ToString();
                int Sceneindex = index + LevelIndex;
                Buttons[i].onClick.AddListener(delegate { LoadScene(Sceneindex); });
            }
            else
            {
                Buttons[i].GetComponent<Image>().sprite = LockSprite;
                Buttons[i].GetComponent<Button>().enabled = false;
            }
            index++;
        }
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
    void StartOperation()
    {
        ButtonSound.volume = memory.Get_Float("FxSound");
        languageMainData = dataManager.LoadLanguageList();
        languageText.Add(languageMainData[3]);
    }
    IEnumerator LoadingAsync(int Index)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(Index);
        loadingPanel.SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            LoadingSlider.value = progress;
            yield return null;
        }
    }
}
