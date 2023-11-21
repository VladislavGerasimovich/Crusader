using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game: MonoBehaviour
{
    [SerializeField] private GameOverScreen _gameOverScreen;
    [SerializeField] private Player _player;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private LevelScreen _levelScreen;
    [SerializeField] private HealthBar _healthBar;

    public void OnGameOver()
    {
        Time.timeScale = 0;
        _gameOverScreen.Open();
    }

    private void OnEnable()
    {
        _gameOverScreen.RestartButtonClick += OnRestartButtonClick;
        _player.GameOver += OnGameOver;
        _spawner.GameOver += OnGameOver;
    }

    private void OnDisable()
    {
        _gameOverScreen.RestartButtonClick -= OnRestartButtonClick;
        _player.GameOver -= OnGameOver;
        _spawner.GameOver -= OnGameOver;
    }

    private void OnRestartButtonClick()
    {
        _gameOverScreen.Close();
        StartGame();
    }

    private void StartGame()
    {
        Time.timeScale = 1.0f;
        _spawner.Reset();
        _healthBar.Reset();
        _player.Reset();
        _levelScreen.Reset();
    }
}
