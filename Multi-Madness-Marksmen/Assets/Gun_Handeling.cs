using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Handeling : MonoBehaviour
{
    // Start is called before the first frame update
    public bool is_gun_activated = true;
    public GameObject assault;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("i")) {
            if(is_gun_activated) {
                assault.SetActive(false);
                is_gun_activated = false;
            } 
            else {
                assault.SetActive(true);
                is_gun_activated = true;
            }
            }
    }
}
