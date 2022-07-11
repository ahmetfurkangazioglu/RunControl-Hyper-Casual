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
    MemoryManager memory = new MemoryManager();
    public Button[] Buttons;
    public Sprite LockSprite;
    public int LevelIndex =4;
    public AudioSource ButtonSound;
    [Header("LanguageSettings")]
    public TextMeshProUGUI[] AllText;
    public List<LanguageSet> languageMainData = new List<LanguageSet>();
    List<LanguageSet> languageText = new List<LanguageSet>();
    DataManager dataManager = new DataManager();
    void Start()
    {
        ButtonSound.volume = memory.Get_Float("FxSound");
        languageMainData = dataManager.LoadLanguageList();
        languageText.Add(languageMainData[3]);
        SetLanguage(memory.Get_String("Language"));
        SetLevel();
    } 
    public void LoadScene(int index)
    {
        ButtonSound.Play();
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

}
