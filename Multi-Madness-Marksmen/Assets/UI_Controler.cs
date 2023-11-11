using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Controler : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject gunScriptReference;

    public TextMeshProUGUI maxAmmoText;
    public TextMeshProUGUI currentAmmoText;
    void Start()
    {
        maxAmmoText.text = gunScriptReference.GetComponent<Universal_Gun_Script>().magazineSize.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        currentAmmoText.text = gunScriptReference.GetComponent<Universal_Gun_Script>().currentAmmo.ToString();

    }
}
