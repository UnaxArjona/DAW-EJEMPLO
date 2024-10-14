using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;




/*
REPASO

QUE HEMOS VISTO?
-CREAR OBJETOS A LA ESCENA
-CREAR EMPTYOBJECTS (PARA HACER EL GENERADOR NUMEROS)
-PREFABS (PARA CREAR OBJETOS CUANDO EL JUEGO ESTA EN EJECUCION "PARA CREARLOS LO ARRASTRAMOS A LA CARPETA PREFABS",PARA CREAR UN PREFAB A LA ESCENA INSTANCIATE (VARIABLE PREFAB))
-VARIABLEPREFAB:VARIABLE DE TIPOS GAMEOBJECT.

-ENCONTRAR POSICION OBJETO ACTUAL(TRANSFORM.POSITION)
-ENCONTRAR MARGENES PANTALLA (CAMERA.MAIN.VIEWPORT...)
-[SerializeField]:PARA HACER UNA VARIABLE DE LA CLASE PRIVATE DE LA CLASE SE MUESTRE AL DITOR DE UNITY
-UTILIZAR UNA IMAGEN SPRITE COMO SI FUESE MAS DE UNA (SUB IMAGENES) SELECIONAMOS EL SPRITE, Y EN EL MODE LO CAMBIABAMOS A MULTIPLE Y CLICAMOS A APLY Y ELEGIMOS LAS OPCIONES DEL SPRITE EDITOR
-DESTRUIR OBJECTO ACTUAL CON EL DESTROY(GAMEOBJECT)
-LLAMAR UN METODO EN EL CABO DE X SEGUNDOS: INVOKE(NOMBRE METODO, XF)
-LLAMAR UN METODO EN EL CABO DE X SEGUNDOS Y CADA SEGUNDO:INVOKEREPEATING(NOMBRE METODO , XF, XY)
-COMO PARAR UNINVOKEREPEATING: CANCELINVOKE("NOMBREMETODO")
-DETECTAR OBJECTOS TOCA A OTROS
-AÑADIR LOS OBJECTOS QUE QUEREMOS QUE SE TOQUEN, LOS COMPONENTES: BOXCOLLIDER2D Y RIGIBODY2D
-EN BOXCOLLIDER2D: ACTIVAR CHECKBOX IS TRIGGER
-EN RIGIBODY2D: GRAVITUSCALE PONERLO A 0



*/

public class NauJugador : MonoBehaviour
{
    private float _vel;

    private Vector2 minPantalla, maxPantalla;

   [SerializeField] private GameObject prefabProjectil;
   [SerializeField] private GameObject prefabExplosio;
   [SerializeField] private TMPro.TextMeshProUGUI componentTextVides;

    private int videsNau;

    

    // Start is called before the first frame update
    void Start()
    {
        _vel = 8f;
        minPantalla = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        maxPantalla = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        float meitatMidaImatgeX = GetComponent<SpriteRenderer>().sprite.bounds.size.x * transform.localScale.x / 2;
        float meitatMidaImatgeY = GetComponent<SpriteRenderer>().sprite.bounds.size.y * transform.localScale.y / 2;


        minPantalla.x = minPantalla.x + meitatMidaImatgeX;
        maxPantalla.x = maxPantalla.x - meitatMidaImatgeX;
        minPantalla.y += meitatMidaImatgeY;
        maxPantalla.y -= meitatMidaImatgeY;

        videsNau = 3;


         
    }

    // Update is called once per frame
    void Update()
    {
        MoureNau();
        DisparaProjectil();
        
    }
    private void DisparaProjectil()
    {
        if (Input.GetKeyDown("space"))
        {
            GameObject projectil = Instantiate(prefabProjectil);
            projectil.transform.position = transform.position;
        }
    }

    private void MoureNau()
    {
        float direccioIndicadaX = Input.GetAxisRaw("Horizontal");
        float direccioIndicadaY = Input.GetAxisRaw("Vertical");
        //Debug.Log("X: " + direccioIndicadaX + " - Y: " + direccioIndicadaY);
        Vector2 direccioIndicada = new Vector2(direccioIndicadaX, direccioIndicadaY).normalized;

        Vector2 novaPos = transform.position;// transform.position: pos actual de la nau.
        novaPos = novaPos + direccioIndicada * _vel * Time.deltaTime;
        //Debug.Log(Time.deltaTime);

        novaPos.x = Mathf.Clamp(novaPos.x, minPantalla.x, maxPantalla.x);
        novaPos.y = Mathf.Clamp(novaPos.y, minPantalla.y, maxPantalla.y);

        transform.position = novaPos;
    }

    private void OnTriggerEnter2D(Collider2D objecteTocat)
    {
        if(objecteTocat.tag == "Numero")
        {
            videsNau--;

            componentTextVides.text = "Vides:" +videsNau.ToString();


            if (videsNau <= 0)
            {
                GameObject explosio = Instantiate(prefabExplosio);
                explosio.transform.position = transform.position;

                SceneManager.LoadScene("PantallaResultats");

                Destroy(gameObject);
            }
          
        }
    }
}
