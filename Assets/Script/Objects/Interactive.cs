using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class Interactive : MonoBehaviour
{
#region Variables
    protected bool state = false;
    protected bool lastState = false;
    [SerializeField] private Vector2 detectSize= new Vector2(3,3); 
    [SerializeField] private LayerMask layer;
    
#endregion

#region MonoBehaviour
    protected virtual void Update()
    {
        float angle = transform.eulerAngles.z;
        if(Physics2D.OverlapBox(transform.position,detectSize,angle,layer) 
        && Input.Push && !StageManager.I.Pausing)
        {
            state = !state;
            //Debug.Log(state);
            changeSprite();
        }
    }
    private void LateUpdate() => lastState = state;
    public bool Activited => !lastState&&state;
    public bool DisActivited => lastState&&!state;
#endregion

#region sprite
    [Header("Sprite")]
    [SerializeField] private Sprite on;
    [SerializeField] private Sprite off;
    protected void changeSprite()
    {
            if(on!=null && state)
                GetComponent<SpriteRenderer>().sprite = on;
            else if(off!=null && !state)
                GetComponent<SpriteRenderer>().sprite = off;
    }
#endregion
#region SceneGUI
    [System.Serializable] private enum DrawMode{AlwaysDraw,DrawWhenSelected}
    [SerializeField] DrawMode drawMode = DrawMode.AlwaysDraw;
    [SerializeField] Color color = Color.red;

    void OnDrawGizmos()
    {
        Gizmos.color = color;
        if(drawMode == DrawMode.AlwaysDraw)
            Gizmos.DrawWireCube(transform.position,detectSize);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = color;
        if(drawMode == DrawMode.DrawWhenSelected)
            Gizmos.DrawWireCube(transform.position,detectSize);
    }
#endregion
}
