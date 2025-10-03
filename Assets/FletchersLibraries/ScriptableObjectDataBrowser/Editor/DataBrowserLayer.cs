using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace FletcherLibraries.DataBrowser.Editor
{
    /// <summary>
    ///     Represents one layer within the DataBrowserWindow, in which Scriptable Objects of a given type can be selected from a list, and their properties inspected.
    ///     </summary>
    /// <remarks>
    ///     Listens to callback from EditorDataPanel for when a property that links to another Scriptable Object has been highlighted, and notifies the DataBrowserWindow to add another layer for that object type if possible.
    ///     </remarks>
    public class DataBrowserLayer
    {
        public delegate void DrawDividerDelegate();
        public delegate void DataPropertySelectedDelegate(int layerId, string propertyType, string currentObjectName);
        public delegate void ObjectSelectedDelegate(int layerId, ScriptableObject objectSelected);

        private System.Type _dataType;
        private EditorObjectsListPanel _objectsListPanel;
        private EditorDataPanel _editorDataPanel;
        private DrawDividerDelegate _drawDividerCallback;
        private DataPropertySelectedDelegate _dataPropertySelectedCallback;
        private ObjectSelectedDelegate _objectSelectedCallback;
        private EditorDataPanel.ValidateTypeDelegate _validateTypeCallback;

        private int _layerId;
        private int[] _showingDataIndices;
        private int _showingDataFieldIndex = -1;
        private bool _enterWasPressed;
        private bool _unclaimedEnterPress;

        public DataBrowserLayer(int layerId, System.Type dataType, EditorDataPanel.ValidateTypeDelegate validateTypeCallback, DrawDividerDelegate drawDividerCallback, ObjectSelectedDelegate objectSelectedCallback, DataPropertySelectedDelegate dataPropertySelectedCallback)
        {
            _layerId = layerId;
            _dataType = dataType;
            _showingDataIndices = new int[0];
            _objectsListPanel = new EditorObjectsListPanel();
            _objectsListPanel.PopulatePanelWithType(dataType);
            _drawDividerCallback = drawDividerCallback;
            _objectSelectedCallback = objectSelectedCallback;
            _dataPropertySelectedCallback = dataPropertySelectedCallback;
            _validateTypeCallback = validateTypeCallback;
        }

        public void HighlightObject(string objectName)
        {
            _objectsListPanel.HighlightEntry(objectName);
        }

        public void ApplySelectedObjectChange(ScriptableObject selectedObject)
        {
            if (_editorDataPanel != null)
                _editorDataPanel.ApplySelectedObjectChange(selectedObject);
        }

        public void Draw(float objectsOptionWidth, float objectsColumnWidth, float dataOptionWidth, float dataColumnWidth)
        {
            _objectsListPanel.Draw(objectsOptionWidth, objectsColumnWidth);
            _drawDividerCallback?.Invoke();

            if (!_objectsListPanel.CheckMatchingSelectedIndices(_showingDataIndices))
            {
                if (_editorDataPanel != null)
                {
                    _editorDataPanel.IgnoreFurtherChanges();
                    showNewDataPanel();
                }

                GUIUtility.keyboardControl = 0; // Remove focus from any fields that were selected

                if (_editorDataPanel == null)
                    showNewDataPanel();
            }

            if (_editorDataPanel != null)
            {
                bool didClaimEnterPress = _editorDataPanel.Draw(_unclaimedEnterPress, dataOptionWidth, dataColumnWidth);
                _unclaimedEnterPress = _unclaimedEnterPress && !didClaimEnterPress;
                _drawDividerCallback?.Invoke();

                if (_editorDataPanel.SelectedDataFieldIndex != _showingDataFieldIndex)
                {
                    _showingDataFieldIndex = _editorDataPanel.SelectedDataFieldIndex;
                    _dataPropertySelectedCallback?.Invoke(_layerId, _showingDataFieldIndex >= 0 ? _editorDataPanel.SelectedPropertyType : null, _editorDataPanel.SelectedPropertyValueName);
                }
            }
        }

        private void showNewDataPanel()
        {
            _showingDataIndices = _objectsListPanel.GetSelectedIndices();
            ScriptableObject[] selectedObjects = _objectsListPanel.GetObjectsAtIndices(_showingDataIndices);
            _editorDataPanel = selectedObjects.Length > 0 ? new EditorDataPanel(selectedObjects, _validateTypeCallback, onChangedName) : null;
            _objectSelectedCallback?.Invoke(_layerId, _objectsListPanel.GetObjectAtIndex(_objectsListPanel.SelectedIndex));
        }

        public void ProcessEvent(Event e)
        {
            _objectsListPanel.ProcessEvent(e);

            bool enterPressed = e.keyCode == KeyCode.Return || e.keyCode == KeyCode.KeypadEnter;

            if (_enterWasPressed != enterPressed)
            {
                _enterWasPressed = enterPressed;
                _unclaimedEnterPress = enterPressed;
            }

            if (_editorDataPanel != null)
            {
                bool claimedEnterPressed = _editorDataPanel.ProcessEnterPressed(_unclaimedEnterPress);
                _unclaimedEnterPress = _unclaimedEnterPress && !claimedEnterPressed;
            }
        }

        public void Clear()
        {
            _objectsListPanel = null;
            _editorDataPanel = null;
            _drawDividerCallback = null;
            _dataPropertySelectedCallback = null;
        }

        private void onChangedName(string newName)
        {
            _objectsListPanel.PopulatePanelWithType(_dataType);
            _objectsListPanel.HighlightEntry(newName);
        }
    }
}
