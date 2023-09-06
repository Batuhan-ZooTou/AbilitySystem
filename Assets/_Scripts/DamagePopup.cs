using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DamagePopup : MonoBehaviour
{
    [SerializeField]private TextMeshPro damageNumber;
    [Header("PopupAnimationVariables")]
    [SerializeField] float aliveTime = 1.0f;
    [SerializeField] float travelSpeed;
    [MinTo(0.0f, 2.0f)]
    [SerializeField] Vector2 circleRadius;
    [MinTo(0.0f, 10.0f)]
    public Vector2 heightRange;
    [Header("AnimationCurvesToSmooth")]
    [SerializeField] AnimationCurve smoothTravelSpeedCurve;
    [SerializeField] AnimationCurve smoothFadingCurve;
    [SerializeField] AnimationCurve smoothScalingCurve;
    float aliveCounter;
    Vector3 startPos;
    Vector3 endPos;
    Vector3 centerPoint;
    public void SetDamagePopup(float damageNumberToPop,Vector3 spawnPoint)
    {
        transform.position = spawnPoint;
        startPos = spawnPoint;
        aliveCounter = 0;
        //set Damage Number
        damageNumber.SetText(damageNumberToPop.ToString());
        //Random circle on point to fall
        Vector2 circle = Random.insideUnitCircle * Random.Range(circleRadius.x, circleRadius.y);
        endPos = startPos + new Vector3(circle.x, 0, circle.y);

        // The center of the arc
        centerPoint = (startPos + endPos) * 0.5F;
        centerPoint += new Vector3(0, 1 * Random.Range(heightRange.x,heightRange.y), 0);
    }
    private void Update()
    {
        //normalized time
        float fracComplete = aliveCounter / aliveTime;
        aliveCounter += Time.deltaTime;
        //change alpha 
        damageNumber.alpha = smoothFadingCurve.Evaluate(fracComplete*travelSpeed);
        //change scale
        transform.localScale= Vector3.one* smoothScalingCurve.Evaluate(fracComplete * travelSpeed);
        //calculate curve from 2 lerp
        Vector3 a = Vector3.Lerp(startPos, centerPoint, smoothTravelSpeedCurve.Evaluate(fracComplete) * travelSpeed);
        Vector3 b = Vector3.Lerp(centerPoint, endPos, smoothTravelSpeedCurve.Evaluate(fracComplete) * travelSpeed);
        //move along curve
        transform.position = Vector3.Lerp(a, b, smoothTravelSpeedCurve.Evaluate(fracComplete) * travelSpeed);
        //back to pool
        if (aliveCounter>=aliveTime)
        {
            ProjectilePool.Instance.damagePopupPool.Release(this);
        }
    }
}
