using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    public Transform target; // El jugador o el objeto que la cámara seguirá
    public Vector3 offset = new Vector3(0f, 2f, -10f); // Offset inicial de la cámara respecto al jugador
    public float smoothSpeed = 0.125f; // Velocidad de suavizado del movimiento de la cámara

    void LateUpdate()
    {
        if (target != null)
        {
            // Calcula la posición deseada
            Vector3 desiredPosition = target.position + offset;

            // Suaviza el movimiento de la cámara
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Asigna la posición suavizada a la cámara
            transform.position = smoothedPosition;
        }
    }
}

