using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject towerPrefab;

    private bool canPlace = false;

    public bool GetCanPlace()
    {
        return canPlace;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.GetComponent<Plot>() != null)
        {
            if (other.gameObject.GetComponent<Plot>().GetCurrentGround() == towerPrefab.GetComponent<TowerCore>().GetRequiredGround())
            {
                Debug.Log("dirt under me");
                canPlace = true;
            }
            else
            {
                Debug.Log(" no dirt under me");
                canPlace = false;
            }

        }
        else
        {
            canPlace = false;
        }


    }
}
