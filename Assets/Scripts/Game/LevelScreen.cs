using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class LevelScreen : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private TMP_Text _info;
    [SerializeField] protected CanvasGroup CanvasGroup;

    private int _level = 1;
    private int _installedLevel = 1;

    public void Reset()
    {
        _level = _installedLevel;
    }

    private void OnEnable()
    {
        _spawner.OpenScreen += Open;
        _spawner.CloseScreen += Close;
    }

    private void OnDisable()
    {
        _spawner.OpenScreen -= Open;
        _spawner.CloseScreen -= Close;
    }

    private void Open()
    {
        _info.text = $"level {_level}";
        CanvasGroup.alpha = 1;
    }

    private void Close()
    {
        _level++;
        CanvasGroup.alpha = 0;
    }
}
