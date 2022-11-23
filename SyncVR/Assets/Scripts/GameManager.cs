using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Cached Refs")]
    [SerializeField] Image _panel;
    [SerializeField] Image _descriptionClock;
    [SerializeField] Image _descriptionClockPointer;
    [SerializeField] GameObject _startButton;
    [SerializeField] TMPro.TMP_Text _description;
    [SerializeField] TMPro.TMP_Text _finalScore;
    [SerializeField] WormsManager _wormsManager;


    [Header("Events")]
    public UnityEvent GameSessionStarts;
    public UnityEvent GameSessionEnds;

    private Color _visible = new Color(1, 1, 1, 1);
    private Color _transparent = new Color(1, 1, 1, 0);


    //private void Start()
    //{
    //    StartSession();
    //}
    public void StartSession()
    {
        GameSessionStarts.Invoke();

        StartCoroutine(FadeImage(_panel, _transparent, 0.7f));
        StartCoroutine(FadeImage(_descriptionClock, _transparent, 0.7f));
        StartCoroutine(FadeImage(_descriptionClockPointer, _transparent, 0.7f));
        StartCoroutine(FadeText(_description, _transparent, 0.7f));
    }
    public void EndSession() => GameSessionEnds.Invoke();

    public void FadeInScoreScreen()
    {
        StartCoroutine(FadeImage(_panel, _visible, 0.7f));
        StartCoroutine(FadeText(_finalScore, _visible, 0.7f));
        _finalScore.text = "Your Score is: " + _wormsManager.GetNumberOfWormsPlucked();
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
    private IEnumerator FadeText(TMPro.TMP_Text text, Color endValue, float duration)
    {
        float time = 0;
        Color startValue = text.color;
        while (time < duration)
        {
            text.color = Color.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        text.color = endValue;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
