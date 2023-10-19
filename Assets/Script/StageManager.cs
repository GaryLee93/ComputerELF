using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    [SerializeField] List<GameObject> numberSystem;
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
    public void GameStart()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1;
        pausing = false;
        UIboard.I.ResetCounter();
        UIboard.I.setReaminTime(time);
        for(int i=0;i<numberSystem.Count;i++)
        {
            if(numberSystem[i]!=null)
                numberSystem[i].GetComponent<NumberSystem>().GenerateNum();
        }
    }
    private void gamePuase()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0;
        pausing = true;
    }
}
