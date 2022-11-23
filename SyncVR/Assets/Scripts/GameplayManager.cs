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
    public readonly int DayDuration = 40;
    public readonly int NightDuration = 20;

    [Header("Pulling a Worm")]

    [Tooltip("Every time the player hits left-click on a worm the slider value is increased by this step")]
    public readonly float StepIncreasePull = 1;

    [Tooltip("The max value of the pulling slider that the player has to reach by left-clicking in order to pluck out the worm")]
    public readonly float MaxValueOfPull = 10;

    [Tooltip("Every time the player hits leftclick on a worm the slider value is increased by this step")]
    public readonly float ReducingRatePull = 0.5f;

}
