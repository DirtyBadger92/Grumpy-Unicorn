using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpHeight = 5.0f;
    public float gravity = -30f;
    public float groundCheckDistance = 0.1f;
    public float minMoveDistance = 0.1f;

    public int carrotsCollected = 0;

    private CharacterController _characterController;
    private Vector3 _velocity;
    private bool _isGrounded;
    private Vector3 _targetPosition;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        if (_characterController == null)
        {
            _characterController = gameObject.AddComponent<CharacterController>();
        }
        _targetPosition = transform.position;
    }

    private void Update()
    {
        _isGrounded = CheckIfGrounded();
        if (_isGrounded)
        {
            _velocity.y = 0f;
        }

        _velocity.y += gravity * Time.deltaTime;

        if (Vector3.Distance(transform.position, _targetPosition) > minMoveDistance)
        {
            Vector3 direction = (_targetPosition - transform.position).normalized;
            _characterController.Move((direction * speed + _velocity) * Time.deltaTime);
        }

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                _targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            }
        }
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
        else if (other.gameObject.CompareTag("Carrot"))
        {
            AddCarrot();
            Destroy(other.gameObject);
        }
    }

    public void AddCarrot()
    {
        carrotsCollected++;
        // Aktualizuj interfejs u¿ytkownika
    }
}
