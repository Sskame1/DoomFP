using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DoorUse : MonoBehaviour
{
    private Transform _gameObj;
    private BoxCollider _objectCollider;
    private Vector3 _targetPosition;

    public float moveSpeed = 1.0f;

    private bool OpenDoor = false;
    
    void Start()
    {
        _gameObj = GetComponent<Transform>();
        _objectCollider = GetComponent<BoxCollider>();
        _objectCollider.isTrigger = true;

        _targetPosition = new Vector3(_gameObj.transform.position.x, _gameObj.transform.position.y + 5, _gameObj.transform.position.z);
    }

    private void Update() {
        if(OpenDoor) {
            _gameObj.transform.position = Vector3.Lerp(_gameObj.transform.position, _targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("player")) {
            OpenDoor = true;
        }
    }
}
