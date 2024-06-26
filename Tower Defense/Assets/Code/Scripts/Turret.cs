using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class Turret : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TowerCore towerCore;
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    [SerializeField] private GameObject upgradeUI;
    [SerializeField] private Button upgradeButton;



    [Header("Attribute")]
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float bps = 1f; //Bullets Per Second
    [SerializeField] private int baseUpgradeCost = 100;

    

    private float bpsBase;
    private float targetingRangeBase;

    private Transform target;
    private float timeUntilFire;

    private int level = 1;



    private void OnDrawGizmosSelected()
    {
       //Handles.color = Color.cyan;
       //Handles.DrawWireDisc(transform.position, transform.forward, towerCore.GetTargetingRange());
    }
    // Start is called before the first frame update
    void Start()
    {
        bpsBase = bps;
        targetingRangeBase = towerCore.GetTargetingRange();

        //upgradeButton.onClick.AddListener(Upgrade);
    }

    // Update is called once per frame
    private void Update()
    {

        

        if(target == null)
        {
            FindTarget();
            return;
        }

        RotateTowardsTarget();

        if(!CheckTargetIsInRange())
        {
            target = null;
        }
        else
        {
            
            timeUntilFire += Time.deltaTime;
            
            if(towerCore.GetFoodNear()>0)
            {
                if (timeUntilFire >= 1f / (bps * (towerCore.GetFoodNear() * towerCore.GetFoodMulti())))
                {
                    //Debug.Log(1f / (bps * (towerCore.GetFoodNear() * towerCore.GetFoodMulti())));
                    Shoot();
                    timeUntilFire = 0f;
                }
            }
            else
            {
                if (timeUntilFire >= 1f / bps)
                {
                    //Debug.Log(1f / (bps * (towerCore.GetFoodNear() * towerCore.GetFoodMulti())));
                    Shoot();
                    timeUntilFire = 0f;
                }
            }
            
        }
        
    }

    private void Shoot()
    {
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
        bulletScript.SetTarget(target);

        if (towerCore.GetWaterNear() > 0)
        {
            bulletScript.MultiplyDamage(towerCore.GetWaterNear() * towerCore.GetWaterMulti());
        }
    }

    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, towerCore.GetTargetingRange(), (Vector2)transform.position, 0f, enemyMask);

        if(hits.Length > 0)
        {
            target = hits[0].transform;
        }
    }

    private bool CheckTargetIsInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= towerCore.GetTargetingRange();
    }

    public void OpenUpgradeUI()
    {
        upgradeUI.SetActive(true);
    }

    public void CloseUpgradeUI()
    {
        upgradeUI.SetActive(false);
        UIManager.main.SetHoveringState(false);
    }

    public void Upgrade()
    {
        if(CalculateCost() > LevelManager.main.currency)
        {
            Debug.Log("You can't afford this Upgrade");
            return;
        }

        LevelManager.main.SpendCurrency(CalculateCost());

        level++;

        bps = CalculateBPS();
        //targetingRange = CalculateRange();

        CloseUpgradeUI();

        Debug.Log("New BPS: " + bps);
        //Debug.Log("New range: " + targetingRange);
        Debug.Log("New cost: " + CalculateCost());

    }
    
    private int CalculateCost()
    {
        return Mathf.RoundToInt(baseUpgradeCost*Mathf.Pow(level, 0.8f));
    }

    private float CalculateBPS()
    {
        return bpsBase * Mathf.Pow(level, 0.6f);
    }

    private float CalculateRange()
    {
        return targetingRangeBase * Mathf.Pow(level, 0.4f);
    }

    private void RotateTowardsTarget()
    {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

}
