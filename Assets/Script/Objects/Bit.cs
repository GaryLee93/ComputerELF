using UnityEngine;

[System.Serializable]
public class Bit : Interactive
{
    public int bit() => state? 1:0;
    public void Reset()
    {
        state = false;
        changeSprite();
    }
}
