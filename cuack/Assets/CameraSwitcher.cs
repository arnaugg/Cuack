using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera camera2D;  // Cámara ortográfica (2D)
    public Camera camera3D;  // Cámara en primera persona (FPS)
    public PlayerMovement playerMovement;  // Referencia al script de movimiento del jugador
    public FirstPersonCamera firstPersonCamera; // Referencia al script de cámara en primera persona
    public GameObject puntero;
    public GameObject pared;
    

    void Start()
    {
        // Asegúrate de que la cámara 2D esté activada al inicio y la cámara 3D esté desactivada
        camera2D.enabled = true;
        camera3D.enabled = false;
        puntero.SetActive(false);
        playerMovement.isInFirstPerson = false;
        pared.SetActive(false);

        // Desactivar la rotación de la cámara en 2D
        firstPersonCamera.enabled = false;
    }

    void Update()
    {
        // Cambiar entre cámaras con el clic derecho
        if (Input.GetMouseButton(1))  // Botón derecho del ratón
        {
            if (camera2D.enabled && playerMovement.playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle") == true)
            {

                // Activar cámara en primera persona
                camera2D.enabled = false;
                camera3D.enabled = true;
                puntero.SetActive(true);
                playerMovement.isInFirstPerson = true;
                pared.SetActive(true);
                
                // Habilitar la cámara en 3D
                firstPersonCamera.enabled = true;

                // Habilitamos el efecto Slowmotion
                playerMovement.timeManager.DoSlownmotion();

                // Detener el movimiento horizontal que este realizando en 2D
                playerMovement.rb.velocity = new Vector3(0f, playerMovement.rb.velocity.y, 0f);

            }
            
        }
        else
        {
            // Activar cámara en vista 2D
            camera2D.enabled = true;
            camera3D.enabled = false;
            puntero.SetActive(false);
            playerMovement.isInFirstPerson = false;
            pared.SetActive(false);

            // Desactivar la cámara en 2D
            firstPersonCamera.enabled = false;

            // Fijamos la rotación de la camara para que vuelva a su posicion inicial cuando volvamos a la camara 3D

            firstPersonCamera.transform.rotation = Quaternion.Euler(0f, firstPersonCamera.transform.eulerAngles.y, 0f);

            firstPersonCamera.xRotation = 0f;

            // Revertimos el efecto Slowmotion

            playerMovement.timeManager.DoNormal();

        }
    }
}

