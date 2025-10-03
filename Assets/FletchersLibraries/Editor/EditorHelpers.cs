using UnityEngine;
using UnityEditor;
using System.Reflection;

namespace FletcherLibraries
{
    /// <summary>
    ///     Helper methods useful when creating custom Editor windows
    ///     </summary>
    public static class EditorHelpers
    {
        /// <summary>
        ///     Returns script attribute (if any) applied to the given property, such as NaughtyAttributes attributes
        ///     </summary>
        /// <remarks>
        ///     https://forum.unity.com/threads/serialiedproperty-check-if-it-has-a-propertyattribute.436103/
        ///     </remarks>
        public static T GetAttribute<T>(SerializedProperty prop, bool inherit) where T : System.Attribute
        {
            if (prop == null)
                return null;

            System.Type t = prop.serializedObject.targetObject.GetType();
            FieldInfo f = null;
            PropertyInfo p = null;
            BindingFlags bindings = inherit ? (BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.FlattenHierarchy) : (BindingFlags)(-1);

            try
            {
                foreach (var name in prop.propertyPath.Split('.'))
                {
                    f = t.GetField(name, bindings);

                    if (f == null)
                    {
                        p = t.GetProperty(name, bindings);
                        if (p == null)
                            return null;
                        t = p.PropertyType;
                    }
                    else
                    {
                        t = f.FieldType;
                    }
                }
            }
            catch (System.Exception tie)
            {
                Debug.LogError(tie.Message);
            }

            T[] attributes;
            if (f != null)
                attributes = f.GetCustomAttributes(typeof(T), inherit) as T[];
            else if (p != null)
                attributes = p.GetCustomAttributes(typeof(T), inherit) as T[];
            else
                return null;

            // Debug.LogFormat("Found property {0}: attributes length = {1}", prop.displayName, attributes.Length);
            return attributes.Length > 0 ? attributes[0] : null;
        }

        /// <summary>
        ///     Returns the System.Type of the given property
        ///     </summary>
        /// <remarks>
        ///     https://answers.unity.com/questions/1347203/a-smarter-way-to-get-the-type-of-serializedpropert.html
        ///     </remarks>
        public static System.Type GetPropertyType(SerializedProperty property)
        {
            System.Type parentType = property.serializedObject.targetObject.GetType();
            FieldInfo fi = GetFieldViaPath(parentType, property.propertyPath);
            if (fi == null)
            {
                Debug.LogError("No field info found for property " + property.propertyPath);
                return null;
            }
            return fi.FieldType;
        }

        /// <summary>
        ///     Returns FieldInfo for a field of the given 'path' within an object of System.Type 'type'
        ///     </summary>
        public static FieldInfo GetFieldViaPath(System.Type type, string path)
        {
            System.Type parentType = type;
            FieldInfo ogFi = type.GetField(path);
            // if (ogFi == null)
            // {
            //     Debug.LogError("No field info found for property " + path);
            //     return null;
            // }

            FieldInfo fi = ogFi;
            string[] perDot = path.Split('.');
            foreach (string fieldName in perDot)
            {
                fi = parentType.GetField(fieldName);
                if (fi != null)
                    parentType = fi.FieldType;
                else
                    return ogFi;
            }

            return fi != null ? fi : ogFi;
        }

        // [MenuItem("Tools/Save Editor Skins")]
        // public static void SaveEditorSkinsToAssets()
        // {
        //     var skin = Object.Instantiate(EditorGUIUtility.GetBuiltinSkin(EditorSkin.Inspector));
        //     AssetDatabase.CreateAsset(skin, "Assets/Resources/EditorInspectorSkin.guiskin");
        //     skin = Object.Instantiate(EditorGUIUtility.GetBuiltinSkin(EditorSkin.Game));
        //     AssetDatabase.CreateAsset(skin, "Assets/Resources/EditorGameSkin.guiskin");
        //     skin = Object.Instantiate(EditorGUIUtility.GetBuiltinSkin(EditorSkin.Scene));
        //     AssetDatabase.CreateAsset(skin, "Assets/Resources/EditorSceneSkin.guiskin");
        //     AssetDatabase.SaveAssets();
        // }
    }
}
