using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyTypeOneTemplate;
    [SerializeField] private GameObject _enemyTypeTwoTemplate;
    [SerializeField] private GameObject _enemyTypeThreeTemplate;
    [SerializeField] private GameObject _enemyTypeFourTemplate;
    [SerializeField] private GameObject _enemyTypeFiveTemplate;
    [SerializeField] private GameObject _boss;
    [SerializeField] private Transform _enemyContainer;
    [SerializeField] private Transform[] _spawnerPoints;

    private const string CommandEnemyTypeOne = "EnemyTypeOne";
    private const string CommandEnemyTypeTwo = "EnemyTypeTwo";
    private const string CommandEnemyTypeThree = "EnemyTypeThree";
    private const string CommandEnemyTypeFour = "EnemyTypeFour";
    private const string CommandEnemyTypeFive = "EnemyTypeFive";
    private const string CommandBoss = "Boss";

    private int _currentWaveNumber;
    private List<string> _currentWave;
    private int _currentEnemy;
    private List<string> _waveOne = new List<string>() { "EnemyTypeOne" };
    private List<string> _waveTwo = new List<string>() { "EnemyTypeTwo" };
    private List<string> _waveThree = new List<string>() { "EnemyTypeThree" };
    private List<string> _waveFour = new List<string>() { "EnemyTypeFour" };
    private List<string> _waveFive = new List<string>() { "EnemyTypeFive" };
    private List<string> _waveSix = new List<string>() { "Boss" };
    private List<List<string>> _waves;
    private float _timeToInfo = 3;

    public event UnityAction OpenScreen;
    public event UnityAction CloseScreen;
    public event UnityAction GameOver;

    public void Reset()
    {
        for (int i = 0; i < _enemyContainer.childCount; i++)
        {
            Destroy(_enemyContainer.GetChild(i).gameObject);
        }

        _currentWaveNumber = 0;
        _currentEnemy = 0;
        SetWave(_currentWaveNumber);
    }

    private void SetWave(int index)
    {
        _currentWave = _waves[index];
    }

    private void WorkWithWawe(int index)
    {
        switch(_currentWave[index])
        {
            case CommandEnemyTypeOne:
                CreateEnemy(_enemyTypeOneTemplate, _currentWave[index]);
                break;
            case CommandEnemyTypeTwo:
                CreateEnemy(_enemyTypeTwoTemplate, _currentWave[index]);
                break;
            case CommandEnemyTypeThree:
                CreateEnemy(_enemyTypeThreeTemplate, _currentWave[index]);
                break;
            case CommandEnemyTypeFour:
                CreateEnemy(_enemyTypeFourTemplate, _currentWave[index]);
                break;
            case CommandEnemyTypeFive:
                CreateEnemy(_enemyTypeFiveTemplate, _currentWave[index]);
                break;
            case CommandBoss:
                CreateEnemy(_boss, _currentWave[index]);
                break;
        }
    }

    private void CreateEnemy(GameObject enemy, string text)
    {
        foreach (var point in _spawnerPoints)
        {
            if (point.tag == text)
            {
                Instantiate(enemy, point.position, Quaternion.identity, _enemyContainer);
            }
        }
    }

    private void Start()
    {
        _waves = new List<List<string>>() { _waveOne, _waveTwo, _waveThree, _waveFour, _waveFive, _waveSix };
        SetWave(_currentWaveNumber);
    }

    private void Update()
    {
        if(_currentWave == null)
        {
            GameOver?.Invoke();
            return;
        }

        if(_enemyContainer.transform.childCount == 0 && _currentEnemy == 0)
        {
            OpenScreen?.Invoke();
            _timeToInfo -= Time.deltaTime;

            if (_timeToInfo < 0)
            {
                _timeToInfo = 3;
                CloseScreen?.Invoke();
                WorkWithWawe(_currentEnemy);
                _currentEnemy++;
            }
        }
        else if (_enemyContainer.transform.childCount == 0 && _currentEnemy > 0)
        {
            if (_currentEnemy < _currentWave.Count)
            {
                WorkWithWawe(_currentEnemy);
                _currentEnemy++;
            }
            else
            {
                _currentWaveNumber++;
                _currentEnemy = 0;

                if (_currentWaveNumber < _waves.Count)
                {
                    SetWave(_currentWaveNumber);
                }
                else
                {
                    _currentWave = null;
                }
            }
        }
    }
}
