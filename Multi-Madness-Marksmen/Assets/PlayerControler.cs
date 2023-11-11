using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject wepponAK;
    public GameObject wepponAKHolder;
    public GameObject wepponSniper;
    public GameObject wepponSniperHolder;
    public GameObject wepponShootgun;
    public GameObject wepponShootgunHolder;


    public TextMeshProUGUI killCountText;

    public int kills = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(wepponAKHolder.activeSelf){
            if(wepponAK.GetComponent<Universal_Gun_Script>().killCount > kills){
                kills = wepponAK.GetComponent<Universal_Gun_Script>().killCount;
                killCountText.text = kills.ToString();
                if(!wepponAK.GetComponent<Universal_Gun_Script>().wasAiming){
                    Debug.Log("NOSCOPE XD");
                }
            }
        } else if(wepponSniperHolder.activeSelf){
            kills = wepponSniper.GetComponent<Universal_Gun_Script>().killCount;
            killCountText.text = kills.ToString();
        }else if(wepponShootgunHolder.activeSelf){
            kills = wepponShootgun.GetComponent<Universal_Gun_Script>().killCount;
            killCountText.text = kills.ToString();
        }
    }
}
