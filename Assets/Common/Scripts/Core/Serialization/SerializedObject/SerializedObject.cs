using UnityEngine;

public abstract class SerializedObject<T> : MonoBehaviour, ISerializedObject<T>
{
    [SerializeField] protected string _savename;
    public abstract void Load(T save);
    public abstract T Save();
    protected virtual void Start() => LoadAutoSave();
    public void LoadAutoSave() {
        if (Serializer.ContainsSave(SaveSlot.AutoSave, _savename, ".json"))
            Load(Serializer.LoadFile<T>(_savename + ".json", SaveSlot.AutoSave));
    }
    void OnApplicationQuit() => Serializer.SaveFile(Save(), _savename + ".json", SaveSlot.AutoSave);
}