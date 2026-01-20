using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public GameObject myPrefab;

    public Rigidbody rb;

    public float moveSpeed;

    public InputActionReference move;

    private Vector2 _moveDirection;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        _moveDirection = move.action.ReadValue<Vector2>();

        rb.linearVelocity = new Vector3(_moveDirection.x * moveSpeed,
            rb.linearVelocity.y,
            _moveDirection.y * moveSpeed
        );

    }

}