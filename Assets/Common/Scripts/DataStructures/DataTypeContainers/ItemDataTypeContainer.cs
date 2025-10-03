
using System;
using System.Collections.Generic;
using FletcherLibraries.DataBrowser;
using UnityEngine;

[CreateAssetMenu(fileName = "DataTypeContainer", menuName = "DataTypeContainers/Items", order = 1)]
public class ItemDataTypeContainer : DataBrowserSupportedTypesContainer
{
    public override void GetSupportedDataBrowserTypes(Dictionary<string, Type> supportedTypes) {
        supportedTypes.Add("Items", typeof(ItemSO));
    }
}