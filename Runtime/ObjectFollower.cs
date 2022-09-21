using System;
using UnityEngine;

namespace Ineor.Utils.ObjectFollower {

// enums
public enum RotationFollowMode { None, Copy, LookAt };

/// <summary>
/// This script is used to simply follow a target object.
/// You can choose to copy the position, rotation or scale.
///
/// This can be used to make the hierarchy of a character easier to read. For example instead of
/// childing an axe to the palm of a character which is several levels down the hierarchy you put the script
/// on the axe and set it to follow the palm.
///
/// Made by: Mihael Golob, 30. 8. 2022
/// </summary>
public class ObjectFollower : MonoBehaviour {
    // inspector assigned
    [SerializeField] private Transform _target;

    [Header("Follow position")] 
    [SerializeField] private bool _followPosition = false;
    [SerializeField] private Vector3 _positionOffset = Vector3.zero;
    [SerializeField] private bool _lerpMovement = true;
    [SerializeField] private float _lerpMovementSpeed = 3f;

    [Header("Follow Rotation")]
    [SerializeField] private RotationFollowMode _followRotationMode = RotationFollowMode.None;
    [SerializeField] private bool _lerpRotation = true;
    [SerializeField] private float _lerpRotationSpeed = 5f;
    
    [Header("Follow Scale")]
    [SerializeField] private bool _followScale = false;

    // public properties
    public Transform Target {
        get => _target;
        set => _target = value;
    }
    
    public bool FollowPosition {
        get => _followPosition;
        set => _followPosition = value;
    }

    public RotationFollowMode FollowRotationMode {
        get => _followRotationMode;
        set => _followRotationMode = value;
    }
    
    public bool FollowScale {
        get => _followScale;
        set => _followScale = value;
    }
    
    // Unity event methods
    private void Start() {
        if (_target == null) throw new NullReferenceException($"{gameObject.name}: Target is null");
    }

    private void Update() {
        UpdatePosition();
        UpdateRotation();
        UpdateScale();
    }
    
    // helper methods
    private void UpdatePosition() {
        if (!_followPosition) return;

        if (!_lerpMovement) {
            transform.position = _target.position + _positionOffset;
        }
        else {
            transform.position = Vector3.Lerp(transform.position, _target.position + _positionOffset, _lerpMovementSpeed * Time.deltaTime);
        }
    }

    private void UpdateRotation() {
        if (transform.position == _target.transform.position) return;
        
        switch (_followRotationMode) {
            case RotationFollowMode.None:
            default:
                return;
            case RotationFollowMode.Copy:
                // copy the rotation of the target
                transform.rotation = _target.rotation;
                break;
            // look at the target
            case RotationFollowMode.LookAt when !_lerpRotation:
                transform.LookAt(_target);
                break;
            case RotationFollowMode.LookAt:
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_target.position - transform.position), _lerpRotationSpeed * Time.deltaTime);
                break;
        }
    }

    private void UpdateScale() {
        if (!_followScale) return;

        transform.localScale = _target.localScale;
    }
}

}