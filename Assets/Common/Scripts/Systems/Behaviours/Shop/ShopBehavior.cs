
using UnityEngine;

public class ShopBehaviour : MonoBehaviour
{
    public ShopComponent Shop { get; private set; }
    [SerializeField] ItemStack[] _items;
    [SerializeField] string[] _dialogue;
    void Awake() {
        Shop = new(this, _items, _dialogue);  
    }
}