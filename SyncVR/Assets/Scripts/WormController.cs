using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This controller has 2 functionalities. 
/// 1) Change accordingly the animator state when the PlayerPulling and PulledOut events are invoked.
/// 2) Invoke the WormsPulledOut event in order for the Worms Manager to increase the current pulled out
/// worms counter.
/// Also, implements an interface for an easier future expansion of the game(e.g. pulling things with different
/// behaviours, animations etc).
/// </summary>
public interface IPullable
{
    public void Pulling();
    public void PulledOut();
}
public class WormController : MonoBehaviour, IPullable
{
    [Header("Cached Refs")]
    [SerializeField] private Animator myAnimator;

    public UnityEvent WormPulledOut; // This event is invoked when the player is pulling out this worm

    public void Pulling() => myAnimator.SetTrigger("Pulled");
    public void PulledOut()
    {
        WormPulledOut.Invoke();
        myAnimator.SetTrigger("PulledOut");
    }

    //This method is used as an animation-event in the end of Worm_PulledOut animation. 
    public void DisableMe()
    {
        WormPulledOut.RemoveAllListeners();
        Destroy(this.gameObject);
    }
}


