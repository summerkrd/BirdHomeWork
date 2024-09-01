using UnityEngine;

public class CharacterFlyControl : MonoBehaviour
{
    [SerializeField] private int _forceValueForUp;
    [SerializeField] private float _forceValueForLeftRight;

    private Rigidbody _rigidbody;

    public int JumpCount { get; private set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rigidbody.AddForce(Vector3.up * _forceValueForUp, ForceMode.Impulse);
            JumpCount++;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            _rigidbody.AddForce(Vector3.right * _forceValueForLeftRight, ForceMode.VelocityChange);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _rigidbody.AddForce(Vector3.right * -_forceValueForLeftRight, ForceMode.VelocityChange);
        }

        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            _rigidbody.velocity = Vector3.zero;
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
}
