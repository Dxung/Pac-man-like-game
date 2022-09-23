using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEffectController : MonoBehaviour
{
    [Header("Ghost State Controller")]
    private Blinky_Pinky_Inky_StateController _ghostStateController;

    [Header("Ghosts' Material")]
    [SerializeField] private List<Material> _ghostMaterials;

    [Header("Ghosts's Part to disable when in eaten mode")]
    [SerializeField] private GameObject[] _partsToDisappear;

    [Header("Ghost's Material Colors")]
    [SerializeField] private List<Color> _ghostOriginMaterialColors;
  
    [SerializeField] Color _wantedColor;

    

    /// <summary>
    /// these for decrease the number of methods called each frame
    /// </summary>
    [Header("bool value")]
    [SerializeField] private bool _disappear;
    [SerializeField] private bool _changedColor;

    private void Start()
    {
        _ghostStateController = this.transform.parent.parent.gameObject.GetComponentInChildren<Blinky_Pinky_Inky_StateController>();
        _disappear = false;
        _changedColor = false;


        StoreOriginColor();
    }

    private void Update()
    {
        updateGhostEffect();
    }


    private void updateGhostEffect()
    {
        if(_ghostStateController.CheckCurrentState(GhostState.chase) || _ghostStateController.CheckCurrentState(GhostState.scatter))
        {
            if (_changedColor)
            {
                ResetToOriginColor();
            }

            if (_disappear)
            {
                ReAppearParts();
            }
            
        }
        else if (_ghostStateController.CheckCurrentState(GhostState.frightened))
        {
            PlayFrightenedEffect();
        }
        else if (_ghostStateController.CheckCurrentState(GhostState.eaten))
        {
            PLayEatenEffect();
        }
    }

    private void PlayFrightenedEffect()
    {
        if (!_changedColor)
        {
            foreach (Material ghostMaterial in _ghostMaterials)
            {
                ghostMaterial.SetColor("_EmissionColor", _wantedColor);
            }

            _changedColor = true;
        }
    }

    private void PLayEatenEffect()
    {
        if (!_disappear)
        {
            DisapearParts();   
        }
        
    }

    private void StoreOriginColor()
    {
        for(int i = 0; i < _ghostMaterials.Count; i++)
        {
            _ghostOriginMaterialColors.Add(_ghostMaterials[i].GetColor("_EmissionColor"));
        }
    }

    private void ResetToOriginColor()
    {
        for (int i = 0; i < _ghostMaterials.Count; i++)
        {
            _ghostMaterials[i].SetColor("_EmissionColor", _ghostOriginMaterialColors[i]);
        }

        _changedColor = false;
    }

    private void ReAppearParts()
    {
        foreach (GameObject part in _partsToDisappear)
        {
            part.SetActive(true);
        }

        _disappear = false;
    }

    private void DisapearParts()
    {
        foreach (GameObject part in _partsToDisappear)
        {
            part.SetActive(false);
        }

        _disappear = true;
    }
}
