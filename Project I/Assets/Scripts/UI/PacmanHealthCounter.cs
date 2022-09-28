using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PacmanHealthCounter : MonoBehaviour
{
    private int _pacmanHealth;
    [SerializeField] private GameObject[] _healthObject;

    private void Start()
    {
        _pacmanHealth = 3;
        UpdatePacmanHealthUI();
    }

    public void UpdatePacmanHealthUI()
    {
        if (_pacmanHealth == 0)
        {
            foreach (GameObject healthobject in _healthObject)
            {
                healthobject.SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < _pacmanHealth; i++)
            {
                _healthObject[i].SetActive(true);
            }
            for (int i = _pacmanHealth; i < _healthObject.Length; i++)
            {
                _healthObject[i].SetActive(false);
            }
        }
    }

    public bool CheckPacmanDeath()
    {
        _pacmanHealth -= 1;
        if (_pacmanHealth == 0)
        {
            return true;
        }

        else
        {
            return false;
        }
    }
}
