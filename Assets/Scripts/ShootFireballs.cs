using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootFireballs : MonoBehaviour
{

    [SerializeField]
    private float timeBetweenFireballs = 2f;

    private int stageCounter = 0;

    [SerializeField]
    private int fireballsAmount = 20;

    [SerializeField]
    private float startAngle = 90f, endAngle = 270f;

    private Vector2 fireballMoveDirection;

    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating("Fire", 0f, timeBetweenFireballs / 2);
    }

    private void Fire()
    {
        int transition = 20;
        int reset = 40;

        if (stageCounter < transition && (stageCounter % 2 == 0))
        {
            FireballFanOut();
        }
        else if (stageCounter >= transition)
        {
            FireballTarget();
        }
        stageCounter++;
        if (stageCounter >= reset)
        {
            stageCounter = 0;
        }
    }

    private void FireballFanOut()
    {
        float angleStep = (endAngle - startAngle) / fireballsAmount;
        float angle = startAngle;

        for (int i = 0; i <= fireballsAmount; i++)
        {
            float bulDirX = transform.position.x + Mathf.Sin(angle * Mathf.Deg2Rad);
            float bulDirY = transform.position.y + Mathf.Cos(angle * Mathf.Deg2Rad);

            Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
            Vector2 bulDir = (bulMoveVector - transform.position).normalized;

            GameObject fb = FireballPool.fireballInstance.GetFireball();


            if (fb != null)
            {
                fb.transform.position = transform.position;
                fb.transform.eulerAngles = new Vector3(0, 0, -angle);
                fb.SetActive(true);

                if (fb.GetComponent<Fireball>() != null)
                {
                    fb.GetComponent<Fireball>().SetMoveDirection(bulDir);
                }
            }


            angle += angleStep;
        }
    }

    private void FireballTarget()
    {

        Vector2 bulDir = (player.transform.position - transform.position).normalized;

        GameObject fb = FireballPool.fireballInstance.GetFireball();
        if (fb != null)
        {
            fb.transform.position = transform.position;
            fb.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(bulDir.y, bulDir.x) * Mathf.Rad2Deg);
            fb.SetActive(true);

            if (fb.GetComponent<Fireball>() != null)
            {
                fb.GetComponent<Fireball>().SetMoveDirection(bulDir);
            }
        }
    }

}
