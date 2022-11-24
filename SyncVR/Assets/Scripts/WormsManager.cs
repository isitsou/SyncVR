using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class WormsManager : MonoBehaviour
{
    [SerializeField] private GameplayManager _GPM;
    [SerializeField] private GameObject _wormPrefab;
    [SerializeField] private PullingWormsController _pullingWormsController;
    [SerializeField] private Transform[] _wormsPositions;
    [SerializeField] private Image[] _wormsScreen;

    public UnityEvent AllWormsPulledOut;

    private Color _visible = new Color(1, 1, 1, 1);
    private int _currentWorm;
    private int _numberOfWormsToWin;
    private int _previousPosIndex = 0;

    void Start()
    {
        _numberOfWormsToWin = _GPM.GetNumberOfWormsToWin();

        AllWormsPulledOut.AddListener(() => Debug.Log("All worms pulled out"));
    }


    public void InitializeWormsScreen()
    {
        for (int i = 0; i < _numberOfWormsToWin; i++)
        {
            _wormsScreen[i].enabled = true;
        }
    }

    public void SpawnWorm()
    {
        int randomPosIndex = CreateRandomPosIndex();

        WormHandler worm = Instantiate(_wormPrefab, _wormsPositions[randomPosIndex]).GetComponent<WormHandler>();
        WormHandler worm0 = Instantiate(_wormPrefab, _wormsPositions[randomPosIndex]).GetComponent<WormHandler>();
        worm.SetPullingController(_pullingWormsController);
        worm0.SetPullingController(_pullingWormsController);
    }

    private int CreateRandomPosIndex()
    {
        int randomPosIndex = UnityEngine.Random.Range(0, _wormsPositions.Length);
        if (randomPosIndex == _previousPosIndex) randomPosIndex = UnityEngine.Random.Range(0, _wormsPositions.Length);
        else _previousPosIndex = randomPosIndex;
        return randomPosIndex;
    }

    public void PullOutWorm()
    {
        _wormsScreen[_currentWorm].color = _visible;

        _currentWorm++;
        int nextWormIndex = _currentWorm;

        if (nextWormIndex == _numberOfWormsToWin) AllWormsPulledOut.Invoke();
        else SpawnWorm();
    }

    public int GetNumberOfWormsPlucked() => _currentWorm;

    

}
