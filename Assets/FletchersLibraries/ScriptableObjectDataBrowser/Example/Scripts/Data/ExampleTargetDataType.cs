using UnityEngine;

namespace FletcherLibraries.DataBrowser.Example
{
    /// <summary>
    /// Basic example of a Scriptable Object data type that contains information about type of target in the game world
    /// </summary>
    [CreateAssetMenu(fileName = "ExampleTargetDataType", menuName = "Example Static Data/ExampleTargetDataType")]
    public class ExampleTargetDataType : StaticDataObject
    {
        [SerializeField] private string DisplayName;
        [SerializeField] private int TargetHp;
        [SerializeField] private ExampleColorDataType MainColorData;
        [SerializeField] private ExampleColorDataType SecondaryColorData;

        public ExampleColorDataType GetMainColorData() => this.MainColorData;
        public ExampleColorDataType GetSecondaryColorData() => this.SecondaryColorData;
    }
}
