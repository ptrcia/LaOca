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
    }

    // Método para obtener una posición aleatoria dentro del área de patrulla
    Vector3 GetRandomPosition()
    {
        float randomX = Random.Range(minBounds.x, maxBounds.x);
        float randomZ = Random.Range(minBounds.y, maxBounds.y);
        return new Vector3(randomX, transform.position.y, randomZ);
    }

    // Rutina para cambiar de destino periódicamente
    System.Collections.IEnumerator ChangeDestination()
    {
        while (true)
        {
            yield return new WaitForSeconds(changeDestinationInterval);
            targetPosition = GetRandomPosition();
        }
    }
}
