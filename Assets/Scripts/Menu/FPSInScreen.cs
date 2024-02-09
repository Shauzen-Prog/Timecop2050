using TMPro;
using UnityEngine;

public class FPSInScreen : MonoBehaviour
{
    private int _lastFrameIndex;
    private float[] _frameDeltaTimeArray;
    private TextMeshProUGUI _fpsCounter;

    private void Awake()
    {
        _frameDeltaTimeArray = new float[50];
        _fpsCounter = GetComponent<TextMeshProUGUI>();
    }
    
    // Update is called once per frame
    void Update()
    {
        _frameDeltaTimeArray[_lastFrameIndex] = Time.unscaledDeltaTime;
        _lastFrameIndex = (_lastFrameIndex + 1) % _frameDeltaTimeArray.Length;

        _fpsCounter.text = "FPS: " + Mathf.RoundToInt(CalculateFPS());
    }

    private float CalculateFPS()
    {
        var total = 0f;
        var index = 0;
        for (; index < _frameDeltaTimeArray.Length; index++)
        {
            var deltaTime = _frameDeltaTimeArray[index];
            total += deltaTime;
        }

        return _frameDeltaTimeArray.Length / total;
    }
}
