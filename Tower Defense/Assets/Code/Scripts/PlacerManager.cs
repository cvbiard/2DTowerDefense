using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacerManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private GameObject towerPrefab;
    [SerializeField] private GameObject towerPlaceRadius;
    [SerializeField] private LayerMask turretMask;
    [SerializeField] private Color canPlace;
    [SerializeField] private Color cannotPlace;
    [SerializeField] private GameObject groundDetector;

    private Vector2 mousePosition;

    private Transform target;

    private GameObject towerObject;
    public Turret turret;

    // Start is called before the first frame update
    void Start()
    {
        sr.sprite = towerPrefab.GetComponent<TowerCore>().GetSprite();
        towerPlaceRadius.transform.localScale = new Vector2 (towerPrefab.GetComponent<TowerCore>().GetPlacingRange(), towerPrefab.GetComponent<TowerCore>().GetPlacingRange());
    }

    // Update is called once per frame
    void Update()
    {
       
            mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = mousePosition;

       
        if(FindOtherTurrets() == true || groundDetector.GetComponent<GroundDetector>().GetCanPlace() == false)
        {
            towerPlaceRadius.GetComponent<SpriteRenderer>().color = cannotPlace;
            return;
        }
        else
        {
            towerPlaceRadius.GetComponent<SpriteRenderer>().color = canPlace;
        }

        

        if (Input.GetMouseButtonDown(0))
        {
            Tower towerToBuild = BuildManager.main.GetSelectedTower();

            if (towerToBuild.cost > LevelManager.main.currency)
            {
                Debug.Log("You can't afford this tower");
                return;
            }

            if(groundDetector.GetComponent<GroundDetector>().GetCanPlace())
            {
                LevelManager.main.SpendCurrency(towerToBuild.cost);

                towerObject = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);
                //turret = towerObject.GetComponent<Turret>();
                Destroy(gameObject);
            }
            
        }



    }

    private bool FindOtherTurrets()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, towerPrefab.GetComponent<TowerCore>().GetPlacingRange()/2, (Vector2)transform.position, 0f, turretMask);

        if (hits.Length > 0)
        {
            //Debug.Log("overlapping anoter turret");
            
            return true;
        }
        else
        {
            
            return false;
        }
    }
}
