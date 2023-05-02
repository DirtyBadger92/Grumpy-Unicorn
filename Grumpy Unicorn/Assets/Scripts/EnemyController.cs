using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 2.0f;
    public float distance = 5.0f;

    private CharacterController _characterController;
    private Vector3 _startPosition;
    private Vector3 _destination;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        if (_characterController == null)
        {
            _characterController = gameObject.AddComponent<CharacterController>();
        }

        _startPosition = transform.position;
        _destination = _startPosition + new Vector3(distance, 0, 0);
    }

    private void Update()
    {
        MoveTowardsDestination();
    }

    private void MoveTowardsDestination()
    {
        Vector3 direction = (_destination - transform.position).normalized;
        float step = speed * Time.deltaTime;
        Vector3 movement = direction * step;
        _characterController.Move(movement);

        if (Vector3.Distance(transform.position, _destination) < 0.001f)
        {
            _destination = _destination == _startPosition ? _startPosition + new Vector3(distance, 0, 0) : _startPosition;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(other.gameObject);
            Debug.Log("Gracz zgin¹³!");
        }
    }
}

