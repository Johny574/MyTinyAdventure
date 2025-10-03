using UnityEngine;

namespace FletcherLibraries.DataBrowser.Example
{
    /// <summary>
    /// Scriptable Object data collection that allows access to color data by ID
    /// </summary>
    [CreateAssetMenu(fileName = "ExampleColorDataTypeCollection", menuName = "Example Static Data/ExampleColorDataTypeCollection")]
    public class ExampleColorDataTypeCollection : StaticDataCollection<ExampleColorDataType>
    {
    }
}
