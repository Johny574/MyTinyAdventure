using System.Collections.Generic;
using UnityEditor;
using UnityEditor.U2D.Sprites;
using UnityEngine;

public class SpriteSliceCopier : EditorWindow {
    public Texture2D textureRectsToCopy;
    SerializedObject _SO;
    Object[] Objects;
    void OnEnable(){
        _SO = new(this);
    }

    [MenuItem("Tools/Sprite Tools/Sprite Slice Copier")]
    static void OpenSliceCopier() {
        EditorWindow.GetWindow(typeof(SpriteSliceCopier));
    }

    void OnGUI(){
        EditorGUILayout.PropertyField(_SO.FindProperty("textureRectsToCopy"), true);
        _SO.ApplyModifiedProperties();
        
        if (GUILayout.Button("Slice")){
            Objects = Selection.GetFiltered<Texture2D>(SelectionMode.Assets);
            
            var factory = new SpriteDataProviderFactories();    
            factory.Init();

            var copyDataProvider = factory.GetSpriteEditorDataProviderFromObject(textureRectsToCopy);
            copyDataProvider.InitSpriteEditorDataProvider();

            SpriteRect[] spriteRects = copyDataProvider.GetSpriteRects();

            for (int i = 0; i < Objects.Length; i++) {
                var t = Objects[i] as Texture2D;
                var dataProvider = factory.GetSpriteEditorDataProviderFromObject(t);
                dataProvider.InitSpriteEditorDataProvider();
                dataProvider.SetSpriteRects(spriteRects);
                dataProvider.Apply();
                AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(t), ImportAssetOptions.ForceUpdate);
            }

        }        
    } 
}