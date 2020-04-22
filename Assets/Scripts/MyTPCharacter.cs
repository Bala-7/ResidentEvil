using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTPCharacter : MonoBehaviour
{
    private Animator _animator;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }


    public Animator GetAnimator() { return _animator; }
}
