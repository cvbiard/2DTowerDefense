using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TurretSlowmo : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject freezeVisualBase;
    [SerializeField] private TowerCore towerCore;

    [Header("Attribute")]
    //[SerializeField] private float targetingRange = 5f;
    [SerializeField] private float aps = 4f; //attacks Per Second
    [SerializeField] private float freezeTime = 1f;
    [SerializeField] private Color freezeVisualColor;

    private float timeUntilFire;

    private void Start()
    {
        
        freezeVisualBase.SetActive(false);
    }
    private void Update()
    {

        timeUntilFire += Time.deltaTime;

        if (timeUntilFire >= 1f * (aps * (towerCore.GetFoodNear() * towerCore.GetFoodMulti())))
        {
            FreezeEnemies();
            timeUntilFire = 0f;
        }


    }

    private void FreezeEnemies()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, towerCore.GetTargetingRange(), (Vector2)transform.position, 0f, enemyMask);

        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];

                EnemyMovement em = hit.transform.GetComponent<EnemyMovement>();
                em.UpdateSpeed(0.25f);

                StartCoroutine(ResetEnemySpeed(em));
                freezeVisualBase.SetActive(true);
            }
        }

    }

    private IEnumerator ResetEnemySpeed(EnemyMovement em)
    {
        yield return new WaitForSeconds(freezeTime * (towerCore.GetFoodNear() * towerCore.GetFoodMulti()));

        em.ResetSpeed();
        freezeVisualBase.SetActive(false);
    }

    private void OnDrawGizmosSelected()
    {
        //Handles.color = Color.cyan;
        //Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }
}
