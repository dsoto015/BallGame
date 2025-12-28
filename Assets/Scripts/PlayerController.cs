using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidBody; 
    private float _movementX;
    private float _movementY;
    public float speed = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigidBody = GetComponent <Rigidbody>();
    }

    private void FixedUpdate() 
    {
        Vector3 movement = new Vector3 (_movementX, 0.0f, _movementY);
        var velocity = movement * speed;
        _rigidBody.AddForce(velocity); 

    }

    void OnMove (InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>(); 
        _movementX = movementVector.x; 
        _movementY = movementVector.y; 
    }
}
