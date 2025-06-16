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
        Debug.Log("���ӽ���");
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
        Debug.Log("��������");
        Application.Quit();
    }
    public void Title()
    {
        Debug.Log("Ÿ��Ʋ");
        SceneManager.LoadScene("Title");
    }
    public void ResetClearTime()
    {
        PlayerPrefs.DeleteKey("BestClearTime");
    }
}
