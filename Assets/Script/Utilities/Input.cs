public class Input : PersistentSingleton<Input>
{
    public static float Hor,Ver,RawHor,RawVer;
    public static bool JumpDown,JumpUp;
    public static bool Push;
    void Update()
    {
        Hor = UnityEngine.Input.GetAxis("Horizontal");
        Ver = UnityEngine.Input.GetAxis("Vertical");
        RawHor = UnityEngine.Input.GetAxisRaw("Horizontal");
        RawVer = UnityEngine.Input.GetAxisRaw("Vertical");
        Push = UnityEngine.Input.GetButtonDown("Push");
        JumpDown = UnityEngine.Input.GetButtonDown("Jump");
        JumpUp = UnityEngine.Input.GetButtonUp("Jump");
    }
}
