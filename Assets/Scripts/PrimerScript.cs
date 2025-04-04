using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimerScript : MonoBehaviour
{
    // Variables = Cajitas
    public int numerosEnteros = 5; //Variable numeros enteros
    private float numeroDecimal = 7.5f; //Variable numeros decimales
    bool boleana = true; //Variable verdadero o falso
    string cadenaTexto = "Hola bobi"; //Variable que escribe

    // Start is called before the first frame update
    void Start()
    {
        Calculos();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Calculos()
    {
        Debug.Log(numerosEnteros);
        
        numerosEnteros = 17;
        Debug.Log(numerosEnteros);

        numerosEnteros = 7 + 5;
        Debug.Log(numerosEnteros);

        numerosEnteros++;
        Debug.Log(numerosEnteros);

        numerosEnteros += 2;
        Debug.Log(numerosEnteros);
    }
}
