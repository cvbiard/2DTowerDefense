using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeFactory : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LayerMask pointMask;
    [SerializeField] private GameObject freezeVisualBase;
    [SerializeField] private TowerCore towerCore;
    [SerializeField] private GameObject spikePrefab;

    [Header("Attribute")]
    //[SerializeField] private float targetingRange = 5f;
    [SerializeField] private float aps = 4f; //attacks Per Second
    [SerializeField] private float freezeTime = 1f;
    [SerializeField] private Color freezeVisualColor;

    private float timeUntilFire;

    private void Start()
    {

       // freezeVisualBase.SetActive(false);
    }
    private void Update()
    {

        timeUntilFire += Time.deltaTime;

        if (timeUntilFire >= 1f / aps)
        {
            LaySpikes();
            timeUntilFire = 0f;
        }


    }

    private void LaySpikes()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, towerCore.GetTargetingRange(), (Vector2)transform.position, 0f, pointMask);

        if (hits.Length > 0 && EnemySpawner.main.isBetweenWaves == false)
        {
            StartCoroutine(PlaceSpike(hits, 0));
        }

    }

    private IEnumerator PlaceSpike(RaycastHit2D[] targets, int index)
    {
        yield return new WaitForSeconds(freezeTime);

        if(targets.Length > index)
        {
            RaycastHit2D hit = targets[index];

            Point targetPoint = hit.transform.GetComponent<Point>();

            targetPoint.EnableSpike();

            StartCoroutine(PlaceSpike(targets, index+1));
        }
        yield break;
    }

    private void OnDrawGizmosSelected()
    {
        //Handles.color = Color.cyan;
        //Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }
}
