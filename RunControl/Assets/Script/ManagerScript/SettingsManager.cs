using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using MyLibrary;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    public Slider[] slider;
    public Button[] ArrowBut;
    public TextMeshProUGUI LanguageText;
    public AudioSource ButtonSounds;
    MemoryManager memory = new MemoryManager();
    [Header("LanguageSettings")]
    public TextMeshProUGUI[] AllText;
    public List<LanguageSet> languageMainData = new List<LanguageSet>();
    List<LanguageSet> languageText = new List<LanguageSet>();
    DataManager dataManager = new DataManager();
    void Start()
    {
        languageMainData = dataManager.LoadLanguageList();
        languageText.Add(languageMainData[5]);
        SetLanguage(memory.Get_String("Language"));
        ButtonControl();
        SetVolume();
    }
    public void SettingManager(string Operation)
    {
        switch (Operation)
        {
            case "MenuSound":
                memory.Save_float("MenuSound", slider[0].value);
                break;
            case "FxSound":
                memory.Save_float("FxSound", slider[1].value);
                ButtonSounds.volume = memory.Get_Float("FxSound");
                break;
            case "GameSound":
                memory.Save_float("GameSound", slider[2].value);
                break;
        }
    }
    public void MainMenu()
    {
        ButtonSounds.Play();
        SceneManager.LoadScene(0);
    }
    void SetVolume()
    {
        slider[0].value = memory.Get_Float("MenuSound");
        slider[1].value = memory.Get_Float("FxSound");
        slider[2].value = memory.Get_Float("GameSound");
        ButtonSounds.volume = memory.Get_Float("FxSound");
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
    public void ChangeLanguage(string Value)
    {
        ButtonSounds.Play();
        if (Value=="Next")
        {
            LanguageText.text = "English";
            ArrowBut[0].interactable = true;
            ArrowBut[1].interactable = false;
            memory.Save_string("Language", "EN");
        }
        else
        {
            LanguageText.text = "Türkçe";
            ArrowBut[0].interactable = false;
            ArrowBut[1].interactable = true;
            memory.Save_string("Language", "TR");
        }
        SetLanguage(memory.Get_String("Language"));
    }
    void ButtonControl()
    {
        if (memory.Get_String("Language")=="EN")
        {
            ArrowBut[1].interactable = false;
            LanguageText.text = "English";
        }
        else
        {
            ArrowBut[0].interactable = false;
            LanguageText.text = "Türkçe";
        }
    }
}
