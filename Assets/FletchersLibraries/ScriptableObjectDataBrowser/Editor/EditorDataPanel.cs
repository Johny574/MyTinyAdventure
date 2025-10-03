using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using FletcherLibraries;
using System.Linq;
using System.Reflection;
using NaughtyAttributes;
using NaughtyAttributes.Editor;

namespace FletcherLibraries.DataBrowser.Editor
{
    /// <summary>
    ///     Displays the properties of a scriptable object in a custom Editor view. Uses a custom display for properties which reference other scriptable objects, and calls a callback when such a property is highlighted by the User.
    ///     </summary>
    public class EditorDataPanel
    {
        public delegate bool ValidateTypeDelegate(string typeName);
        public delegate void ChangedNameDelegate(string newName);

        public int SelectedDataFieldIndex => _selectedDataFieldIndex;
        public string SelectedPropertyType => _selectedPropertyType;
        public string SelectedPropertyValueName => _selectedPropertyValueName;

        private ScriptableObject[] _data;
        private SerializedObject _serializedData;
        private Vector2 _scrollPos;
        private int _selectedDataFieldIndex = -1;
        private string _selectedPropertyType;
        private string _selectedPropertyValueName;
        private ValidateTypeDelegate _validateTypeCallback;
        private ChangedNameDelegate _newNameCallback;
        private bool _includeNameAndProjectButton;
        private bool _ignoreFurtherChanges;
        private string _cachedNameField;

        private const int LIST_SELECTION_OFF = 609;
        private const int LIST_SELECTION_ON = 610;
        private const float PROPERTY_NAME_WIDTH = 170;
        private const float ARRAY_BUTTON_SIZE = 32;
        private const float ARRAY_INDENT = 120;
        private const int HEIGHT = 18;

        public EditorDataPanel(ScriptableObject[] data, ValidateTypeDelegate validateTypeCallback, ChangedNameDelegate changedNameCallback, bool includeNameAndProjectButton = true)
        {
            // _waitOnNameChangeTimer = 2;
            _data = data;
            _cachedNameField = data.Length > 0 ? data[0].name : string.Empty;
            _serializedData = new SerializedObject(data); //data.Select(d => new SerializedObject(d)).ToArray();
            _validateTypeCallback = validateTypeCallback;
            _newNameCallback = changedNameCallback;
            _includeNameAndProjectButton = includeNameAndProjectButton;
        }

        public void IgnoreFurtherChanges()
        {
            _ignoreFurtherChanges = true;
        }

        public void ApplySelectedObjectChange(ScriptableObject selectedObject)
        {
            if (_selectedDataFieldIndex >= 0 && _serializedData != null)
            {
                SerializedProperty property = _serializedData.GetIterator();
                if (property != null && property.Next(true))
                {
                    int objectPropsIndex = 0;
                    do
                    {
                        if (!property.name.HasPrefix("m_"))
                        {
                            bool canShow = true;
                            ShowIfAttributeBase showAttribute = EditorHelpers.GetAttribute<ShowIfAttribute>(property, true);
                            if (showAttribute != null)
                                canShow = checkShowIfCondition(showAttribute.Inverted, showAttribute.Conditions[0]);

                            if (canShow && isDataProperty(property))
                            {
                                if (property.isArray)
                                {
                                    for (int i = 0; i < property.arraySize; ++i)
                                    {
                                        if (objectPropsIndex == _selectedDataFieldIndex)
                                        {
                                            property.GetArrayElementAtIndex(i).objectReferenceValue = selectedObject;
                                            return;
                                        }
                                        ++objectPropsIndex;
                                    }
                                }
                                else
                                {
                                    if (objectPropsIndex == _selectedDataFieldIndex)
                                    {
                                        property.objectReferenceValue = selectedObject;
                                        return;
                                    }
                                    ++objectPropsIndex;
                                }
                            }
                        }
                    }
                    while (property.NextVisible(false));
                }

                _serializedData.ApplyModifiedProperties();
            }
        }

        // Returns whether enter press was claimed
        public bool Draw(bool unlcaimedEnterPress, float optionWidth, float columnWidth)
        {
            if (_serializedData == null)
                return false;

            ScriptableObject data = _data.First();
            bool claimedEnterPress = false;

            try
            {
                using (var scrollView = new EditorGUILayout.ScrollViewScope(_scrollPos, GUIStyle.none, GUI.skin.verticalScrollbar, GUILayout.Width(columnWidth)))
                {
                    _scrollPos = scrollView.scrollPosition;

                    if (_serializedData != null)
                    {
                        // string newName = data.name;
                        if (_includeNameAndProjectButton)
                        {
                            if (_data.Length == 1)
                            {
                                if (_ignoreFurtherChanges)
                                {
                                    // Disable the name display for a frame to prevent it from being overriden by Unity's cached field data
                                    // EditorGUI.BeginDisabledGroup(true);
                                    // EditorGUILayout.LabelField("Name", data.name);
                                    // EditorGUILayout.DelayedTextField("Name", data.name);
                                    EditorGUILayout.TextField("Name", data.name);
                                    // EditorGUI.EndDisabledGroup();
                                }
                                else
                                {
                                    // newName = EditorGUILayout.DelayedTextField("Name", data.name);
                                    _cachedNameField = EditorGUILayout.TextField("Name", _cachedNameField);
                                }
                            }
                            else
                            {
                                EditorGUI.BeginDisabledGroup(true);
                                // EditorGUILayout.DelayedTextField("Name", "—");
                                EditorGUILayout.TextField("Name", "—");
                                EditorGUI.EndDisabledGroup();
                            }
                        }

                        SerializedProperty property = _serializedData.GetIterator();
                        if (property != null && property.Next(true))
                        {
                            int objectPropsIndex = 0;

                            do
                            {
                                if (!property.name.HasPrefix("m_"))
                                {
                                    bool canShow = true;

                                    // Only display properties not hidden by HideIf and ShowIf property attributes
                                    ShowIfAttributeBase showAttribute = EditorHelpers.GetAttribute<ShowIfAttributeBase>(property, true);
                                    if (showAttribute != null)
                                        canShow = checkShowIfCondition(showAttribute.Inverted, showAttribute.Conditions[0]);

                                    if (canShow)
                                    {
                                        // Show standard property field UNLESS this field is a reference to another scriptable object, in which case use a custom field
                                        if (!isDataProperty(property))
                                        {
                                            using (var horizontalView = new GUILayout.HorizontalScope(GUILayout.Width(optionWidth)))
                                            {
                                                // Don't manually draw title for dropdown properties because dropdown drawer draws the title.
                                                DropdownAttribute dropdownAttribute = EditorHelpers.GetAttribute<DropdownAttribute>(property, true);
                                                float optWidth = optionWidth;
                                                if (dropdownAttribute == null)
                                                {
                                                    GUILayout.Label(property.displayName, EditorStyles.label, GUILayout.Width(PROPERTY_NAME_WIDTH));
                                                    GUILayout.Space(0);
                                                }
                                                else
                                                {
                                                    optWidth += PROPERTY_NAME_WIDTH;
                                                }

                                                // Display ReadOnly properties as disabled
                                                ReadOnlyAttribute readOnlyAttribute = EditorHelpers.GetAttribute<ReadOnlyAttribute>(property, true);
                                                EditorGUI.showMixedValue = true;

                                                if (readOnlyAttribute != null)
                                                {
                                                    EditorGUI.BeginDisabledGroup(true);
                                                    EditorGUILayout.PropertyField(property, GUIContent.none, true, GUILayout.MaxWidth(optWidth - PROPERTY_NAME_WIDTH - 4));
                                                    EditorGUI.EndDisabledGroup();
                                                }
                                                else
                                                {
                                                    // using (var propScope = new EditorGUI.PropertyScope(new Rect(0, 0, optionWidth, 24), GUIContent.none, property))
                                                    // {
                                                    //     EditorGUI.BeginChangeCheck();
                                                    //     if (EditorGUI.EndChangeCheck())
                                                    //     {
                                                    //     }
                                                    // }
                                                    EditorGUILayout.PropertyField(property, GUIContent.none, true, GUILayout.MaxWidth(optWidth - PROPERTY_NAME_WIDTH - 4));
                                                }
                                                EditorGUI.showMixedValue = false;
                                            }
                                        }
                                        else
                                        {
                                            if (property.isArray)
                                            {
                                                GUILayout.Label(property.displayName + ":", GUILayout.Width(optionWidth));
                                                for (int i = 0; i < property.arraySize; ++i)
                                                {
                                                    SerializedProperty elementProperty = property.GetArrayElementAtIndex(i);
                                                    using (var horizontalView = new GUILayout.HorizontalScope())
                                                    {
                                                        GUILayout.Space(ARRAY_INDENT);
                                                        drawDataProperty(elementProperty, objectPropsIndex, optionWidth - ARRAY_INDENT - ARRAY_BUTTON_SIZE * 2 - 8, ARRAY_INDENT);
                                                        GUILayout.FlexibleSpace();
                                                        if (GUILayout.Button("▲", GUILayout.Width(ARRAY_BUTTON_SIZE)) && i > 0)
                                                        {
                                                            property.MoveArrayElement(i, i - 1);
                                                            _selectedDataFieldIndex = -1;
                                                        }
                                                        if (GUILayout.Button("▼", GUILayout.Width(ARRAY_BUTTON_SIZE)) && i < property.arraySize - 1)
                                                        {
                                                            property.MoveArrayElement(i, i + 1);
                                                            _selectedDataFieldIndex = -1;
                                                        }
                                                        GUILayout.Space(9);
                                                    }
                                                    ++objectPropsIndex;
                                                }

                                                using (var horizontalView = new GUILayout.HorizontalScope(GUILayout.Width(optionWidth + 2)))
                                                {
                                                    GUILayout.FlexibleSpace();
                                                    // GUILayout.Space(1);
                                                    if (GUILayout.Button("+", GUILayout.Width(ARRAY_BUTTON_SIZE)))
                                                    {
                                                        property.InsertArrayElementAtIndex(property.arraySize);
                                                        _selectedDataFieldIndex = -1;
                                                    }
                                                    if (GUILayout.Button("-", GUILayout.Width(ARRAY_BUTTON_SIZE)) && property.arraySize > 0)
                                                    {
                                                        property.DeleteArrayElementAtIndex(property.arraySize - 1);
                                                        _selectedDataFieldIndex = -1;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                drawDataProperty(property, objectPropsIndex, optionWidth, 0);
                                                ++objectPropsIndex;
                                            }
                                        }
                                    }
                                }
                            }
                            while (property.NextVisible(false));
                        }

                        _serializedData.ApplyModifiedProperties();
                    }

                    if (_includeNameAndProjectButton && GUILayout.Button("Select Object In Project", GUILayout.Width(optionWidth)))
                    {
                        Selection.activeObject = data;
                    }
                }
            }
            finally
            {
            }

            return claimedEnterPress;
        }

        public bool ProcessEnterPressed(bool unclaimedEnterPress)
        {
            if (unclaimedEnterPress && _data.Length == 1 && _cachedNameField != _data[0].name && !_ignoreFurtherChanges)
            {
                AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(_data[0]), _cachedNameField + ".asset");
                _data[0].name = _cachedNameField;
                EditorUtility.SetDirty(_data[0]);
                AssetDatabase.SaveAssets();
                _serializedData = new SerializedObject(_data[0]);
                _newNameCallback?.Invoke(_cachedNameField);
                return true;
            }

            return false;
        }

        private void drawDataProperty(SerializedProperty property, int objectPropsIndex, float optionWidth, float indent)
        {
            bool toggle = _selectedDataFieldIndex == objectPropsIndex;
            Object objRefVal = property.hasMultipleDifferentValues ? null : property.objectReferenceValue;
            string propertyValueName = objRefVal == null ? (property.hasMultipleDifferentValues ? "—" : "") : objRefVal.name;
            string labelText = propertyValueName;
            Texture icon = objRefVal != null ? AssetDatabase.GetCachedIcon(AssetDatabase.GetAssetPath(objRefVal)) : null;

            using (var h = new GUILayout.HorizontalScope())
            {
                GUILayout.Label(property.displayName, GUI.skin.label, GUILayout.Height(HEIGHT), GUILayout.Width(PROPERTY_NAME_WIDTH - indent));
                GUILayout.Space(0);
                toggle = GUILayout.Toggle(toggle, new GUIContent(labelText, icon), GUI.skin.customStyles[toggle ? LIST_SELECTION_ON : LIST_SELECTION_OFF], GUILayout.Height(HEIGHT), GUILayout.Width(optionWidth - (PROPERTY_NAME_WIDTH - indent) - 4));
            }
            if (toggle)
            {
                _selectedDataFieldIndex = objectPropsIndex;
                _selectedPropertyType = property.type;
                _selectedPropertyValueName = propertyValueName;
            }
        }

        private bool isDataProperty(SerializedProperty property)
        {
            if (property.isArray)
            {
                if (property.arraySize == 0)
                    return false;
                
                SerializedProperty arrayProperty = property.GetArrayElementAtIndex(0);
                // Debug.Log("array element type " + property.arrayElementType);
                return arrayProperty.propertyType == SerializedPropertyType.ObjectReference && _validateTypeCallback(property.arrayElementType);
            }
            return property.propertyType == SerializedPropertyType.ObjectReference && _validateTypeCallback(property.type);
        }

        public bool checkShowIfCondition(bool inverted, string condition)
        {
            if (_data.Length > 1) // If selecting multiple objects, just show all fields
                return true;
            
            ScriptableObject data = _data.First();
            FieldInfo conditionField = ReflectionUtility.GetField(data, condition);
            if (conditionField != null &&
                conditionField.FieldType == typeof(bool))
            {
                bool v = (bool)conditionField.GetValue(data);
                return inverted ? !v : v;
            }

            PropertyInfo conditionProperty = ReflectionUtility.GetProperty(data, condition);
            if (conditionProperty != null &&
                conditionProperty.PropertyType == typeof(bool))
            {
                bool v = (bool)conditionProperty.GetValue(data);
                return inverted ? !v : v;
            }

            MethodInfo conditionMethod = ReflectionUtility.GetMethod(data, condition);
            if (conditionMethod != null &&
                conditionMethod.ReturnType == typeof(bool) &&
                conditionMethod.GetParameters().Length == 0)
            {
                bool v = (bool)conditionMethod.Invoke(data, null);
                return inverted ? !v : v;
            }

            return inverted;
        }
    }
}
