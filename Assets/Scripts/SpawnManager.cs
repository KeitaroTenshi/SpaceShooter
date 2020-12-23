using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;
    [SerializeField] private GameObject _enemyContainer;
    [SerializeField] private GameObject[] _powerUps;
    private bool _isPlayerAlive = true;

    public void StartSpawning()
    {
        StartCoroutine(EnemySpawningRoutine(Random.Range(3.0f, 6.0f)));
        StartCoroutine(PowerUpSpawningRoutine(Random.Range(8.0f, 15.0f)));
    }

    IEnumerator EnemySpawningRoutine(float randomSpawnRate)
    {
        yield return new WaitForSeconds(3.0f);
        while (_isPlayerAlive)
        {
            Vector3 randomSpawnLocation = new Vector3(Random.Range(-9.21f, 9.21f), 7.5f, 0);
           GameObject newEnemy = Instantiate(_enemy, randomSpawnLocation, Quaternion.identity);
           newEnemy.transform.parent = _enemyContainer.transform;

            yield return new WaitForSeconds(randomSpawnRate);
        }
    }

    IEnumerator PowerUpSpawningRoutine(float randomSpawnRate)
    {
        yield return new WaitForSeconds(3.0f);
        while (_isPlayerAlive)
        {
            Vector3 randomSpawnLocation = new Vector3(Random.Range(-9.21f, 9.21f), 7.5f, 0);
            Instantiate(_powerUps[Random.Range(0, _powerUps.Length)], randomSpawnLocation, Quaternion.identity); //don't forget to add other powerups after creating 'em
            yield return new WaitForSeconds(randomSpawnRate);
        }
    }

   public void OnPlayerDeath()
    {
        _isPlayerAlive = false;
    }
}
