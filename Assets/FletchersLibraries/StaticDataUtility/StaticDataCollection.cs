using UnityEngine;
using NaughtyAttributes;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace FletcherLibraries
{
    /// <summary>
    /// Parent class for collection of a type of Static Data. Can be used to reference objects of the type by their ID.
    /// </summary>
    public abstract class StaticDataCollection<T> : IStaticDataCollection where T : StaticDataObject
    {
        [SerializeField] protected T[] DataObjects;
        [SerializeField] protected TypeDictionary LookupTable;

        [Serializable]
        public class TypeDictionary : SerializableDictionary<int, T> { }

        /// <summary>
        /// Returns the total number of Static Data objects in this collection
        /// </summary>
        public override int GetCount() => this.DataObjects.Length;

        /// <summary>
        /// Returns the Type collected by this collection
        /// </summary>
        public override Type GetCollectedType() => typeof(T);

        /// <summary>
        /// Returns the Static Data object within this collection corresponding to ID 'id'
        /// </summary>
        public T LookupDataWithId(int id)
        {
            T retVal;
            bool success = this.LookupTable.TryGetValue(id, out retVal);
            return success ? retVal : null;
        }

        /// <summary>
        /// Calls 'callback' with each Static Data object in this collection
        /// </summary>
        public void ForEachData(Action<T> callback)
        {
            foreach (T t in this.DataObjects)
                callback(t);
        }

#if UNITY_EDITOR
        /// <summary>
        /// Finds all data in the project of the type colleced by this Collection, and adds them to this Collection
        /// </summary>
        [Button("Compile Data")]
        public override void CompileData()
        {
            this.DataObjects = StaticDataHelpers.CompileAllObjectsOfType(StaticDataHelpers.DEFAULT_DATAOBJECT_FOLDER, this.DataObjects);
            if (this.LookupTable == null)
                this.LookupTable = new TypeDictionary();
            else
                this.LookupTable.Clear();

            foreach (var data in this.DataObjects)
                this.LookupTable.Add(data.ID, data);

            EditorUtility.SetDirty(this);
        }
#endif
    }

    public abstract class IStaticDataCollection : ScriptableObject
    {
        public abstract int GetCount();
        public abstract Type GetCollectedType();

#if UNITY_EDITOR
        public abstract void CompileData();
#endif
    }
}
