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
    private float _gravity;
    private float _yVelocity;
    public static int Health = 5;
    private bool _jumpRequest;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>(); //dont interact in update
    }


    void Update()
    {
        _move = new Vector3(
            Input.GetAxis("Vertical"),
            Input.GetAxis("Jump"),  //maybe back to zero
            Input.GetAxis("Horizontal")
        );
        
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded()) //can only jump if player is on ground, stops flying
        {
            _jumpRequest = true;
        }
            
    }

    void FixedUpdate() //interact with rigidbody in fixedupdate
    {
        /*//ySpeed += Physics.gravity.y * Time.fixedDeltaTime;
        if (Input.GetKey(KeyCode.Space))
        {
            _ySpeed = jumpSpeed;
        }*/
        
        if (_move.magnitude < 0.01f)
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
            
        
        //jump//
        
        if (_jumpRequest)
        {
            _rigidbody.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
            _jumpRequest = false;
        }
        
    }
    
    bool IsGrounded() 
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f); //raycast sees if player is on ground
    }
}
