using UnityEngine;
using System;
//using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class DrawGizmoTest : MonoBehaviour
{
    public List<Vector2> poses;
    public string Name;
    private void OnDrawGizmos() 
    {
        Color originColor = Gizmos.color;
        Gizmos.color = Color.red;
        if( poses!=null && poses.Count>0 )
        {
            //Draw Sphere
            for (int i = 0; i < poses.Count; i++)
            {
                Gizmos.DrawSphere( poses[i], 0.2f );
            }
            //Draw Line
            Gizmos.color = Color.yellow;
            Vector2 lastPos = Vector2.zero;
            for (int i = 0; i < poses.Count; i++)
            {
                if( i > 0 )
                {
                    Gizmos.DrawLine( lastPos, poses[i] );
                }
                lastPos = poses[i];
            }
        }
        Gizmos.color = originColor;
    } 
    
}
