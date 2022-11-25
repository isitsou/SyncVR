using UnityEngine;

/// <summary>
/// This scriptable object contains almost all of the values that effect the "play experience". 
/// It is created for convenience because everything is one place and that makes playtesting easy and fast.
/// </summary>
[CreateAssetMenu(menuName = "GameplayManager", fileName = "GameplayManager", order = 51)]
public class GameplayManager : ScriptableObject
{
    [Header("Game Duration")]
    [SerializeField] private int _dayDuration = 40;
    [SerializeField] private int _nightDuration = 20;

    [Header("Pulling a Worm")]

    [Tooltip("Every time the player hits left-click on a worm the slider value is increased by this step")]
    [SerializeField] private float _stepIncreasePull = 1;

    [Tooltip("The max value of the pulling slider that the player has to reach by left-clicking in order to pluck out the worm")]
    [SerializeField] private float _maxValueOfPull = 10;

    [Tooltip("Every time the player hits leftclick on a worm the slider value is increased by this step")]
    [SerializeField] private float _reducingRatePull = 0.5f;

    [Tooltip("Number of active worms in the scene. Every time the player pulls out a worm another will replace it on another position.")]
    [SerializeField] private int _numberOfActiveWorms = 5;

    //Getters
    public int GetDayDuration() => _dayDuration;
    public int GetNightDuration() => _nightDuration;
    public float GetStepIncreasePull() => _stepIncreasePull;
    public float GetMaxValueOfPull() => _maxValueOfPull;
    public float GetReducingRatePull() => _reducingRatePull;
    public int GetNumberOfActiveWorms() => _numberOfActiveWorms;
 
}
