using System;
using System.Collections.Generic;
using FletcherLibraries.DataBrowser;
using UnityEngine;

[CreateAssetMenu(fileName = "DataTypeContainer", menuName = "DataTypeContainers/Quests", order = 1)]
public class QuestDataTypeContainer : DataBrowserSupportedTypesContainer
{
    public override void GetSupportedDataBrowserTypes(Dictionary<string, Type> supportedTypes) {
        supportedTypes.Add("Quests", typeof(QuestSO));
        supportedTypes.Add("Queststeps", typeof(QueststepData));
    }
}