
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour
{

    public float scrollSpeed;
    public float starSpeed;
    public float tileSizeZ;
    private Vector3 startPosition;
    public GameController gameController;
    public GameObject starfield;
    public ParticleSystem starfield1;
    public ParticleSystem starfield2;



    // Use this for initialization
    void Start()
    {
       





        startPosition = transform.position;

        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }

    // Update is called once per frame
    void Update()
    {
       
        if (gameController.score >= 100)
        {
            scrollSpeed += -0.005f;

            starfield1 = GameObject.FindWithTag("starfield1").GetComponent<ParticleSystem>();

            var main1 = starfield1.main;
            main1.startSpeed = new ParticleSystem.MinMaxCurve(20.0f, 50.0f);

            starfield2 = GameObject.FindWithTag("starfield2").GetComponent<ParticleSystem>();

            var main2 = starfield2.main;
            main2.startSpeed = new ParticleSystem.MinMaxCurve(20.0f, 50.0f);

        }
        float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeZ);
        transform.position = startPosition + Vector3.forward * newPosition;

    }
}