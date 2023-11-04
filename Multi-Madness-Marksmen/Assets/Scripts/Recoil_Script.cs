using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoil_Script : MonoBehaviour
{
 [Header("Animation Curve")]
    // All recoil curves must start and end with zero.
    public AnimationCurve RotationX = AnimationCurve.EaseInOut(0.0f, 2.0f, 1.0f, 0.0f);
    public AnimationCurve RotationY = AnimationCurve.EaseInOut(0.0f, 2.0f, 1.0f, 0.0f);
    public AnimationCurve RotationZ = AnimationCurve.EaseInOut(0.0f, 2.0f, 1.0f, 0.0f);
    public AnimationCurve PositionX = AnimationCurve.EaseInOut(0.0f, 2.0f, 1.0f, 0.0f);
    public AnimationCurve PositionY = AnimationCurve.EaseInOut(0.0f, 2.0f, 1.0f, 0.0f);
    public AnimationCurve PositionZ = AnimationCurve.EaseInOut(0.0f, 2.0f, 1.0f, 0.0f);

[Header("Animation Curve Aim")]

    public AnimationCurve RotationXA = AnimationCurve.EaseInOut(0.0f, 2.0f, 1.0f, 0.0f);
    public AnimationCurve RotationYA = AnimationCurve.EaseInOut(0.0f, 2.0f, 1.0f, 0.0f);
    public AnimationCurve RotationZA = AnimationCurve.EaseInOut(0.0f, 2.0f, 1.0f, 0.0f);
    public AnimationCurve PositionXA = AnimationCurve.EaseInOut(0.0f, 2.0f, 1.0f, 0.0f);
    public AnimationCurve PositionYA = AnimationCurve.EaseInOut(0.0f, 2.0f, 1.0f, 0.0f);
    public AnimationCurve PositionZA = AnimationCurve.EaseInOut(0.0f, 2.0f, 1.0f, 0.0f);
    public float AnimationcurveTime = 0;
    public float duration = 0.2f;
    private float TimePassed;
    private Coroutine Recoil;
    private IEnumerator StartRecoil()
    {

        TimePassed = 0;
        AnimationcurveTime = 0;
        while (TimePassed < duration)
        {
            TimePassed += Time.deltaTime;
            AnimationcurveTime = TimePassed / duration;
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(CalculateNextRotation()), AnimationcurveTime);
            transform.localPosition = Vector3.Lerp(transform.localPosition, CalculateNextPosition(), AnimationcurveTime);
            yield return null;
        }
    }

    private IEnumerator StartRecoilA()
    {

        TimePassed = 0;
        AnimationcurveTime = 0;
        while (TimePassed < duration)
        {
            TimePassed += Time.deltaTime;
            AnimationcurveTime = TimePassed / duration;
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(CalculateNextRotationA()), AnimationcurveTime);
            transform.localPosition = Vector3.Lerp(transform.localPosition, CalculateNextPositionA(), AnimationcurveTime);
            yield return null;
        }
    }
    private Vector3 CurrentRotation, CurrentPosition;
    public Vector3 CalculateNextRotation()
    {
        float Rotationx = RotationX.Evaluate(AnimationcurveTime);
        float Rotationy = RotationY.Evaluate(AnimationcurveTime);
        float Rotationz = RotationZ.Evaluate(AnimationcurveTime);

        CurrentRotation =
                     new Vector3(
                     Rotationx,
                     Rotationy,
                     Rotationz
                    );

        return CurrentRotation;
    }
    public Vector3 CalculateNextPosition()
    {
        float Positionx = PositionX.Evaluate(AnimationcurveTime);
        float Positiony = PositionY.Evaluate(AnimationcurveTime);
        float Positionz = PositionZ.Evaluate(AnimationcurveTime);

        CurrentPosition =
                  new Vector3
                  (
                  Positionx,
                  Positiony,
                  Positionz
                  );
        return CurrentPosition;
    }

    public Vector3 CalculateNextRotationA()
    {
        float Rotationx = RotationXA.Evaluate(AnimationcurveTime);
        float Rotationy = RotationYA.Evaluate(AnimationcurveTime);
        float Rotationz = RotationZA.Evaluate(AnimationcurveTime);

        CurrentRotation =
                     new Vector3(
                     Rotationx,
                     Rotationy,
                     Rotationz
                    );

        return CurrentRotation;
    }
    public Vector3 CalculateNextPositionA()
    {
        float Positionx = PositionXA.Evaluate(AnimationcurveTime);
        float Positiony = PositionYA.Evaluate(AnimationcurveTime);
        float Positionz = PositionZA.Evaluate(AnimationcurveTime);

        CurrentPosition =
                  new Vector3
                  (
                  Positionx,
                  Positiony,
                  Positionz
                  );
        return CurrentPosition;
    }
    public void ApplyRecoil(bool isAiming)
    {
        if (Recoil != null)
        {
            StopCoroutine(Recoil);
        }

        if(isAiming){
        Recoil = StartCoroutine(StartRecoilA());   
        }
        else{
        Recoil = StartCoroutine(StartRecoil());
        }
    }

}
