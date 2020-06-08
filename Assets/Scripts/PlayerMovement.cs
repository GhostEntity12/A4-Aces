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
    Vector3 camCache;

    [Header("Stats")]
    public float turnSpeed = 100f;
    public float moveSpeed = 2f;
    public float recoverySpeed = 5f;

    [Header("Movement")]
    Quaternion rotDiff;
    float size;

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
    public float deathTime = 5;

    // Start is called before the first frame update
    void Start()
    {
        // Setting variables
        planeRb = plane.GetComponent<Rigidbody>();
        planeTf = plane.transform;
        camCache = cameraPosTf.transform.position - planeTf.position;
        planeRb.angularDrag = recoverySpeed;
    }

    // Update is called once per frame
    void Update()
    {
        rotDiff = planeTf.rotation * Quaternion.Inverse(cameraRotTf.rotation);
        Quaternion absRotDiff = new Quaternion(Mathf.Abs(cameraPosTf.rotation.x - rotDiff.x), Mathf.Abs(cameraPosTf.rotation.y - rotDiff.y), Mathf.Abs(cameraPosTf.rotation.z - rotDiff.z), 0); //cameraPosTf.rotation.w - rotDiff.w);
        size = absRotDiff.x + absRotDiff.y + absRotDiff.z;
    }

    private void FixedUpdate()
    {
        if (controllable)
        {
            cameraPosTf.position = planeTf.position + planeTf.forward * camCache.z + planeTf.up * camCache.y;
            planeTf.rotation = Quaternion.RotateTowards(planeTf.rotation, cameraRotTf.rotation, turnSpeed * size * Time.fixedDeltaTime);
            planeRb.velocity = planeTf.forward * moveSpeed;
        }
        else
        {
            deathProgress += Time.fixedDeltaTime / deathTime;
            if (deathProgress < deathTime)
            {
                print(planeRb.velocity);
                cameraPosTf.position = Vector3.MoveTowards(cameraPosTf.position, cacheCamEndPos, 0.7f * Time.fixedDeltaTime);
                planeRb.velocity = planeTf.forward * Mathf.SmoothStep(moveSpeed, 0f, deathProgress);
                planeRb.velocity += Physics.gravity * Mathf.Lerp(0, 1, deathProgress);
                planeRb.useGravity = true;
            }
        }
    }
}
