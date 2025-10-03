using UnityEngine;
using FletcherLibraries;

namespace FletcherLibraries.DataBrowser.Example
{
    /// <summary>
    /// Example of a Singleton manager that can be used to access Scriptable Object data in the Database by ID, both in editor (if this object is placed in an open scene) and at runtime
    /// </summary>
    public class ExampleStaticDataManager : Singleton<ExampleStaticDataManager>
    {
        [SerializeField] private ExampleStaticDatabase Database;

        void Awake()
        {
            this.Database = Resources.Load<ExampleStaticDatabase>("ExampleStaticDatabase");
        }

        public ExampleTargetDataType GetTargetType(int id) => this.Database.TargetTypes.LookupDataWithId(id);
        public ExampleColorDataType GetColorType(int id) => this.Database.ColorTypes.LookupDataWithId(id);
    }
}
