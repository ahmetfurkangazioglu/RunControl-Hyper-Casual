using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyLibrary;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject ExitPanel;
    MemoryManager memoryManager = new MemoryManager();
    DataManager dataManager = new DataManager();
    public List<ItemInfo> _ItemInfo = new List<ItemInfo>();
    public AudioSource ButtonSound;
    void Start()
    {
        dataManager.FirstSave(_ItemInfo,"ItemDatas");
        memoryManager.PrefsControl();
       ButtonSound.volume = memoryManager.Get_Float("FxSound");
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
}
