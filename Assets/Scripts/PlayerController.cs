using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public GameObject loseTextObject;

	private Rigidbody rb;
    private int count;
	private float movementX;
	private float movementY;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
    }

    void OnMove(InputValue movementValue) 
    {
    	Vector2 movementVector = movementValue.Get<Vector2>();
    	movementX = movementVector.x;
    	movementY = movementVector.y;
    }

    void FixedUpdate()
    {
    	Vector3 movement = new Vector3(movementX, 0.0f, movementY);
    	rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp")) 
        {
            rb.gameObject.transform.localScale += new Vector3(0.2f, 0.2f, 0.2f);
            other.gameObject.SetActive(false);
            count += 1;
            SetCountText();
        }
        if (other.gameObject.CompareTag("Bomb")) 
        {
            winTextObject.SetActive(false);
            loseTextObject.SetActive(true);
        }
    }

        void SetCountText()
    {
        countText.text = "Count: " + count.ToString() + "/5";
        if (count >= 5) 
        {
            loseTextObject.SetActive(false);
            winTextObject.SetActive(true);
        }
    }
}
