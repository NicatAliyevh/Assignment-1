using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballMovement : MonoBehaviour
{
    private bool reversedControl = false;
    public float jumpForce = 5f; 
    private bool isGrounded = true; 
    private Rigidbody rb;   
    public float speed = 5f; 

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");

        Vector3 movement = reversedControl ? new Vector3(-Horizontal, 0, -Vertical) * speed : new Vector3(Horizontal, 0, Vertical) * speed;

        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); 
        isGrounded = false; 
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        else
        {
            switch(collision.gameObject.name){
                case "Red":
                    Destroy(collision.gameObject);
                    Debug.Log("Cube touched the Red ball! Speed has decreased!");
                    speed = 3f;
                    GetComponent<Renderer>().material.color = Color.red;
                    break;
                case "Green":
                    Destroy(collision.gameObject);
                    Debug.Log("Cube touched the green ball! Speed has increased!");
                    speed = 15f;
                    GetComponent<Renderer>().material.color = Color.green;
                    break;
                case "Blue":
                    Destroy(collision.gameObject);
                    Debug.Log("Cube touched the blue ball! Jump height has increased!");
                    jumpForce = 15f;
                    GetComponent<Renderer>().material.color = Color.blue;
                    break;
                case "Black":
                    Destroy(collision.gameObject);
                    Debug.Log("Cube touched the black ball! Jump skill has been disabled!");
                    jumpForce = 0f;
                    GetComponent<Renderer>().material.color = Color.black;
                    break;
                case "Yellow":
                    Destroy(collision.gameObject);
                    Debug.Log("Cube touched the yellow ball! Controls have been reversed!");
                    reversedControl = true;
                    GetComponent<Renderer>().material.color = Color.yellow;
                    break;
                case "White":
                    Rigidbody otherRb = collision.rigidbody;
                    Debug.Log("Cube touched the white ball!");
                    Vector3 randomDirection = new Vector3(
                        Random.Range(-1f, 1f),  
                        Random.Range(-1f, 1f),  
                        Random.Range(-1f, 1f)   
                    ).normalized;
                    float randomForce = Random.Range(5f, 15f);
                    otherRb.AddForce(randomForce * randomDirection, ForceMode.Impulse);
                    GetComponent<Renderer>().material.color = Color.white;
                    break;
            }
        }
    }
}
