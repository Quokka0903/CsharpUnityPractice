using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [Header("General Getup Settings")]
    [Tooltip("How fast 비행선 컨트롤")]
    [SerializeField] float controlSpeed = 40f;
    [SerializeField] float xRange = 16f;
    [SerializeField] float yRange = 9f;

    [SerializeField] float positionPitchFactor = 2f;
    [SerializeField] float positionYawFactor = 2f;
    [SerializeField] float controlPitchFactor = -20f;
    [SerializeField] float controlRollFactor = -20f;

    [SerializeField] GameObject[] lasers;

    float xThrow, yThrow;

    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();

    }

    void ProcessRotation() {

        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;
        float yawDueToPosition = transform.localPosition.x * positionYawFactor;
        float rollDueToControlThrow = xThrow * controlRollFactor;

        float pitch = pitchDueToPosition + pitchDueToControlThrow;
        float yaw = yawDueToPosition;
        float roll = rollDueToControlThrow;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);

    }

    void ProcessTranslation() {
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");

        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float yOffset = yThrow * Time.deltaTime * controlSpeed;


        float rawXPos = transform.localPosition.x + xOffset;
        float rawYPos = transform.localPosition.y + yOffset;

        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3 (
            clampedXPos,
            clampedYPos, 
            transform.localPosition.z
            );
    }

    void ProcessFiring() {
        // 스페이스 누르면 발싸
        if (Input.GetButton("Fire1")) {
            SetLasersActive(true);
        } else {
            SetLasersActive(false);
        }
    }

    void SetLasersActive(bool isActive) {

        foreach (GameObject laser in lasers)
        {
            // laser.SetActive(true);   
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }

}
