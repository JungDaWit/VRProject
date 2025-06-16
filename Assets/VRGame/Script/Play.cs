using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using UnityEditor.Purchasing;

public class Play : MonoBehaviour
{
   [SerializeField] private AudioClip clickSound;
   [SerializeField] private AudioSource audioSource;
    public void GamePlay()
    {
        Debug.Log("게임시작");
        SceneManager.LoadScene("InGame");
       
    }
    public void ButtonSound()
    {
        if (audioSource != null && clickSound != null) 
        {
            audioSource.PlayOneShot(clickSound);
        }
    }
    public void GameExit()
    {
        Debug.Log("게임종료");
        Application.Quit();
    }
    public void Title()
    {
        Debug.Log("타이틀");
        SceneManager.LoadScene("Title");
    }
    public void ResetClearTime()
    {
        PlayerPrefs.DeleteKey("BestClearTime");
    }
}
