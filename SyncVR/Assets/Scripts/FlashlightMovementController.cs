using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightMovementController : MonoBehaviour
{
    [Header("Cached Refs")]
    [SerializeField] private Transform _flashlight;
    [SerializeField] private Transform _centerOfRotation;
    [SerializeField] private GameObject _visibilityMask;



    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = Camera.main.transform.position.z + Camera.main.nearClipPlane;


        _visibilityMask.transform.position = mousePosition;

        Vector2 direction = mousePosition - _centerOfRotation.position;
        float angle = Vector2.SignedAngle(Vector2.right, direction);
        _flashlight.rotation = Quaternion.Euler(0, 0, angle);
    }
}
