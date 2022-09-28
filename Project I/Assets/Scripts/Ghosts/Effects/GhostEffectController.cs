using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEffectController : MonoBehaviour
{
    [Header("Ghost State Controller")]
    private Blinky_Pinky_Inky_StateController _ghostStateController;

    [Header("Ghost Movement Controller")]
    private Blinky_Pinky_ClydeMovementController _ghostMovementController;

    [Header("Ghosts' Material")]
    [SerializeField] private List<Material> _ghostMaterials;

    [Header("Ghosts's Part to disable when in eaten mode")]
    [SerializeField] private GameObject[] _partsToDisappear;

    [SerializeField] GhostColor _ghostColor;
    [SerializeField] Color _originColor;
    

    /// <summary>
    /// these for decrease the number of methods called each frame
    /// </summary>
    [Header("bool value")]
    [SerializeField] private bool _disappear;
    [SerializeField] private bool _changedColor;
    [SerializeField] bool _eatenSoundPlayed;

    [Header("flickering")]
    [SerializeField] private float _timeToStartFlickering;
    [SerializeField] private float _timeBetweenFlickering;
    [SerializeField] private float _flickerTimer;
    [SerializeField] private bool _flicked;

    private void Start()
    {
        //chỉ có 1 ".parent" thôi, trong trường hợp có 2 ".parent",lúc này sẽ tham chiếu đến "ghosts tổng", do đó tất cả ghost lúc này sẽ check với component <StateController> đầu tiên - <Blinky's State Controller>
        //Lúc đó, khi blinky chuyển sang eaten, tất cả các ghost sử dụng hàm <UpdateGhostEffect> ở dưới đều dùng <Blinky's State Controller> để so sánh trạng thái
        //Dẫn đến việc tất cả đều dùng hiệu ứng eaten cho dù bản thân đang ở trạng thái khác
        _ghostStateController = this.transform.parent.gameObject.GetComponentInChildren<Blinky_Pinky_Inky_StateController>();
        _ghostMovementController = this.transform.parent.gameObject.GetComponentInChildren<Blinky_Pinky_ClydeMovementController>();

        SetupOriginColor();
        ResetToOriginColor();

        _disappear = false;
        _changedColor = false;
        _eatenSoundPlayed = false;
        _flicked = false;

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
            if (_eatenSoundPlayed)
            {
                _eatenSoundPlayed = false;
            }
            if (_flicked)
            {
                _flicked = false;
            }
            ResetFlickerTimer();

        }
        else if (_ghostStateController.CheckCurrentState(GhostState.frightened))
        {
            PlayFrightenedEffect();
        }
        else if (_ghostStateController.CheckCurrentState(GhostState.eaten))
        {
            PlayEatenEffect();
            PlayEatenSound();
            ResetFlickerTimer();
        }
    }

    private void PlayFrightenedEffect()
    {
        if (_ghostStateController.FrightenNearlyOver(_timeToStartFlickering))
        {
            CheckIfSwitchOrNot();
            if (!_flicked)
            {
                foreach (Material ghostMaterial in _ghostMaterials)
                {
                    ghostMaterial.SetColor("_EmissionColor", _ghostColor.GetFrightenedColor());
                }
                _changedColor = true;
            }
            else if(_flicked)
            {
                foreach (Material ghostMaterial in _ghostMaterials)
                {
                    ghostMaterial.SetColor("_EmissionColor", _ghostColor.GetFlickeringColor());
                }
                _changedColor = true;
            }
        }
        else
        {
            if (!_changedColor)
            {
                foreach (Material ghostMaterial in _ghostMaterials)
                {
                    ghostMaterial.SetColor("_EmissionColor", _ghostColor.GetFrightenedColor());
                }

                _changedColor = true;
            }
        }
        

    }

    private void PlayEatenEffect()
    {
        if (!_disappear)
        {
            DisapearParts();   
        }

        
    }

    ///Color
    private void SetupOriginColor()
    {
        if (_ghostMovementController.IsItThisGhost(GhostName.blinky))
        {
            _originColor = _ghostColor.GetBlinkyColor();
        }
        else if (_ghostMovementController.IsItThisGhost(GhostName.pinky))
        {
            _originColor = _ghostColor.GetPinkyColor();
        }
        else if (_ghostMovementController.IsItThisGhost(GhostName.inky))
        {
            _originColor = _ghostColor.GetInkyColor();
        }
        else if (_ghostMovementController.IsItThisGhost(GhostName.clyde))
        {
            _originColor = _ghostColor.GetClydeColor();
        }
    }


    private void ResetToOriginColor()
    {
       
        foreach(Material ghostMaterial in _ghostMaterials)
        {
            ghostMaterial.SetColor("_EmissionColor", _originColor);
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

    private void PlayEatenSound()
    {
        if (!_eatenSoundPlayed)
        {
            this.gameObject.GetComponent<AudioManager>().Play("eaten");
            _eatenSoundPlayed = true;
        }
    }


    private void CheckIfSwitchOrNot()
    {
        if (IsFlickerTimeOut(_timeBetweenFlickering))
        {
            ResetFlickerTimer();
            _flicked = !_flicked;
            _changedColor = false;
        }
        else
        {
            FlickerTimeRun();
        }
    }

    ///flickering
    private bool IsFlickerTimeOut(float timeToFlick)
    {
        return _flickerTimer > timeToFlick;
    }

    private void ResetFlickerTimer()
    {
        _flickerTimer = 0;
    }

    private void FlickerTimeRun()
    {
        _flickerTimer += Time.deltaTime;
    }
}
