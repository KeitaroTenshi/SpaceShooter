using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 5.0f;
    [SerializeField] private GameObject _explosion;
    SpawnManager _spawnManager;
    void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    void Update()
    {
        transform.Rotate(Vector3.forward * _rotationSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            Instantiate(_explosion, transform.position, Quaternion.identity);
            _spawnManager.StartSpawning();
            Destroy(this.gameObject, 0.25f);

        }
    }
}
