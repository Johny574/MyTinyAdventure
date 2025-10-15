

using System;
using System.Collections.Generic;
using FletcherLibraries.DataBrowser;
using UnityEngine;

[CreateAssetMenu(fileName = "DataTypeContainer", menuName = "DataTypeContainers/Skills", order = 1)]
public class SkillDataTypeContainer : DataBrowserSupportedTypesContainer
{
    public override void GetSupportedDataBrowserTypes(Dictionary<string, Type> supportedTypes) {
        supportedTypes.Add("Skills", typeof(SkillSO));
    }
}
