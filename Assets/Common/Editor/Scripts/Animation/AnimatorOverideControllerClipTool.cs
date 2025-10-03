using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.ShortcutManagement;
using UnityEngine;

public class AnimatorOverideControllerClipTool : EditorWindow  {
    public AnimationClip[] _exposedClips;
    public AnimatorOverrideController _animatorOverideController;
    SerializedObject _SO;

    // TOOL TAKES IN A ANIMATOR AND CLIPS, IT THEN GOES TROUGH THE CLIPS AND TRIES TO MATCH AND ASSIGN THEM BY NAME

    [MenuItem("Tools/Animation Tools/Animator Clip Link")] 
    public static void OpenAnimatorOverideControllerClipTool() {
        EditorWindow window = EditorWindow.GetWindow<AnimatorOverideControllerClipTool>();
        window.titleContent = new GUIContent("AnimatorOverideController ClipTool");
    }

    void OnEnable(){
       _SO = new(this);
    } 

    void OnGUI(){
        EditorGUILayout.PropertyField(_SO.FindProperty("_exposedClips"), true);
        EditorGUILayout.PropertyField(_SO.FindProperty("_animatorOverideController"), true);
        _SO.ApplyModifiedProperties(); 
        
        if (GUILayout.Button("Start")){
           ReplaceAll(); 
        }
    }


    void ReplaceAll(){
        AnimationClipOverrides clipOverrides = new AnimationClipOverrides(_animatorOverideController.overridesCount);
        _animatorOverideController.GetOverrides(clipOverrides);
        AnimationClip _clipHandle;
        for (int i = 0; i < clipOverrides.Count; i++) {
            try{
                _clipHandle = _exposedClips.ToList().Find(x => x.name.ToLower().Contains(clipOverrides[i].Key.name.ToString().ToLower()));
                clipOverrides[clipOverrides[i].Key.name] = _clipHandle;
            }
            catch{
            
            }
        }
        _animatorOverideController.ApplyOverrides(clipOverrides);
    }

}

public class AnimationClipOverrides : List<KeyValuePair<AnimationClip, AnimationClip>> {
    public AnimationClipOverrides(int capacity) : base(capacity) {}

    public AnimationClip this[string name] {
        get { return this.Find(x => x.Key.name.Equals(name)).Value; }
        set {
            int index = this.FindIndex(x => x.Key.name.Equals(name));
            if (index != -1)
                this[index] = new KeyValuePair<AnimationClip, AnimationClip>(this[index].Key, value);
        }
    }
}