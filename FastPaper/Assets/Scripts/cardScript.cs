using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cardScript : MonoBehaviour {

    // Use this for initialization   public int experience;
    public GameObject obj;
    public int experience;
    
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
