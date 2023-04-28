using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MonsterMovement : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform target;
    [Range(1.1f,3f)][SerializeField] private float monsterMovementModifier = 2f;
    [SerializeField] private float movementRelativeToPlayerCameraModifier = 300f;
    private float playerSpeed;
    private float playerCameraSpeed = 0;
    private float prevPlayerCameraSpeed = 0;
    private Rigidbody _rb;
    public GameObject monsterRig;
    private Animator _monsterAnimator;

    private void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody>();
        _rb.interpolation = RigidbodyInterpolation.Interpolate;
        _rb.isKinematic = true;
        _rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        _monsterAnimator = monsterRig.GetComponent<Animator>();
    }

    private void Update()
    {
        playerSpeed = Mathf.Abs(PlayerMovement.Instance._rb.velocity.magnitude); 
        playerCameraSpeed = Mathf.Abs(PlayerCamController.Instance.GetCameraMovement());
    }

    private void LateUpdate()
    {
        //playerCameraSpeed = Mathf.Abs((PlayerCamController.Instance.GetCameraMovement() - playerCameraSpeed) * 10f);
        agent.SetDestination(target.position);
        
        // Use a coroutine to compare the current player camera speed to a value from a few frames ago
        StartCoroutine(ComparePlayerCameraSpeed());
        
        agent.speed = (playerSpeed + playerCameraSpeed) * monsterMovementModifier;
        _monsterAnimator.SetFloat("speed", agent.speed/2f);
    }
    
    private IEnumerator ComparePlayerCameraSpeed()
    {
        // Comparing the current player camera speed to a value from a few frames ago
        var currentPlayerCameraSpeed = Mathf.Abs(PlayerCamController.Instance.GetCameraMovement());
        var deltaPlayerCameraSpeed = Mathf.Abs(currentPlayerCameraSpeed - prevPlayerCameraSpeed);
        playerCameraSpeed = deltaPlayerCameraSpeed * movementRelativeToPlayerCameraModifier;
        
        // Wait for a few frames, otherwise the movement is very rough
        yield return new WaitForSeconds(0.2f);
        
        // Update the previous player camera speed value
        prevPlayerCameraSpeed = currentPlayerCameraSpeed;
    }
}
