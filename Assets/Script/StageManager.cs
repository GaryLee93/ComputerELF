using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    [SerializeField] GameObject PausePanel;
    [SerializeField] float time;
    private static bool pausing = false;
    public bool Pausing => pausing; 
    private void Start()
    {
        GameStart();
    }
    private void Update()
    {
        if(UIboard.I.TimeIsUp())
        {
            gamePuase();
        }
    }
    public void correct() => UIboard.I.AddCounter();
    public void NumberAppear(int num) => UIboard.I.OutputNumber(num);
    public void GameStart()
    {
        PausePanel.SetActive(false);
        NumberSystem.I.GenerateNum();
        Time.timeScale = 1;
        pausing = false;
        UIboard.I.ResetCounter();
        UIboard.I.setReaminTime(time);
    }
    private void gamePuase()
    {
        UIboard.I.ClearNumber();
        PausePanel.SetActive(true);
        Time.timeScale = 0;
        pausing = true;
    }
}
