using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using MyLibrary;
using TMPro;


public class LevelManager : MonoBehaviour
{
    MemoryManager memory = new MemoryManager();
    public Button[] Buttons;
    public Sprite LockSprite;
    public int LevelIndex =4;
    public AudioSource ButtonSound;
    int CurrentLevel;
    void Start()
    {
       CurrentLevel = memory.Get_int("CurrentLevel") - LevelIndex;
        int index = 1;
        for (int i = 0; i < Buttons.Length; i++)
        {
            if (i < CurrentLevel)
            {
                Buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = index.ToString();
                int Sceneindex = index+ LevelIndex;
                Buttons[i].onClick.AddListener(delegate { LoadScene(Sceneindex);});
            }
            else
            {
                Buttons[i].GetComponent<Image>().sprite = LockSprite;
                Buttons[i].GetComponent<Button>().enabled = false;
            }
            index++;
        }
    } 
    public void LoadScene(int index)
    {
        ButtonSound.Play();
        SceneManager.LoadScene(index);
    }

}
