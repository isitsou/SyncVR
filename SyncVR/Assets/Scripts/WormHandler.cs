using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormHandler : MonoBehaviour
{
    [Header("Cached Refs")]
    [SerializeField] private Animator myAnimator;

    private PullingWormsController _pullingWormsController;

    private void Start()
    {
        _pullingWormsController.Pulling.AddListener(PlayPullingAnim);
        _pullingWormsController.PulledOut.AddListener(PlayPulledOutAnim);
    }

    //Disables itself at the end of the pulled out animation
    public void DisableMe() => this.gameObject.SetActive(false);

    private void PlayPullingAnim() => myAnimator.Play("Pulling");
    private void PlayPulledOutAnim() => myAnimator.Play("Pulled Out");

    private void OnDisable()
    {
        _pullingWormsController.Pulling.RemoveListener(PlayPullingAnim);
        _pullingWormsController.PulledOut.RemoveListener(PlayPulledOutAnim);
    }

    public void SetPullingController(PullingWormsController controller) => _pullingWormsController = controller;

}
