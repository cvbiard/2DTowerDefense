using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;

public class StinkBug : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TowerCore towerCore;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab;

    public Transform[] firingPoints;
    [SerializeField] private Transform firingPointN;
    [SerializeField] private Transform firingPointS;
    [SerializeField] private Transform firingPointE;
    [SerializeField] private Transform firingPointW;
    [SerializeField] private Transform firingPointNE;
    [SerializeField] private Transform firingPointSE;
    [SerializeField] private Transform firingPointSW;
    [SerializeField] private Transform firingPointNW;




    [Header("Attribute")]
    [SerializeField] private float bps = 1f; //Bullets Per Second


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
        if (target == null)
        {
            FindTarget();
            return;
        }

        //RotateTowardsTarget();

        if (!CheckTargetIsInRange())
        {
            target = null;
        }
        else
        {

            timeUntilFire += Time.deltaTime;

            if (timeUntilFire >= 1f / bps)
            {
                Shoot();
                timeUntilFire = 0f;
            }
        }

    }

    private void Shoot()
    {
        //GameObject bulletObj = Instantiate(bulletPrefab, firingPointN.position, Quaternion.identity);
        //StinkBugBullet bulletScript = bulletObj.GetComponent<StinkBugBullet>();
        //bulletScript.SetDirection(Vector2.up);

        //bulletObj = Instantiate(bulletPrefab, firingPointS.position, Quaternion.identity);
        //bulletScript = bulletObj.GetComponent<StinkBugBullet>();
        //bulletScript.SetDirection(Vector2.down);

        //bulletObj = Instantiate(bulletPrefab, firingPointE.position, Quaternion.identity);
        //bulletScript = bulletObj.GetComponent<StinkBugBullet>();
        //bulletScript.SetDirection(Vector2.right);

        //bulletObj = Instantiate(bulletPrefab, firingPointW.position, Quaternion.identity);
        //bulletScript = bulletObj.GetComponent<StinkBugBullet>();
        //bulletScript.SetDirection(Vector2.left);

        //bulletObj = Instantiate(bulletPrefab, firingPointNE.position, Quaternion.identity);
        //bulletScript = bulletObj.GetComponent<StinkBugBullet>();
        //bulletScript.SetDirection(firingPointNE.localPosition);

        for(int i = 0; i < firingPoints.Length; i++)
        {
            GameObject bulletObj = Instantiate(bulletPrefab, firingPoints[i].position, Quaternion.identity);
            StinkBugBullet bulletScript = bulletObj.GetComponent<StinkBugBullet>();
            bulletScript.SetDirection(firingPoints[i].localPosition);
        }
        //bulletScript.SetTarget(target);
    }

    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, towerCore.GetTargetingRange(), (Vector2)transform.position, 0f, enemyMask);

        if (hits.Length > 0)
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
        //upgradeUI.SetActive(true);
    }

    public void CloseUpgradeUI()
    {
        //upgradeUI.SetActive(false);
        //UIManager.main.SetHoveringState(false);
    }

    public void Upgrade()
    {
        if (CalculateCost() > LevelManager.main.currency)
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
       return 0;
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
        //turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
