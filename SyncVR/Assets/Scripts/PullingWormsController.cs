using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This controller implements 2 main functionalities.
/// 1) Subscribe each worm that touches the visibility mask collider to the events PlayerPulling and PlayerPulledOut.
/// 2) Check if the mask is touching a worm and the player is left-clicking, invoke the necessary event and adjust the
/// pulling slider accordingly.
/// </summary>
public class PullingWormsController : MonoBehaviour
{
    [Header("Cached Refs")]
    [SerializeField] private GameplayManager _GPM;
    [SerializeField] private Collider2D _visibilityMask;    
    [SerializeField] private UnityEngine.UI.Slider _pullingUI;

    [Header("Events")]

    public UnityEvent PlayerPulling;
    public UnityEvent PlayerPulledOut; // This event is invoked when the player is pulling out 1 or more worms

    private float _maxValueOfPullSlider;
    private float _reducingRatePullSlider;
    private float _stepIncreasePullSlider;
    private float _currentAmountOfPull;
    private LayerMask _worms;


    private void OnEnable()
    {
        PlayerPulledOut.AddListener(ResetNumberOfClicks);
    }
    private void Start()
    {
        _maxValueOfPullSlider = _GPM.GetMaxValueOfPull();
        _reducingRatePullSlider = _GPM.GetReducingRatePull();
        _stepIncreasePullSlider = _GPM.GetStepIncreasePull();

        _pullingUI.maxValue = _maxValueOfPullSlider;
        _worms = LayerMask.GetMask("Worms");
    }

    #region Pulling Process
    private void Update()
    {
        bool maskIsTouchingWorm = _visibilityMask.IsTouchingLayers(_worms);
        bool leftClickPressed = Input.GetMouseButtonDown(0);
       

        if (maskIsTouchingWorm && leftClickPressed)
        {
            _currentAmountOfPull += _stepIncreasePullSlider;

            bool playerClickedEnoughTimes = _currentAmountOfPull > _maxValueOfPullSlider;

            if (playerClickedEnoughTimes)
            {
                PlayerPulledOut.Invoke();
            }              
            else
            {
                PlayerPulling.Invoke();
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
        bool playerHasPullAtLeastOneTime = maskIsTouchingWorm && _currentAmountOfPull > 0;
        _pullingUI.gameObject.SetActive(playerHasPullAtLeastOneTime);
        _pullingUI.value = _currentAmountOfPull;
    }
    private void ResetNumberOfClicks()
    {
        _currentAmountOfPull = 0;
    }
    #endregion

    #region Subscription of touching Pullables
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IPullable>(out IPullable worm))
        {
            PlayerPulling.AddListener(worm.Pulling);
            PlayerPulledOut.AddListener(worm.PulledOut);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IPullable>(out IPullable worm))
        {
            PlayerPulling.RemoveListener(worm.Pulling);
            PlayerPulledOut.RemoveListener(worm.PulledOut);
        }
    }
    #endregion
}
