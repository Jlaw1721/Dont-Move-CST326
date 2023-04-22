using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MonsterMovement : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform target;
    [Range(1,3)][SerializeField] private float monsterMovementModifier;
    [SerializeField] private float movementRelativeToPlayerCameraModifier = 100f;
    private float playerSpeed;
    private float playerCameraSpeed = 0;
    private float prevPlayerCameraSpeed = 0;
    
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
