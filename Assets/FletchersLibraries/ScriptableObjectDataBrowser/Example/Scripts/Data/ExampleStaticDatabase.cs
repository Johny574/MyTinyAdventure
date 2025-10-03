using UnityEngine;
using NaughtyAttributes;
using FletcherLibraries;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace FletcherLibraries.DataBrowser.Example
{
    /// <summary>
    /// Example of a Static Database that allows referencing Scriptable Object types by their ID both in Editor and at Runtime
    /// </summary>
    [CreateAssetMenu(fileName = "ExampleStaticDatabase", menuName = "Example Static Data/ExampleStaticDatabase")]
    public class ExampleStaticDatabase : ScriptableObject
    {
#if UNITY_EDITOR
        [Tooltip("Point this to the parent folder of where your Collection objects for your Scriptable Objects are located. Should end in a '/'.")]
        [SerializeField] private string PathToDataCollections = "Assets/FletchersLibraries/StaticDataBrowser/Example/DataObjects/Collections/";
#endif
        [ReadOnly] public ExampleTargetDataTypeCollection TargetTypes;
        [ReadOnly] public ExampleColorDataTypeCollection ColorTypes;

#if UNITY_EDITOR
        /// <summary>
        /// Adds a button to the Inspector for this object that automatically populates its field with the first available valid collections found in the project
        /// </summary>
        [Button("Compile All Data")]
        public void CompileAllData()
        {
            this.TargetTypes = AssetDatabaseHelpers.GetFirstAtPath<ExampleTargetDataTypeCollection>(this.PathToDataCollections);
            this.ColorTypes = AssetDatabaseHelpers.GetFirstAtPath<ExampleColorDataTypeCollection>(this.PathToDataCollections);

            StaticDataHelpers.RunForEachObjectOfType<IStaticDataCollection>(collection => collection.CompileData());
            EditorUtility.SetDirty(this);
        }

        /// <summary>
        /// Allows access to the Scriptable Object Database while in Editor code
        /// </summary>
        public static ExampleStaticDatabase EditorGetDatabase()
        {
            string[] guids = AssetDatabase.FindAssets(string.Format("t:{0}", typeof(ExampleStaticDatabase)));
            if (guids.Length == 0)
            {
                Debug.LogError("Couldn't find a database to load in editor");
                return null;
            }

            return AssetDatabase.LoadAssetAtPath<ExampleStaticDatabase>(AssetDatabase.GUIDToAssetPath(guids[0]));
        }
#endif
    }
}
