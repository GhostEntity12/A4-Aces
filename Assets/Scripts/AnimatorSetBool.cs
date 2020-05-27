using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorSetBool : MonoBehaviour
{
    public Animator animator;
    public string boolName;

    public void SetBool(bool value)
    {
        animator.SetBool(boolName, value);
    }

    private void Awake()
    {
        transform.LookAt(Camera.main.transform.position);
    }
}
