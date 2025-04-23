using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimerScript : MonoBehaviour
{
    // Variables = Cajitas
    public int numerosEnteros = 5; //Variable numeros enteros
    private float numeroDecimal = 7.5f; //Variable numeros decimales
    bool boleana = true; //Variable verdadero o falso
    string cadenaTexto = "Hola preciosa"; //Variable que escribe

    private int[] numeros = {75, 1 , 3}; 

    public int [] numeros2;

    private int[ , ] numeros3 = {{7,45,6}, { 9,22,45 }};  // , dentro del array para indicar que es de 2 dimensiones

    // private int[ , ] numeros3 = {{7,45,6}, { 9,22,45 }, {74, 5, 6}}; igual que arriba pero con una columna extra 

    List<int> listaDeNumeros = new List<int> {3,5,8,9,5,12,18};

    // Start is called before the first frame update
    void Start()
    {
        foreach(int numero in listaDeNumeros)
        {
            Debug.Log(numero);
        }

        listaDeNumeros.Sort(); // ordenar lalista de menor a mayor
        listaDeNumeros.Reverse(); // ordenar de mayor a menor 

       // listaDeNumeros.Clear(); limpiar la lista
        /*listaDeNumeros.Add(22); // añadir el numero 22 a la lista (ultima posición)
        listaDeNumeros.Remove(5); // eliminar el 5 concretamente 
        listaDeNumeros.RemoveAt(0) // eliminar el primer numero de la lista
        listaDeNumeros.RemoveAt(listaDeNumeros.Count - 1) // para eliminar el ultimo numero de la lista */


        foreach(int numero in listaDeNumeros)
        {
            Debug.Log(numero);
        }



        //Debug.Log(numeros[0]);

        //Debug.Log(numeros3[1,2]); // se empieza a contar des de 0, el tercer objeto de la segunda columna es 1,2 
        //Calculos();

        /*foreach(int numero in numeros )
        {
            Debug.Log(numero);
        }
        for(int i = 0; i < 15; i ++)
        {
            Debug.Log(i);
        } */
       /* int i = 0;

        while(i < numeros.Length)
        {
            Debug.Log(numeros[i]);
        }*/
        /*int i = 75;
        do
        {
            Debug.Log("asasa");
        }
        while (i < numeros.Length);
        */
    }

    // Update is called once per frame
    void Update()
    {

    }

   /* void Calculos()
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
    }*/
}
