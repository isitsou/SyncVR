using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormController : MonoBehaviour
{
    [Header("Cached Refs")]
    [SerializeField] private PluckingController _pluckingController;
    [SerializeField] private Animator myAnimator;
    void OnEnable()
    {
        _pluckingController.Plucking.AddListener(()=>myAnimator.Play("Plucking"));
        _pluckingController.PluckedOut.AddListener(()=>myAnimator.Play("Plucked Out"));
    }
  
}
