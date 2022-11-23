using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioThemeHandler : MonoBehaviour
{
    private static AudioThemeHandler instance = null;
    public static AudioThemeHandler Instance
    {
        get { return instance; }
    }
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();
    }
}
