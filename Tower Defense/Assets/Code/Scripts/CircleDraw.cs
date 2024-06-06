using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleDraw : MonoBehaviour
{
    public int Radius = 10;
    public Sprite Wall;

    // Use this for initialization
    void Start()
    {
        DrawCircle();

    }

    private void DrawCircle()
    {
        int radiusSqrd = Radius * Radius;

        for (int x = -Radius; x <= Radius; x++)
        {
            int y = (int)(Mathf.Sqrt(radiusSqrd - x * x) + 0.5f);

            //This draws one half of the the circle
            GameObject go = new GameObject(x + ", " + y);

            go.transform.position = new Vector2(x, y);
            go.transform.parent = transform;

            SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
            sr.sprite = Wall;



            //This draws the other half of the circle
            //Because squaring includes a ±
            GameObject go2 = new GameObject(x + ", " + -y);

            go2.transform.position = new Vector2(x, -y);

            SpriteRenderer sr2 = go2.AddComponent<SpriteRenderer>();
            sr2.sprite = Wall;

            go2.transform.parent = transform;


        }
    }
}
