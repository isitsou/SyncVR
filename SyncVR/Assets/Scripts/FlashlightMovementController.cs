using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class FlashlightMovementController : MonoBehaviour
{
    [Header("Cached Refs")]
    [SerializeField] private Transform _flashlightRotator;
    [SerializeField] private Light2D _flashlight;
    [SerializeField] private Transform _centerOfRotation;
    [SerializeField] private GameObject _visibilityMask;
 

    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = Camera.main.transform.position.z + Camera.main.nearClipPlane;

        MoveMaskToMouse(mousePosition);

        RotateFlashlight(mousePosition);

        _flashlight.pointLightOuterRadius = Vector2.Distance(_flashlightRotator.GetChild(0).position, mousePosition);
    }

    private void MoveMaskToMouse(Vector3 mousePosition)
    {
        _visibilityMask.transform.position = mousePosition;
    }

    private void RotateFlashlight(Vector3 mousePosition)
    {
        Vector2 direction = mousePosition - _centerOfRotation.position;
        float angle = Vector2.SignedAngle(Vector2.right, direction);
        _flashlightRotator.rotation = Quaternion.Euler(0, 0, angle);
    }

}
