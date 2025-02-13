using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour
{



    public GameObject bala;
    public Transform spawnbala;

    public float shootForce = 1500;
    public float shotRate = 0.5f;

    private float shotRateTime = 0;




    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (Time.time > shotRateTime)
            {
                GameObject newbala;

                newbala = Instantiate(bala,spawnbala.position,spawnbala.rotation);

                newbala.GetComponent<Rigidbody>().AddForce(spawnbala.forward * shootForce);

                shotRateTime = Time.time + shotRate;

                Destroy(newbala, 2);
            }
        }
    }
}
