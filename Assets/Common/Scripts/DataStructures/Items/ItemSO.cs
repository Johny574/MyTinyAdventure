using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "Items/Items/ItemSO", order = 1)]
public class ItemSO : ScriptableObject {
    #if UNITY_EDITOR
        protected void OnValidate() {
            string path = AssetDatabase.GetAssetPath(this);
            UID = AssetDatabase.AssetPathToGUID(path);
            EditorUtility.SetDirty(this);
        }
    #endif
    public string UID;
    public string DisplayName;
    public string Description;
    public Sprite Sprite;
    public ItemGrade Grade;
    public int Limit = 16;
    public int CostPrice;
    public float SellPercentage = .7f;
}