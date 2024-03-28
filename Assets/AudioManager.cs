using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public static AudioManager Instance;
    public AudioSource[] audioSources;
    
    private void Awake() {
        // Singleton pattern
        if (Instance != null && Instance != this) { 
            Destroy(this); 
        }  else { 
            Instance = this; 
        }
    }
    
    private void Start() {
        audioSources = GetComponents<AudioSource>();
        audioSources[0].Play();
        StartCoroutine(FadeIn(audioSources[0], 6f));
    }

    public void PlayDamage() {
        audioSources[2].Play();
    }

    public void PlayDeath() {
        audioSources[3].Play();
    }

    public void PlayPickup() {
        audioSources[4].Play();
    }
    
    public void PlayFlush() {
        audioSources[5].Play();
        StartCoroutine(FadeOut(audioSources[5], 5f));
        audioSources[1].Play();
        StartCoroutine(FadeIn(audioSources[1], 3f));
    }
    
    public void PlayMenuClick() {
        audioSources[6].Play();
    }

    public void GameOver() {
        StartCoroutine(FadeOut(audioSources[1], 12f));
        audioSources[2].enabled = false;
        audioSources[3].enabled = false;
        audioSources[4].enabled = false;
    }
    
    public IEnumerator FadeOut(AudioSource audioSource, float fadeDuration)
    {
        float startVolume = audioSource.volume;
        while (audioSource.volume > 0) {
            audioSource.volume -= startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }
        
        audioSource.Stop();
        audioSource.volume = startVolume;
    }
    
    public IEnumerator FadeIn(AudioSource audioSource, float fadeDuration)
    {
        while (audioSource.volume < 1)
        {
            audioSource.volume += Time.deltaTime / fadeDuration;
            yield return null;
        }
        
        audioSource.volume = 1f;
    }
}
