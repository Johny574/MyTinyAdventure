using UnityEditor;
using UnityEngine;




[CustomEditor(typeof(RoomFirstGenerator))]
public class CustomDungeonGenerator : Editor 
{
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        if (GUILayout.Button("Clear"))
            ((AbstractDungeonGenerator)target).ClearAll();

        if (GUILayout.Button("Generate Dungeon"))
            ((AbstractDungeonGenerator)target).GenerateDungeon();
        
        if (GUILayout.Button("Generate Floor"))
            ((AbstractDungeonGenerator)target).GenerateFloor();

        if (GUILayout.Button("Generate Walls"))
            ((AbstractDungeonGenerator)target).GenerateWalls();
    }
}