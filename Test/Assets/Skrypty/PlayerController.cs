using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlaterController : MonoBehaviour
{
    // etap 1
    public float moveSpeed;
    public float jumpForce;

    private Rigidbody2D rb; //PAMIĘTAJ O 2D

		// --- NOWE ---

    //skakanie
    public LayerMask WhatIsGround; // Sprawdzamy co jest podłogą a co nie
    private bool isGrounded;

    // nie skakanie w nieskończoność 
    public Transform GroundCheckPoint, GroundCheckPoint2;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // 2D !!!
    }

    void Update()
    {
        //poruszanie horyzontalne
        float x = 0f;
        if (Keyboard.current == null)
            return;

        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) 
            x -= 1f;
        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) 
            x += 1f;

        rb.linearVelocity = new Vector2(x * moveSpeed, rb.linearVelocity.y);
				//Dajemy prędkość do rigitbody, bierzemy Axis Horizontal (AD strzałki)

				// --- NOWE ---
                                                                                                    // Mnożymy przez prędnoś
        //sprawdza czy player jest na ziemi

        isGrounded = Physics2D.OverlapCircle(GroundCheckPoint.position, 0.1f, WhatIsGround)
			 || Physics2D.OverlapCircle(GroundCheckPoint2.position, 0.1f, WhatIsGround);

        // Jak zmienisz na 1f to masz double jump :o tylko nie jest to idealne
				// bo możesz zrobić quad jumpa przy krawędziach


        // OverlapCircle sprawdza czy są collidery w okół wyznaczonej pozycji. 
				//Tworzy okrągły trigger o promienu .1f i sprawdza leyer 

        //skakanie

        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded) // DODAJEMY  isgrounded jako warunek
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

    }
}
