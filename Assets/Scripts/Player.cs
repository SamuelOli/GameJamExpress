using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float Speed;
    private Animator anim;
    [SerializeField]
    private float raio;
    [SerializeField]
    private LayerMask enemys;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        transform.position += dir * Speed * Time.deltaTime;

        anim.SetFloat("Speed", dir.magnitude);
        anim.SetFloat("Horizontal", dir.x);
        anim.SetFloat("Vertical",dir.y);
        if(Input.GetMouseButtonDown(0))
        {
            Atack();
        }
    }

    void Atack()
    {
        Collider2D [] col = Physics2D.OverlapCircleAll(transform.position, raio,enemys);
        foreach (Collider2D c in col) 
        { 
         Destroy(c.gameObject);
        }
    }
}
