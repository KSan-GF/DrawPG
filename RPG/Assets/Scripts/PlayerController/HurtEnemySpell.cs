﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEnemySpell : MonoBehaviour
{

    public int playerDamage;
    public GameObject damageEffect;
    public GameObject damageDisplay;
    private Vector3 damageUIPosition;


    public float stunTime;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            //Destroy(other.gameObject);
            other.gameObject.GetComponent<EnemyHealthManager>().hurtEnemy(playerDamage);
            other.gameObject.GetComponent<SlimeController>().stunEnemy();


            Instantiate(damageEffect, transform.position, transform.rotation);
            //damageUIPosition = new Vector3(hitPoint.position.x, hitPoint.position.y + 1, hitPoint.position.z);
            var clone = (GameObject)Instantiate(damageDisplay, transform.position , Quaternion.Euler(Vector3.zero));
            clone.GetComponent<damageDisplay>().setDamageToDisplay(playerDamage);

        }
    }
}
