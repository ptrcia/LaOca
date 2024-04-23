using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRaycast : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    [SerializeField]
    LayerMask layer;
    [HideInInspector]
    public int diceResult;

    private void Update()
    {
        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.green);
    }

    public int CheckForColliders()
    {
        ray = new Ray(transform.position, transform.up);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer))
        {
            //diceResult = Int32.Parse(gameObject.name);
            diceResult = Int32.Parse(gameObject.tag.ToString());
            Debug.Log("DiceRaycast ->" + diceResult);

            return diceResult;
        } else { 
            return 0;
        }
    }
    /*public void CheckForColliders()
    {
        ray = new Ray(transform.position, transform.up);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer))
        {
            diceResult = Int32.Parse(gameObject.tag.ToString());
            Debug.Log(diceResult);
        }
    }*/
}
