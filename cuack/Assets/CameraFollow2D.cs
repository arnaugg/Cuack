using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    public Transform target; // El jugador o el objeto que la c�mara seguir�
    public Vector3 offset = new Vector3(0f, 2f, -10f); // Offset inicial de la c�mara respecto al jugador
    public float smoothSpeed = 0.125f; // Velocidad de suavizado del movimiento de la c�mara

    void LateUpdate()
    {
        if (target != null)
        {
            // Calcula la posici�n deseada
            Vector3 desiredPosition = target.position + offset;

            // Suaviza el movimiento de la c�mara
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Asigna la posici�n suavizada a la c�mara
            transform.position = smoothedPosition;
        }
    }
}

