using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]

public class Attractor : MonoBehaviour
{

    const float G = 2f;
    public Vector3 vel;

    public Rigidbody rb;

    private Button starter { get { return GetComponent<Button>(); } }
    public bool on;
    
    void Start()
    {
        //rb.velocity = new Vector3(0f,0f,0f);
        rb.velocity = vel;
        on = true;
        starter.onClick.AddListener(() => sim_on());
    }

    void sim_on()
    {
        Debug.Log("click workings");
        on = !on;
        rb.velocity = new Vector3(0f, 0f, 0f);

    }


    private void FixedUpdate()
    {

        if (on == true)
        {
            
            Attractor[] attractors = FindObjectsOfType<Attractor>();
            foreach (Attractor attractor in attractors)
            {
                if (attractor != this)
                {
                    Attract(attractor);
                }

            }
        }
        
    }

    void Attract(Attractor objToAttract)
    {
        //rb.velocity = vel;
        Rigidbody rbToAttract = objToAttract.rb;

        Vector3 direction = rb.position - rbToAttract.position;
        float distance = direction.magnitude;

        float forceMagnitude = G * (rb.mass * rbToAttract.mass) / Mathf.Pow(distance, 2);
        Vector3 force = direction.normalized * forceMagnitude;

        rbToAttract.AddForce(force);

        // Application.LoadLevel(Application.loadedLevel);

    }


}
