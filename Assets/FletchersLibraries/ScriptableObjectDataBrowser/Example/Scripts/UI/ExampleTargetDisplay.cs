using UnityEngine;
using UnityEngine.UI;

namespace FletcherLibraries.DataBrowser.Example
{
    /// <summary>
    /// Basic example demonstrating usage of Scriptable Object data to configure a target in the game scene
    /// </summary>
    public class ExampleTargetDisplay : MonoBehaviour
    {
        [SerializeField] private Image[] MainTargetRings;
        [SerializeField] private Image[] SecondaryTargetRings;
        [SerializeField] private ExampleTargetDataType InitialTargetData;

        void OnValidate() => this.ConfigureForInitialTargetData();
        public void ConfigureForInitialTargetData() => this.ConfigureForTargetData(this.InitialTargetData);

        public void ConfigureForTargetData(ExampleTargetDataType targetData)
        {
            if (targetData == null)
                return;

            foreach (Image image in this.MainTargetRings)
            {
                image.color = targetData.GetMainColorData().GetDisplayColor();
            }

            foreach (Image image in this.SecondaryTargetRings)
            {
                image.color = targetData.GetSecondaryColorData().GetDisplayColor();
            }
        }
    }
}
