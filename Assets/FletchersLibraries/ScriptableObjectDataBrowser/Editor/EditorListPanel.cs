using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using FletcherLibraries;
using System.Linq;

namespace FletcherLibraries.DataBrowser.Editor
{
    /// <summary>
    ///     Displays a vertical list of options in an Editor view.
    ///     </summary>
    public class EditorListPanel
    {
        public int SelectedIndex { get; private set; }
        public bool AllowMultiSelect = true;

        private Vector2 _scrollPos;
        private string[] _options;
        private Texture[] _icons;
        private List<int> _selectedIndices = new List<int>();

        private const int LIST_SELECTION_OFF = 609;
        private const int LIST_SELECTION_ON = 610;
        private const int HEIGHT = 18;

        public void PopulatePanel(string[] options)
        {
            this.SelectedIndex = -1;
            _options = options;
            _icons = null;
        }

        public void PopulatePanel(string[] options, Texture[] icons)
        {
            this.SelectedIndex = -1;
            _options = options;
            _icons = icons;
        }

        public bool CheckMatchingSelectedIndices(int[] indices)
        {
            if (indices.Length != _selectedIndices.Count)
                return false;
            
            for (int i = 0; i < indices.Length; ++i)
            {
                if (indices[i] != _selectedIndices[i])
                    return false;
            }
            return true;
        }

        public int[] GetSelectedIndices() => _selectedIndices.ToArray();

        public string GetOption(int index) => index >= 0 && index < _options.Length ? _options[index] : null;
        public Texture GetIcon(int index) => _icons != null && index >= 0 && index < _icons.Length ? _icons[index] : null;

        public virtual void Draw(float optionWidth, float columnWidth)
        {
            using (var scrollView = new EditorGUILayout.ScrollViewScope(_scrollPos, GUIStyle.none, GUI.skin.verticalScrollbar, GUILayout.Width(columnWidth)))
            {
                _scrollPos = scrollView.scrollPosition;
                
                bool toggle = this.SelectedIndex == -1;
                toggle = GUILayout.Toggle(toggle, new GUIContent("[NONE]", this.GetIcon(0)), GUI.skin.customStyles[toggle ? LIST_SELECTION_ON : LIST_SELECTION_OFF], GUILayout.Height(HEIGHT), GUILayout.Width(optionWidth));
                if (toggle)
                    this.SoloSelectIndex(-1);
                
                if (_options != null)
                {
                    for (int i = 0; i < _options.Length; ++i)
                    {
                        toggle = _selectedIndices.Contains(i);
                        toggle = GUILayout.Toggle(toggle, new GUIContent(_options[i], this.GetIcon(i)), GUI.skin.customStyles[toggle ? LIST_SELECTION_ON : LIST_SELECTION_OFF], GUILayout.Height(HEIGHT), GUILayout.Width(optionWidth));
                        if (toggle && !_selectedIndices.Contains(i))
                        {
                            if (this.AllowMultiSelect && Event.current.modifiers.HasFlag(EventModifiers.Control))
                                this.AddSelectionIndex(i);
                            else if (this.AllowMultiSelect && Event.current.modifiers.HasFlag(EventModifiers.Shift))
                                this.SelectUpTo(i);
                            else
                                this.SoloSelectIndex(i);
                        }
                        else if (!toggle && this.AllowMultiSelect && _selectedIndices.Contains(i) && Event.current.modifiers.HasFlag(EventModifiers.Control))
                        {
                            _selectedIndices.Remove(i);
                            if (this.SelectedIndex == i)
                                this.SelectedIndex = _selectedIndices.Count > 0 ? _selectedIndices.First() : -1;
                        }
                    }
                }

                drawAdditionalContent(optionWidth, columnWidth);
            }
        }

        public void ProcessEvent(Event e)
        {
            if (e.type != EventType.KeyDown)
                return;
            
            if (e.keyCode == KeyCode.DownArrow || e.keyCode == KeyCode.UpArrow)
            {
                bool down = e.keyCode == KeyCode.DownArrow;
                int increment = down ? 1 : -1;

                if (this.AllowMultiSelect && (e.modifiers.HasFlag(EventModifiers.Control) || e.modifiers.HasFlag(EventModifiers.Shift)))
                {
                    int v = down ? 0 : 99999;
                    if (down)
                    {
                        for (int i = 0; i < _selectedIndices.Count; ++i)
                            if (_selectedIndices[i] > v)
                                v = _selectedIndices[i];
                    }
                    else
                    {
                        for (int i = 0; i < _selectedIndices.Count; ++i)
                            if (_selectedIndices[i] < v)
                                v = _selectedIndices[i];
                    }

                    v += increment;
                    if (v < _options.Length && v >= 0)
                    {
                        if (e.modifiers.HasFlag(EventModifiers.Control))
                            this.AddSelectionIndex(v);
                        else
                            this.SelectUpTo(v);
                    }
                }
                else if (this.SelectedIndex + increment < _options.Length && this.SelectedIndex + increment >= -1)
                {
                    this.SoloSelectIndex(this.SelectedIndex + increment);
                }
                e.Use();
            }
        }
        
        protected void SoloSelectIndex(int index)
        {
            this.SelectedIndex = index;
            _selectedIndices.Clear();
            if (index >= 0)
                _selectedIndices.Add(index);
        }

        protected void AddSelectionIndex(int index)
        {
            if (index >= 0)
                _selectedIndices.Add(index);
            if (this.SelectedIndex == -1)
                this.SelectedIndex = index;
        }

        protected void SelectUpTo(int index)
        {
            int min = 99999;
            int max = 0;

            for (int i = 0; i < _selectedIndices.Count; ++i)
            {
                if (_selectedIndices[i] < min)
                    min = _selectedIndices[i];
                if (_selectedIndices[i] > max)
                    max = _selectedIndices[i];
            }

            if (index > min)
            {
                for (int i = min + 1; i <= index; ++i)
                    if (!_selectedIndices.Contains(i))
                        _selectedIndices.Add(i);
            }
            else if (index < max)
            {
                for (int i = max - 1; i >= index; --i)
                    if (!_selectedIndices.Contains(i))
                        _selectedIndices.Add(i);
            }
            
            if (this.SelectedIndex == -1)
                this.SelectedIndex = index;
        }

        protected virtual void drawAdditionalContent(float optionWidth, float columnWidth) { }
        
        public void HighlightEntry(string objectName)
        {
            for (int i = 0; i < _options.Length; ++i)
            {
                if (_options[i] == objectName)
                {
                    this.SoloSelectIndex(i);
                    break;
                }
            }
        }
    }
}
