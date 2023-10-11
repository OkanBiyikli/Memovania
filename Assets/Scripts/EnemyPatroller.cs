using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemyPatroller : MonoBehaviour
{
    public Transform[] patrolPoints;
    private int currentPoint;

    public float moveSpeed, waitAtPoints;
    private float wiatCounter;

    public float jumpForce;

    public Rigidbody2D theRB;
    public Animator anim;

    void Start()
    {
        wiatCounter = waitAtPoints;

        foreach(Transform pPoint in patrolPoints)
        {
            pPoint.SetParent(null);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(transform.position.x - patrolPoints[currentPoint].position.x) > .2f)
        {
            if(transform.position.x < patrolPoints[currentPoint].position.x)
            {
                theRB.velocity = new Vector2(moveSpeed, theRB.velocity.y);
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }else
            {
                theRB.velocity = new Vector2(-moveSpeed, theRB.velocity.y);
                transform.localScale = Vector3.one;
            }

            if(transform.position.y < patrolPoints[currentPoint].position.y - .5f && theRB.velocity.y < .1f)
            {//eğer pozisyonumuz gittiğimiz patrol noktasından aşağıda bi yerdeyse y ye jumpforce ekle
            //unity içinden patrol noktalarını düzgünce ayarla(zemine yakın yerleştir)
                theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
            }
        }else
        {
            theRB.velocity = new Vector2(0f, theRB.velocity.y);

            wiatCounter -= Time.deltaTime;
            if(wiatCounter <= 0)
            {
                wiatCounter = waitAtPoints;

                currentPoint++;

                if(currentPoint >= patrolPoints.Length)
                {
                    currentPoint = 0;
                }
            }
        }

        anim.SetFloat("speed", Mathf.Abs(theRB.velocity.x));
    }
}
