using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class Parallax : MonoBehaviour
{
    [SerializeField] private float _speed;

    private RawImage _image;
    private float _imagePositionY;

    private void Start()
    {
        _image = GetComponent<RawImage>();
    }

    private void Update()
    {
        _imagePositionY += _speed * Time.deltaTime;
        _image.uvRect = new Rect(0, _imagePositionY, _image.uvRect.width, _image.uvRect.height);
    }
}
