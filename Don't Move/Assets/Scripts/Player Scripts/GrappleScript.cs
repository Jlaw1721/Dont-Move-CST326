using System.Collections;
using UnityEngine;

public class GrappleScript : MonoBehaviour
{
    public Transform monster;
    public Transform player;
    [SerializeField] private int counterGoal = 5;
    public Collider triggerCollider;
    private MonsterMovement _monsterMovement;
    private static GrappleScript _instance;
    public static GrappleScript Instance => _instance;
    private Coroutine StunnedEvent;
    private void Awake()
    {
        if (_instance == null)
            _instance = this;
    }

    private void Start()
    {
        _monsterMovement = monster.GetComponent<MonsterMovement>();
    }

    private void Update()
    {
        if (triggerCollider.enabled == false)
        {
            _monsterMovement.agent.isStopped = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Monster"))
        {
            LockPositions();
            StartCoroutine(Grapple());
        }
    }

    private void LockPositions()
    {
        player.LookAt(monster);
        PlayerCamController.Instance.currentOrientation.LookAt(monster);
        PlayerMovement.Instance.canMove = false;
        PlayerCamController.Instance.canMoveCamera = false;
        _monsterMovement.agent.isStopped = true;
    }
    
    public void UnlockPositions()
    {
        PlayerMovement.Instance.canMove = true;
        PlayerCamController.Instance.canMoveCamera = true;
        
        StunnedEvent = StartCoroutine(DisableTrigger());
    }
    
    private IEnumerator DisableTrigger()
    {
        triggerCollider.enabled = false;
        


        yield return new WaitForSeconds(5f);
        
        triggerCollider.enabled = true;
    }

    private IEnumerator Grapple()
    {
        int counter = 0;
        float timeElapsed = 0f;

        while (timeElapsed < 4f && counter < counterGoal)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                counter++;
            }

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        if (counter >= counterGoal)
        {
            UnlockPositions();
        }
        else
        {
            Debug.Log("Failed QTE");
        }
    }
}
