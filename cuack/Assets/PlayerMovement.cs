using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed2d;
    public float moveSpeed3d;
    public float jumpForce;
    public GameObject bala;
    public Transform spawnbala;
    public float shootForce = 1500;
    public float shotRate = 0.5f;
    public float bulletDespawn = 0.5f;
    public TimeManager timeManager;

    private float shotRateTime = 0;

    public bool isInFirstPerson = false;  // Indica si estamos en modo 3D (primera persona)
    public Transform playerCamera;  // La cámara que usas para la vista en primera persona (referencia a la cámara FPS)

    public Animator playerAnimator;
    public string SwordAttackTriggerName;

    public float dashSpeed2d;
    public float dashSpeed3d;
    public float dash2dEffectTime;
    public float dashCooldown2d;
    public float dashCooldown3d;
    
    private bool isDashing;
    private bool ableToDash;

    [HideInInspector] public Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isDashing = false;
        ableToDash = true;
    }

    void Update()
    {
        // Obtener la entrada del ratón y del teclado
        float moveX = Input.GetAxis("Horizontal");  // Movimiento en el eje X (izquierda/derecha)
        float moveY = Input.GetAxis("Jump");  // Salto (eje Y, barra espaciadora)
        //animator.SetBool("Atacando", animator);
        float dashing = Input.GetAxis("Dash");

        if (!isInFirstPerson)
        {
            // Modo 2D: No permitimos rotación, solo movimiento lateral en el eje X 

            if (isDashing == false)
            {

                if (dashing != 1 || ableToDash == false || moveX == 0)
                {
                    rb.velocity = new Vector3(moveX * moveSpeed2d, rb.velocity.y, 0f);
                }

                else
                {
                    // Si el personaje no esta saltando y el cooldown del dash no esta en efecto le aplicamos el dash y el cooldown
                    isDashing = true;
                    ableToDash = false;
                    rb.velocity = new Vector3(moveX * moveSpeed2d * dashSpeed2d, rb.velocity.y, 0f);
                    StartCoroutine("Dash2DCooldown");

                }
            }

            // Fijamos la rotación del personaje en la dirección correcta segun la dirección en la que se este desplazando

            if (moveX > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            else if (moveX < 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }

            // Si el personaje ha hecho una rotación en 3D, le damos una rotación especifica en 2D segun la dirección a la que este mirando 

            else
            {
                if (transform.rotation.eulerAngles.y > -90 && transform.rotation.eulerAngles.y < 90)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                }
            }

            if (Input.GetButtonDown("Fire1"))
            {

                if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle") == true)
                {
                    playerAnimator.SetTrigger(SwordAttackTriggerName);
                }
                
            }
        }

        else
        {
            // Modo 3D: Permitimos el movimiento y la rotación en todos los ejes
            float moveZ = Input.GetAxis("Vertical");  // Movimiento hacia adelante/atrás

            if (isDashing == false)
            {
                if (dashing != 1 || ableToDash == false || (moveX == 0 && moveZ == 0))
                {
                    transform.Translate(new Vector3(moveZ * moveSpeed3d * Time.timeScale * Time.deltaTime, 0f, -moveX * moveSpeed3d * Time.timeScale * Time.deltaTime));
                }

                else
                {
                    // Si el personaje no esta saltando y el cooldown del dash no esta en efecto le aplicamos el dash y el cooldown
                    isDashing = true;
                    ableToDash = false;
                    transform.Translate(new Vector3(moveZ * moveSpeed3d * Time.timeScale * Time.deltaTime * dashSpeed3d, 0f, -moveX * moveSpeed3d * Time.timeScale * Time.deltaTime * dashSpeed3d));
                    StartCoroutine("Dash3DCooldown");

                }
            }

            // Codigo del disparo

            if (Input.GetButtonDown("Fire1"))
            {
                if (Time.time > shotRateTime)
                {
                    GameObject newbala;

                    newbala = Instantiate(bala, spawnbala.position, spawnbala.rotation);

                    newbala.GetComponent<Rigidbody>().AddForce(spawnbala.forward * shootForce);

                    shotRateTime = Time.time + shotRate;

                    Destroy(newbala, 2);
                }
            }
        }

        if (moveY > 0 && (rb.velocity.y < 0.001f && rb.velocity.y >= -0.001f))
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, 0f);
        }
    }

    IEnumerator Dash2DCooldown()
    {
        yield return new WaitForSeconds(dash2dEffectTime);
        rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown2d);
        ableToDash = true;
        yield return null;
    }

    IEnumerator Dash3DCooldown()
    {
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown3d);
        ableToDash = true;
        yield return null;
    }

}