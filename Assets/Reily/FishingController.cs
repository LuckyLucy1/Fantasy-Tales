using UnityEngine;

public class FishingController : MonoBehaviour
{
    [Header("References")]
    public Transform playerOrigin;   
    public Transform hook;          
    public Fish fish;           

    [Header("Cast Settings")]
    public float maxCharge = 2f;
    public float maxDistance = 8f;
    public float maxDepth = 5f;
    public float reelSpeed = 2f;

    [Header("Line Settings")]
    public float lineHealth = 3f;
    public float struggleDamageRate = 1f;

    private float currentCharge = 0f;
    private bool charging = false;
    private bool lineCast = false;
    private bool fishHooked = false;
    private bool gameEnded = false;

    private Vector3 targetHookPosition;
    private float currentLineHealth;

    void Start()
    {
        currentLineHealth = lineHealth;
        hook.position = playerOrigin.position;
        targetHookPosition = hook.position;
    }

    void Update()
    {
        if (gameEnded) return;

        if (!lineCast)
        {
            HandleCasting();
        }
        else
        {
            HandleReeling();
            CheckFishCollision();
        }

        if (fishHooked)
        {
            HandleFishFight();
        }

        hook.position = Vector3.MoveTowards(hook.position, targetHookPosition, reelSpeed * Time.deltaTime);
    }

    void HandleCasting()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            charging = true;
            currentCharge = 0f;
        }

        if (Input.GetKey(KeyCode.Space) && charging)
        {
            currentCharge += Time.deltaTime;
            currentCharge = Mathf.Clamp(currentCharge, 0f, maxCharge);
        }

        if (Input.GetKeyUp(KeyCode.Space) && charging)
        {
            charging = false;
            lineCast = true;

            float chargePercent = currentCharge / maxCharge;

            float castX = playerOrigin.position.x - (chargePercent * maxDistance);
            float castY = playerOrigin.position.y - (chargePercent * maxDepth);

            targetHookPosition = new Vector3(castX, castY, 0f);
        }
    }

    void HandleReeling()
    {
        if (Input.GetKey(KeyCode.W))
        {
            targetHookPosition.y += reelSpeed * Time.deltaTime;
            targetHookPosition.x += reelSpeed * 0.5f * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            targetHookPosition.y -= reelSpeed * Time.deltaTime;
            targetHookPosition.x -= reelSpeed * 0.5f * Time.deltaTime;
        }

        targetHookPosition.x = Mathf.Clamp(targetHookPosition.x, playerOrigin.position.x - maxDistance, playerOrigin.position.x);
        targetHookPosition.y = Mathf.Clamp(targetHookPosition.y, playerOrigin.position.y - maxDepth, playerOrigin.position.y);

        if (fishHooked && hook.position.y >= playerOrigin.position.y - 0.1f)
        {
            CatchFish();
        }
    }

    void CheckFishCollision()
    {
        if (fishHooked) return;
        if (fish == null) return;

        float distance = Vector2.Distance(hook.position, fish.transform.position);

        if (distance < 0.5f)
        {
            fishHooked = true;
            fish.HookFish();
        }
    }

    void HandleFishFight()
    {
        if (fish == null) return;

        if (fish.isStruggling && Input.GetKey(KeyCode.W))
        {
            currentLineHealth -= struggleDamageRate * Time.deltaTime;

            if (currentLineHealth <= 0f)
            {
                BreakLine();
            }
        }

        if (fishHooked)
        {
            fish.transform.position = hook.position;
        }
    }

    void CatchFish()
    {
        gameEnded = true;
        Debug.Log("Fish caught!");
        fish.CaughtFish();
    }

    void BreakLine()
    {
        gameEnded = true;
        fishHooked = false;
        Debug.Log("Line broke!");
        fish.LineBroken();
    }
}