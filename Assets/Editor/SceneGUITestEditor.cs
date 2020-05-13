using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DrawGizmoTest))]
public class SceneGUITestEditor : Editor 
{
    protected DrawGizmoTest ctr;
    
    [SerializeField]
    public string Name = "Hi";


    private void OnEnable() 
    {
        ctr = target as DrawGizmoTest;
        var data = EditorPrefs.GetString( "WINDOW_KEY", JsonUtility.ToJson( this, false ) );
        JsonUtility.FromJsonOverwrite( data, this );
    }
    
    public override void OnInspectorGUI() 
    {
        base.OnInspectorGUI();
        if(GUILayout.Button("Click Me Test"))
        {
            //Logic
            DrawGizmoTest ctr = target as DrawGizmoTest;
            EditorUtility.SetDirty( ctr );

            // //Logic
            // serializedObject.FindProperty("Name").stringValue = "Codinggamer";
            // serializedObject.ApplyModifiedProperties();

        }
    }

    private void OnSceneGUI() 
    {
        Handles.Label(ctr.transform.position, "This is a test!");
        Event _event = Event.current;

        if( _event.type == EventType.Repaint )
        {
            Draw();
        }
        else if ( _event.type == EventType.Layout )
        {
            HandleUtility.AddDefaultControl( GUIUtility.GetControlID( FocusType.Passive ) );
        }
        else
        {
            HandleInput( _event );
            HandleUtility.Repaint();
        }
    }

    void HandleInput( Event guiEvent )
    {
        Ray mouseRay = HandleUtility.GUIPointToWorldRay( guiEvent.mousePosition );
        Vector2 mousePosition = mouseRay.origin;
        if( guiEvent.type == EventType.MouseDown && guiEvent.button == 0 )
        {
            Debug.Log("mousePosition:"+mousePosition);
            ctr.poses.Add( mousePosition );
        }
    }

    void Draw()
    {
        //Draw a sphere
        Color originColor = Handles.color;
        Color circleColor = Color.red;
        Color lineColor = Color.yellow;
        Vector2 lastPos = Vector2.zero;
        for (int i = 0; i < ctr.poses.Count; i++)
        {
            var pos = ctr.poses[i];
            Vector2 targetPos = ctr.transform.position;
            //Draw Circle
            Handles.color = circleColor;
            Vector2 finalPos = targetPos + new Vector2( pos.x, pos.y);

            Handles.SphereHandleCap(  GUIUtility.GetControlID(FocusType.Passive ) , finalPos , Quaternion.identity, 0.2f , EventType.Repaint );
            //Draw line
            if(i > 0) 
            {
                Handles.color = lineColor;
                Handles.DrawLine( lastPos, pos );
            }
            lastPos = pos;
        }
        Handles.color = originColor;
    }

    private void OnDisable() 
    {
        var data = JsonUtility.ToJson( this, false  );
        EditorPrefs.SetString("WINDOW_KEY", data);
    }
}