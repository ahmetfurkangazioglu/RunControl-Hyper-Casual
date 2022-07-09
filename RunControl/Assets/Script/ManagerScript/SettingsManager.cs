using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using MyLibrary;

public class SettingsManager : MonoBehaviour
{
    public Slider[] slider;
    public AudioSource ButtonSounds;
    MemoryManager memory = new MemoryManager();
    void Start()
    {
        slider[0].value = memory.Get_Float("MenuSound");
        slider[1].value = memory.Get_Float("FxSound");
        slider[2].value = memory.Get_Float("GameSound");
        ButtonSounds.volume = memory.Get_Float("FxSound");
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
}
