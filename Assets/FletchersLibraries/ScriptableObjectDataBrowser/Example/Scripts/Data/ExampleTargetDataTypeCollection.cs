using UnityEngine;

namespace FletcherLibraries.DataBrowser.Example
{
    /// <summary>
    /// Scriptable Object data collection that allows access to target data by ID
    /// </summary>
    [CreateAssetMenu(fileName = "ExampleTargetDataTypeCollection", menuName = "Example Static Data/ExampleTargetDataTypeCollection")]
    public class ExampleTargetDataTypeCollection : StaticDataCollection<ExampleTargetDataType>
    {
    }
}
