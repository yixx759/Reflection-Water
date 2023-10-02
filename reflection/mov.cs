using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mov : MonoBehaviour
{


    [SerializeField, Range(0f, 100f)]
    float Maxspeed = 10f;
    [SerializeField, Range(0f, 100f)]
    float Maxacceleration = 10f;
    Vector3 velocity, desiredvelocity;
    Quaternion Rot;
    Rigidbody body;
    // Start is called before the first frame update
    void Start()
    {
        
      
        Rot = transform.rotation;
        velocity = Vector3.zero;
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector2 playerinput;
        playerinput.x = Input.GetAxis("Horizontal");
        playerinput.y = Input.GetAxis("Vertical");
        playerinput = Vector2.ClampMagnitude(playerinput, 1f);
        desiredvelocity = new Vector3(playerinput.x, 0, playerinput.y) * Maxspeed;
       
       
    }
    private void FixedUpdate()
    {
     

            velocity = body.velocity;
            Rot = body.rotation;
            float maxspeedchange = Maxacceleration * Time.deltaTime;
            velocity.x = Mathf.MoveTowards(velocity.x, desiredvelocity.x, maxspeedchange);
            velocity.z = Mathf.MoveTowards(velocity.z, desiredvelocity.z, maxspeedchange);

            body.velocity = velocity;
            // if (desiredvelocity != Vector3.zero)
            // {
            //     Rot = Quaternion.LookRotation(body.velocity, body.transform.up);
            //     body.rotation = Rot;
            // }

            Debug.DrawLine(transform.position, transform.position + transform.forward, Color.red);
        
    }


   



}
