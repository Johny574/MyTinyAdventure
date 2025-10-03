using UnityEngine;

namespace FletcherLibraries.DataBrowser.Example
{
    /// <summary>
    /// Basic example of a Scriptable Object data type that contains information about a color display
    /// </summary>
    [CreateAssetMenu(fileName = "ExampleColorDataType", menuName = "Example Static Data/ExampleColorDataType")]
    public class ExampleColorDataType : StaticDataObject
    {
        [SerializeField] private Color DisplayColor;

        public Color GetDisplayColor() => this.DisplayColor;
    }
}
