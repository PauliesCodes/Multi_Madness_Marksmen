using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;
using MySql.Data.MySqlClient;

using System;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using UnityEngine.Networking;


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


    private string secretKey = "mySecretKey";
    public string addScoreURL = 
        "http://localhost/HighScoreGame/addscore.php?";
    public string highscoreURL = 
         "http://localhost/HighScoreGame/display.php";
    public Text nameTextInput;
    public Text scoreTextInput;
    public Text nameResultText;
    public Text scoreResultText;

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

        conectionString = "Server = mysql.pearhost.cz ; Database = 7honza_1 ; User = mysql_7honza ; Password = lyx3k5lGR; Charset = utf8;";
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
    
IEnumerator GetScores()
{
	UnityWebRequest hs_get = UnityWebRequest.Get(highscoreURL);
	yield return hs_get.SendWebRequest();
	if (hs_get.error != null)
		Debug.Log("There was an error getting the high score: "
                + hs_get.error);
	else
	{
		string dataText = hs_get.downloadHandler.text;
		MatchCollection mc = Regex.Matches(dataText, @"_");
		if (mc.Count > 0)
		{
			string[] splitData = Regex.Split(dataText, @"_");
			for (int i =0; i < mc.Count; i++)
			{
				if (i % 2 == 0)
					nameResultText.text += 
                                        splitData[i];
				else
					scoreResultText.text += 
                                        splitData[i];
			}
		}
	}
}
IEnumerator PostScores(string name, int score)
{
	string hash = HashInput(name + score + secretKey);
	string post_url = addScoreURL + "name=" + 
           UnityWebRequest.EscapeURL(name) + "&score=" 
           + score + "&hash=" + hash;
	UnityWebRequest hs_post = UnityWebRequest.Post(post_url, hash);
	yield return hs_post.SendWebRequest();
	if (hs_post.error != null)
		Debug.Log("There was an error posting the high score: " 
                + hs_post.error);
}

public string HashInput(string input)
{
	SHA256Managed hm = new SHA256Managed();
	byte[] hashValue = 	
            hm.ComputeHash(System.Text.Encoding.ASCII.GetBytes(input));
	string hash_convert = 
             BitConverter.ToString(hashValue).Replace("-", "").ToLower();
	return hash_convert;
}

}