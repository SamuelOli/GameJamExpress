using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float Speed;
    private Animator anim;
    [SerializeField]
    AudioClip _atack_audio;
    float timer;
    private AudioManager AM;

    void Start()
    {
        anim = GetComponent<Animator>();
        AM = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        transform.position += dir * Speed * Time.deltaTime;

        anim.SetFloat("Speed", dir.magnitude);
        anim.SetFloat("Horizontal", dir.x);
        anim.SetFloat("Vertical",dir.y);

        if(dir != Vector3.zero)
        {
            anim.SetFloat("HI", dir.x);
            anim.SetFloat("VI", dir.y);
        }

        timer -= Time.deltaTime;
        if(Input.GetMouseButtonDown(0) && timer <0)
        {
            Atack();
            timer = 0.7f;
        }
    }

    void Atack()
    {
        anim.SetTrigger("AT");
        AM.Tocar_Audio(_atack_audio);
    }
}
