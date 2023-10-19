using UnityEngine;

public class Extensions : MonoBehaviour
{
    public static Vector2 GetPlayer()
    {
        return PlayerController.I.transform.position;
    }
}
