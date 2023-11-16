using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor.PackageManager.UI;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;
using MySql.Data.MySqlClient;



public class SQLManager : MonoBehaviour
{
    // Start is called before the first frame update

    public InputField namee;
    public InputField password;
    public GameObject error;
    public TextMeshProUGUI errorMessage;


    private string conectionString;
    private MySqlConnection MS_Conection;
    private MySqlCommand MS_Command;

    void Start()
    {
        
    }

    private bool isInputValid(){

        bool isValid = false;

        if(namee.text != "" && password.text != "" && namee.text.Length < 25 && password.text.Length < 30)
        {
            isValid = true;
        }
        return isValid;
    }

    public void signInButton(){

        bool isValid = isInputValid();

        if(isValid)
        {
            // Zde kodik pro zjištění jestli už nahodou jmeno neexistuje a pokud ne tak založení acc

            conection();
        }
        else
        {
            
            immageError("Kamo tož zadej to prostě dobře :D");
        }
    }


    public void signUpButton(){

        bool isValid = isInputValid();

        if(isValid)
        {
            // Zde kodik pro zjištění jestli už nahodou jmeno neexistuje a pokud ne tak založení acc


        }
        else
        {
            
            Debug.Log("Spatne ztadanio");
        }



    }

    // Update is called once per frame
    void Update()
    {
    }

    private void immageError(string errorMassage){
        error.SetActive(true);
        errorMessage.text = errorMassage;
    }

    public void conection(){

        conectionString = "Server = sql.freedb.tech ; Database = freedb_MultiMadnessMarksmen ; User = freedb_clienttt ; Password = zxuueK#8%GS&5nE; Charset = utf8;";
        MS_Conection = new MySqlConnection(conectionString);
        MS_Conection.Open();
        if (MS_Conection.State == System.Data.ConnectionState.Open)
        {
            Debug.Log("Connected to MySQL database!");
        }
        else
        {
            Debug.Log("Failed to connect to MySQL database!");
        }
    }

}