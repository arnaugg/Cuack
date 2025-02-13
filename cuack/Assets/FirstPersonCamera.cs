using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public float lookSpeedX = 2f;
    public float lookSpeedY = 2f;
    public Transform playerBody;  // Referencia al cuerpo del jugador (usualmente la c�psula)
    //public float distanciaDisparo = 50.0f;
    //public float fuerzaDisparo = 15.0f;
    [HideInInspector] public float xRotation = 0f;

    void Start()
    {
        // Aseguramos que la c�mara est� correctamente alineada al cuerpo del jugador
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // [MODIFICACI�N]: Esta parte del codigo se tiene que borrar
        /*
        // Alineamos la c�mara solo en el eje Y con la rotaci�n del cuerpo del jugador
        transform.localRotation = Quaternion.Euler(0f, playerBody.eulerAngles.y, 0f); // Esto asegura que la c�mara no se incline hacia un lateral
        */
        // [FIN DE LA MODIFICACI�N]

    }

    void Update()
    {
        if (playerBody != null)
        {
            // Obtener la entrada del rat�n para la rotaci�n de la c�mara
            float mouseX = Input.GetAxis("Mouse X") * lookSpeedX;
            float mouseY = Input.GetAxis("Mouse Y") * lookSpeedY;

            // Limitar la rotaci�n vertical (evitar que la c�mara se voltee hacia arriba o hacia abajo)
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);  // Limitar la rotaci�n vertical

            // Aplicar la rotaci�n de la c�mara

            // [MODIFICACI�N]: La formula para definir la rotaci�n tiene que cambiar ligeramente
            
            // transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);  // Solo rotamos en el eje X (vertical)
            
            transform.localRotation = Quaternion.Euler(xRotation, transform.localEulerAngles.y, 0f);  // Solo rotamos en el eje X (vertical)

            // [FIN DE LA MODIFICACI�N]

            // Rotaci�n del cuerpo del jugador (solo en el eje Y, para mirar horizontalmente)
            playerBody.Rotate(Vector3.up * mouseX);  // Rotar el cuerpo del jugador (en Y)

        }


        
    }

        /*if (Input.GetMouseButtonDown(0))
        {
            RaycastHit impacto;

            Physics.Raycast(transform.position, transform.forward, out impacto, distanciaDisparo);

            if (impacto.rigidbody != null)
            {
                impacto.rigidbody.AddForceAtPosition(-impacto.normal * fuerzaDisparo, impacto.point, ForceMode.Impulse);
            }
        }*/
    
}





