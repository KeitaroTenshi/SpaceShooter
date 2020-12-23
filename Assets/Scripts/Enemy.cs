using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _enemySpeed = 5.0f;
    Animator animator;
    Player player;
    BoxCollider2D enemyCollider;
    [SerializeField] private AudioClip _enemyExplosionSound;
    [SerializeField] private GameObject _enemyLaserPrefab;
    private bool _isEnemyDead;
    private float _nextFire = -1.0f;
    Vector3 _cameraPosition;

    private void Start()
    {
        _isEnemyDead = false;
        _cameraPosition = new Vector3(0, 1, -10);
        player = GameObject.Find("Player").GetComponent<Player>();
        animator = gameObject.GetComponent<Animator>();
        enemyCollider = gameObject.GetComponent<BoxCollider2D>();

        if (player == null)
        {
            Debug.LogError("player is NULL");
        }
        if (animator == null)
        {
            Debug.LogError("animator is NULL");
        }
        if (enemyCollider == null)
        {
            Debug.LogError("enemyCollider is NULL");
        }
    }
    void Update()
    {
        MovementCalculation();
        ShootingCalculation(Random.Range(3.0f, 7.0f));
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            if (player != null)
            {
                player.AddingScore(10);
                Destroy(other.gameObject);
                _enemySpeed = 0;
                animator.SetTrigger("OnEnemyDeath");
                AudioSource.PlayClipAtPoint(_enemyExplosionSound, _cameraPosition);
                enemyCollider.enabled = false;
                _isEnemyDead = true;
                Destroy(this.gameObject, 2f);
            }
        }
        else if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.PlayerDamage();
            }
            _enemySpeed = 0;
            animator.SetTrigger("OnEnemyDeath");
            AudioSource.PlayClipAtPoint(_enemyExplosionSound, _cameraPosition);
            enemyCollider.enabled = false;
            _isEnemyDead = true;
            Destroy(this.gameObject, 1f);
        }
    }
    void MovementCalculation()
    {
        transform.Translate(Vector3.down * _enemySpeed * Time.deltaTime);

        if (transform.position.y <= -5.4f)
        {
            float randomPos = Random.Range(-9.5f, 9.5f);
            transform.position = new Vector3(randomPos, 7.3f, transform.position.z);
        }
    }
    void ShootingCalculation(float _fireRate)
    {
        if (Time.time > _nextFire && _isEnemyDead == false)
        {
            _nextFire = Time.time + _fireRate;
            GameObject enemyLaser = Instantiate(_enemyLaserPrefab, transform.position, Quaternion.identity);
            Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();
            
            for (int i = 0; i < lasers.Length; i++)
            {
                lasers[i].AssignEnemyLaser();
            }

        }
    }

}
