using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;
public class AreaOfEffect : MonoBehaviour
{
    private List<CoreHealth> detectedEnemys = new List<CoreHealth>();
    public AreaOfEffectSO areaOfEffectSO;
    [SerializeField]private GameObject timingCircle;
    public Ability ability { private get; set; }
    private void OnEnable()
    {
        timingCircle.transform.DOScale(1, areaOfEffectSO.timingCircleTime).OnComplete(() => EndOfAnimation());
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out CoreHealth enemy))
        {
            detectedEnemys.Add(enemy);
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.TryGetComponent(out CoreHealth enemy))
        {
            detectedEnemys.Remove(enemy);
        }
    }

    public void DuringAnimation()
    {
        GetComponent<MeshRenderer>().material.DOFade(1, areaOfEffectSO.timingCircleTime);
    }
    public void EndOfAnimation()
    {
        timingCircle.GetComponent<MeshRenderer>().material.DOFade(0, areaOfEffectSO.liveTime).SetEase(Ease.InExpo);
        GetComponent<MeshRenderer>().material.DOFade(0, areaOfEffectSO.liveTime).SetEase(Ease.InExpo);
        StartCoroutine(AfterAnimation());
        foreach (CoreHealth item in detectedEnemys.ToList())
        {
            ability.Effect(item);
            detectedEnemys.Remove(item);
        }
    }
    public IEnumerator AfterAnimation()
    {
        yield return new WaitForSeconds(areaOfEffectSO.liveTime);
        Destroy(this.gameObject);

    }
}
