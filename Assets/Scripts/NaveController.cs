using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaveController : MonoBehaviour
{
    public float velocidad = 5f;
    private float minX, maxX, minY, maxY;
    private float mitadAncho, mitadAlto;

    void Start()
    {
        // Límites de la cámara
        Vector3 limiteInfIzq = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 limiteSupDer = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));

        minX = limiteInfIzq.x;
        minY = limiteInfIzq.y;
        maxX = limiteSupDer.x;
        maxY = limiteSupDer.y;

        // Tamaño del sprite
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        mitadAncho = sr.bounds.size.x / 2f;
        mitadAlto = sr.bounds.size.y / 2f;
    }

    void Update()
    {
        float move = 0f;

        // ANDROID 
        if (Input.touchCount > 0)
        {
            Touch t = Input.GetTouch(0);

            if (t.phase == TouchPhase.Stationary || t.phase == TouchPhase.Moved)
            {
                if (t.position.x < Screen.width / 2f)
                    move = -1f; // izquierda
                else
                    move = 1f;  // derecha
            }
        }

        // PC  
        if (Input.GetKey(KeyCode.A))
            move = -1f;

        if (Input.GetKey(KeyCode.D))
            move = 1f;

        // Movimiento
        transform.Translate(Vector3.right * move * velocidad * Time.deltaTime);

        // Límites
        float posX = Mathf.Clamp(transform.position.x, minX + mitadAncho, maxX - mitadAncho);
        float posY = Mathf.Clamp(transform.position.y, minY + mitadAlto, maxY - mitadAlto);

        transform.position = new Vector3(posX, posY, transform.position.z);
    }
}
