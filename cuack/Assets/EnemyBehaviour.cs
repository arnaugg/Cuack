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
            // Llamar a una funci�n espec�fica
            ReceiveAttack();
            if (other.gameObject.name == "bala(Clone)")
            {
                Destroy(other.gameObject);
            }
        }
    }

    private void ReceiveAttack()
    {
        // Aqu� defines lo que pasa con el enemigo
        Debug.Log("Ataque impact� al enemigo: " + gameObject.name);

        // Ejemplo: Destruir el enemigo
        Destroy(gameObject);

        // Puedes a�adir m�s l�gica aqu� (animaciones, efectos, etc.)
    }
}
