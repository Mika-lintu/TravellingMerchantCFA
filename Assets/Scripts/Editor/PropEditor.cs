using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SpriteChanger))]
public class PropEditor : Editor
{
    /*
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Next Sprite"))
        {
            Selection.activeTransform.gameObject.GetComponent<SpriteChanger>().NextSprite();
            //SegmentSQLEditor segEditor = GameObject.FindGameObjectWithTag("SegmentEditor").GetComponent<SegmentSQLEditor>();
            SpriteChanger spriteCha = Selection.activeTransform.gameObject.GetComponent<SpriteChanger>();
            //segEditor.UpdateProp(spriteCha.spriteID, spriteCha.currentSprite, spriteCha.transform.position.x, spriteCha.transform.position.y);
        }

        if (GUILayout.Button("Previous Sprite"))
        {
            Selection.activeTransform.gameObject.GetComponent<SpriteChanger>().PreviousSprite();
            //SegmentSQLEditor segEditor = GameObject.FindGameObjectWithTag("SegmentEditor").GetComponent<SegmentSQLEditor>();
            SpriteChanger spriteCha = Selection.activeTransform.gameObject.GetComponent<SpriteChanger>();
            //segEditor.UpdateProp(spriteCha.spriteID, spriteCha.currentSprite, spriteCha.transform.position.x, spriteCha.transform.position.y);
        }

        if (GUILayout.Button("Save/Update"))
        {
            //SegmentSQLEditor segEditor = GameObject.FindGameObjectWithTag("SegmentEditor").GetComponent<SegmentSQLEditor>();
            SpriteChanger spriteCha = Selection.activeTransform.gameObject.GetComponent<SpriteChanger>();
            //segEditor.UpdateProp(spriteCha.spriteID, spriteCha.currentSprite, spriteCha.transform.position.x, spriteCha.transform.position.y);
        }

        if (GUILayout.Button("Remove Prop"))
        {
            SpriteChanger spriteCha = Selection.activeTransform.gameObject.GetComponent<SpriteChanger>();
            //SegmentSQLEditor segEditor = GameObject.FindGameObjectWithTag("SegmentEditor").GetComponent<SegmentSQLEditor>();
            //segEditor.RemoveProp(spriteCha.spriteID);
            DestroyImmediate(Selection.activeTransform.gameObject);
        }
        
    }

    void OnSceneGUI()
    {

        if (Event.current.type == EventType.MouseUp && Event.current.button == 0)
        {
            if (Selection.activeTransform.hasChanged)
            {
                Selection.activeTransform.hasChanged = false;
                //SegmentSQLEditor segEditor = GameObject.FindGameObjectWithTag("SegmentEditor").GetComponent<SegmentSQLEditor>();
                SpriteChanger spriteCha = Selection.activeTransform.gameObject.GetComponent<SpriteChanger>();
                //segEditor.UpdateProp(spriteCha.spriteID, spriteCha.currentSprite, spriteCha.transform.position.x, spriteCha.transform.position.y);

            }
        }
    }
    */
}
