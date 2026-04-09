using UnityEngine;

public class Dot : MonoBehaviour
{
    private DotCluster owner;

    public void SetOwner(DotCluster cluster)
    {
        owner = cluster;
    }

    private void OnMouseDown()
    {
        if (owner != null)
        {
            owner.DotClicked(gameObject);
        }
    }
}