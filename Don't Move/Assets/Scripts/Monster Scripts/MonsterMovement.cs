using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class MonsterMovement : MonoBehaviour
{
    public NavMeshAgent agent;
    [SerializeField] private Transform target;
    [Range(1.1f,3f)][SerializeField] private float monsterMovementModifier = 2f;
    [SerializeField] private float movementRelativeToPlayerCameraModifier = 300f;
    [SerializeField] private float maxMoveSpeed;
    private float _playerSpeed;
    private float _playerCameraSpeed = 0;
    private float _prevPlayerCameraSpeed = 0;
    private Rigidbody _rb;
    public GameObject monsterRig;
    private Animator _monsterAnimator;
<<<<<<< Updated upstream
    public bool isStunned;
    public static Transform instance = null;
=======
<<<<<<< Updated upstream
    public bool isStunned;
=======
>>>>>>> Stashed changes
>>>>>>> Stashed changes

    private void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody>();
        _rb.interpolation = RigidbodyInterpolation.None;
        _rb.isKinematic = true;
        _rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        _monsterAnimator = monsterRig.GetComponent<Animator>();
        if (instance == null)
        {
            instance = transform;
        }
    }

    private void Update()
    {
        
        agent.SetDestination(target.position);
        //agent.Move((agent.desiredVelocity / 20f) * Time.deltaTime);

        transform.position = agent.nextPosition; // update child object position

        TurnAgent();
        
        // Getting movement modifiers
        _playerSpeed = Mathf.Abs(PlayerMovement.Instance._rb.velocity.magnitude); 
        _playerCameraSpeed = Mathf.Abs(PlayerCamController.Instance.GetCameraMovement());
        movementRelativeToPlayerCameraModifier = ((PlayerCamController.Instance.mouseSensitivity.x + PlayerCamController.Instance.mouseSensitivity.y) / GameSettings.Instance.mouseSensitivitySlider.value) / 10f;

        // Use a coroutine to compare the current player camera speed to a value from a few frames ago
        StartCoroutine(ComparePlayerCameraSpeed());
<<<<<<< Updated upstream
        agent.speed = (_playerSpeed + _playerCameraSpeed) * monsterMovementModifier;

        if (agent.speed > maxMoveSpeed)
        {
            agent.speed = maxMoveSpeed;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            TriggerStun(10f);
        }

        if (_playerSpeed == 0f && _playerCameraSpeed == 0f || isStunned == true) //  || GrappleScript.Instance.triggerCollider.enabled == false )
        {
            agent.isStopped = true;
            agent.speed = 0;
        }
        else
        {
            agent.isStopped = false;
        }
        _monsterAnimator.SetFloat("speed", agent.velocity.magnitude);
        
=======
        
        agent.speed = (playerSpeed + playerCameraSpeed) * monsterMovementModifier;
        _monsterAnimator.SetFloat("speed", agent.speed/2f);
>>>>>>> Stashed changes
    }

    private void TurnAgent()
    {
        // Getting the direction to face
        var targetPosition = target.position;
        var transformPosition = transform.position;
        var direction = (targetPosition - transformPosition);
        
        // Raycast between the monster and player to determine if the monster should be turning toward the player or not
        var hasVisualContact = Physics.Raycast(transformPosition, targetPosition - transformPosition, out var hitInfo) && hitInfo.collider.CompareTag("Player");
        Debug.DrawRay(transformPosition, direction,Color.cyan);
        // Calculate the desired velocity based on the direction towards the target and the current speed
        var desiredVelocity = direction * agent.speed;
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, agent.transform.rotation, agent.angularSpeed * Time.deltaTime);
        transform.rotation = agent.transform.rotation;
        transform.LookAt(target); // Instantaneous direction change, desired velocity path
        
        // If the raycast connects the monster is turned to face the player
        if (hasVisualContact)
        {        
            // Update the agent's velocity to match the desired velocity
            agent.velocity = desiredVelocity;
        }

    }
    
    
    private IEnumerator ComparePlayerCameraSpeed()
    {
        // Comparing the current player camera speed to a value from a few frames ago
        var currentPlayerCameraSpeed = Mathf.Abs(PlayerCamController.Instance.GetCameraMovement());
        var deltaPlayerCameraSpeed = Mathf.Abs(currentPlayerCameraSpeed - _prevPlayerCameraSpeed);
        _playerCameraSpeed = (deltaPlayerCameraSpeed * movementRelativeToPlayerCameraModifier) / GameSettings.Instance.mouseSensitivitySlider.value;
        
        // Wait for a few frames, otherwise the movement is very rough
        yield return new WaitForSeconds(0.05f);

        // Update the previous player camera speed value
        _prevPlayerCameraSpeed = currentPlayerCameraSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Physics.IgnoreCollision(collision.collider, agent.GetComponent<Collider>());
        }
    }

    IEnumerator CreateStun(float duration)
    {
        isStunned = true;
        _monsterAnimator.SetBool("Stunned", true);
        yield return new WaitForSeconds(duration);
        isStunned = false;
        _monsterAnimator.SetBool("Stunned", false);
        _monsterAnimator.Play("Roar");
        maxMoveSpeed *= 1.5f;
    }

    public void TriggerStun(float duration)
    {
        StartCoroutine(CreateStun(duration));
    }
}
