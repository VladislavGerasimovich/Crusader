using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyTypeTwo : Enemy
{
    [SerializeField] private float _speed;
    [SerializeField] private Animator _animator;
    [SerializeField] private AnimationClip _animationClip;
    [SerializeField] private Transform _laserRay;

    private GameObject _player;
    private Vector3 _endPosition;
    private Vector3 _direction;
    private Quaternion _toRotation;
    private float _observationTime = 3.5f;

    private void Start()
    {
        _player = GameObject.Find("Player");
        _endPosition = new Vector3() { x = Random.Range(-1.5f, 1.5f), y = Random.Range(1, 0), z = 0 };
        MountDirection(_endPosition);

        StartCoroutine(MainCoroutine());
    }

    private void MountDirection(Vector3 endPoint)
    {
        _direction = (endPoint - transform.position).normalized;
        _toRotation = Quaternion.LookRotation(Vector3.forward, _direction);
    }

    private IEnumerator MainCoroutine()
    {
        yield return StartCoroutine(Move());

        while (true)
        {
            yield return StartCoroutine(Aim());
            yield return StartCoroutine(Shoot());
            yield return null;
        }
    }

    private IEnumerator Move()
    {
        while (transform.position != _endPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, _endPosition, _speed * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _toRotation, 300 * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator Aim()
    {
        _observationTime = 3.5f;

        while (_observationTime > 0)
        {
            _observationTime -= Time.deltaTime;
            MountDirection(_player.transform.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _toRotation, 300 * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator Shoot()
    {
        float delay = _animationClip.length;
        _animator.SetTrigger("IncreaseScaleTrigger");

        while(delay >= 0)
        {
            delay -= Time.deltaTime;
            yield return null;
        }
    }
}
