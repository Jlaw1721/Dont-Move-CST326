using System.Collections;
using UnityEngine;

public class GrappleScript : MonoBehaviour
{
    public Transform monster;
    public Animator monsterAnimator;
    public Transform player;
    public GameObject grappleUI;
    [SerializeField] private float timeLimit = 4f;
    [SerializeField] private float timeLostPerEncounter = 0.5f;
    [SerializeField] private int counterGoal = 5;
    [HideInInspector]public Collider triggerCollider;
    private MonsterMovement _monsterMovement;
    private bool hasExecuted = false;
    private static GrappleScript _instance;
    public static GrappleScript Instance => _instance;
    private void Awake()
    {
        if (_instance == null)
            _instance = this;
    }

    private void Start()
    {
        _monsterMovement = monster.GetComponent<MonsterMovement>();
        triggerCollider = gameObject.GetComponent<Collider>();
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
        monsterAnimator.SetBool("Attack", true);
        
        PlayerCamController.Instance.currentOrientation.LookAt(monster);
        PlayerMovement.Instance.canMove = false;
        PlayerCamController.Instance.canMoveCamera = false;
        _monsterMovement.agent.isStopped = true;
    }

    private void UnlockPositions()
    {
        monsterAnimator.SetBool("Attack", false);
        PlayerMovement.Instance.canMove = true;
        PlayerCamController.Instance.canMoveCamera = true;
        
        StartCoroutine(DisableTrigger());
    }
    
    private IEnumerator DisableTrigger()
    {
        triggerCollider.enabled = false;
        _monsterMovement.TriggerStun(5f);
        
        yield return new WaitForSeconds(5f);
        
        triggerCollider.enabled = true;
    }

    private IEnumerator Grapple()
    {
        hasExecuted = false;
        grappleUI.SetActive(true);
        int counter = 0;
        float timeElapsed = 0f;

        while (timeElapsed < timeLimit && counter < counterGoal)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                counter++;
            }

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        if (!hasExecuted)
        {

            if (counter >= counterGoal)
            {
                grappleUI.SetActive(false);
                UnlockPositions();
                if (timeLimit > 2f)
                    timeLimit -= timeLostPerEncounter;
            }
            else
            {
                StopCoroutine(Grapple());
                grappleUI.SetActive(false);
                GameOver.Instance.EndGame();
            }

            hasExecuted = true;
        }
    }
}
