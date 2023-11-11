using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunChangeTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }


    public GameObject Sniper;
    public GameObject AK;
    public GameObject Shootgun;


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I)){
            Sniper.SetActive(true);
            AK.SetActive(false);
            Shootgun.SetActive(false);
        }

        if(Input.GetKeyDown(KeyCode.O)){
            Sniper.SetActive(false);
            AK.SetActive(true);
            Shootgun.SetActive(false);
        }

        if(Input.GetKeyDown(KeyCode.P)){
            Sniper.SetActive(false);
            AK.SetActive(false);
            Shootgun.SetActive(true);  
        }

    }
}
