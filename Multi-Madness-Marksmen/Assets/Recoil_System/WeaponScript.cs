using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    RecoilSystem1 recoil;
    [SerializeField] float shootDelay = 1f;
    float T_ShootDelay;
    void Start()
    {
        recoil = GetComponent<RecoilSystem1>();
        T_ShootDelay = shootDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (T_ShootDelay < shootDelay)
            T_ShootDelay += Time.deltaTime;

        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (T_ShootDelay >= shootDelay)
            {
                T_ShootDelay = 0;
                recoil.ApplyRecoil();
            }
        }
    }
}
