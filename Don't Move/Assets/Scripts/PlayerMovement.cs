using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private Transform currentOrientation;      
    [SerializeField] private LayerMask groundLayer;
    private Rigidbody _rb;
    private static PlayerMovement _instance;
    public static PlayerMovement Instance => _instance;
    
    [Header("Walking")] 
    [SerializeField] private bool canMove;                      // This is only here if we want to disable player movement during any interactions, maybe when the monster attacks and there's a grapple or something
    [SerializeField] private float acceleration;
    [SerializeField] private float maxSpeed;
    private Vector2 _playerInput;
    private Vector3 _direction;
    private Vector3 _groundedVelocity;
    
    [SerializeField] private float playerHeight;
    [SerializeField] private float walkingDrag;
    [SerializeField] private float aerialDrag;                  // I'm not sure if we want jumping to be an option for the player, let me know and i'll either expand upon what I have or get rid of this (same thing with sprinting)
    [SerializeField] private float groundCheckRayCastLength;
    private bool _isGrounded;
    
    private void Awake()
    {
        // Singleton declaration because there should never be more than one of this script in the scene at any given time
        // DontDestroyOnLoad will also keep the player around when switching scenes, so making levels connect to each other should be pretty easy
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // I decided to hardcode the player's rigidbody options so they don't get messed on other people's machines somehow
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
        _rb.interpolation = RigidbodyInterpolation.Interpolate;
        _rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    private void Update()
    {
        GroundedVelocityClamp();
        
        GroundCheck();
        
        // Gathering player input - I think it would be better idea to have a script in charge of gathering all the player inputs that we can just reference whenever we need, but I didn't want to start that without knowing how you plan on tackling picking up and moving items
        if (!canMove) return;
        _playerInput.x = Input.GetAxisRaw("Horizontal");
        _playerInput.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        Walk();
    }

    private void Walk()
    {
        // Calculating new direction to move in
        _direction = currentOrientation.forward * _playerInput.y + currentOrientation.right * _playerInput.x;
        
        _rb.AddForce(_direction.normalized * (maxSpeed * acceleration), ForceMode.Force);
    }

    private void GroundedVelocityClamp()
    {
        // Storing grounded velocity
        var velocity = _rb.velocity;
        _groundedVelocity = new Vector3(velocity.x, 0f, velocity.z);

        // Clamping the player's x and z velocity to maxSpeed
        if (!(_groundedVelocity.magnitude > maxSpeed)) return;
        var clampedVelocity = _groundedVelocity.normalized * maxSpeed;
        
        // Setting a new velocity as maxSpeed in the proper direction
        _rb.velocity = new Vector3(clampedVelocity.x, _rb.velocity.y, clampedVelocity.z);
    }

    private void GroundCheck()
    {
        // Casting a ray from the player's feet to see if they are grounded or not
        _isGrounded = Physics.Raycast(transform.position, Vector3.down, (playerHeight / 2) + groundCheckRayCastLength, groundLayer);

        // If the player is on the ground the drag is set to walkingDrag, otherwise its set to aerialDrag
        _rb.drag = !_isGrounded ? aerialDrag : walkingDrag;
    }
}
