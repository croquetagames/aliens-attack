using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip enemyHitSfx;

    private AudioSource _audioSource;

    public void EnemyHitPlay()
    {
        _audioSource.PlayOneShot(enemyHitSfx);
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
}