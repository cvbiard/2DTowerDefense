using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacerManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private GameObject towerPrefab;

    private Vector2 mousePosition;
    public float moveSpeed = 0.1f;

    private GameObject towerObject;
    public Turret turret;

    // Start is called before the first frame update
    void Start()
    {
        sr.sprite = towerPrefab.GetComponent<Turret>().GetSprite().sprite;
    }

    // Update is called once per frame
    void Update()
    {
       
            mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = mousePosition;


        if (Input.GetMouseButtonDown(0))
        {
            Tower towerToBuild = BuildManager.main.GetSelectedTower();

            if (towerToBuild.cost > LevelManager.main.currency)
            {
                Debug.Log("You can't afford this tower");
                return;
            }

            LevelManager.main.SpendCurrency(towerToBuild.cost);

           towerObject = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);
           //turret = towerObject.GetComponent<Turret>();
           Destroy(gameObject);
        }



    }

    private void OnMouseDown()
    {
       
    }
}
