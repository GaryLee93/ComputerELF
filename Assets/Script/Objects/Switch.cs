using Unity.VisualScripting;
using UnityEngine;

public class Switch : Interactive
{
    public bool Enable => state;
    private bool opened = false;
    private bool lastIsOpened = false;
    protected override void Update()
    {
        base.Update();
        opened = state;
        debug();
    }
    private void LateUpdate() => lastIsOpened = opened;
    public void TrunOff() => state = false;

#region Debug
    void debug()
    {
        if(state)
            GetComponent<SpriteRenderer>().color = Color.red;
        else 
            GetComponent<SpriteRenderer>().color = Color.white;
    }
#endregion
}
