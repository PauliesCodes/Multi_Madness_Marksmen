using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class SettingsManager : MonoBehaviour
{

    public AudioMixer audioMixer;
    
    public Dropdown resolutionDropdown;

    public TMP_InputField xSens;

    public TMP_InputField ySens;

    public GameObject mouseScriptReference;

    Resolution[] resolutions;

    void Start()
    {
        if(xSens && ySens != null){
        xSens.text = mouseScriptReference.GetComponent<PlayerCam>().sensX.ToString();
        
        ySens.text = mouseScriptReference.GetComponent<PlayerCam>().sensY.ToString();
        }



        resolutions = Screen.resolutions;   

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for(int i = 0; i < resolutions.Length; i++){

            string option = resolutions[i].width + " x " + resolutions[i].height;

            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height){

                currentResolutionIndex = i;

            }

        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
    public void setVolume (float volume){

        audioMixer.SetFloat("volume", volume);

    }

    public void setQuality(int qualityIndex){

        QualitySettings.SetQualityLevel(qualityIndex);

    }

    public void setFullscreen (bool isFullscreen){

        if(isFullscreen){

            Screen.fullScreen = true;

        }
        else {

            Screen.fullScreen = false;

        }
    }
    

    public void setResolution(int resolutionIndex){

        Resolution resolution = resolutions[resolutionIndex];

        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

    }

    public void setXSens(){

        if(xSens.text != ""){

            if(int.TryParse(xSens.text, out int x)){
                
                mouseScriptReference.GetComponent<PlayerCam>().sensX = x;

            }

        }

    }

    public void setYSens(){
        
        if(ySens.text != ""){

            if(int.TryParse(ySens.text, out int y)){
                
                mouseScriptReference.GetComponent<PlayerCam>().sensY = y;

            }

        }

    }

}
