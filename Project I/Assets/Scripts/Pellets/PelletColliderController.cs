using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelletColliderController : MonoBehaviour
{
    [Header("Pellet Particle System")]
    private ParticleSystem _pelletParticleSystem;
    private float _particleSystemDuration;

    [Header("other references to use when particle effect happens")]
    private Collider _pelletTrigger;
    private MeshRenderer _pelletMesh;
    private Light _pelletLight;


    private void Awake()
    {

        _pelletParticleSystem = this.transform.parent.GetComponentInChildren<ParticleSystem>();
        _pelletMesh = this.transform.parent.GetComponent<MeshRenderer>();
        _pelletTrigger = this.GetComponent<Collider>();
        _pelletLight = this.transform.parent.GetComponentInChildren<Light>();

        //get the time that particle system complete all circle
        _particleSystemDuration = _pelletParticleSystem.main.duration + _pelletParticleSystem.main.startLifetime.constant;


    }
    //this script will work when pellets be touched by player
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //turn off mesh, so you will not see the pellet remains on screen when particles come out
            _pelletMesh.enabled = false;

            //turn off light
            _pelletLight.enabled = false;

            //turn off trigger collider
            _pelletTrigger.enabled = false;

            //play particle effect
            _pelletParticleSystem.Play();

            Destroy(this.transform.parent.gameObject, _particleSystemDuration);

            
            
        }
    }
}
