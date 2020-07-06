﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 10.0f;
    [SerializeField] private float _boostedSpeed = 15.0f;
    [SerializeField] private float _fireRate = 0.15f;
    [SerializeField] private int _playerLives = 3;
    private float _nextFire = -1f;
    private Vector3 _cameraPosition;
    [SerializeField] private GameObject _laser;
    [SerializeField] private GameObject _trippleShot;
    [SerializeField] private GameObject _shieldSprite;
    [SerializeField] private GameObject[] _playerDamageSprites;
    [SerializeField] private GameObject _playerExplosionSprite;
    [SerializeField] private AudioClip _laserAudio;
    private bool _isTrippleShotActive;
    private bool _isSpeedBoostActive;
    private bool _isShieldActive;
    private int _score;
    private SpawnManager _spawnManager;
    private UIManager _uiManager;
    void Start()
    {
        _cameraPosition = new Vector3(0, 1, -10);
        _shieldSprite.SetActive(false);
        transform.position = Vector3.zero;
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();

        if (_spawnManager == null)
        {
            Debug.LogError("SpawnManager is NULL");
        }
        if (_uiManager == null)
        {
            Debug.LogError("UIManager is NULL");
        }
    }

    void Update()
    {
        MovementCalculation();
        ShootingLasers();
    }

    void MovementCalculation()
    {
       float _horizontalInput = Input.GetAxis("Horizontal");
       float _verticalInput = Input.GetAxis("Vertical");

        if (_isSpeedBoostActive == true)
        {
            transform.Translate(new Vector3(_horizontalInput, _verticalInput, 0) * _boostedSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(new Vector3(_horizontalInput, _verticalInput, 0) * _speed * Time.deltaTime);
        }

        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
        else if (transform.position.y < -3.12f)
        {
            transform.position = new Vector3(transform.position.x, -3.12f, transform.position.z);
        }

        if (transform.position.x > 11)
        {
            transform.position = new Vector3(-11, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -11)
        {
            transform.position = new Vector3(11, transform.position.y, transform.position.z);
        }
    }

    void ShootingLasers()
    {
            if (Input.GetKeyDown(KeyCode.Space) && _nextFire < Time.time)
            {
            _nextFire = Time.time + _fireRate;
            AudioSource.PlayClipAtPoint(_laserAudio, _cameraPosition);

            if (!_isTrippleShotActive)
            {
                Vector3 laserPos = new Vector3(transform.position.x, transform.position.y + 1.3f, transform.position.z);
                Instantiate(_laser, laserPos, Quaternion.identity);
            }
            else if (_isTrippleShotActive)
            {
                Instantiate(_trippleShot, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            }
            }
    }

    public void PlayerDamage()
    {
        if (_isShieldActive == true)
        {
            _isShieldActive = false;
            _shieldSprite.SetActive(false);
        }
        else
        {
        _playerLives--;
        _uiManager.LivesUpdate(_playerLives);
        if (_playerLives == 2)
            {
                _playerDamageSprites[0].SetActive(true);
            }
        else if (_playerLives == 1)
            {
                _playerDamageSprites[1].SetActive(true);
            }
        }

        if (_playerLives <= 0)
        {
            Instantiate(_playerExplosionSprite, transform.position, Quaternion.identity);
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void TrippleShotActive()
    {
        _isTrippleShotActive = true;
        StartCoroutine(TrippleShotPowerDownRoutine());
    }
    public void SpeeedUpActive()
    {
        _isSpeedBoostActive = true;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }
    public void ShieldActive()
    {
        _isShieldActive = true;
        _shieldSprite.SetActive(true);
    }

    IEnumerator TrippleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTrippleShotActive = false;
    }
    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isSpeedBoostActive = false;
    }
    public void AddingScore(int scoreToAdd)
    {
        _score += scoreToAdd;
        _uiManager.ScoreUpdate(_score);
    }
}
