using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// This script helps on managing multiple variables on the game in one place in order to make the 
/// balacing process of the game easier and faster.
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

    [SerializeField] private int _numberOfWormsToWin = 10;


    //Getters
    public int GetDayDuration() => _dayDuration;
    public int GetNightDuration() => _nightDuration;
    public float GetStepIncreasePull() => _stepIncreasePull;
    public float GetMaxValueOfPull() => _maxValueOfPull;
    public float GetReducingRatePull() => _reducingRatePull;
    public int GetNumberOfWormsToWin() => _numberOfWormsToWin;
}
