using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardType { red, green, blue};

public class cardScript : MonoBehaviour 
{

    public GameObject obj;
    public int experience;
    public CardType type;
    
    public int Level
    {
        get{ return experience/750; }
    }

    void Start()
    {
        obj = gameObject;
    }


    public void BuildObject()
    {
        Instantiate(obj, new Vector3(0,0,0), Quaternion.identity);
    }
}
