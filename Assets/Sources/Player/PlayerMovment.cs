using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovment : MonoBehaviour
{
    [SerializeField] private JoystickForMovment _joystickForMovment;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _turnSpeed;

    private CharacterController _characterController;
    private Animator _animator;

    private Vector3 _direction;

    private int _idSpeed;
    private float _animationBlendSpeed;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();

        DistributionId();
    }
    
    private void OnEnable()
    {
        _joystickForMovment.OnJoystickMove += ChangeDirection;
    }

    private void OnDisable()
    {
        _joystickForMovment.OnJoystickMove -= ChangeDirection;
    }

    private void Update()
    {
        Move();
        Turn();
    }

    private void DistributionId()
    {
        _idSpeed = Animator.StringToHash("f_Speed");
    }

    private void Move()
    {
        var currentSpeed = _direction * _moveSpeed;

        if (_direction != Vector3.zero)
        {
            _characterController.Move(currentSpeed * Time.deltaTime);
        }

        _animationBlendSpeed = Mathf.Lerp(_animationBlendSpeed, currentSpeed.magnitude, Time.deltaTime * 5);

        _animator.SetFloat(_idSpeed, _animationBlendSpeed);
    }

    private void Turn()
    {
        if (_direction == Vector3.zero)
            return;

        Quaternion toTurn = Quaternion.LookRotation((_direction), Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toTurn, _turnSpeed * Time.deltaTime);
    }

    private void ChangeDirection(Vector2 vector)
    {
        _direction.x = vector.x;
        _direction.z = vector.y;
    }
}