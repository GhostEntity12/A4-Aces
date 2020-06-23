using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Mesh")]
    public GameObject plane;
    Rigidbody planeRb;
    Transform planeTf;

    [Header("Camera")]
    public Transform cameraPosTf;
    public Transform cameraRotTf;

    [Header("Stats")]
    public float turnSpeed = 100f;
    public float moveSpeed = 2f;
    public float recoverySpeed = 5f;

    [Header("Movement")]
    float angle;
    public Vector2 cameraOffset;

    [Header("Death handling")]
    public bool controllable = true;
    [HideInInspector]
    public Vector3 cacheSpeed;
    [HideInInspector]
    public Vector3 cacheCamStartPos;
    [HideInInspector]
    public Vector3 cacheCamEndPos;
    [HideInInspector]
    public float deathProgress = 0;
    public float deathTime = 3;

    // Start is called before the first frame update
    void Start()
    {
        // Setting variables
        planeRb = plane.GetComponent<Rigidbody>();
        planeTf = plane.transform;
        planeRb.angularDrag = recoverySpeed;
    }

    // Update is called once per frame
    void Update()
    {
        angle = Quaternion.Angle(planeTf.rotation, cameraRotTf.rotation);
    }

    private void FixedUpdate()
    {
        if (controllable)
        {
            cameraPosTf.position = planeTf.position + planeTf.forward * cameraOffset.x + planeTf.up * cameraOffset.y;
            planeTf.rotation = Quaternion.RotateTowards(planeTf.rotation, cameraRotTf.rotation, (turnSpeed * angle * Time.fixedDeltaTime) + 0.1f);
            planeRb.velocity = planeTf.forward * moveSpeed;
        }
        else
        {
            deathProgress += Time.fixedDeltaTime / deathTime;
            if (deathProgress < deathTime)
            {
                cameraPosTf.position = Vector3.MoveTowards(cameraPosTf.position, cacheCamEndPos, 0.7f * Time.fixedDeltaTime);
                planeRb.velocity = planeTf.forward * Mathf.SmoothStep(moveSpeed, 0f, deathProgress);
                planeRb.velocity += Physics.gravity * Mathf.Lerp(0, 1, deathProgress);
                planeRb.useGravity = true;
            }
        }
    }
}
