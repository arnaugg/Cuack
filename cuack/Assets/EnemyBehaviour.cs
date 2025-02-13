using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private void OnTriggerStay(Collider other) 
    { 
        // Verificar si el objeto con el que colisionamos tiene la etiqueta "Enemy"
        if (other.tag == "Attack")
        {
            // Llamar a una función específica
            ReceiveAttack();
            if (other.gameObject.name == "bala(Clone)")
            {
                Destroy(other.gameObject);
            }
        }
    }

    private void ReceiveAttack()
    {
        // Aquí defines lo que pasa con el enemigo
        Debug.Log("Ataque impactó al enemigo: " + gameObject.name);

        // Ejemplo: Destruir el enemigo
        Destroy(gameObject);

        // Puedes añadir más lógica aquí (animaciones, efectos, etc.)
    }
}
