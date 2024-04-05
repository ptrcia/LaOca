using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DucksPatrol : MonoBehaviour

{
    [Header("Patrol")]
    [SerializeField] Transform[] checkPoints;

    private NavMeshAgent agent;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        StartCoroutine(nameof(Patrol));
    }
    IEnumerator Patrol()
    {

        #region Patrol Random

        Vector3 destination = transform.position;
        while (true)
        {
            while (Vector3.Distance(transform.position, destination) > agent.stoppingDistance)
            {
                yield return null;
            }

            yield return new WaitForSeconds(Random.Range(1, 3));
            //cojo punto aleatorio dentro de una esfera fija
            Vector3 randomPoint = checkPoints[0].position + (Random.insideUnitSphere * 5);
            NavMeshHit hit;
            NavMesh.SamplePosition(randomPoint, out hit, 10, 1);
            destination = hit.position;
            agent.SetDestination(destination);
        }
        #endregion
    }
}
