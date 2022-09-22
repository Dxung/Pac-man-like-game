using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelletColliderController : MonoBehaviour
{
    [Header("Pellet Particle System")]
    protected ParticleSystem _pelletParticleSystem;
    protected float _particleSystemDuration;

    [Header("other references to use when particle effect happens")]
    protected Collider _pelletTrigger;
    protected MeshRenderer _pelletMesh;
    protected Light _pelletLight;

    [Header("State Controller")]
    [SerializeField] protected StateForPellets _StateControllerForPellet;


    protected void Awake()
    {
        AddStateForPelletFunction();

        _pelletParticleSystem = this.transform.parent.GetComponentInChildren<ParticleSystem>();
        _pelletMesh = this.transform.parent.GetComponent<MeshRenderer>();
        _pelletTrigger = this.GetComponent<Collider>();
        _pelletLight = this.transform.parent.GetComponentInChildren<Light>();

        ParticleSystemDuration();


    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //change player state
            ChangeplayerState();

            ChangePelletStatus();


        }
    }

    //change player state to consume (if not in powerup)
    protected virtual void ChangeplayerState()
    {
        PlayerStateController playerStateControl = _StateControllerForPellet.GetPlayerStateController();
        playerStateControl.TurnToConsumeState();
    }

    protected void AddStateForPelletFunction()
    {
        GameObject StateControllerForPellet = GameObject.Find("State for pellets");
        _StateControllerForPellet = StateControllerForPellet.GetComponent<StateForPellets>();
    }

    //use when pellet be collided
    protected void ChangePelletStatus()
    {
        _pelletMesh.enabled = false;
        _pelletLight.enabled = false;
        _pelletTrigger.enabled = false;

        _pelletParticleSystem.Play();

        Destroy(this.transform.parent.gameObject, _particleSystemDuration);
    }

    protected void ParticleSystemDuration()
    {
        _particleSystemDuration = _pelletParticleSystem.main.duration + _pelletParticleSystem.main.startLifetime.constant;
    }
}
