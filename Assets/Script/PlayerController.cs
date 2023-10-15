
using UnityEngine;
public class PlayerController : Singleton<PlayerController>
{
#region Varibles
    [Header("Components")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform tf;
    [SerializeField] CheckBox groundedChecker;
    private float dTime;
#endregion

#region Monobehaviour
    private void Update()
    {
        if(Input.JumpDown)
            timer.setJumpCD(jumpBuffer);
        jumpDectect();   
    }
    private void FixedUpdate()
    {
        dTime = Time.fixedDeltaTime;
        gravityCalculated();
        runCalculated();
        timer.UpdateAll(dTime);
        //Debug.Log(rb.velocity);
    }
#endregion

#region Timer
    [System.Serializable]
    private struct Timer
    {
        private float jumpCD;
        public readonly bool JumpNotCold => jumpCD>0;
        public void UpdateAll(float deltaTime)
        {
            void updateTimer(ref float timer)
            {
                timer -= deltaTime;
                if(timer <= -1)
                    timer = -1;
            }
            updateTimer(ref jumpCD);
        }
        public void setJumpCD(float time) => jumpCD = time; 
    }
    private Timer timer;
#endregion

#region Move
    [Header("Move")]
    [SerializeField] private float maxHoriSpeed = 13f;
    [SerializeField] private float runAccelertion=90f,runDeccelertion=90f;

    private void runCalculated()
    {
        float RawHor = Input.RawHor;
        float v = rb.velocity.x;
        if(RawHor !=0)
        {
            v += RawHor*runAccelertion*dTime;
            v = Mathf.Clamp(v,-maxHoriSpeed,maxHoriSpeed);
        }
        else 
        {
            float abs = Mathf.Abs(v);
            abs -= runDeccelertion*dTime;
            if(abs<0) 
                abs = 0;
            v = v<0? -abs:abs;
        }
        rb.velocity = new Vector2(v,rb.velocity.y);
    }
#endregion

#region Gravity
    [Header("Gravity")]
    [SerializeField] private float gravity = 80f;
    [SerializeField] private float terminalSpeed = 30f;
    private void gravityCalculated()
    {
        rb.gravityScale = gravity / Mathf.Abs(Physics2D.gravity.y);
        if(jumpApex)
            rb.gravityScale *= jumpApexGravityScale;
        else if(jumpCutting)
            rb.gravityScale *= jumpCutGravityScale;

        float v = rb.velocity.y;

        // limit falling velocity to terminal velocity
        v = Mathf.Max(v,-terminalSpeed);
        rb.velocity = new Vector2(rb.velocity.x,v);
    }
#endregion

#region Jump
    [Header("Jump")]
    //for jump
    [SerializeField] private float jumpSpeed = 30f;
    [SerializeField] private float jumpBuffer = 0.1f;
    private bool isJumping = false;
    //for jumpCut
    [SerializeField] private float jumpCutSpeedScale = 0.5f;
    [SerializeField] private float jumpCutGravityScale = 2f;
    private bool jumpCutting = false;
    //for jumpApexStop
    [SerializeField] private float jumpApexTheresHold = 3f;
    [SerializeField] private float jumpApexGravityScale = 0.5f;
    private bool jumpApex = false;
    private void jump(float speed)
    {
        if(isJumping)
            return ;
        rb.velocity = new Vector2(rb.velocity.x,speed);
        isJumping = true;
        jumpCutting = false;
        timer.setJumpCD(0f);
    }
    private void jumpDectect()
    {   
        if(groundedChecker.Detect() && Input.JumpDown && timer.JumpNotCold)
            jump(jumpSpeed);

        //all reset
        if(groundedChecker.Detect() && isJumping )
        {
            jumpCutting = false;
            isJumping = false;
        }

        // apex stop
        jumpApex = !groundedChecker.Detect() && Mathf.Abs(rb.velocity.y)<=jumpApexTheresHold;
        
        // jumping be cut
        if(!groundedChecker.Detect() && Input.JumpUp && !jumpCutting && rb.velocity.y>0)
        {
            jumpCutting = true;
            rb.velocity = new(rb.velocity.x,rb.velocity.y*jumpCutSpeedScale);
        }
    }
#endregion

}
