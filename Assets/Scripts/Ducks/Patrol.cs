using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public Vector2 minBounds; // Límite mínimo del área de patrulla
    public Vector2 maxBounds; // Límite máximo del área de patrulla
    public float speed = 5f; // Velocidad de movimiento del objeto
    public float changeDestinationInterval = 5f; // Intervalo para cambiar de destino

    private Vector3 targetPosition; // Posición actual del destino
    public float rotationSpeed = 5f;

    void Start()
    {
        // Inicializar la posición de destino aleatoria dentro del área de patrulla
        targetPosition = GetRandomPosition();
        // Comenzar la rutina para cambiar de destino periódicamente
        StartCoroutine(ChangeDestination());
    }

    void Update()
    {
        // Mover el objeto hacia el destino
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Si el objeto llega al destino, obtener uno nuevo aleatorio
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            targetPosition = GetRandomPosition();
        }

        LookAtTarget();

    }

    // Método para obtener una posición aleatoria dentro del área de patrulla
    Vector3 GetRandomPosition()
    {
        float randomX = Random.Range(minBounds.x, maxBounds.x);
        float randomZ = Random.Range(minBounds.y, maxBounds.y);
        return new Vector3(randomX, transform.position.y, randomZ);
    }

    // Rutina para cambiar de destino periódicamente
    IEnumerator ChangeDestination()
    {
        while (true)
        {
            yield return new WaitForSeconds(changeDestinationInterval);
            targetPosition = GetRandomPosition();
        }
    }

    void LookAtTarget()
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        // Calcula la rotación suavizada
        Quaternion newRotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

        // Aplica la rotación al objeto
        transform.rotation = newRotation;
    }
}
/*
 public class Patroller : MonoBehaviour
{
    public Transform[] patrolPoints; // Puntos de patrulla
    private int currentPatrolIndex = 0; // Índice del punto de patrulla actual
    private NavMeshAgent navMeshAgent; // Referencia al componente NavMeshAgent

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>(); // Obtiene la referencia al componente NavMeshAgent
        SetNextPatrolPoint(); // Establece el primer punto de patrulla
    }

    void Update()
    {
        // Si el objeto llega al punto de patrulla actual, establece el siguiente punto de patrulla
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.1f)
        {
            SetNextPatrolPoint();
        }
    }

    void SetNextPatrolPoint()
    {
        // Establece el siguiente punto de patrulla como destino
        navMeshAgent.destination = patrolPoints[currentPatrolIndex].position;
        // Incrementa el índice del punto de patrulla actual, reiniciándolo si llega al final del array
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
    }
}*/