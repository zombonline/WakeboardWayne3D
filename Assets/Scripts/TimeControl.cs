using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeControl : MonoBehaviour
{
    public bool timePaused = false;

    float timeScaleBeforePause;
    public void PauseTime()
    {
        timeScaleBeforePause = Time.timeScale;
        timePaused= true;
        Time.timeScale = 0f;
    }

    public void ResumeTime()
    {
        timePaused= false;
        Time.timeScale = timeScaleBeforePause;
    }

}
