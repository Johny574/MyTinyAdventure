using UnityEngine;

public class AimComponent : Component
{
    #region References
    Camera _mainCamera;
    FlipComponent _flip;
    Transform _light;
    #endregion

    #region Aim
    public Vector2 Aim { get; private set; }
    public Vector2 AimDelta { get; set; }
    public float LookAngle { get; set; }
    #endregion

    public AimComponent(AimBehaviour behaviour, Transform light) : base(behaviour) {
        _light = light;
    }

    public void Initilize(FlipComponent flip) {
        _flip = flip;
        _mainCamera = Camera.main;
    }

    public void Update() {
        Aim = _mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _mainCamera.nearClipPlane));
        AimDelta = (Aim - (Vector2)Behaviour.transform.position).normalized;
        LookAngle = Rotation2D.LookAngle(AimDelta, 0);

        if (LookAngle < 0)
            LookAngle += 360;

        _light.transform.rotation = Quaternion.Euler(0, 0, LookAngle - 90);
        _flip.Flip(Vector3.SignedAngle(Behaviour.transform.up, AimDelta, Behaviour.transform.forward) < 0 ? true : false);
    }
}