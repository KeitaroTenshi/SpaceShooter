using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    Animator _camAnim;

    private void Start()
    {
        _camAnim = GetComponent<Animator>();

        if (_camAnim == null)
        {
            Debug.LogError("null component _camAnim::CameraBehaviour");
        }
    }

    public void CameraShake()
    {
        _camAnim.SetTrigger("CameraShake");
    }
}
