using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public Transform playerTransform; //Variable que se puede usar en todos los sitios
    public Vector3 offset;
    public Vector2 maxPosition;
    public Vector2 minPosition;
    public float interpolationRatio = 0.5f;


    void Awake()
    {
        //Busca un obeto por el nombre de la escena y el .getcomponent de despues busca el transform dentro de ese objeto
        //playerTransform = GameObject.Find("Player").GetComponent<Transform>(); ^^^^^^^^^^
        
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>(); //Busca el objeto por el tag que tenga
    }

    void FixedUpdate()
    {
        if(playerTransform == null) //null combrueba si en esa variable hay algo, en este caso comprueba si esta vacia
        {
            return; //esto hace que si el if no se cumple deje de ejectuar todo lo que hay despues 
        }

        Vector3 desiredPosition = playerTransform.position + offset; //Variable local que solo se puede usar en update

        float clampX = Mathf.Clamp(desiredPosition.x, minPosition.x, maxPosition.x); //Mathf.Clamp es una funcion para limitar algo entre dos valores
        float clampY = Mathf.Clamp(desiredPosition.y, minPosition.y, maxPosition.y); //Son float pq los vectores siempre podran ser numeros decimales
        Vector3 clampedPosition = new Vector3(clampX, clampY, desiredPosition.z);

        Vector3 lerpedPosition = Vector3.Lerp(transform.position, clampedPosition, interpolationRatio);
        
        transform.position = lerpedPosition;
        
    }
}
