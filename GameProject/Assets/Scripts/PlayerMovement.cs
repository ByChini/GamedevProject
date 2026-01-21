using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public GameObject playerPrefab;

    public Rigidbody rb;

    public InputActionReference move;
    public float moveSpeed;
    private Vector2 _moveDirection;

    public InputActionReference look;
    public float cameraSensibilty;
    private Vector2 _lookDirection;

    public InputActionReference jump;
    public float _jumpStrengh;

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
        Vector3 currentVelocity = rb.linearVelocity;
        
        _moveDirection = move.action.ReadValue<Vector2>();
        Vector3 horizontalVelocity =             
            _moveDirection.y * moveSpeed * transform.forward * Time.deltaTime +
            _moveDirection.x * moveSpeed * transform.right * Time.deltaTime;

        rb.linearVelocity = new Vector3(
            horizontalVelocity.x,
            currentVelocity.y,
            horizontalVelocity.z
        );


    }

    void OnJump()
    {
        rb.linearVelocity = new Vector3 (rb.linearVelocity.x,
                                        _jumpStrengh,
                                        rb.linearVelocity.z);
    }
}