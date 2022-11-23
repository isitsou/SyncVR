using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormController : MonoBehaviour
{
    [Header("Cached Refs")]
    [SerializeField] private PullingWormsController _pluckingController;
    [SerializeField] private Animator myAnimator;
    void OnEnable()
    {
        _pluckingController.Pulling.AddListener(PlayPullingAnim);
        _pluckingController.PulledOut.AddListener(PlayPulledOutAnim);              
    }

    //Disables itself at the end of the pulled out animation
    public void DisableMe() => this.gameObject.SetActive(false);

    private void PlayPullingAnim() => myAnimator.Play("Pulling");
    private void PlayPulledOutAnim() => myAnimator.Play("Pulled Out");
    
    private void OnDisable()
    {
        _pluckingController.Pulling.RemoveListener(PlayPullingAnim);
        _pluckingController.PulledOut.RemoveListener(PlayPulledOutAnim);
    }

}
