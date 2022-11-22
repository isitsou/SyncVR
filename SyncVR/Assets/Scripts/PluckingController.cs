using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PluckingController : MonoBehaviour
{
    [Header("Cached Refs")]
    [SerializeField] private Collider2D _visibilityMask;
    [SerializeField] private LayerMask _interactiveLayers;
    [SerializeField] private UnityEngine.UI.Slider _plucking;

    [Header("Events")]

    public UnityEvent Plucking;
    public UnityEvent PluckedOut;

    private float _maxAmountOfPull = 10; // for GPM
    private float _reducingRatePull = 0.5f; // for GPM
    private float _currentAmountOfPull;

    private void OnEnable()
    {
        PluckedOut.AddListener(ResetNumberOfClicks);
        // Plucking.AddListener(() => { Debug.Log("plucking"); });
        //PluckedOut.AddListener(() => { Debug.Log("plucked Out"); });
    }
    private void Start()
    {
        _plucking.maxValue = _maxAmountOfPull;
    }
    void Update()
    {
        bool maskIsTouchingWorm = _visibilityMask.IsTouchingLayers(_interactiveLayers);
        bool leftClickPressed = Input.GetMouseButtonDown(0);

        if (maskIsTouchingWorm && leftClickPressed)
        {
            _currentAmountOfPull++;

            bool playerClickedEnoughTimes = _currentAmountOfPull >= _maxAmountOfPull;
            if (playerClickedEnoughTimes)
            {
                PluckedOut.Invoke();
            }
            else
            {
                Plucking.Invoke();
            }
        }

        if (_currentAmountOfPull > 0)
        {
            _currentAmountOfPull -= _reducingRatePull * Time.deltaTime;
        }
        HandleSlider(maskIsTouchingWorm);
    }

    private void HandleSlider(bool maskIsTouchingWorm)
    {
        _plucking.gameObject.SetActive(maskIsTouchingWorm);
        _plucking.value = _currentAmountOfPull;
    }

    private void ResetNumberOfClicks()
    {
        _currentAmountOfPull = 0;
    }
}
