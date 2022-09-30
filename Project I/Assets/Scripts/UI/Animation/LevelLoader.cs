using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public Animator _transition;
    public float _transitionTime = 20f;

    public void LoadNextLevelAnimation()
    {
        StartCoroutine(NextLevelAnimation());
    }

    IEnumerator NextLevelAnimation()
    {
        _transition.Play("switch mainmenu to newgame");
        yield return new WaitForSeconds(_transitionTime);
        //_transition.Play("end mainmenu to newgame");
    }
}
