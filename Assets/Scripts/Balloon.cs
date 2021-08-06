using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.up * Random.Range(70, 170), ForceMode2D.Force);
        Destroy(gameObject, 15f);
    }

   
}
