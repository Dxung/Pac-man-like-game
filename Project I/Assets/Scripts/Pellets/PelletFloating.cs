using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelletFloating : MonoBehaviour
{
    private Vector3 _startPoint;
    [SerializeField] private float _value;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _amplitude = 0.5f;
    
    


    // Start is called before the first frame update
    void Start()
    {

        //Store starting position and get the random number for "_value" so each pellet will float uniquely
        _startPoint = transform.position;
        _value = Random.Range(0, 10.5f);

    }

    // Update is called once per frame
    void Update()
    {
        //to make the distance of floating change continuously (because Sin curve changes continuously) 
        _value += Time.deltaTime;

        //Sin range from -1 to 1, so pellet moving will never excess "max range" == "_amplitude"
        transform.position = _startPoint + Vector3.up * Mathf.Sin(_value * _maxSpeed) * _amplitude;
    }
}
