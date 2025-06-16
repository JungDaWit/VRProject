using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BestClearTime : MonoBehaviour
{
    public TextMeshProUGUI bestTimeText;
    public TextMeshProUGUI lastTimeText;

    private void OnEnable()
    {
        float Last = PlayerPrefs.GetFloat("LastClearTime", -1f);
        float Best = PlayerPrefs.GetFloat("BestClearTime", -1f);

        if (Last >= 0)
            lastTimeText.text = $"LAST TIME : {Mathf.Ceil(Last)} sec";

        if (Best >= 0)
            bestTimeText.text = $"BEST TIME : {Mathf.Ceil(Best)} sec";

    }

}
