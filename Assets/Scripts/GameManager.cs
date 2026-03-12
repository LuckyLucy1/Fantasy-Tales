using UnityEngine;

public class GameManager : MonoBehaviour
{
    // The static reference to the single instance
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            // If another instance already exists, destroy this duplicate
            Destroy(this.gameObject);
        }
        else
        {
            // Otherwise, set the instance to this object
            Instance = this;
            // Optional: Keep the object alive across scene loads
            // DontDestroyOnLoad(this.gameObject); 
        }
    }

    public void IDidThing()
    {
        Debug.Log("Accessed manager");
    }

    // Example method that can be accessed globally
    public void PlayerDied()
    {
        Debug.Log("Player died. Game Over!");
    }
}