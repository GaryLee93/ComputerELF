using Unity.VisualScripting;
using UnityEngine;

public class CheckBox: MonoBehaviour
{
    [SerializeField] private Vector2 direction = Vector2.down;
    [SerializeField] private float distance = 1f;
    [SerializeField] private int rayCount = 3;
    [SerializeField] private Vector2 rightOffSet = Vector2.zero;
    [SerializeField] private Vector2 leftOffSet = Vector2.zero;
    [SerializeField] private LayerMask layer;
    public bool Detect()
    {
        if(rayCount==1)
            return Physics2D.Raycast(transform.position,direction,distance,layer);
        
        for(int i=1;i<rayCount-1;i++) // not detect the outline
        {
            float rate = (float)i/(rayCount-1);
            Vector2 pos = (Vector2)transform.position + Vector2.Lerp(leftOffSet,rightOffSet,rate);
            if(Physics2D.Raycast(pos,direction,distance,layer))
                return true;
        }

        // no path return true
        return false;
    }

#region SceneGUI
    [System.Serializable] private enum DrawMode{AlwaysDraw,DrawWhenSelected}
    [SerializeField] DrawMode drawMode = DrawMode.AlwaysDraw;
    [SerializeField] Color color = Color.red;
    void OnDrawGizmos()
    {
        if(drawMode == DrawMode.AlwaysDraw)
        {
            Gizmos.color = color;
            if(rayCount==1)
                Gizmos.DrawLine(transform.position,(Vector2)transform.position+direction*distance);
            
            for(int i=0;i<rayCount;i++)
            {
                float rate = (float)i/(rayCount-1);
                Vector2 pos = (Vector2)transform.position + Vector2.Lerp(leftOffSet,rightOffSet,rate);
                Gizmos.DrawLine(pos,pos+direction*distance);
            }
        }
    }

    private void OnDrawGizmosSelected() 
    {
        if(drawMode == DrawMode.DrawWhenSelected)
        {
            Gizmos.color = color;
            if(rayCount==1)
                Gizmos.DrawLine(transform.position,(Vector2)transform.position+direction*distance);
            
            for(int i=0;i<rayCount;i++)
            {
                float rate = (float)i/(rayCount-1);
                Vector2 pos = (Vector2)transform.position + Vector2.Lerp(leftOffSet,rightOffSet,rate);
                Gizmos.DrawLine(pos,pos+direction*distance);
            }
        }
    }    
#endregion
    
}
