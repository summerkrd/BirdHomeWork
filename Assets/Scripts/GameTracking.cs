using UnityEngine;

public class GameTracking : MonoBehaviour
{
    [SerializeField] private CharacterFlyControl _characterFlyControl;
    [SerializeField] private SoundManager _soundManager;    
    
    [SerializeField] private float _upAndDownLimit;
    [SerializeField] private float _leftAndRightLimit;
    
    [SerializeField] private Transform _upperObstacle;
    [SerializeField] private Transform _lowerObstacle;

    [SerializeField] private Animator _CameraAnimator;
    [SerializeField] private ParticleSystem _explotion;
    
    private Vector3 _playerPosition;
    private string _cameraShakeAnimation = "Shake";

    private void Start()
    {
        StartGame();

        _upperObstacle.position = new Vector3(0, _upAndDownLimit, 0);
        _lowerObstacle.position = new Vector3(0, -_upAndDownLimit, 0);
    }

    private void Update()
    {
        _playerPosition = _characterFlyControl.transform.position;
              
        if (_playerPosition.y > _upAndDownLimit || _playerPosition.y < -_upAndDownLimit)
        {            
            PlayerDie();            
        }
        
        if (_playerPosition.x > _leftAndRightLimit)
        {
            _playerPosition.x = _leftAndRightLimit;
        }
        else if (_playerPosition.x < -_leftAndRightLimit)
        {
            _playerPosition.x = -_leftAndRightLimit;
        }

        _characterFlyControl.transform.position = _playerPosition;

        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartGame();            
        }        
    }

    private void StartGame()
    {
        _soundManager.PlayBackgroundSound(true);        
        
        _characterFlyControl.ResetVelocity();
        _characterFlyControl.gameObject.SetActive(true);   
        _characterFlyControl.ResetJumpCounter();
        _characterFlyControl.SwitchCharacter();
       
        StartCoroutine(_characterFlyControl.JumpOnStart());
    }

    private void PlayerDie()
    {
        _CameraAnimator.SetTrigger(_cameraShakeAnimation);             
        
        _characterFlyControl.gameObject.SetActive(false);

        _explotion.transform.position = _playerPosition;
        _explotion.Play();

        _playerPosition = Vector3.zero;

        _soundManager.PlayHitSound();
        _soundManager.PlayBackgroundSound(false);
    }
}

