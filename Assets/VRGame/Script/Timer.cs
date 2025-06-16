using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;



public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    private float time;

    private void Awake()
    {
        time = 0f;
    }

    private void Update()
    {
        if (time >= 0)
            time += Time.deltaTime;

        timeText.text = Mathf.Ceil(time).ToString();
    }
    public float GetClearTime() => time;
}

