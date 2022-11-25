using UnityEngine;

/// <summary>
/// This manager is responsible for counting how many worms the user has pulled out. Also,
/// when one is pulled out another is being created in order to have a consistant number
/// of worms active in the scene.
/// </summary>
public class WormsManager : MonoBehaviour
{
    [Header("Cached Refs")]
    [SerializeField] private GameplayManager _GPM;
    [SerializeField] private GameObject _wormPrefab;
    [SerializeField] private Transform[] _wormsPositions;

    private int _currentWormCounter;
    private int _numberOfActiveWorms;

    public int GetNumberOfWormsPulledOut() => _currentWormCounter;

    private void Start()
    {
        _numberOfActiveWorms = _GPM.GetNumberOfActiveWorms();

        if (_numberOfActiveWorms > _wormsPositions.Length - 1)
        {
            Debug.LogError("At least one spawn position should be available!");
        }

        InitializeWormsInScene();
    }

    private void InitializeWormsInScene()
    {
        for (int i = 0; i < _numberOfActiveWorms; i++)
        {
            GameObject worm = Instantiate(_wormPrefab, _wormsPositions[i]);
            SubscribeToWormPulledOutEvent(worm);
        }
    }

    private void AddPulledOutWormsCounter()
    {
        _currentWormCounter++;
    }
    private void SpawnWorms()
    {
        Transform nextAvailablePos = GetNextAvailableSpawnPos();
        GameObject worm = Instantiate(_wormPrefab, nextAvailablePos);
        SubscribeToWormPulledOutEvent(worm);
    }

    private void SubscribeToWormPulledOutEvent(GameObject spawnedWorm)
    {
        WormController spawnedWormHandler = spawnedWorm.GetComponent<WormController>();
        spawnedWormHandler.WormPulledOut.AddListener(AddPulledOutWormsCounter);
        spawnedWormHandler.WormPulledOut.AddListener(SpawnWorms);
    }

    //Cycles consecutively through all the positions in the _wormsPositions array and return the next available 
    private Transform GetNextAvailableSpawnPos()
    {
        int currentSpawnPosIndex = _currentWormCounter % _wormsPositions.Length; 

        int nextSpawnPosIndex = currentSpawnPosIndex + _numberOfActiveWorms-1;

        nextSpawnPosIndex %= _wormsPositions.Length; 

        return _wormsPositions[nextSpawnPosIndex];
    }


}
