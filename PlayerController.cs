using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]


public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float tilt;
    public Boundary boundary;

    private Rigidbody rb;

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    public int powerUp;
    public int shield;
    public GameObject shieldobject;
    

    private float nextFire;
    public AudioSource musicSource;
    public AudioClip musicClipOne;
    public float fireRateMod;




    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        shieldobject = GameObject.Find("Shield");
    }

    void Update()
    {
        if (powerUp > 0) 
        {
            fireRateMod = 0.1f;
        }
        else
        {
            fireRateMod = 0f;
        }

        if (shield > 0)
        {
            shieldobject.active = true;
        }
        else
        {
            shieldobject.active = false;
        }
        

        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + (fireRate - fireRateMod);
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            musicSource.clip = musicClipOne;
            musicSource.Play();

        }

        powerUp = Mathf.Clamp(powerUp - 1, 0, 300);
        shield = Mathf.Clamp(shield - 1, 0, 300);
    }
    

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.velocity = movement * speed;

        rb.position = new Vector3
        (
             Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
             0.0f,
             Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
        );

        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
    }
}