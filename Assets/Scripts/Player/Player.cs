using PlayerMove;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    [SerializeField] private Rigidbody _player;

    [SerializeField] private float _playerSpeed;
    [SerializeField] private float _buffSpeed;



    [SerializeField] private float _jumpPower;
    [SerializeField] private float _groundCheckDistance;

    private Animator _anim;
    private PlayerMovement _inputManager;
    private bool _isGrounded;
    private bool _isBuffActive;

    private void Awake()
    {
        _anim = GetComponent<Animator>();

        _inputManager = new PlayerMovement();

        _inputManager.PlayerWASD.Jump.performed += Jump;
        _inputManager.PlayerWASD.Buff.performed += Buff;
        //_inputManager.PlayerWASD.Slide.performed += Slide;

    }
    private void Jump(InputAction.CallbackContext context)
    {
        _isGrounded = Physics.Raycast(_player.transform.position, Vector3.down, _groundCheckDistance);
        Debug.DrawLine(_player.transform.position, _player.transform.position + Vector3.down * _groundCheckDistance, Color.blue);

        if (_isGrounded && !_anim.GetBool("Jump"))
        {
            _anim.SetBool("Jump", true);
            _player.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
            Invoke("ResetJumpAnimation", 1.0f);
        }
    }
    private void FixedUpdate()
    {
        var moveDirection = _inputManager.PlayerWASD.WASD.ReadValue<Vector2>();
        var moveInput = new Vector3(moveDirection.x, 0f, moveDirection.y);

        moveInput = moveInput.normalized * _playerSpeed;
        moveInput = transform.TransformDirection(moveInput);

        _player.velocity = new Vector3(moveInput.x, _player.velocity.y, moveInput.z);

    }
    private void Buff(InputAction.CallbackContext context)
    {
        if (!_isBuffActive)
        {
            _isBuffActive = true;
            _playerSpeed *= _buffSpeed;
            StartCoroutine(DisableBuffAfterDelay(5f));
        }
    }

    private IEnumerator DisableBuffAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        _playerSpeed /= _buffSpeed;
        _isBuffActive = false;
    }
    private void OnEnable()
    {
        _inputManager.Enable();
    }
    private void OnDisable()
    {
        _inputManager.Disable();
    }
}
