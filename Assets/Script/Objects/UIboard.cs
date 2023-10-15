using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UIboard : Singleton<UIboard>
{
    [SerializeField] GameObject timer;
    [SerializeField] GameObject counter;
    [SerializeField] GameObject number;
    private int correctTimes = 0;
    private float remainTime;
    public void setReaminTime(float time) => remainTime = time;
    public bool TimeIsUp() => remainTime<=0;
    public void ResetCounter() => correctTimes = 0;
    public void AddCounter() => correctTimes++;
    public void ClearNumber() => number.GetComponent<TextMeshProUGUI>().text = "";
    public void OutputNumber(int num) => number.GetComponent<TextMeshProUGUI>().text = num.ToString();
    private void FixedUpdate()
    {
        remainTime -= Time.deltaTime;
        if(remainTime<0) 
            remainTime = 0;
    }
    private void Update()
    {
        timer.GetComponent<TextMeshProUGUI>().text = remainTime.ToString("0.00");
        counter.GetComponent<TextMeshProUGUI>().text = correctTimes.ToString();
    }
}
