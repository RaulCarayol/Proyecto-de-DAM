using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalayerController : MonoBehaviour
{
    public float velocidad = 5f;
    Animator anim;
    Rigidbody2D rb2d;
    Vector2 movimiento;
    void Start()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
         movimiento = new Vector2(Input.GetAxisRaw("Horizontal"),
                                         Input.GetAxisRaw("Vertical"));
        /*
        transform.position = Vector2.MoveTowards(
            transform.position,
            transform.position + movimiento,
            velocidad * Time.deltaTime);
            */
        if (movimiento != Vector2.zero)
        {
            anim.SetFloat("movX", movimiento.x);
            anim.SetFloat("movY", movimiento.y);
            anim.SetBool("caminando", true);
        }
        else
        {
            anim.SetBool("caminando", false);
        }
    }
    private void FixedUpdate()
    {
        rb2d.MovePosition(rb2d.position + movimiento * velocidad * Time.deltaTime);
    }
}
