using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using FletcherLibraries;
using System.Linq;

namespace FletcherLibraries.DataBrowser.Editor
{
    /// <summary>
    ///     Version of EditorListPanel which displays options for all existing Scriptable Objects in the project of a given type.
    ///     </summary>
    public class EditorObjectsListPanel : EditorListPanel
    {
        private ScriptableObject[] _dataObjects;

        public void PopulatePanelWithType(System.Type type)
        {
            IEnumerable<string> paths = AssetDatabase.FindAssets(string.Format("t:{0}", type)).Select(o => AssetDatabase.GUIDToAssetPath(o));

            _dataObjects = paths.Select(p =>
                AssetDatabase.LoadAssetAtPath(p, type) as ScriptableObject
            ).ToArray();

            this.PopulatePanel(_dataObjects.Select(o => o.name).ToArray(), paths.Select(p => AssetDatabase.GetCachedIcon(p)).ToArray());
        }

        public void PopulatePanelWithTypeAtWithinPath<T>(string path) where T : ScriptableObject
        {
            _dataObjects = AssetDatabaseHelpers.GetAtPath<T>(path);
            this.PopulatePanel(_dataObjects.Select(o => o.name).ToArray());
        }

        public ScriptableObject GetObjectAtIndex(int index) => index >= 0 && index < _dataObjects.Length ? _dataObjects[index] : null;
        public ScriptableObject[] GetObjectsAtIndices(int[] indices) => indices.Select(i => _dataObjects[i]).ToArray();
        
        protected override void drawAdditionalContent(float optionWidth, float columnWidth)
        {
            using (var horizontalView = new GUILayout.HorizontalScope(GUILayout.Width(optionWidth)))
            {
                if (_dataObjects.Length > 0)
                {
                    if (GUILayout.Button("+"))
                    {
                        ScriptableObject baseObject = this.SelectedIndex < 0 ? _dataObjects.Last() : _dataObjects[this.SelectedIndex];
                        string basePath = AssetDatabase.GetAssetPath(baseObject);
                        string fullBaseName = baseObject.name + ".asset";
                        string newPath = basePath.Remove(basePath.Length - fullBaseName.Length, fullBaseName.Length);
                        string newName = baseObject.name + "_DUP";

                        ScriptableObject newObject = Object.Instantiate(baseObject);
                        newObject.name = newName;
                        StaticDataHelpers.SaveAsset(newObject, newPath + newName + ".asset");

                        this.PopulatePanelWithType(baseObject.GetType());
                        for (int i = 0; i < _dataObjects.Length; ++i)
                        {
                            if (_dataObjects[i].name == newName)
                            {
                                this.SoloSelectIndex(i);
                                break;
                            }
                        }

                        // Recompile the database so an id gets assigned to the new object
                        StaticDataHelpers.RunForEachObjectOfType<IStaticDataCollection>(collection =>
                        {
                            if (collection.GetCollectedType() == baseObject.GetType())
                                collection.CompileData();
                        });
                    }

                    if (GUILayout.Button("-"))
                    {
                        ScriptableObject baseObject = this.SelectedIndex < 0 ? _dataObjects.Last() : _dataObjects[this.SelectedIndex];
                        this.SoloSelectIndex(-1);
                        AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(baseObject));
                        AssetDatabase.SaveAssets();

                        this.PopulatePanelWithType(baseObject.GetType());

                        // Recompile the database so an id gets assigned to the new object
                        StaticDataHelpers.RunForEachObjectOfType<IStaticDataCollection>(collection =>
                        {
                            if (collection.GetCollectedType() == baseObject.GetType())
                                collection.CompileData();
                        });
                    }
                }
            }
        }
    }
}
