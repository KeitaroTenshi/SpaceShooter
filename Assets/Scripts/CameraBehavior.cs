using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    private float _timeElapsed;
    private float _positionX;
    private float _positionY;
    public IEnumerator CameraShake(float duration, float magnitude)
    {
        Vector3 _position = transform.localPosition;

        while(_timeElapsed < duration)
        {
            _positionX = Random.Range(-1f, 1f) * magnitude;
            _positionY = Random.Range(-1f, 1f) * magnitude;

            transform.position = new Vector3(_positionX, _positionY, _position.z);
            _timeElapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = _position;
        _timeElapsed = 0;
    }
}
