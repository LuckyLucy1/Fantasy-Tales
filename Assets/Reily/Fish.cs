using UnityEngine;

public class Fish : MonoBehaviour
{
    public bool isStruggling;

    [Header("State Timing")]
    public float minStateTime = 1f;
    public float maxStateTime = 2.5f;

    [Header("Colours")]
    public SpriteRenderer sr;
    public Color restColor = Color.blue;
    public Color struggleColor = Color.red;

    private bool hooked = false;
    private float timer = 0f;
    private float nextSwitchTime = 1f;

    void Start()
    {
        SetRestState();
        nextSwitchTime = Random.Range(minStateTime, maxStateTime);
    }

    void Update()
    {
        if (!hooked) return;

        timer += Time.deltaTime;

        if (timer >= nextSwitchTime)
        {
            timer = 0f;
            nextSwitchTime = Random.Range(minStateTime, maxStateTime);

            if (isStruggling)
                SetRestState();
            else
                SetStruggleState();
        }
    }

    public void HookFish()
    {
        hooked = true;
        timer = 0f;
    }

    public void CaughtFish()
    {
        gameObject.SetActive(false);
        Debug.Log("Play catch animation here");
    }

    public void LineBroken()
    {
        Debug.Log("Fish escaped");
    }

    void SetRestState()
    {
        isStruggling = false;
        if (sr != null) sr.color = restColor;
    }

    void SetStruggleState()
    {
        isStruggling = true;
        if (sr != null) sr.color = struggleColor;
    }
}