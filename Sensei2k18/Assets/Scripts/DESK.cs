using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DESK : MonoBehaviour {

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Human")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
