using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PlayerController : MonoBehaviour
{
    //[SerializeField] private int health = 5;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpSpeed = 30f;
    [SerializeField] [Range(0.001f, 1f)] private float rotationSmoothness = 0.5f;

    private Rigidbody _rigidbody; 
    private Vector3 _move;
    private Vector3 _jump;
    private float _gravity;
    private float _yVelocity;
    private bool _jumpRequest;

    public bool isImmune = false;
    
    private PlayerFlight playerFlight;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>(); //dont interact in update
        playerFlight = GetComponent<PlayerFlight>();
    }


    void Update()
    {
        float horizontal = 0f;
        float vertical = 0f;
        
        if (Input.GetKey(KeyCode.A)) horizontal = -1f;
        if (Input.GetKey(KeyCode.D)) horizontal = 1f;
        if (Input.GetKey(KeyCode.Space)) vertical = 1f;
        
        _move = new Vector3(0f, 0f, horizontal);
        _jump = new Vector3(0f, vertical, 0f);
        
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded()) //can only jump if player is on ground, stops flying
        {
            _jumpRequest = true;
        }
            
    }

    void FixedUpdate() //interact with rigidbody in fixedupdate
    {
        //jump//
        
        if (_jumpRequest)
        {
            _rigidbody.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
            _jumpRequest = false;
        }
        
        if (_move.sqrMagnitude < 0.001f)
        {
            _rigidbody.linearVelocity = new Vector3(0, _rigidbody.linearVelocity.y, 0); //grav
            return;
        }
        
        Vector3 velocity = _move.normalized * moveSpeed;
        velocity.y = _rigidbody.linearVelocity.y; //no feather falling
        _rigidbody.linearVelocity = velocity;
        
        float rotationAngle = Mathf.Atan2(velocity.x, velocity.z);
        Quaternion currentRotation = _rigidbody.rotation;
        Quaternion targetRotation = Quaternion.Euler(0, rotationAngle * Mathf.Rad2Deg, 0);
        Quaternion newrotation = Quaternion.Lerp(currentRotation, targetRotation, rotationSmoothness) ; 
        
        _rigidbody.MoveRotation(newrotation);
        _rigidbody.angularVelocity = Vector3.zero;

    }
    
    bool IsGrounded() 
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f); //raycast sees if player is on ground
    }
    
    //for flying pickup
    public void ActivateFlight(float duration, float height)
    {
        if (playerFlight != null && !playerFlight.isFlying)
        {
            StartCoroutine(playerFlight.Fly(duration, height));
        }
    }
    
}
