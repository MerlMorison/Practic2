using PlayerMove;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody _player;
    [SerializeField] private BoxCollider _playerCollider;

    [SerializeField] private float _playerSpeed;
    [SerializeField] private float _buffSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _jumpPower;
    [SerializeField] private LayerMask _collision;

    private Animator _anim;
    private PlayerMovement _inputManager;
    private bool _canUseBuff = true; // Флаг, показывающий, можно ли использовать бафф
    private Vector3 _originalColliderSize;
    private float _originalHeight;
    private float _accelerationInterval = 5f; // Интервал ускорения
    private float _accelerationTimer = 0f; // Таймер для ускорения

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _inputManager = new PlayerMovement();
        _inputManager.PlayerWASD.Jump.performed += Jump;
        _inputManager.PlayerWASD.Buff.performed += Buff;
    }
    private void Start()
    {
        _originalColliderSize = _playerCollider.size;
        _originalHeight = _playerCollider.size.y;
    }
    private void FixedUpdate()
    {
        MovePlayer();
        RotatePlayer();
        AcceleratePlayer();
    }

    // ______________________________________________________________________СТРИБКИ________________________________________________________________________________________
    private void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded())
        {
            _playerCollider.size = new Vector3(_originalColliderSize.x, _originalHeight / 2f, _originalColliderSize.z);
            _player.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
            _anim.SetBool("Jump", true);
            Invoke("ResetJumpAnimation", 1.2f);
        }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(_playerCollider.bounds.center, Vector3.down, _playerCollider.bounds.extents.y + 2f, _collision);
    }

    private void ResetJumpAnimation()
    {
        _anim.SetBool("Jump", false);
        _playerCollider.size = _originalColliderSize;
    }
    // ___________________________________________________________________РУХ ПЕРСОНАЖА_____________________________________________________________________________________
    private void MovePlayer()
    {
        var moveDirection = _inputManager.PlayerWASD.WASD.ReadValue<Vector2>();
        var moveInput = new Vector3(moveDirection.x, 0f, moveDirection.y);

        moveInput = moveInput.normalized * _playerSpeed;
        moveInput = transform.TransformDirection(moveInput);

        var targetSpeed = Mathf.Abs(moveDirection.y) < 0.01f ? Mathf.Abs(moveDirection.x) : moveDirection.y * _playerSpeed;
        var direction = Mathf.Lerp(_anim.GetFloat("Direction"), moveDirection.x, 1f);
        var speed = Mathf.Lerp(_anim.GetFloat("Speed"), targetSpeed, 1f);

        _anim.SetFloat("Speed", speed);
        _anim.SetFloat("Direction", direction);

        _player.velocity = new Vector3(moveInput.x, _player.velocity.y, moveInput.z);
    }

    private void RotatePlayer()
    {
        var moveDirection = _inputManager.PlayerWASD.WASD.ReadValue<Vector2>();
        if (moveDirection != Vector2.zero)
        {
            var lookRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.y));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * _rotationSpeed);
        }
    }

    private void AcceleratePlayer()
    {
        _accelerationTimer += Time.fixedDeltaTime;
        if (_accelerationTimer >= _accelerationInterval)
        {
            _playerSpeed += 1f;
            _accelerationTimer = 0f;
        }
    }
    // ___________________________________________________________________БАФ ПЕРСОНАЖА_____________________________________________________________________________________
    private void Buff(InputAction.CallbackContext context)
    {
        if (_canUseBuff)
        {
            _canUseBuff = false; // Блокируем возможность повторного использования баффа
            _playerSpeed -= 4f; // Уменьшаем скорость игрока на 4
            StartCoroutine(EnableBuffAfterDelay(5f)); // Запускаем корутину для разблокировки возможности повторного использования баффа
        }
    }

    private IEnumerator EnableBuffAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        _canUseBuff = true; // Разрешаем повторное использование баффа
    }
    // _______________________________________________________________________ІНШЕ_________________________________________________________________________________________
    private void OnEnable() => _inputManager.Enable();

    private void OnDisable() => _inputManager.Disable();
}
