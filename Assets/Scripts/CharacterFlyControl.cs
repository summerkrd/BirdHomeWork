using System.Collections;
using UnityEngine;

public class CharacterFlyControl : MonoBehaviour
{
    [SerializeField] private SoundManager _soundManager;
    [SerializeField] private Animator _animator;

    [SerializeField] private int _forceValueForUp;
    [SerializeField] private float _forceValueForLeftRight;    

    private Rigidbody _rigidbody;
    private int _playerRotationAngleLimit = 30;
    private string _jumpAnimation = "Jump";

    public int JumpCount { get; private set; }    

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {        
        PlayerRotation();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerMove(Vector3.up, _forceValueForUp, ForceMode.Impulse);
            _soundManager.PlayFlapSound();
            _animator.SetTrigger(_jumpAnimation);
            JumpCount++;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            PlayerMove(Vector3.right.normalized, _forceValueForLeftRight, ForceMode.VelocityChange);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            PlayerMove(Vector3.right.normalized, -_forceValueForLeftRight, ForceMode.VelocityChange);
        }
    }

    public void ResetJumpCounter()
    {
        JumpCount = 0;
    }

    public void ResetVelocity()
    {
        _rigidbody.velocity = Vector3.zero;        
    }

    private void PlayerMove(Vector3 moveDirection, float forceValue, ForceMode forceMode)
    {
        _rigidbody.AddForce(moveDirection * forceValue, forceMode);
    }

    private void PlayerRotation()
    {
        if (_rigidbody.velocity.x > 0.2f)
        {
            transform.localEulerAngles = new Vector3(0, _playerRotationAngleLimit, 0);
        }
        else if (_rigidbody.velocity.x < - 0.2f)
        {
            transform.localEulerAngles = new Vector3(0, -_playerRotationAngleLimit, 0);
        }
        else
        {
            transform.eulerAngles = Vector3.zero;
        }
    }

    public IEnumerator JumpOnStart()
    {
        yield return new WaitForSeconds(0.2f);

        PlayerMove(Vector3.up, _forceValueForUp, ForceMode.Impulse);
    }
}
