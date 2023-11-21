using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSelect : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject akSelected;
    public GameObject srSelected;
    public GameObject sgSelected;
    public int selectedGun = 0;

    public void akSelect(){

        akSelected.SetActive(true);
        srSelected.SetActive(false);
        sgSelected.SetActive(false);
        selectedGun = 0;
        Debug.Log("akIsSelected");
    }

    public void srSelect(){

        akSelected.SetActive(false);
        srSelected.SetActive(true);
        sgSelected.SetActive(false);
        selectedGun = 1;
        Debug.Log("sniperisselected");
    }
    public void sgSelect(){

        akSelected.SetActive(false);
        srSelected.SetActive(false);
        sgSelected.SetActive(true);
        selectedGun = 2;
        Debug.Log("brokaisselected");
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
