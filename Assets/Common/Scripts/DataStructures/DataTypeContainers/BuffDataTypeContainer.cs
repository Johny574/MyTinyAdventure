using System;
using System.Collections.Generic;
using FletcherLibraries.DataBrowser;
using UnityEngine;

[CreateAssetMenu(fileName = "DataTypeContainer", menuName = "DataTypeContainers/Buffs", order = 1)]
public class BuffDataTypeContainer : DataBrowserSupportedTypesContainer
{
    public override void GetSupportedDataBrowserTypes(Dictionary<string, Type> supportedTypes) {
        supportedTypes.Add("Buffs", typeof(BuffSO));
    }
}