using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

/// <summary>
/// This controller is responsible for 3 things.
/// 1) Moving the game object that contains the sprite mask to the mouse 
/// position.
/// 2) Rotate the parent game object of the flashlight with respect to the mouse position.
/// 3) Adjust the outer radius of the flashlight's 2D PointLight based on the distance from the mouse.
/// </summary>
public class FlashlightMovementController : MonoBehaviour
{
    [Header("Cached Refs")]
    [SerializeField] private Transform _flashlightRotator; // parent gameObject of flashlight
    [SerializeField] private Light2D _flashlight;    
    [SerializeField] private GameObject _visibilityMask;
 

    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = Camera.main.transform.position.z + Camera.main.nearClipPlane;

        MoveMaskToMouse(mousePosition);

        RotateFlashlight(mousePosition);

        HandlePointLight(mousePosition);
    }

    private void HandlePointLight(Vector3 mousePosition)
    {
        _flashlight.pointLightOuterRadius = Vector2.Distance(_flashlightRotator.GetChild(0).position, mousePosition);
    }

    private void MoveMaskToMouse(Vector3 mousePosition)
    {
        _visibilityMask.transform.position = mousePosition;
    }

    private void RotateFlashlight(Vector3 mousePosition)
    {
        Vector2 direction = mousePosition - _flashlightRotator.position;
        float angle = Vector2.SignedAngle(Vector2.right, direction);
        _flashlightRotator.rotation = Quaternion.Euler(0, 0, angle);
    }

}
