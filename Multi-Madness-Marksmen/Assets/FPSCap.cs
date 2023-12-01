using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCap : MonoBehaviour
{

    void Awake() {
        
        Application.targetFrameRate = 120;

    }

    void Start() {
    
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;

    }


}
