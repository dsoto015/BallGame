using TMPro;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI winText;
    public GameObject winTextObject;

    private Rigidbody _rigidBody; 
    private float _movementX;
    private float _movementY;
    private int _count = 0;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigidBody = GetComponent <Rigidbody>();
        SetCountText();
        winTextObject.SetActive(false);
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

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);    
            _count++;
            SetCountText();
            if(CheckWinCondition(5))
            {
                winTextObject.SetActive(true);
            }
        }
    }

    bool CheckWinCondition(int pickUpCount)
    {
        if(_count < pickUpCount)
        {
            return false;
        }
        return true;
    }

    void SetCountText()
    {
        countText.text = $"Count {_count}";
    }
}
