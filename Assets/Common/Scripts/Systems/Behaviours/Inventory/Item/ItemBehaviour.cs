using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class ItemBehaviour : MonoBehaviour, IInteractable, IPoolObject<ItemStack>  {
    ItemComponent _item; 
    protected IPoolObject<string> _tag;
    [SerializeField] Animator _animator;
    [SerializeField] ItemStack _stack;
    [SerializeField] SpriteRenderer _renderer;

    void Awake() {
        _item = new(this, _stack, _renderer);
    }
    
    void Start() {
        _item.Initilize();
        SceneTracker.Instance.Register<ItemBehaviour>(gameObject);   
        CreateTag();
    }

    void OnDisable() {
        SceneTracker.Instance?.Unregister<ItemBehaviour>(gameObject);   
    }

    public void Interact(GameObject accesor) {
        var tagobj = ((MonoBehaviour)_tag).gameObject;
        _item.Interact(accesor);
        tagobj.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    public void CancelTarget() {

    }

    public void Target() {
        // float floatYmin = 0f;
        // float floatYmax = .5f;
        // DOTween.Sequence()
        // .Append(DOTween.To(() => floatYmin, x => {
        //     floatYmin = x;
        //     transform.position = new Vector2(transform.position.x, transform.position.y + floatYmin);
        // }, floatYmin, .1f)).SetEase(Ease.OutElastic)
        // .Append(DOTween.To(() => floatYmax, x => {
        //     floatYmin = x;
        //     transform.position = new Vector2(transform.position.x, transform.position.y + floatYmax);
        // }, floatYmin, .1f)).SetLoops(-1, LoopType.Incremental);
    }

    async void CreateTag() {
        _tag = await TagFactory.Instance.Pool.GetObject<string>();
        _tag.Bind(gameObject.name);
        
        var tagobj = ((MonoBehaviour)_tag).gameObject;
        tagobj.GetComponent<Follower>().Follow(gameObject);
    }

    public void Bind(ItemStack variant) {
        _item.Bind(variant);
    }
}