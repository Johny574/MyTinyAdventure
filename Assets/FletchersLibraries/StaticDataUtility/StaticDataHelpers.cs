using UnityEngine;
using System.IO;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace FletcherLibraries
{
    /// <summary>
    /// Contains helper methods for bulk interaction with scriptable objects representing static data in the project
    /// </summary>
    public static class StaticDataHelpers
    {
#if UNITY_EDITOR
        public const string DEFAULT_DATAOBJECT_FOLDER = "Assets/"; // Can edit this to make it more precise for better static data compliation performance. Make sure this ends in a slash to get everything in a folder.
        public delegate void ForEachDelegate<T>(T obj);

        /// <summary>
        /// Calls 'forEachCallback' on every object in the project of type T. Supports inheritance.
        /// </summary>
        public static void RunForEachObjectOfType<T>(ForEachDelegate<T> forEachCallback) where T : ScriptableObject
        {
            string[] guids = AssetDatabase.FindAssets(string.Format("t:{0}", typeof(T)));
            for (int i = 0; i < guids.Length; ++i)
            {
                T obj = AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(guids[i]));
                forEachCallback(obj);
            }
        }

        /// <summary>
        /// Returns an array of all Static Data Scriptable Objects of the given type in the project. Used in the process of creating a database of all Static Data objects.
        /// </summary>
        public static T[] CompileAllObjectsOfType<T>(string folderPath, IEnumerable<T> existingCollection) where T : StaticDataObject
        {
            // path = path.Replace(this.name + ".asset", "");
            List<T> allObjects = new List<T>();
            AssetDatabaseHelpers.GetAtPath<T>(folderPath, allObjects, false);
            allObjects.Sort((T a, T b) =>
            {
                if (a.ID == b.ID) return 0;
                if (a.ID < 0) return 1; // Move missing status effect ids to the end of the list
                if (b.ID < 0) return -1;
                return b.ID - a.ID; // Otherwise lower ids come first
            });

            var newList = existingCollection != null ? new List<T>(existingCollection) : new List<T>();
            newList.RemoveAll(o => o == null);

            for (int i = allObjects.Count - 1; i >= 0; --i)
            {
                for (int j = 0; j < newList.Count; ++j)
                {
                    if (newList[j] == allObjects[i])
                    {
                        allObjects[i].ID = j;
                        EditorUtility.SetDirty(allObjects[i]);
                        allObjects.RemoveAt(i);
                        break;
                    }
                }
            }

            int startingId = newList.Count > 0 ? newList[newList.Count - 1].ID + 1 : 0;

            for (int i = 0; i < allObjects.Count; ++i)
            {
                allObjects[i].ID = newList.Count;
                newList.Add(allObjects[i]);
                EditorUtility.SetDirty(allObjects[i]);
            }

            return newList.ToArray();
        }

        /// <summary>
        /// Helper method to create a Scriptable Object of type T at the given 'path'
        /// </summary>
        public static void CreateAsset<T>(string path) where T : ScriptableObject
        {
            SaveAsset(ScriptableObject.CreateInstance<T>(), path);
        }

        /// <summary>
        /// Helper method to savea a created Scriptable Object to disk within the project, focusing selection on it as well
        /// </summary>
        public static void SaveAsset(this ScriptableObject asset, string path)
        {
            AssetDatabase.CreateAsset(asset, path);
            AssetDatabase.SaveAssets();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = asset;
        }
#endif
        /// <summary>
        /// Helper method to create a data dictionary for Static Objects, mapping their ID to the object
        /// </summary>
        public static Dictionary<int, T> CreateDataDict<T>(T[] collection) where T : StaticDataObject
        {
            var dict = new Dictionary<int, T>();
            foreach (T data in collection)
                dict.Add(data.ID, data);
            return dict;
        }
    }
}
