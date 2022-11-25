using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;

/// <summary>
/// This manager is responsible for the implementation of a timer that controls the duration the game session.
/// When the timer ends this script informs Game Manager that the game session is over.
/// Also, controls the UI elements and the lights that are associated with the passing of time.
/// </summary>
public class DayNightManager : MonoBehaviour
{
    [Header("Cached Refs")]
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private GameplayManager _GPM;
    [SerializeField] private Light2D _globalLight;
    [SerializeField] private Image _clockPointer;
    [SerializeField] private Image _clockBackground;



    private int _dayDuration;
    private int _nightDuration;
    private Color _visible = new Color(1, 1, 1, 1);
    public void StartDayNight()
    {
        StartCoroutine(DayNightTimer());
        StartCoroutine(DayNightUIPointerRotator());
        FadeInUIElements();
    }

    private void Start()
    {
        _dayDuration = _GPM.GetDayDuration();
        _nightDuration = _GPM.GetNightDuration();

        if (_nightDuration * 2 != _dayDuration)
        {
            Debug.LogError("Day duration should be double the night duration. You are breaking the timer UI!");
        }
    }
    private void FadeInUIElements()
    {
        StartCoroutine(FadeImage(_clockPointer, _visible, 1f));
        StartCoroutine(FadeImage(_clockBackground, _visible, 1f));
    }
    private IEnumerator DayNightTimer()
    {

        StartCoroutine(FadeLight(_globalLight, 0.6f, _dayDuration));
        yield return new WaitForSeconds(_dayDuration);
        StartCoroutine(FadeLight(_globalLight, 0.4f, _nightDuration));

        yield return new WaitForSeconds(_nightDuration);
        StartCoroutine(FadeLight(_globalLight, 1f, 1f));

        _gameManager.EndSession();
    }
    private IEnumerator DayNightUIPointerRotator()
    {
        float time = 0;
        float gameSessionDuration = _dayDuration + _nightDuration;
        Quaternion startAngle = _clockPointer.transform.rotation;
        Quaternion endAngle = Quaternion.Euler(new Vector3(0, 0, -180));

        while (time < gameSessionDuration)
        {
            _clockPointer.transform.rotation = Quaternion.Lerp(startAngle, endAngle, time / gameSessionDuration);
            time += Time.deltaTime;
            yield return null;
        }
    }


    #region FadeIEnumerators
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

        light.intensity = endValue;
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
    #endregion

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
