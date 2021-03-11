using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    // Play music
    private AudioSource musics;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        musics = GetComponent<AudioSource>();
    }
}
