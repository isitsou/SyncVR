using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class WormsManager : MonoBehaviour
{
    [SerializeField] private PullingWormsController _pluckingController;

    public UnityEvent AllWormsPulledOut;

    private GameObject[] worms;
    private int _currentWorm;
    
    void Start()
    {
        InitializeWorms();
        AllWormsPulledOut.AddListener(() => Debug.Log("All worms plucked out"));
    }

    private void InitializeWorms()
    {
        worms = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            worms[i] = transform.GetChild(i).gameObject;
            worms[i].SetActive(false);
        }        
    }

    public void ActivateWormByIndex(int wormIndex) => worms[wormIndex].SetActive(true);
   

    public void PullOutWorm()
    {
        _currentWorm++;
        int nextWormIndex = _currentWorm;

        if (nextWormIndex == worms.Length) AllWormsPulledOut.Invoke();
        else ActivateWormByIndex(nextWormIndex);
    }
    public int GetNumberOfWormsPlucked() => _currentWorm;
    
}
