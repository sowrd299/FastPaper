using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cardScript : MonoBehaviour {

    // Use this for initialization   public int experience;
    public GameObject obj;
    public int experience;
    
    [SerializeField] public int Level
    {
        get{return experience/750;}
    }

    public void BuildObject()
    {
        Instantiate(obj, new Vector3(0,0,0), Quaternion.identity);
    }
}
