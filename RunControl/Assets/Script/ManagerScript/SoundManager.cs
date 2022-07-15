using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static GameObject instantiate;
    public AudioSource MainSound;
    void Start()
    {
        MainSound.Play();
        MainSound.volume = PlayerPrefs.GetFloat("MenuSound");
        DontDestroyOnLoad(gameObject);
        if (instantiate==null)
        {
            instantiate = gameObject;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void LateUpdate()
    {
        MainSound.volume = PlayerPrefs.GetFloat("MenuSound");
    }
}
