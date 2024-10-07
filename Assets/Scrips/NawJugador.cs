using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NouJugador : MonoBehaviour
{

    private float _vel;
   
    Vector2 minPantalla, maxPantalla;
    [SerializeField] private GameObject prefabProjectil;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        _vel = 8f;
        minPantalla = Camera.main.ViewportToWorldPoint(new Vector2(0,0));
        maxPantalla = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        
        float meitatMidaImatgeX =GetComponent<SpriteRenderer>().sprite.bounds.size.x * transform.localScale.x /2;
        float meitatMidaImatgeY =GetComponent<SpriteRenderer>().sprite.bounds.size.y * transform.localScale.y /2;


        //horizontal
        minPantalla.x = minPantalla.x + meitatMidaImatgeX;
        maxPantalla.x = maxPantalla.x - meitatMidaImatgeX;

        //vertical 

        minPantalla.y = minPantalla.y + meitatMidaImatgeY;
        maxPantalla.y = maxPantalla.y - meitatMidaImatgeY;



    }

    // Update is called once per frame
    void Update()
    {
        MoureNau();
        DisparaProjectil();
    }


    private void DisparaProjectil()
    {
        if(Input.GetKeyDown("space")){

            GameObject Projectil = Instantiate(prefabProjectil);
            Projectil.transform.position =  transform.position;
        }



    }

    private void MoureNau()
    {

        float direccioindicadaX = Input.GetAxisRaw("Horizontal");
        float direccioindicadaY = Input.GetAxisRaw("Vertical");
        //Debug.Log("x: " + direccioindicadaX + "y: " + direccioindicadaY);
        Vector2 direccioindicada = new Vector2(direccioindicadaX, direccioindicadaY).normalized;
        
        Vector2 novaPos =transform.position;//transform.position pos actual de la nau
        novaPos=novaPos+direccioindicada*_vel*Time.deltaTime;
        //Debug.Log(Time.deltaTime);

        novaPos.x = Mathf.Clamp(novaPos.x,minPantalla.x, maxPantalla.x);
        novaPos.y = Mathf.Clamp(novaPos.y, minPantalla.y, maxPantalla.y);

        transform.position = novaPos;

    
    
    }

    private void OnTriggerEnter2D(Collider2D objecteTocat)
    {   
        if(objecteTocat.tag=="Numero")
        {
        Destroy(gameObject);
        }


    }

}
