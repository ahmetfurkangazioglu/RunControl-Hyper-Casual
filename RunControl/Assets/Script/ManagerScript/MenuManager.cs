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
    void Start()
    {
        dataManager.FirstSave(_ItemInfo,"ItemDatas");
        memoryManager.PrefsControl();
    }
    public void LoadScenes(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(memoryManager.Get_int("CurrentLevel"));
    }
    public void Quit(string exit)
    {
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
