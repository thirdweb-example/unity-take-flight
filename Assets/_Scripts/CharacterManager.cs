using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

[RequireComponent(typeof(CharacterController))]
public class CharacterManager : MonoBehaviour
{
    public UnityEvent<float> OnDeath;
    public bool IsEnabled { get; set; }
    public bool IsInputEnabled { get; set; }
    public float DistanceTravelled { get; private set; }

    [SerializeField]
    private float _forwardSpeed = 10.0f;

    [SerializeField]
    private float _lateralSpeed = 5.0f;

    [SerializeField]
    private float _rotationSpeed = 5.0f;

    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private List<Transform> _lanesSToSW;

    private int _currentLaneIndex;

    private CharacterController _characterController;

    public static CharacterManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        _characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        IsEnabled = true;
        _currentLaneIndex = 0;
        BlockchainManager.Instance.OnLoggedIn.AddListener((address) => IsInputEnabled = true);
    }

    private void Update()
    {
        if (IsEnabled)
        {
            HandleInput();
            Move();
            Rotate();
        }
    }

    private void HandleInput()
    {
        if (IsInputEnabled)
        {
            DistanceTravelled += _forwardSpeed * Time.deltaTime;

            // Handle lane switching
            if (Input.GetKeyDown(KeyCode.A))
            {
                _currentLaneIndex--;
                if (_currentLaneIndex < 0)
                    _currentLaneIndex = _lanesSToSW.Count - 1;
                _animator.SetTrigger("Roll_Left");
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                _currentLaneIndex++;
                if (_currentLaneIndex >= _lanesSToSW.Count)
                    _currentLaneIndex = 0;
                _animator.SetTrigger("Roll_Right");
            }
        }
    }

    private void Move()
    {
        // Target lane position with adjusted Y to match character's current Y position
        Vector3 targetPosition = new Vector3(
            _lanesSToSW[_currentLaneIndex].position.x,
            _lanesSToSW[_currentLaneIndex].position.y,
            _characterController.transform.position.z
        );

        // Smoothly interpolate to the target position
        Vector3 smoothedPosition = Vector3.Slerp(
            _characterController.transform.position,
            targetPosition,
            _lateralSpeed * Time.deltaTime
        );
        _characterController.Move(smoothedPosition - _characterController.transform.position);

        // Apply forward movement
        Vector3 forwardMovement = _forwardSpeed * Time.deltaTime * Vector3.forward;
        _characterController.Move(forwardMovement);
    }

    private void Rotate()
    {
        // Rotate player to match the lane's/wall's orientation
        Quaternion targetRotation = Quaternion.LookRotation(
            Vector3.forward,
            _lanesSToSW[_currentLaneIndex].up
        );
        _characterController.transform.rotation = Quaternion.Slerp(
            _characterController.transform.rotation,
            targetRotation,
            Time.deltaTime * _rotationSpeed
        );
    }

    public int GetLaneIndex()
    {
        return _currentLaneIndex;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            IsEnabled = false;
            IsInputEnabled = false;
            // _animator.SetTrigger("Death");
            OnDeath?.Invoke(DistanceTravelled);
        }
    }
}
