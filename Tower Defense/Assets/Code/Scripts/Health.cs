using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject childPrefab;
    [SerializeField] private int childEnemyNum;
    [SerializeField] private EnemyMovement localMovementComp;


    [Header("Attributes")]
    [SerializeField] private float hitPoints = 2f;
    [SerializeField] private int currencyWorth = 50;
    
    private GameObject childObj;
    public EnemyMovement childMovementComp;
    private int childrenSpawned = 0;

    private bool isDestroyed = false;

    public void TakeDamage(float dmg)
    {
        hitPoints -= dmg;

        if(hitPoints <= 0 && !isDestroyed)
        {
            EnemySpawner.onEnemyDestroy.Invoke();
            LevelManager.main.IncreaseCurrency(currencyWorth);
            isDestroyed = true;

            if(childPrefab != null)
            {
                for (int i = 0; i < childEnemyNum; i++)
                {
                    childObj = Instantiate(childPrefab, transform.position, Quaternion.identity);
                    childMovementComp = childObj.GetComponent<EnemyMovement>();
                    childMovementComp.SetPathIndex(localMovementComp.pathIndex);
                    childMovementComp.UpdateSpeed((i+1) * 0.9f);
                    childMovementComp.DelayResetSpeed();
                    EnemySpawner.main.IncrementAlive();
                }
            }
                
            
          
           Destroy(gameObject);
            
            
        }
    }
    IEnumerator SpawnChildren()
    {
        yield return new WaitForSeconds(0.25f);
        childObj = Instantiate(childPrefab, transform.position, Quaternion.identity);
        childMovementComp = childObj.GetComponent<EnemyMovement>();
        childMovementComp.SetPathIndex(localMovementComp.pathIndex);
        childrenSpawned++;
       
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
