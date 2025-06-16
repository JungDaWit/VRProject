using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClearTime : MonoBehaviour
{
    public TextMeshProUGUI clearTimeText;

    private void OnEnable()
    {
        float lastClearTime = PlayerPrefs.GetFloat("LastClearTime", 0);
        Debug.Log(" 불러온 클리어 타임: " + lastClearTime);
        clearTimeText.text = $"CLEAR TIME : {Mathf.Ceil(lastClearTime)} sec !";
    }
}
