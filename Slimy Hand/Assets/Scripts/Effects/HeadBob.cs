using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBob : MonoBehaviour
{
    [SerializeField] private float frequency = 0.1f;
    [SerializeField] private float amplitude = 0.1f;

    [SerializeField] private float rotAmplitude = 5;

    [SerializeField] private float smoothing = 0.5f;

    [SerializeField] private PlayerController controller;
    [SerializeField] private AnimationCurve amplitudeMultCurve;
    [SerializeField] private AnimationCurve footStepMultCurve;
    [SerializeField] private Footsteps footsteps;

    private Vector3 startPos;
    private float footStepTime;
    private float timer;

    private void Awake()
    {
        startPos = transform.localPosition;
        footStepTime = Mathf.Abs(Mathf.Sin(frequency)) * (Mathf.PI/2);
        timer = footStepTime;
    }
    private void Update()
    {
        float walkingMagnitude = controller.isMovingAndGrounded ? controller.controller.velocity.magnitude : 0;
        float footstepFreq = walkingMagnitude;

        walkingMagnitude *= amplitudeMultCurve.Evaluate(walkingMagnitude/controller.maxSpeed);
        footstepFreq *= footStepMultCurve.Evaluate(footstepFreq / controller.maxSpeed);

        float sin = Mathf.Abs(Mathf.Sin(Time.time * frequency) * amplitude * walkingMagnitude);

        float cos = Mathf.Cos(Time.time * frequency) * rotAmplitude * walkingMagnitude;

        transform.localPosition = Vector3.Lerp(transform.localPosition, startPos + Vector3.up * sin, Time.deltaTime/smoothing);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, 0, cos), Time.deltaTime/smoothing);

        timer -= Time.deltaTime * footstepFreq;

        if(timer <= 0)
        {
            timer = footStepTime;
            //footsteps.PlayFootStep();
        }
    }
}