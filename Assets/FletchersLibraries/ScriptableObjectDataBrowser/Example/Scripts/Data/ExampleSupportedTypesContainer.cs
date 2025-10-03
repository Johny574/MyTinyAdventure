using UnityEngine;
using System.Collections.Generic;
using System.Text.RegularExpressions;
#if UNITY_EDITOR
using UnityEditor;
using Unity.Properties;
#endif

namespace FletcherLibraries.DataBrowser.Example
{
    /// <summary>
    /// Example of how to implement 'DataBrowserSupportedTypesContainer' to specify what types of Scriptable Objects you want displayed in your Data Browser window
    /// </summary>
    [CreateAssetMenu(fileName = "ExampleSupportedTypesContainer", menuName = "Example Static Data/ExampleSupportedTypesContainer")]
    public class ExampleSupportedTypesContainer : DataBrowserSupportedTypesContainer
    {
        /// <summary>
        /// Adds the Scriptable Object types we want displayed in the Data Browser
        /// </summary>
        public override void GetSupportedDataBrowserTypes(Dictionary<string, System.Type> supportedTypes)
        {
            // You can add types you want tracked manually here:
            supportedTypes.Add("Example Targets", typeof(ExampleTargetDataType));
            supportedTypes.Add("Example Colors", typeof(ExampleColorDataType));

            // OR do something fancy like scan a directory for types to populate the browser with, like this:
// #if UNITY_EDITOR
//             // Search our folder's path (put this object at a folder level at or above all Scriptable Objects you want to show in the Browser for this to work)
//             string highestLevel = "Assets";
//             string path = AssetDatabase.GetAssetPath(this);
//             if (path[path.Length - 1] == '/')
//                 path = path.Remove(path.Length - 1);
//             int lastSplitIndex = path.LastIndexOf('/');
//             if (lastSplitIndex > highestLevel.Length)
//                 path = path.Remove(lastSplitIndex + 1);
//             path += '/';

//             List<ScriptableObject> allObjects = new List<ScriptableObject>();
//             AssetDatabaseHelpers.GetAtPath(path, allObjects, false);

//             string key;
//             foreach (ScriptableObject obj in allObjects)
//             {
//                 if (obj is IStaticDataCollection || obj is DataBrowserSupportedTypesContainer)
//                     continue;
                
//                 key = TypeUtility.GetTypeDisplayName(obj.GetType());
                
//                 // Nicer display of type names
//                 // if (key.HasSuffix("Type"))
//                 //     key = key.Remove(key.Length - "Type".Length);
//                 // if (key.HasSuffix("Data"))
//                 //     key = key.Remove(key.Length - "Data".Length);
//                 // key = Regex.Replace(key, "(?<!^)([A-Z][a-z]|(?<=[a-z])[A-Z])", " $1");
//                 // if (key[key.Length - 1] != 's')
//                 //     key += 's';
//                 // else
//                 //     key += "es";

//                 if (!supportedTypes.ContainsKey(key))
//                     supportedTypes.Add(key, obj.GetType());
//             }
// #endif
        }
    }
}
