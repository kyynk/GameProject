using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MENU : MonoBehaviour
{
    public GameObject pausemenu;
    public AudioMixer audioMxer;
        
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void PauseGame()
    {
        pausemenu.SetActive(true);
        Time.timeScale = 0f;
    }
    public void PauseQuit()
    {
        pausemenu.SetActive(false);
        Time.timeScale = 1f;
    }
    public void SetVolume(float value)
    {
        audioMxer.SetFloat("MainVolume", value);
    }
}