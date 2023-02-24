using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 300f;
    [SerializeField] float rotationThrust = 400f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem thrustParticles;
    Rigidbody rb;
    AudioSource audioSource;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust() {
        if (Input.GetKey(KeyCode.Space)) {
            if(!thrustParticles.isPlaying) {
                thrustParticles.Play();
            }
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            if (!audioSource.isPlaying) {
                audioSource.PlayOneShot(mainEngine);
            }
        } else {
            audioSource.Stop();
        }
    }

    void ProcessRotation() {

        if (Input.GetKey(KeyCode.A)) {
            ApplyRotation(rotationThrust);
        } else if (Input.GetKey(KeyCode.D)) {
            ApplyRotation(-rotationThrust);
        }

        void ApplyRotation(float rotationThisFrame) {
            rb.freezeRotation = true; // 물리엔진이 주는 로테이션 고정
            transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
            rb.freezeRotation = false; // 물리엔진이 주는 로테이션 고정 해제
        }
    }
}
