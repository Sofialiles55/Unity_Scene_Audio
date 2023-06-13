using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovSet : MonoBehaviour
{
    // Una funcion para desplazarse que reciba el desplazamiento en X y en Z
    // El movimiento en X y Z esta determinado por la variable velocidad

    // Una funcion para saltar usando el rigidbody

    public Rigidbody rb;
    
    public float playerVel;
    public float fuerzaSalto;
    bool puedeSaltar = false;

    void Start()
    {
        
        
    }

    
    void Update()
    {
        Movimiento(playerVel * Time.deltaTime);
        Salto(fuerzaSalto);
    }

    

    public void Movimiento(float velocidad)
   {
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(velocidad, 0.0f, 0.0f);
        }
        if(Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-velocidad, 0.0f, 0.0f);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(0.0f, 0.0f, -velocidad);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0.0f, 0.0f, velocidad);
        }
    }

    void Salto(float fuerza)
    {
        if(Input.GetKeyDown(KeyCode.Space) && puedeSaltar)
        {
            rb.AddForce(new Vector3(0.0f, fuerza, 0.0f));
            puedeSaltar = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Suelo"))
        {
            puedeSaltar = true;
        }
    }
    
}
