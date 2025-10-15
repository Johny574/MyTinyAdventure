using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CreateAssetMenu(fileName = "_recipe", menuName = "Recipes/Recipe", order = 1)]
public class Recipe : ScriptableObject {
    #if UNITY_EDITOR
    void OnValidate() {
        string[] guid = AssetDatabase.FindAssets($"t:{typeof(Recipe)}");
        
        for (int i = 0; i < guid.Length; i++) {
            string path = AssetDatabase.GUIDToAssetPath(guid[i]);
            Recipe asset = AssetDatabase.LoadAssetAtPath<Recipe>(path);
            asset.ID = i + 1;
            EditorUtility.SetDirty(asset);
        }    
    }
    #endif
    
    public List<ItemStack> Material;
    public ItemStack Result;
    [SerializeField] private int _id = 0;
    public int ID { get => _id; set => _id = value; }
}