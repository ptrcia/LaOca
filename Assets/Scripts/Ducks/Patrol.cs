using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public Vector2 minBounds; // L�mite m�nimo del �rea de patrulla
    public Vector2 maxBounds; // L�mite m�ximo del �rea de patrulla
    public float speed = 5f; // Velocidad de movimiento del objeto
    public float changeDestinationInterval = 5f; // Intervalo para cambiar de destino

    private Vector3 targetPosition; // Posici�n actual del destino

    void Start()
    {
        // Inicializar la posici�n de destino aleatoria dentro del �rea de patrulla
        targetPosition = GetRandomPosition();
        // Comenzar la rutina para cambiar de destino peri�dicamente
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

    // M�todo para obtener una posici�n aleatoria dentro del �rea de patrulla
    Vector3 GetRandomPosition()
    {
        float randomX = Random.Range(minBounds.x, maxBounds.x);
        float randomZ = Random.Range(minBounds.y, maxBounds.y);
        return new Vector3(randomX, transform.position.y, randomZ);
    }

    // Rutina para cambiar de destino peri�dicamente
    System.Collections.IEnumerator ChangeDestination()
    {
        while (true)
        {
            yield return new WaitForSeconds(changeDestinationInterval);
            targetPosition = GetRandomPosition();
        }
    }
}
