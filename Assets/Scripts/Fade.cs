using System.Collections;
using UnityEngine;

public class Fade : MonoBehaviour
{
    public static Fade instance;

    private void Awake()
    {
        instance = this;
    }

    //public static IEnumerator FadeElement(TextMeshPro tmp, float lerpTime, float start, float end, float delay = 0f)
    //{
    //    yield return new WaitForSeconds(delay);

    //    float timeAtStart = Time.time;
    //    float timeSinceStart;
    //    float percentageComplete = 0;

    //    while (percentageComplete < 1) // Keeps looping until the lerp is complete
    //    {
    //        timeSinceStart = Time.time - timeAtStart;
    //        percentageComplete = timeSinceStart / lerpTime;

    //        float currentValue = Mathf.Lerp(start, end, percentageComplete);


    //        tmp.alpha = currentValue;
    //        yield return new WaitForEndOfFrame();
    //    }
    //}

    public static IEnumerator FadeElement(CanvasGroup cg, float lerpTime, float start, float end, float delay = 0f)
    {
        yield return new WaitForSeconds(delay);

        float timeAtStart = Time.time;
        float timeSinceStart;
        float percentageComplete = 0;

        while (percentageComplete < 1) // Keeps looping until the lerp is complete
        {
            timeSinceStart = Time.time - timeAtStart;
            percentageComplete = timeSinceStart / lerpTime;

            float currentValue = Mathf.Lerp(start, end, percentageComplete);

            cg.alpha = currentValue;
            yield return new WaitForEndOfFrame();
        }
    }

    public static IEnumerator FadeElement(Material m, float lerpTime, float start, float end, float delay = 0f)
    {
        yield return new WaitForSeconds(delay);

        float timeAtStart = Time.time;
        float timeSinceStart;
        float percentageComplete = 0;

        while (percentageComplete < 1) // Keeps looping until the lerp is complete
        {
            timeSinceStart = Time.time - timeAtStart;
            percentageComplete = timeSinceStart / lerpTime;

            float currentValue = Mathf.Lerp(start, end, percentageComplete);

            m.color = new Color(m.color.r, m.color.g, m.color.b, currentValue);
            yield return new WaitForEndOfFrame();
        }
    }

    public static IEnumerator FadeElement(AudioSource audio, float lerpTime, float start, float end, float delay = 0f)
    {
        yield return new WaitForSeconds(delay);

        float timeAtStart = Time.time;
        float timeSinceStart;
        float percentageComplete = 0;

        while (percentageComplete < 1) // Keeps looping until the lerp is complete
        {
            timeSinceStart = Time.time - timeAtStart;
            percentageComplete = timeSinceStart / lerpTime;

            float currentValue = Mathf.Lerp(start, end, percentageComplete);

            audio.volume = currentValue;
            yield return new WaitForEndOfFrame();
        }
    }
}
