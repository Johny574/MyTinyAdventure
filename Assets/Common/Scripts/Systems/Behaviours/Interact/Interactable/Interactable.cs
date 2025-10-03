
using UnityEngine;

public class Interactable : MonoBehaviour {
    private Material _outline;
    [SerializeField] private Shader _outlineShader;
    [SerializeField] protected SpriteRenderer _renderer;
    [SerializeField] private Color _defaultColor = Color.white;
    [SerializeField] private Color _targetColor = Color.red;
    [SerializeField] private float _targetThickness = .5f;
    [SerializeField] private float _thickness = .2f;

    protected virtual void Start() {
        _outline = new Material(_outlineShader);
        _renderer.material = _outline;
        _renderer.material.SetTexture("_MainTex", _renderer.sprite.texture);
        _renderer.material.SetColor("_Color", _defaultColor);
        _renderer.material.SetFloat("_Thickness", _thickness);
        CancelTarget();
    }

    public void CancelTarget() {
        _renderer.material?.SetColor("_Color", _defaultColor);
        _renderer.material.SetFloat("_Thickness", _thickness);
    }

    public void Target() {
        _renderer.material?.SetColor("_Color", _targetColor);
        _renderer.material.SetFloat("_Thickness", _targetThickness);
    }

}