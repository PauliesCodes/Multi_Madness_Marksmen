using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Linq;

public class PlayerInfo{

    public static int score;
    public static string playerName;
    public static int rank;
    public static bool isLogged = false;

}
public class DataBaseConection : MonoBehaviour
{
    public InputField nameInput;
    public InputField passwordInput;
    public InputField scoreInput;

    public TextMeshProUGUI resultText;

    public GameObject LoginxRegisterScrean;

    public GameObject LoggedScrean;

    public TextMeshProUGUI playerName;
    public TextMeshProUGUI playerScore;
    public TextMeshProUGUI playerRank;


    public GameObject popUpObj;
    public TextMeshProUGUI popUpTxt;

    public GameObject errorWindow;
    public TextMeshProUGUI errorWindowText;

    private string serverURL = "https://mmm.9e.cz/TestPHP/";

/*
    private void Update() {
        
        Debug.Log(PlayerInfo.isLogged);

    }
*/

    public void RegisterCheck(){

        if(nameInput.text.Count() <= 20 && nameInput.text.Count() >= 3 && passwordInput.text.Count() <= 20 && passwordInput.text.Count() >= 3){

            Register();

        }
        else{

            showPopUp("Please enter a name and password between 3 and 20 characters");

        }
    }

    public void Login()
    {
        StartCoroutine(LoginRequest());
    }

    public void Register()
    {
        StartCoroutine(RegisterRequest());
    }

    public void GetScore()
    {
        StartCoroutine(GetScoreRequest());
    }

    public void SaveScore()
    {
        StartCoroutine(SaveScoreRequest());
    }
    public void GetRank()
    {
        StartCoroutine(GetRankk());
    }

    IEnumerator GetRankk()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", PlayerInfo.playerName);

        using (UnityWebRequest www = UnityWebRequest.Post(serverURL + "getrank.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {

                if(int.TryParse(www.downloadHandler.text, out int rankk)){

                    PlayerInfo.rank = rankk;
                    playerRank.text = "#"+PlayerInfo.rank.ToString();

                }
                else{

                    showErrorWindow("Database conection error, please check your internet conection");

                }

            }
            else
            {
                showErrorWindow("Database conection error, please check your internet conection");
            }
        }
    }
    IEnumerator LoginRequest()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", nameInput.text);
        form.AddField("password", passwordInput.text);

        using (UnityWebRequest www = UnityWebRequest.Post(serverURL + "login.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                if(www.downloadHandler.text == "ok"){
                    Debug.Log(www.downloadHandler.text);

                    PlayerInfo.playerName = nameInput.text;

                    PlayerInfo.isLogged = true;

                    getLogged();
                }
                else{

                    showPopUp("Please check your login details");

                }


            }
            else
            {
                showErrorWindow("Database conection error, please check your internet conection");
            }
        }
    }

    IEnumerator RegisterRequest()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", nameInput.text);
        form.AddField("password", passwordInput.text);

        using (UnityWebRequest www = UnityWebRequest.Post(serverURL + "register.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {

                if(www.downloadHandler.text == "ok"){

                    Login();

                } 
                else {
                    if(www.downloadHandler.text == "allreadyexists"){

                        showPopUp("Player with this name allready exists");

                    }
                    else{

                        showErrorWindow("Database conection error, please check your internet conection");

                    }


                }

            }
            else
            {
                showErrorWindow("Database conection error, please check your internet conection");
            }
        }
    }

    IEnumerator GetScoreRequest()
    {
        string url = serverURL + "getscore.php?name=" + WWW.EscapeURL(nameInput.text);

        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                if(int.TryParse(www.downloadHandler.text, out int resoult)){

                    Debug.Log(www.downloadHandler.text);

                    PlayerInfo.score = resoult;

                    playerScore.text = PlayerInfo.score.ToString();

                    Debug.Log(PlayerInfo.score);
                }
                else{

                showErrorWindow("Database conection error, please check your internet conection");
                }
            }
            else
            {
                showErrorWindow("Database conection error, please check your internet conection");
            }
        }
    }

    IEnumerator SaveScoreRequest()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", nameInput.text);
        form.AddField("score", scoreInput.text);

        using (UnityWebRequest www = UnityWebRequest.Post(serverURL + "savescore.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                if(www.downloadHandler.text == "ok"){

                    Debug.Log(www.downloadHandler.text);
                }
                else{
                    
                showErrorWindow("Database conection error, please check your internet conection");

                }

            }
            else
            {
                showErrorWindow("Database conection error, please check your internet conection");
            }
        }
    }


    private void getLogged(){

        LoginxRegisterScrean.SetActive(false);

        LoggedScrean.SetActive(true);

        playerName.text = PlayerInfo.playerName;

        GetScore();
        
        GetRank();

    }

    public void ifYouAreLogged(){

        if(PlayerInfo.isLogged){

            GetRank();

            LoginxRegisterScrean.SetActive(false);

            LoggedScrean.SetActive(true);

            playerName.text = PlayerInfo.playerName;

            playerRank.text = "#"+PlayerInfo.rank.ToString();

            playerScore.text = PlayerInfo.score.ToString();

        }
    }
    public void LogOut(){

        PlayerInfo.isLogged = false;

        LoggedScrean.SetActive(false);

        LoginxRegisterScrean.SetActive(true);


    }

    public void showPopUp(string text){

        popUpTxt.text = text;

        popUpObj.SetActive(true);

    }

    public void showErrorWindow(string text){

        errorWindowText.text = text;

        errorWindow.SetActive(true);

    }

}
