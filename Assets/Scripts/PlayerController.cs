using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI winText;
    public GameObject resetButton; 
    public GameObject winTextObject;
  
    private Rigidbody _rigidBody; 
    private float _movementX;
    private float _movementY;
    private int _count = 0;
    private GameObject[] _allPickUps;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigidBody = GetComponent <Rigidbody>();
        SetCountText();
        winTextObject.SetActive(false);
        resetButton.SetActive(false);
        _allPickUps = GameObject.FindGameObjectsWithTag("PickUp");
        
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
            IncreaseScore();
            if(CheckWinCondition(5))
            {
                winTextObject.SetActive(true);
                resetButton.SetActive(true);
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

    void IncreaseScore()
    {
        _count++;
        SetCountText();
    }

    public void ResetGame()
    {
        _count = 0;
        SetCountText();
        winTextObject.SetActive(false);
        foreach (var pickUp in _allPickUps)
        {
            pickUp.SetActive(true);
        }
        resetButton.SetActive(false);
    }

    void SetCountText()
    {
        countText.text = $"Count {_count}";
    }

    public void OnButtonClick()
    {
        ResetGame();
    }
}
