using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PelletCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _smallPelletText;
    [SerializeField] private TextMeshProUGUI _powerPelletText;

    [SerializeField] private int _currentSmallPelletNumber = 0;
    [SerializeField] private int _maxSmallPelletNumber = 0;
    [SerializeField] private int _currentPowerPelletNumber = 0;
    [SerializeField] private int _maxPowerPelletNumber = 0;


    private void Start()
    {
        UpdateSmallPelletCounter();
        UpdatePowerPelletCounter();
    }

    public void AddSmallPelletToCounter()
    {
        _currentSmallPelletNumber = _maxSmallPelletNumber += 1;
        UpdateSmallPelletCounter();
    }
    public void AddPowerPelletToCounter()
    {
        _currentPowerPelletNumber =_maxPowerPelletNumber += 1;
        UpdatePowerPelletCounter();
    }

    public void ConsumeSmallPellet()
    {
        _currentSmallPelletNumber -= 1;
        UpdateSmallPelletCounter();
    }

    public void ConsumePowerPellet()
    {
        _currentPowerPelletNumber -= 1;
        UpdatePowerPelletCounter();
    }

    private void UpdateSmallPelletCounter()
    {
        _smallPelletText.text = _currentSmallPelletNumber.ToString() + "/" + _maxSmallPelletNumber.ToString();

    }

    private void UpdatePowerPelletCounter()
    {
        _powerPelletText.text = _currentPowerPelletNumber.ToString() + "/" + _maxPowerPelletNumber.ToString();

    }
}
    
