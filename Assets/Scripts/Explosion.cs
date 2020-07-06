using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private Vector3 _cameraPosition;
   [SerializeField] private AudioClip _explosionAudio;
    void Start()
    {
        _cameraPosition = new Vector3(0, 1, -10);
        AudioSource.PlayClipAtPoint(_explosionAudio, _cameraPosition);
        Destroy(this.gameObject, 3.0f);
    }
}
