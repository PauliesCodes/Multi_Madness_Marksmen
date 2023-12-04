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
using System.Threading;


public class SQLManager : MonoBehaviour
{
    // Start is called before the first frame update

    public InputField namee;
    public InputField password;
    public Button registerButton;

    public GameObject error;
    public TextMeshProUGUI errorMessage;

    public GameObject errorWindow;


    private string conectionString;
    private MySqlConnection MS_Conection;
    private MySqlCommand MS_Command;


    /// <summary>
    /// private string secretKey = "mySecretKey";
    /// </summary>
    public string addScoreURL = 
        "http://localhost/HighScoreGame/addscore.php?";
    public string highscoreURL = 
         "http://localhost/HighScoreGame/display.php";

    public string loginURL = "https://mmm.9e.cz/login.php";

    public string registerURL = "https://mmm.9e.cz/registrace.php";

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



    public void CallRegister(){

        
        StartCoroutine(Register());


    }


    /*IEnumerator Register(){

        WWWForm form = new WWWForm();

        form.AddField("namee", namee.text);
        form.AddField("password", password.text);

        WWW www = new WWW("https://mmm.9e.cz/registrace.php", form);

        yield return www;

        Debug.Log(www.text);    

        if(www.text == "Nope"){

            errorWindow.SetActive(true);

            Debug.Log("Obnoto fsdkholjnasdfgkjhsdfhjk");

            //KLDyž se člověk registorvbal dobře

        }else {


        }

    }*/

    IEnumerator Register()
    {
    List<IMultipartFormSection> formData = new List<IMultipartFormSection>
    {
        new MultipartFormDataSection("namee", namee.text),
        new MultipartFormDataSection("password", password.text)
    };

    using (UnityWebRequest www = UnityWebRequest.Post("https://mmm.9e.cz/registrace.php", formData))
    {
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            Debug.Log(www.downloadHandler.text);

            if (www.downloadHandler.text == "Nope")
            {
                errorWindow.SetActive(true);
                Debug.Log("Obnoto fsdkholjnasdfgkjhsdfhjk");

                // Když se člověk registroval dobře
            }
            else
            {
                // Pokud je registrace úspěšná
            }
        }
        else
        {
            Debug.LogError("Chyba při spojení se serverem: " + www.error);
        }
    }
}

/*    
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
}*/

}