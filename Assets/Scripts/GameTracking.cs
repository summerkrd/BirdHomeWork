using UnityEngine;

public class GameTracking : MonoBehaviour
{
    [SerializeField] private CharacterFlyControl _characterFlyControl;
    [SerializeField] private SoundManager _soundManager;
    
    [SerializeField] private float _upAndDownLimit;
    [SerializeField] private float _leftAndRightLimit;
    
    [SerializeField] private Transform _upperObstacle;
    [SerializeField] private Transform _lowerObstacle;       

    private void Start()
    {
        StartGame();

        _upperObstacle.position = new Vector3(0, _upAndDownLimit, 0);
        _lowerObstacle.position = new Vector3(0, -_upAndDownLimit, 0);
    }

    private void Update()
    {
        Vector3 playerPosition = _characterFlyControl.transform.position;
              
        if (playerPosition.y > _upAndDownLimit || playerPosition.y < -_upAndDownLimit)
        {            
            PlayerDead();
            
        }
        else if (playerPosition.x > _leftAndRightLimit)
        {
            playerPosition.x = _leftAndRightLimit;
        }
        else if (playerPosition.x < -_leftAndRightLimit)
        {
            playerPosition.x = -_leftAndRightLimit;
        }

        _characterFlyControl.transform.position = playerPosition;        

        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartGame();            
        }        
    }

    private void StartGame()
    {
        _soundManager.PlayBackgroundSound(true);
        _characterFlyControl.transform.position = Vector3.zero;
        _characterFlyControl.ResetVelocity();
        _characterFlyControl.gameObject.SetActive(true);   
        _characterFlyControl.ResetJumpCounter();
        StartCoroutine(_characterFlyControl.JumpOnStart());
    }

    private void PlayerDead()
    {
        _soundManager.PlayBackgroundSound(false);
        _soundManager.PlayHitSound();
        _characterFlyControl.gameObject.SetActive(false);
    }
}

