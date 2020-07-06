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
    private GameManager gameManager;

    private void Start()
    {
        _gameOverText.gameObject.SetActive(false);
        _restartGameText.gameObject.SetActive(false);
        _scoreText.text = "Score: " + 0;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (gameManager == null)
        {
            Debug.LogError("GameManager is Null. UIManager script");
        }
    }
    public void ScoreUpdate(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore;
    }
    public void LivesUpdate(int currentLives)
    {
        _livesImage.sprite = _liveSprites[currentLives];

        if (currentLives <= 0)
        {
            GameOverSequence();
            gameManager.GameOver();
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
