using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(RectTransform))]
public class TileRenderer : MonoBehaviour
{

    public Sprite sprite;

    // Use this for initialization
    void Start()
    {
        try
        {
            var rect = this.GetComponent<RectTransform>();
            var parent = new GameObject("parent");

           // for (int i = 0; i < 10; i++)
           // {
                var go = new GameObject("Test_" );
                var sp = go.AddComponent<SpriteRenderer>();
                sp.sprite = sprite;

                go.transform.parent = parent.transform;
            //}
        }
        catch (Exception ex)
        {
            Debug.Log(ex.ToString());
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
