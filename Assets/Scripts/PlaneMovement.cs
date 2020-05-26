using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMovement : MonoBehaviour
{
    public GameObject plane;
    Rigidbody planeRb;
    Transform planeTf;

    public Transform cameraPosTf;
    public Transform cameraRotTf;

    public float deadzoneRadius = 0.1f;
    public float turnSpeed = 10f;
    public float moveSpeed = 2f;

    Vector3 currentVelocity = Vector3.zero;
    Vector3 rotDirection;
    Quaternion rotDiff;

    // Start is called before the first frame update
    void Start()
    {
        planeRb = plane.GetComponent<Rigidbody>();
        planeTf = plane.transform;
    }

    // Update is called once per frame
    void Update()
    {
        rotDiff = planeTf.rotation * Quaternion.Inverse(cameraRotTf.rotation);
        Quaternion absRotDiff = new Quaternion(Mathf.Abs(cameraPosTf.rotation.x - rotDiff.x), Mathf.Abs(cameraPosTf.rotation.y - rotDiff.y), Mathf.Abs(cameraPosTf.rotation.z - rotDiff.z), 0); //cameraPosTf.rotation.w - rotDiff.w);
        //print(rotDiff.ToString());
        print(absRotDiff.ToString());
    }

    private void FixedUpdate()
    {
        cameraPosTf.position = planeTf.position - planeTf.forward * 0.5f + planeTf.up * 0.2f;
        planeTf.rotation = Quaternion.RotateTowards(planeTf.rotation, cameraRotTf.rotation, turnSpeed * Time.fixedDeltaTime);
        planeRb.velocity = planeTf.forward * moveSpeed;
    }
}
