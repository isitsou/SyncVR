using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// Game Manager is responsible for activation and deactivation of all the game elements in the scene.
/// It does that by invoking 2 main unity events. The GameSessionStarts event is invoked when the user 
/// presses the start UI button. The GameSessionEnds event is invoked when the timer of DayNightManager ends. 
/// </summary>
public class GameManager : MonoBehaviour
{
    [Header("Cached Refs")]
    [SerializeField] Image _panel;
    [SerializeField] Image _descriptionClock;
    [SerializeField] Image _descriptionClockPointer;    
    [SerializeField] TMPro.TMP_Text _description;
    [SerializeField] TMPro.TMP_Text _finalScore;
    [SerializeField] WormsManager _wormsManager;


    [Header("Events")]
    public UnityEvent GameSessionStarts;
    public UnityEvent GameSessionEnds;

    private Color _visible = new Color(1, 1, 1, 1);
    private Color _transparent = new Color(1, 1, 1, 0);
    private Color _transparentLetters = new Color(0, 0, 0, 0);

    public void StartSession()
    {
        GameSessionStarts.Invoke();
        float fadeDuration = 0.6f;
        StartCoroutine(FadeImage(_panel, _transparent, fadeDuration));
        StartCoroutine(FadeImage(_descriptionClock, _transparent, fadeDuration));
        StartCoroutine(FadeImage(_descriptionClockPointer, _transparent, fadeDuration));
        StartCoroutine(FadeText(_description, _transparentLetters, fadeDuration));        
    }
    public void EndSession() => GameSessionEnds.Invoke();
   
    private void Start()
    {
        GameSessionEnds.AddListener(FadeInScoreScreen);
    }

    private void FadeInScoreScreen()
    {
        StartCoroutine(FadeImage(_panel, _visible, 0.7f));
        StartCoroutine(FadeText(_finalScore, _visible, 0.7f));
        _finalScore.text = "You pulled out " + _wormsManager.GetNumberOfWormsPulledOut() + " worms \n Well done!!!";
    }

    #region FadeIEnumerators
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
    #endregion
    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
