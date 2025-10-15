
public class ItemStackData  {
    public string GUID;
    public int Count = 0;
    
    public ItemStackData(string item, int counter) {
        GUID = item;
        Count = counter;
    }
}