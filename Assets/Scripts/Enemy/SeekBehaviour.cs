using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SeekBehaviour : SteeringBehaviour
{
    [SerializeField]
    private float targetRechedTreshold = 0.5f;

    [SerializeField]
    private bool showGizmos = true;

    bool reachedLastTarget = true;

    private Vector2 targetPositioncached;
    private float[] interestResultTemp;

    public override (float[] danger, float[] interest) GetSteering(float[] danger, float[] interest, AIData aiData)
    {
        if (reachedLastTarget)
        {
            if(aiData.targets == null || aiData.targets.Count <= 0)
            {
                aiData.currentTarget = null;
                return (danger, interest);
            }
            else
            {
                reachedLastTarget = false;
                aiData.currentTarget = aiData.targets.OrderBy(target => Vector2.Distance(target.position, transform.position)).FirstOrDefault();
            }
        }

        if (aiData.currentTarget != null && aiData.targets != null && aiData.targets.Contains(aiData.currentTarget))
        {
            targetPositioncached = aiData.currentTarget.position;
        }

        if (Vector2.Distance(transform.position, targetPositioncached) < targetRechedTreshold)
        {
            reachedLastTarget = true;
            aiData.currentTarget = null;
            return (danger, interest);
        }

        Vector2 directionToTarget = (targetPositioncached - (Vector2)transform.position);
        for (int i = 0; i < interest.Length; i++)
        {
            float result = Vector2.Dot(directionToTarget.normalized, Directions.eightDirections[i]);

            if ( result > 0 ) 
            {
                float valueToPutIn = result;
                if (valueToPutIn > interest[i])
                {
                    interest[i] = valueToPutIn;
                }
            }
        }
        interestResultTemp = interest;
        return (danger, interest);

    }

    private void OnDrawGizmos()
    {
        if ( ! showGizmos) { return; }

        Gizmos.DrawSphere(targetPositioncached, 0.2f);

        if ( Application.isPlaying && interestResultTemp != null)
        {
            if (interestResultTemp != null)
            {
                Gizmos.color = Color.green;
                for (int i = 0; i < interestResultTemp.Length; i++)
                {
                    Gizmos.DrawRay(transform.position, Directions.eightDirections[i] * interestResultTemp[i]);
                }
                if (reachedLastTarget == false )
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawSphere(targetPositioncached, 0.1f);
                }
            }
        }
    }
}
