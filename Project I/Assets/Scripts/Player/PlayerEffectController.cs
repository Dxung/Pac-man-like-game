using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffectController : MonoBehaviour
{
    [SerializeField] private PlayerStateController _playerStateController;
    [SerializeField] private AudioManager _playerAudioManager;
    [SerializeField] private bool _soundPlayed;

    [SerializeField] private AudioManager[] _soundToOffWhenPlayerDie;
    

    private void Start()
    {
        _playerStateController = this.transform.parent.gameObject.GetComponentInChildren<PlayerStateController>();
        _playerAudioManager = this.gameObject.GetComponent<AudioManager>();
        _soundPlayed = false;
    }

    private void Update()
    {
        UpdateSoundEffect();
    }
    private void UpdateSoundEffect()
    {
        if (_playerStateController.CheckCurrentState(PlayerState.normal))
        {
            if (_soundPlayed)
            {
                _soundPlayed = false;
            }
        }
        else if (_playerStateController.CheckCurrentState(PlayerState.dead))
        {
            if (!_soundPlayed)
            {
                PlayDeadSound();
                OffSoundWhenPlayerDie();
                _soundPlayed = true;
            }
            
        }
    }
    private void PlayDeadSound()
    {
        if (!_soundPlayed)
        {
            _playerAudioManager.Play("died");
            _soundPlayed = true;
        }
    }

    private void OffSoundWhenPlayerDie()
    {
        foreach(AudioManager audio in _soundToOffWhenPlayerDie)
        {
            audio.StopAll();
        }
     
    }
}
