using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    [SerializeField] private Sprite[] _liveSprites;
    [SerializeField] private Image _livesImage;
    [SerializeField] private Text _gameOverText;
    [SerializeField] private Text _restartGameText;
    [SerializeField] private Text _ammoCountText;
    private GameManager _gameManager;
    private Player _player;

    private void Start()
    {
        _gameOverText.gameObject.SetActive(false);
        _restartGameText.gameObject.SetActive(false);
        _scoreText.text = "Score: " + 0;
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _player = GameObject.Find("Player").GetComponent<Player>();

        if (_gameManager == null)
        {
            Debug.LogError("GameManager is NULL");
        }
        if (_player == null)
        {
            Debug.LogError("Player is NULL");
        }
    }
    public void ScoreUpdate(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore;
    }
    public void AmmoCountUpdate(int ammoLeft)
    {
        _ammoCountText.text = "Ammo: " + ammoLeft;
    }

    public void LivesUpdate(int currentLives)
    {
        _livesImage.sprite = _liveSprites[currentLives];

        if (currentLives <= 0)
        {
            GameOverSequence();
            _gameManager.GameOver();
        }
    }
    
    void GameOverSequence()
    {
        _gameOverText.gameObject.SetActive(true);
        _restartGameText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlickering());
    }
    IEnumerator GameOverFlickering()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            _gameOverText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            _gameOverText.gameObject.SetActive(true);
        }
    }
}
