using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

    public static GameMaster instance;

    private GameObject human;
    private GameObject shadow;

    public void Awake()
    {
        if (instance != null)
            return;
        instance = this;
    }

	void Update () {
		
	}

    public void LooseLevel()
    {
        Debug.Log("Przegrałeś");
    }

    public GameObject Shadow
    {
        get {
            return shadow;
        }
    }

    public GameObject Human
    {
        get {
            return human;
        }

    }
}
