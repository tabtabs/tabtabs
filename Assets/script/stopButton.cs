using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class stopButton : MonoBehaviour
{
    public void stop()
    {
        Time.timeScale = 0.0f;
    }
}
