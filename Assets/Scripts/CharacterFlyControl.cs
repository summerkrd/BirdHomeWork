using UnityEngine;

public class CharacterFlyControl : MonoBehaviour
{
    [SerializeField] private int _forceValueForUp;
    [SerializeField] private float _forceValueForLeftRight;

    private Rigidbody _rigidbody;
    private int _playerRotationAngleLimit = 30;

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
        if (_rigidbody.velocity.x > 0)
        {
            transform.eulerAngles = new Vector3(0, Mathf.Lerp(0, _playerRotationAngleLimit, 10f), 0);
        }
        else if (_rigidbody.velocity.x < 0)
        {
            transform.eulerAngles = new Vector3(0, Mathf.Lerp(0, -_playerRotationAngleLimit, 10f), 0);
        }
        else
        {
            transform.eulerAngles = Vector3.zero;
        }
    }
}
