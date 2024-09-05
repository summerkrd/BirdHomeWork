using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterFlyControl : MonoBehaviour
{
    [SerializeField] private SoundManager _soundManager;
    [SerializeField] private Animator[] _animators;  
    [SerializeField] private List<GameObject> _characterSkin;
    [SerializeField] private TextMeshProUGUI _jumpCountText;

    [SerializeField] private int _forceValueForUp;
    [SerializeField] private float _forceValueForLeftRight;    

    private Rigidbody _rigidbody;
    private int _playerRotationAngleLimit = 30;
    private string _jumpAnimation = "Jump";
    private int _currentCharacter;
    
    public int JumpCount { get; private set; }    

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        
        FindCurrentCharacterID();
    }

    void Update()
    {        
        PlayerRotation();

        _jumpCountText.text = $"Количество прыжков: {JumpCount.ToString()}";

        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerJupm();
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

    private void FindCurrentCharacterID()
    {
        foreach (GameObject character in _characterSkin)
        {
            if (character.activeSelf)
            {
                _currentCharacter = _characterSkin.IndexOf(character);
            }
        }
    }

    public void SwitchCharacter()
    {
        FindCurrentCharacterID();
        _characterSkin[_currentCharacter].SetActive(false);
        _characterSkin[Random.Range(0, _characterSkin.Count)].SetActive(true);
    }

    private void PlayerMove(Vector3 moveDirection, float forceValue, ForceMode forceMode)
    {
        _rigidbody.AddForce(moveDirection * forceValue, forceMode);
    }

    private void PlayerJupm()
    {
        PlayerMove(Vector3.up, _forceValueForUp, ForceMode.Impulse);
        _soundManager.PlayFlapSound();

        foreach (Animator animator in _animators)
        {
            if (animator.gameObject.activeSelf)
            {
                animator.SetTrigger(_jumpAnimation);
            }
        }

        JumpCount++;
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
