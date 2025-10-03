using TMPro;
using UnityEngine;

public class Tag : MonoBehaviour, IPoolObject<string>
{
    public TextMeshProUGUI _textField;

    #if UNITY_EDITOR
        void OnValidate() {
            _textField = GetComponentInChildren<TextMeshProUGUI>();
        }
#endif

    public void Bind(string variant) => _textField.text = variant;
}
