using System;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class NumberSystem : Singleton<NumberSystem>
{
#region Bits
    [SerializeField] private Switch sw;
    [SerializeField] private List<Bit> bits;
    private float deltaTime;
#endregion

#region MonoBehaviour
    private void FixedUpdate()
    {
        deltaTime = Time.fixedDeltaTime;
        timer.UpdateAll(deltaTime);
    }
    private void Update()
    {
        if(checkAnswer())
        {
            resetBits();
            StageManager.I.correct();
            GenerateNum();
        }
        /*
        if(sw.Activited)
        {
            checkAnswer();
            timer.setWaitTime(0.3f);
        }
        if(sw.Enable&&timer.CanTurnOff)
        {
            GenerateNum();
            sw.TrunOff();
        }
        */
    }
#endregion

#region Number
    private int limit = 1; 
    private int number = 0;
    public void GenerateNum()
    {
        limit = 1<<(bits.Count);
        number = UnityEngine.Random.Range(0,limit);
        StageManager.I.NumberAppear(number);
        Debug.Log(number);
    }
    private bool checkAnswer()
    {
        string ans = "";
        for(int i=bits.Count-1;i>=0;i--)
            ans += bits[i].bit()==1? "1":"0";
        if(number == Convert.ToInt32(ans,2))
        {
            
            return true;
        }
        else 
            return false;
    }
    private void resetBits()
    {
        for(int i=0;i<bits.Count;i++)
            bits[i].Reset();
    }
#endregion 

#region Timer
    [System.Serializable]
    private struct Timer
    {
        private float waitTime;
        private float generateTime;
        public readonly bool CanTurnOff => waitTime<=0;
        
        public void UpdateAll(float deltaTime)
        {
            void updateTimer(ref float timer)
            {
                timer -= deltaTime;
                if(timer <= -1)
                    timer = -1;
            }
            updateTimer(ref waitTime);
            updateTimer(ref generateTime);
        }
        public void setWaitTime(float time) => waitTime = time; 
        public void setResetTime(float time) => generateTime = time;
    }
    private Timer timer;
#endregion

}
