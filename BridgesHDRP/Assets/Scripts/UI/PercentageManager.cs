using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PercentageManager: MonoBehaviour
{
    const float sizeDelta = 0.05f;
    const float animTime = 2f;

    [SerializeField] TMP_Text _percentageText;

    float curTime = 0f;

    bool isBig = true;
    
    void Update()
    {
        curTime += Time.deltaTime;

        if(curTime >= animTime)
        {
            isBig = !isBig;        
            curTime = 0f;
        }

        if (isBig)
        {
            _percentageText.fontSize -= sizeDelta;
        }
        else
        {
            _percentageText.fontSize += sizeDelta;
        }
    }

    public void DisplayPercentage(int curCount, int maxCount)
    {
        float percentage = ((float)curCount/ (float)maxCount) * 100f;
        _percentageText.SetText(percentage.ToString("0.") + "%");

    }

}
