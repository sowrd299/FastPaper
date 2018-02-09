using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class lool : MonoBehaviour {

	public UnityEvent lol;

/// <summary>
/// Awake is called when the script instance is being loaded.
/// </summary>
	void Awake()
	{
		lol.AddListener(printLOOL);
		lol.Invoke();
        lol.AddListener(AFSDG);
		lol.Invoke();

    }
	public void printLOOL()
	{
		Debug.Log("DLKJSA");
	}

    public void AFSDG()
    {
        Debug.Log("asdf");
    }
}
