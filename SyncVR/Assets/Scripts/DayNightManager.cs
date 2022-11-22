using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Events;
using UnityEngine.UI;

public class DayNightManager : MonoBehaviour
{
    [Header("Cached Refs")]
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private Light2D _globalLight;   
    [SerializeField] private Image _clockPointer;
    [SerializeField] private Image _clockBackground;

   

    private int _dayDuration = 7; // GPM
    private int _nightDuration = 5; // GPM

   

    public void StartDayNight()
    {
        StartCoroutine(DayNightTimer());
        StartCoroutine(DayNightUI());
        Color visible = new Color(1, 1, 1, 1);
        StartCoroutine(FadeImage(_clockPointer, visible, 1f));
        StartCoroutine(FadeImage(_clockBackground, visible, 1f));
    }

    public void PauseDayNight()
    {
        StopAllCoroutines();
        Color transparent = new Color(1, 1, 1, 0);
        StartCoroutine(FadeImage(_clockPointer, transparent, 0.8f));
        StartCoroutine(FadeImage(_clockBackground, transparent, 0.8f));
    }

    private IEnumerator DayNightTimer()
    {
        float fadeLightDuration = 1.5f;

        yield return new WaitForSeconds(_dayDuration-fadeLightDuration);        
        StartCoroutine(FadeLight(_globalLight, 0.5f, fadeLightDuration));

        yield return new WaitForSeconds(_nightDuration-fadeLightDuration);        
        StartCoroutine(FadeLight(_globalLight, 0.2f, fadeLightDuration));

        yield return new WaitForSeconds(fadeLightDuration);

        _gameManager.EndSession();
    }
    private IEnumerator DayNightUI()
    {
        float time = 0;
        float gameSessionDuration = _dayDuration + _nightDuration;
        Quaternion startAngle = _clockPointer.transform.rotation;
        Quaternion endAngle = Quaternion.Euler(new Vector3(0,0,-180));

        while (time< gameSessionDuration)
        {
            _clockPointer.transform.rotation = Quaternion.Lerp(startAngle, endAngle, time / gameSessionDuration);
            time += Time.deltaTime;
            yield return null;
        }
    }


    private IEnumerator FadeLight(Light2D light, float endValue, float duration)
    {
        float time = 0;
        float startValue = light.intensity;              

        while (time < duration)
        {
            light.intensity = Mathf.Lerp(startValue, endValue, time / duration);
            
            time += Time.deltaTime;
            yield return null;
        }

        light.intensity =  endValue;        
    }
   private IEnumerator FadeImage(Image image, Color endValue, float duration)
    {
        float time = 0;
        Color startValue = image.color;
        while (time < duration)
        {
            image.color = Color.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        image.color = endValue;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
