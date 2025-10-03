using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

namespace FletcherLibraries.DataBrowser.Editor
{
    /// <summary>
    ///     Editor tool for more easily navigating Scriptable Objects used for data in the project. Allows for multiple layers to be open horizontally for editing at once so don't need to keep going back and forth up and down in the project view.
    ///     </summary>
    public class DataBrowserWindow : EditorWindow
    {
        [MenuItem("Tools/Data Browser &d")]
        private static void OpenWindow() => GetWindow<DataBrowserWindow>().titleContent = new GUIContent("Data Browser");

        private Dictionary<string, System.Type> _validBrowserTypes;

        public const float OPTION_BUFFER = 14;
        public const float CATEGORIES_COLUMN_WIDTH = 214;
        public const float OBJECTS_COLUMN_WIDTH = 260;
        public const float DATA_COLUMN_WIDTH = 460;
        public const int MAX_LAYERS = 4;

        private GUIStyle _verticalDividerStyle;
        private GUISkin _skin;
        private EditorListPanel _categoriesPanel;
        private int _selectedCategoryIndex;
        private List<DataBrowserLayer> _layers;
        private Vector2 _scrollPos;

        private Dictionary<string, System.Type> getSupportedBrowserTypes()
        {
            if (_validBrowserTypes != null)
                return _validBrowserTypes;

            _validBrowserTypes = new Dictionary<string, System.Type>();
            StaticDataHelpers.RunForEachObjectOfType<DataBrowserSupportedTypesContainer>(container =>
            {
                if (container.IsActive())
                    container.GetSupportedDataBrowserTypes(_validBrowserTypes);
            });

            return _validBrowserTypes;
        }

        void OnEnable()
        {
            _selectedCategoryIndex = -1;
            _verticalDividerStyle = new GUIStyle();
            _verticalDividerStyle.normal.background = EditorGUIUtility.whiteTexture;
            _verticalDividerStyle.margin = new RectOffset(4, 4, 0, 0);
            _verticalDividerStyle.fixedWidth = 1;
            _verticalDividerStyle.stretchHeight = true;
            _skin = Resources.Load<GUISkin>("DataBrowserSkin");
            _categoriesPanel = new EditorListPanel();
            var supportedTypes = getSupportedBrowserTypes();
            _categoriesPanel.PopulatePanel(supportedTypes.Keys.ToArray(), supportedTypes.Values.Select(v => {
                string guid = AssetDatabase.FindAssets(string.Format("t:{0}", v)).First();
                return !StringExtensions.IsEmpty(guid) ? AssetDatabase.GetCachedIcon(AssetDatabase.GUIDToAssetPath(guid)) : null;
            }).ToArray());
            _categoriesPanel.AllowMultiSelect = false;
            _layers = new List<DataBrowserLayer>(MAX_LAYERS);
        }

        void OnGUI()
        {
            GUISkin oldSkin = GUI.skin;
            GUI.skin = _skin;

            using (var scrollView = new EditorGUILayout.ScrollViewScope(_scrollPos, GUI.skin.horizontalScrollbar, GUIStyle.none))
            {
                _scrollPos = scrollView.scrollPosition;

                using (var horizontalView = new EditorGUILayout.HorizontalScope())
                {
                    _categoriesPanel.Draw(CATEGORIES_COLUMN_WIDTH - OPTION_BUFFER, CATEGORIES_COLUMN_WIDTH);
                    if (_selectedCategoryIndex != _categoriesPanel.SelectedIndex)
                    {
                        _selectedCategoryIndex = _categoriesPanel.SelectedIndex;
                        removeTrailingLayers(-1);
                        if (_selectedCategoryIndex >= 0)
                            _layers.Add(new DataBrowserLayer(0, getSupportedBrowserTypes()[_categoriesPanel.GetOption(_selectedCategoryIndex)], validateType, verticalLine, onSelectedObject, onSelectedDataProperty));
                    }

                    verticalLine();
                    for (int i = 0; i < _layers.Count; ++i)
                    {
                        if (_layers[i] != null)
                            _layers[i].Draw(OBJECTS_COLUMN_WIDTH - OPTION_BUFFER, OBJECTS_COLUMN_WIDTH, DATA_COLUMN_WIDTH - OPTION_BUFFER, DATA_COLUMN_WIDTH);
                    }
                }
            }
            
            processLayerEvents(Event.current);
            // if (GUI.changed) Repaint();
            GUI.skin = oldSkin;
        }

        private bool validateType(string typeName) => findType(typeName) != null;

        private System.Type findType(string typeName)
        {
            foreach (System.Type value in getSupportedBrowserTypes().Values)
            {
                if (typeName.Contains(value.Name))
                    return value;
            }
            return null;
        }

        private void onSelectedObject(int layerId, ScriptableObject selectedObject)
        {
            // Remove layers that come after the layer where a selection was made
            removeTrailingLayers(layerId);

            // If we're not at the top level layer, changing the selected object should change the data field of the property selected in the parent layer
            if (layerId > 0)
            {
                DataBrowserLayer parentLayer = _layers[layerId - 1];
                parentLayer.ApplySelectedObjectChange(selectedObject);
            }
        }

        private void onSelectedDataProperty(int layerId, string propertyType, string propertyValueName)
        {
            // Remove layers that come after the layer where a selection was made
            removeTrailingLayers(layerId);

            // Add a new layer as long as we're below the max
            if (_layers.Count < MAX_LAYERS && !StringExtensions.IsEmpty(propertyType))
            {
                System.Type propType = findType(propertyType);
                DataBrowserLayer layer = new DataBrowserLayer(_layers.Count, propType, validateType, verticalLine, onSelectedObject, onSelectedDataProperty);
                
                GUIUtility.keyboardControl = 0; // Remove focus from any fields that were selected
                layer.HighlightObject(propertyValueName);
                _layers.Add(layer);
            }
        }

        private void removeTrailingLayers(int layerId)
        {
            for (int i = _layers.Count - 1; i > layerId; --i)
            {
                if (_layers[i] != null)
                {
                    _layers[i].Clear();
                    _layers[i] = null;
                }
                _layers.RemoveAt(i);
            }
        }
        
        private void verticalLine()
        {
            var c = GUI.color;
            GUI.color = Color.black;
            GUILayout.Box(GUIContent.none, _verticalDividerStyle);
            GUI.color = c;
        }

        private void processLayerEvents(Event e)
        {
            // Process events in reverse layer order in order to priortize latest layer
            for (int i = _layers.Count - 1; i >= 0; --i)
                _layers[i].ProcessEvent(e);
            _categoriesPanel.ProcessEvent(e);
        }
    }
}
