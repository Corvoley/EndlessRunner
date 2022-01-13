using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBehaviourMagnet : PowerUpBehaviour
{
    [SerializeField] private float attractSpeed = 8;
    [SerializeField] private float scaleSpeed = 2;
    [SerializeField] private float finalScaleMultiplier = 0.3f;
    [SerializeField] private Vector3 attractionBox = Vector3.one * 10;

    private List<Collectable> collectablesToAttract = new List<Collectable>();
    private Collider[] overlapResults = new Collider[20];

    public void Activate(float duration)
    {
        ActivateForDuration(duration);
    }

    protected override void StartBehaviour()
    {
        collectablesToAttract.Clear();
    }

    protected override void UpdateBehaviour()
    {
        GatherCollectablesInRange();
        foreach (Collectable collectable in collectablesToAttract)
        {
            if (collectable != null)
            {
                Vector3 startPos = collectable.transform.position;
                Vector3 endPos = transform.position;
                collectable.transform.position = Vector3.MoveTowards(startPos, endPos, Time.deltaTime * attractSpeed);

                Vector3 startScale = collectable.transform.localScale;
                Vector3 endScale = Vector3.one * finalScaleMultiplier;

                collectable.transform.localScale = Vector3.MoveTowards(startScale, endScale, Time.deltaTime * scaleSpeed);
            }

        }
    }

    protected override void EndBehaviour()
    {
        collectablesToAttract.Clear();
    }

    private void GatherCollectablesInRange()
    {
        int overlapCount = Physics.OverlapBoxNonAlloc(transform.position, attractionBox, overlapResults);
        for (int i = 0; i < overlapCount; i++)
        {
            Collectable collectable = overlapResults[i].GetComponent<Collectable>();
            if (collectable !=null &&                                
                !(collectable is PowerUp) && 
                !collectablesToAttract.Contains(collectable))
            {
                collectablesToAttract.Add(collectable);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, attractionBox);
    }
}
