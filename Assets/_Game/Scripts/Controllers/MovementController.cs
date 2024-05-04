using System;
using UnityEngine;
using UnityEngine.Serialization;

public class MovementController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _horizontalSpeed = 2f;
    [SerializeField] private bool _canMove;
    [SerializeField] private float _clamp_x;
    private InputManager _inputManager;

    private void Start()
    {
        _inputManager = InputManager.Instance;
    }

    void LateUpdate()
    {
        if (!_canMove) return;
        transform.Translate(Vector3.forward * _moveSpeed * Time.deltaTime);

        float x_value = _inputManager.GetDeltaX() * _horizontalSpeed * Time.deltaTime;
        x_value = Mathf.Clamp(x_value, -_clamp_x, _clamp_x);

        transform.Translate(x_value, 0, 0);
        ClampSideMovement();
    }

    public bool CanMove()
    {
        return _canMove;
    }
    public void StopMovement()
    {
        _canMove = false;
    }
    private void ClampSideMovement()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -_clamp_x, _clamp_x);

        transform.position = pos;
    }
}