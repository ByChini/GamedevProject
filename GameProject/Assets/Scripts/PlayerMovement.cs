using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public GameObject playerPrefab;

    public CharacterController controller;

    public InputActionReference move;
    public float moveSpeed;
    private Vector2 _moveDirection;

    public InputActionReference look;
    public float cameraSensibilty;
    private Vector2 _lookDirection;

    public InputActionReference jump;
    public float jumpStrengh;

    private Vector3 velocity;
    public float gravity;
    public float friction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created      
    void Start()
    {
        
    }

    // Update is called once per frame      
    void Update()
    {
        // Rotation
        Vector2 lookDirectionChange = look.action.ReadValue<Vector2>();
        _lookDirection.x -= lookDirectionChange.y * cameraSensibilty;
        _lookDirection.y += lookDirectionChange.x * cameraSensibilty;
        _lookDirection.x = Mathf.Clamp(_lookDirection.x, -90, 90);
        playerPrefab.transform.rotation = Quaternion.Euler(_lookDirection.x, _lookDirection.y, 0);

        // WASD
        // Read Input

        _moveDirection = move.action.ReadValue<Vector2>();             
        Vector3 horizontalVelocityChange =  
            _moveDirection.y * moveSpeed * transform.forward +
            _moveDirection.x * moveSpeed * transform.right;

        float verticalVelocityChange = 0;

        // Apply movement
        velocity = new Vector3(
            Mathf.Clamp(velocity.x + horizontalVelocityChange.x, -moveSpeed, moveSpeed),
            Mathf.Clamp(velocity.y + verticalVelocityChange, gravity, jumpStrengh),
            Mathf.Clamp(velocity.z + horizontalVelocityChange.z, -moveSpeed, moveSpeed)
        );

        controller.Move(velocity * Time.deltaTime);

        // Apply Physics
        if (controller.isGrounded) 
        {
            velocity.x *= friction * Time.deltaTime;
            velocity.z *= friction * Time.deltaTime;   
        } else
        {
            velocity.y += gravity * Time.deltaTime;     
        }
    }

    bool _canJump()
    {
        bool _isGrounded = controller.isGrounded;
        return _isGrounded;
    }
    void OnJump()
    {
        if (_canJump())
        {
            velocity.y = jumpStrengh;
        }

    }
}