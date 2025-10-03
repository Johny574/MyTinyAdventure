using UnityEngine;
using System.Collections.Generic;

namespace FletcherLibraries.DataBrowser
{
    /// <summary>
    /// Create a class that inherits from this and implements 'GetSupportedDataBrowserTypes', and create an instance of that class in your project, in order to specify what Scriptable Object types are displayed in the Data Browser window
    /// </summary>
    public abstract class DataBrowserSupportedTypesContainer : ScriptableObject
    {
        [Tooltip("When this is on, the types added by 'GetSupportedDataBrowserTypes' in this object's code will be used in the Data Browser window")]
        [SerializeField] protected bool Active = true;
        public bool IsActive() => this.Active;

        /// <summary>
        /// Implement this method and add the Scriptable Object types you would like to be displayed in the Data Browser to 'supportedTypes'. The keys should be the intended display name for the object type in the window, while the value should be the type.
        /// </summary>
        public abstract void GetSupportedDataBrowserTypes(Dictionary<string, System.Type> supportedTypes);
    }
}
