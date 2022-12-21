using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] public Slider slider;

    private float animationSeconds = 0.2f;
    public float delta;


    public void ResetSlider()
    {
        slider.value = 0;
    }

    public void ProgressSlider(float delta)
    {
        StartCoroutine(ProgressSliderCoroutine(delta));         
    }

    private IEnumerator ProgressSliderCoroutine(float delta)
    {
        var wait = new WaitForSeconds(animationSeconds);
        slider.value += delta;
        yield return wait;
    }
}
