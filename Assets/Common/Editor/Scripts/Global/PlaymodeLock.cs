// using UnityEditor;
// using UnityEngine;

// [InitializeOnLoad]
// public static class PlayModeLock
// {
//     static PlayModeLock()
//     {
//         EditorApplication.playModeStateChanged += HandlePlayMode;
//     }

//     static void HandlePlayMode(PlayModeStateChange state)
//     {
//         if (EditorApplication.isPlayingOrWillChangePlaymode)
//         {
//             Debug.Log("Playmode disabled");
//             EditorApplication.isPlaying = false;
//         }
//     }
// }
