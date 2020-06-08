using UnityEngine;

/// <summary>
/// Extension class for using EventTriggers to trigger animations
/// </summary>
public class AnimatorSetBool : MonoBehaviour
{
    [Tooltip("The animator on which the bool should be toggled")]
    public Animator animator;
    [Tooltip("The name of the bool to change")]
    public string boolName;

    /// <summary>
    /// Toggles a bool on a given animator
    /// </summary>
    /// <param name="value">The value to set the bool to</param>
    public void SetBool(bool value)
    {
        animator.SetBool(boolName, value);
    }
}
