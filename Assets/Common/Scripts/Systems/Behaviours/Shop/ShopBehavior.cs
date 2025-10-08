
using UnityEngine;

public class ShopBehaviour : MonoBehaviour
{
    public ShopComponent Shop { get; private set; }
    [SerializeField] ItemStack[] _items;
    [SerializeField] DialogueExchange[] _dialogue;
    void Awake() {
        Shop = new(this, _items, _dialogue);  
    }
}