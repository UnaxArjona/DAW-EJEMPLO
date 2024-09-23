using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NouJugador : MonoBehaviour
{

    private float _vel;


    // Start is called before the first frame update
    void Start()
    {
        _vel = 8f;




    }

    // Update is called once per frame
    void Update()
    {


        float direccioindicadaX = Input.GetAxisRaw("Horizontal");
        float direccioindicadaY = Input.GetAxisRaw("Vertical");
        //Debug.Log("x: " + direccioindicadaX + "y: " + direccioindicadaY);
        Vector2 direccioindicada = new Vector2(direccioindicadaX, direccioindicadaY);
        
        Vector2 novaPos =transform.position;//transform.position pos actual de la nau
        novaPos=novaPos+direccioindicada*_vel*Time.deltaTime;
        //Debug.Log(Time.deltaTime);
        transform.position = novaPos;

    }
}
