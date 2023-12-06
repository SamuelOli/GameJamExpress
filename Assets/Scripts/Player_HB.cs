using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_HB : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D c)
    {
        Destroy(c.gameObject);
    }
}
