using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _minWidth;
    [SerializeField] private float _maxWidth;
    [SerializeField] private float _minHeight;
    [SerializeField] private float _maxHeight;

    private float _moveHorizontal;
    private float _moveVertical;
    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _moveHorizontal = Input.GetAxis("Horizontal");
        _moveVertical = Input.GetAxis("Vertical");
        _rigidbody.velocity = new Vector3(_moveHorizontal, _moveVertical, 0) * _speed;
        _rigidbody.position = new Vector3(
            Mathf.Clamp(_rigidbody.position.x, _minWidth, _maxWidth),
            Mathf.Clamp(_rigidbody.position.y, _minHeight, _maxHeight),
            0.0f
        );
    }
}
