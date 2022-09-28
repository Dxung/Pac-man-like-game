using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ghost Color", menuName = "My Game/ Ghosts' Colors")]
public class GhostColor : ScriptableObject
{
    [SerializeField] private Color _blinkyColor;
    [SerializeField] private Color _pinkyColor;
    [SerializeField] private Color _inkyColor;
    [SerializeField] private Color _clydeColor;

    [SerializeField] private Color _frightenedColor;
    [SerializeField] private Color _flickeringColor;

    public Color GetBlinkyColor()
    {
        return _blinkyColor;
    }
    public Color GetPinkyColor()
    {
        return _pinkyColor;
    }
    public Color GetInkyColor()
    {
        return _inkyColor;
    }
    public Color GetClydeColor()
    {
        return _clydeColor;
    }
    public Color GetFrightenedColor()
    {
        return _frightenedColor;
    }
    public Color GetFlickeringColor()
    {
        return _flickeringColor;
    }
}
