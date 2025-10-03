using UnityEngine;

[CreateAssetMenu(fileName = "_itemgrade", menuName = "Items/Grades/Grade", order = 1)]
public class ItemGrade : ScriptableObject {
    public Color Color;
    public ID Grade;
    public enum ID { 
        Common,
        Rare
    } 
}