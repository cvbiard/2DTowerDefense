using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Point : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private GameObject spikePrefab;

    [Header("Attribute")]
    [SerializeField] private bool hasSpike = false;

    public void EnableSpike()
    {
        if(hasSpike == false)
        {
            hasSpike = true;
            GameObject spikeObj = Instantiate(spikePrefab, transform.position, Quaternion.identity);
            spikeObj.GetComponent<Spike>().SetParentPoint(this);
        }

    }

    public void SpikeDestroyed()
    {
        hasSpike = false;
    }
}
