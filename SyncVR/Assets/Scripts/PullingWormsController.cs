using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PullingWormsController : MonoBehaviour
{
    [Header("Cached Refs")]
    [SerializeField] private GameplayManager _GPM;
    [SerializeField] private Collider2D _visibilityMask;
    [SerializeField] private LayerMask _interactiveLayers;
    [SerializeField] private UnityEngine.UI.Slider _plucking;

    [Header("Events")]

    public UnityEvent Pulling;
    public UnityEvent PulledOut;

    private float _maxValueOfPullSlider; 
    private float _reducingRatePullSlider; 
    private float _stepIncreasePullSlider; 
    private float _currentAmountOfPull;

    private void OnEnable()
    {
        PulledOut.AddListener(ResetNumberOfClicks);
        // Plucking.AddListener(() => { Debug.Log("plucking"); });
        //PluckedOut.AddListener(() => { Debug.Log("plucked Out"); });
    }
    private void Start()
    {
        _maxValueOfPullSlider = _GPM.MaxValueOfPull;
        _reducingRatePullSlider = _GPM.ReducingRatePull;
        _stepIncreasePullSlider = _GPM.StepIncreasePull;

        _plucking.maxValue = _maxValueOfPullSlider;
    }
    void Update()
    {
        bool maskIsTouchingWorm = _visibilityMask.IsTouchingLayers(_interactiveLayers);
        bool leftClickPressed = Input.GetMouseButtonDown(0);

        if (maskIsTouchingWorm && leftClickPressed)
        {
            _currentAmountOfPull += _stepIncreasePullSlider;

            bool playerClickedEnoughTimes = _currentAmountOfPull >= _maxValueOfPullSlider;
            if (playerClickedEnoughTimes)
            {
                PulledOut.Invoke();
            }
            else
            {
                Pulling.Invoke();
            }
        }

        if (_currentAmountOfPull > 0)
        {
            _currentAmountOfPull -= _reducingRatePullSlider * Time.deltaTime;
        }
        HandleSlider(maskIsTouchingWorm);
    }

    private void HandleSlider(bool maskIsTouchingWorm)
    {
        bool playerHasPullAtLeastOneTime = maskIsTouchingWorm && (_currentAmountOfPull > 0);
        _plucking.gameObject.SetActive(playerHasPullAtLeastOneTime);
        _plucking.value = _currentAmountOfPull;
    }

    private void ResetNumberOfClicks()
    {
        _currentAmountOfPull = 0;
    }
}
