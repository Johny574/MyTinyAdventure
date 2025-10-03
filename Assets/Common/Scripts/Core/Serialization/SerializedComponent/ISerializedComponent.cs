



public interface ISerializedComponent<T> {
    public abstract T Save();
    public abstract void Load(T save);
}