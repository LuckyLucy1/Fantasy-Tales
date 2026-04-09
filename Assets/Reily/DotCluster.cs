using UnityEngine;
using UnityEngine.SceneManagement;

public class DotCluster : MonoBehaviour
{
    [SerializeField] private GameObject dotPrefab;
    [SerializeField] private int numberOfDots = 5;
    [SerializeField] private Vector2 spawnArea = new Vector2(4f, 4f);
    [SerializeField] private string sceneToLoad;

    private int remainingDots;

    private void Start()
    {
        remainingDots = numberOfDots;
        SpawnDots();
    }

    private void SpawnDots()
    {
        for (int i = 0; i < numberOfDots; i++)
        {
            Vector2 randomOffset = new Vector2(
                Random.Range(-spawnArea.x * 0.5f, spawnArea.x * 0.5f),
                Random.Range(-spawnArea.y * 0.5f, spawnArea.y * 0.5f)
            );

            Vector3 spawnPosition = new Vector3(
                transform.position.x + randomOffset.x,
                transform.position.y + randomOffset.y,
                -1.4f
            );

            GameObject newDot = Instantiate(dotPrefab, spawnPosition, Quaternion.identity, transform);

            Dot dotScript = newDot.GetComponent<Dot>();
            if (dotScript != null)
            {
                dotScript.SetOwner(this);
            }
        }
    }

    public void DotClicked(GameObject clickedDot)
    {
        Destroy(clickedDot);
        remainingDots--;

        if (remainingDots <= 0)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}