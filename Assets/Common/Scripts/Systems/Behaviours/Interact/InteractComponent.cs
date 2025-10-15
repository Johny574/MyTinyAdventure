using System;
using System.Collections.Generic;
using UnityEngine;

public class InteractComponent : Component {

    private GameObject _targetBox = null;
    int _index = 0;
    public IInteractable Target;
    List<IInteractable> _targets;
    public Action<IInteractable> TargetChanged;
    public string TargetName;
    StaminaComponent _stamina;
    public InteractComponent(InteractBehaviour behaviour) : base(behaviour) {
    }

    public void Initilize(StaminaComponent stamina) {
        _stamina = stamina;
        if (_stamina.Sprinting)
            _stamina.Sprinting = false;
            
        _targets = new();
    }

    public void OnTriggerEnter2D(Collider2D col) {
        _targets.Add(col.gameObject.GetComponent<IInteractable>());
    }

    public void OnTriggerExit2D(Collider2D col) {
        IInteractable interactable = col.gameObject.GetComponent<IInteractable>();
        if (_targets.Contains(interactable))
            _targets.Remove(interactable);
    }

    public void Update() {
        if (_targets.Count == 0) {
            if (Target != null) {
                Target.CancelTarget();
                _index = 0;
                Target = null;
                TargetName = null;
                TargetChanged?.Invoke(Target);
            }
            if (_targetBox != null) {
                _targetBox?.gameObject?.SetActive(false);
            }
            return;
        }

        else if (_targets.Count > 0 && Target != null) {
            if (Target != _targets[_index]) {
                Target.CancelTarget();
                Target = _targets[_index];
                Target.Target();
                TargetChanged?.Invoke(Target);
                TargetName = ((MonoBehaviour)Target).name;
            }
        }

        else if (_targets.Count > 0 && Target == null) {
            Target = _targets[_index];
            Target.Target();
            TargetName = ((MonoBehaviour)Target).name;
            TargetChanged?.Invoke(Target);
        }

        if (_targetBox != null) {
            _targetBox.transform.position = ((MonoBehaviour)Target).GetComponentInChildren<SpriteRenderer>().transform.position;
            _targetBox.gameObject.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Tab)) {
            Target.CancelTarget();
            _index = (_index + 1) % _targets.Count;
            Target = _targets[_index];
            Target.Target();
            TargetChanged?.Invoke(Target);
            TargetName = ((MonoBehaviour)Target).name;
        }

        if (Input.GetKeyDown(KeyCode.E)) {
            Target = _targets[_index];
            Target.Target();
            Target.Interact(Behaviour.gameObject);
            if (_stamina.Sprinting)
               _stamina.Sprinting = false;
        }
    }

}