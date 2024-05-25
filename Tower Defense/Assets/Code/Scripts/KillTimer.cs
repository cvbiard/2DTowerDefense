using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillTimer : MonoBehaviour
{
    [SerializeField] private float lifeSpan = 1f; //attacks Per Second
    private float timeUntilDeath;

   // Update is called once per frame
    void Update()
    {
        timeUntilDeath += Time.deltaTime;

        if (timeUntilDeath >= lifeSpan)
        {
            Destroy(gameObject);

        }
    }
}
