using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpHeight = 5.0f;
    public float gravity = -30f;
    public float groundCheckDistance = 0.1f;

    public int carrotsCollected = 0; // Dodane

    private CharacterController _characterController;
    private Vector3 _velocity;
    private bool _isGrounded;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        if (_characterController == null)
        {
            _characterController = gameObject.AddComponent<CharacterController>();
        }
    }

    private void Update()
    {
        _isGrounded = CheckIfGrounded();

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        if (_isGrounded)
        {
            _velocity.y = 0f;

            if (Input.GetButtonDown("Jump"))
            {
                _velocity.y += Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }

        _velocity.y += gravity * Time.deltaTime;
        _characterController.Move((movement * speed + _velocity) * Time.deltaTime);
    }

    private bool CheckIfGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, _characterController.bounds.extents.y + groundCheckDistance);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            Debug.Log("Gracz zgin¹³!");
        }
        else if (other.gameObject.CompareTag("Carrot")) // Dodane
        {
            AddCarrot();
            Destroy(other.gameObject);
        }
    }

    public void AddCarrot() // Dodane
    {
        carrotsCollected++;
        // Aktualizuj interfejs u¿ytkownika
    }
}




