using System;
using System.Collections.Generic;
using FletcherLibraries.DataBrowser;
using UnityEngine;

[CreateAssetMenu(fileName = "DataTypeContainer", menuName = "DataTypeContainers/Recipe", order = 1)]
public class RecipeDataTypeContainer : DataBrowserSupportedTypesContainer
{
    public override void GetSupportedDataBrowserTypes(Dictionary<string, Type> supportedTypes) {
        supportedTypes.Add("Recipes", typeof(Recipe));
    }
}