using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPSInScreen : MonoBehaviour
{
    private int _lastFrameIndex;
    private float[] frameDeltaTimeArray;
    private TextMeshProUGUI _fpsCounter;

    private void Awake()
    {
        frameDeltaTimeArray = new float[50];
        _fpsCounter = GetComponent<TextMeshProUGUI>();
    }
    
    // Update is called once per frame
    void Update()
    {
        frameDeltaTimeArray[_lastFrameIndex] = Time.unscaledDeltaTime;
        _lastFrameIndex = (_lastFrameIndex + 1) % frameDeltaTimeArray.Length;

        _fpsCounter.text = "FPS: " + Mathf.RoundToInt(CalculateFPS());
    }

    private float CalculateFPS()
    {
        var total = 0f;
        var index = 0;
        for (; index < frameDeltaTimeArray.Length; index++)
        {
            var deltaTime = frameDeltaTimeArray[index];
            total += deltaTime;
        }

        return frameDeltaTimeArray.Length / total;
    }
}
