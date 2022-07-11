using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyLibrary;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    MemoryManager memoryManager = new MemoryManager();
    DataManager dataManager = new DataManager();

    [Header("Other Operation")]
    public GameObject ExitPanel;
    public AudioSource ButtonSound;

    [Header("ItemData")]
    public List<ItemInfo> _ItemInfo = new List<ItemInfo>();

    [Header("LanguageSettings")]
    public TextMeshProUGUI[] AllText;
    public List<LanguageSet> languageMainData = new List<LanguageSet>();
    List<LanguageSet> languageText = new List<LanguageSet>();
    void Start()
    {
        dataManager.FirstSave(_ItemInfo,languageMainData);
        memoryManager.PrefsControl();
        ButtonSound.volume = memoryManager.Get_Float("FxSound");
        languageText.Add(languageMainData[0]);
        //memoryManager.Save_string("Language", "TR");
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
        SceneManager.LoadScene(memoryManager.Get_int("CurrentLevel"));
    }
    public void Quit(string exit)
    {
        ButtonSound.Play();
        switch (exit)
        {
            case "Sure":
                ExitPanel.SetActive(true);
                break;
            case "No":
                ExitPanel.SetActive(false);
                break;
            case "Yes":
                Application.Quit();
                break;
        }
    }
    private void SetLanguage(string Value)
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
}
