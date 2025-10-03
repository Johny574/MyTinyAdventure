using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class MassAnimatorOverideControllerClipTool : EditorWindow  {
    public UnityEngine.Object[] _directories;
    SerializedObject _SO;
    
    AnimationClipOverrides _clipOverrides;

    void OnEnable(){
        _SO = new SerializedObject(this);
    }

    void OnGUI(){
        EditorGUILayout.PropertyField(_SO.FindProperty("_directories"), true);
        _SO.ApplyModifiedProperties();

        if (GUILayout.Button("Start")){

            foreach (var obj in _directories) {
                Tuple<AnimationClip[], AnimatorOverrideController> _animationData = GetClipsAndAnimator(obj);
                
                if (_animationData.Item2 == null){
                    Debug.Log("Cant find animator overide controller");
                    return;
                }

                _clipOverrides = new AnimationClipOverrides(_animationData.Item2.overridesCount);
                _animationData.Item2.GetOverrides(_clipOverrides);

                WriteAllClips(_animationData);

            }
        }

        GUILayout.Space(10);

        if (GUILayout.Button("Clear animator clips")){
            foreach (var obj in _directories) {
                Tuple<AnimationClip[], AnimatorOverrideController> _animationData = GetClipsAndAnimator(obj);
                ClearClips(_animationData);
            }
        }

        GUILayout.Space(10);

        if (GUILayout.Button("Clear clip curves")){
              foreach (var obj in _directories) {
                Tuple<AnimationClip[], AnimatorOverrideController> _animationData = GetClipsAndAnimator(obj);
                ClearClipCurves(_animationData);
            }
        }

    }


    void WriteAllClips(Tuple<AnimationClip[], AnimatorOverrideController> animationData){
        AnimationClip _clipHandle;
        for (int i = 0; i < _clipOverrides.Count; i++) {

            var desiredclips = animationData.Item1.ToList().FindAll(x => x.name.ToLower().Contains(_clipOverrides[i].Key.name.ToString().ToLower()));

             _clipHandle = desiredclips.Find(x => x.name.ToLower().Contains(_clipOverrides[i].Key.name.ToString().ToLower())); 

            if (_clipOverrides[i].Key.name.Contains("Gun")){
                _clipHandle = desiredclips.Find(x => x.name.ToLower().Contains(_clipOverrides[i].Key.name.ToString().ToLower()) && x.name.Contains("Gun"));
            }
            else if (_clipOverrides[i].Key.name.Contains("Gift")){
                _clipHandle = desiredclips.Find(x => x.name.ToLower().Contains(_clipOverrides[i].Key.name.ToString().ToLower()) && x.name.Contains("Gift"));
            }
            else if (_clipOverrides[i].Key.name.Contains("Push")){
                _clipHandle = desiredclips.Find(x => x.name.ToLower().Contains(_clipOverrides[i].Key.name.ToString().ToLower()) && x.name.Contains("PushCart"));
            }
            else if (_clipOverrides[i].Key.name.Contains("Sit")){
                _clipHandle = desiredclips.Find(x => x.name.ToLower().Contains(_clipOverrides[i].Key.name.ToString().ToLower()) && x.name.Contains("Sit"));
            }
           
            

            _clipOverrides[_clipOverrides[i].Key.name] = _clipHandle;
            
        }
        animationData.Item2.ApplyOverrides(_clipOverrides);
    }

    Tuple<AnimationClip[], AnimatorOverrideController> GetClipsAndAnimator(UnityEngine.Object directory){
        var path = AssetDatabase.GetAssetPath(directory);
        var files = Directory.GetFiles(path);

        List<AnimationClip> clips = new();
        AnimatorOverrideController controller = null;

        foreach (var f in files) {
            if (f.EndsWith(".overrideController")){
                controller = (AnimatorOverrideController)AssetDatabase.LoadAssetAtPath(f, typeof(AnimatorOverrideController));
            }
            else{
                var c = (AnimationClip)AssetDatabase.LoadAssetAtPath(f, typeof(AnimationClip));
                if (c != null){
                    clips.Add((AnimationClip)AssetDatabase.LoadAssetAtPath(f, typeof(AnimationClip)));
                }
            }
        }
        return Tuple.Create(clips.ToArray(), controller);
    }

    void ClearClipCurves(Tuple<AnimationClip[], AnimatorOverrideController> animationData){
        foreach (var clip in animationData.Item1) {
            clip.ClearCurves(); 
        }
    }


    void ClearClips(Tuple<AnimationClip[], AnimatorOverrideController> animationData){
        for (int i = 0; i < _clipOverrides.Count; i++) {
           _clipOverrides[_clipOverrides[i].Key.name] = null;    
        }
        animationData.Item2.ApplyOverrides(_clipOverrides);
    }

    // TOOL TAKES IN A ANIMATOR AND CLIPS, IT THEN GOES TROUGH THE CLIPS AND TRIES TO MATCH AND ASSIGN THEM BY NAME

    [MenuItem("Tools/Animation Tools/Mass Animator Clip Link")]
    public static void OpenMassAnimatorOverideControllerClipTool() {
        EditorWindow window = EditorWindow.GetWindow<MassAnimatorOverideControllerClipTool>();
        window.titleContent = new GUIContent("Mass AnimatorOverideController ClipTool");
    }
}