using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballPool : MonoBehaviour
{
    public static FireballPool fireballInstance;

    [SerializeField]
    private GameObject pooledFireball;
    private bool notEnoughFireballsInPool = true;

    private List<GameObject> fireballs;

    private void Awake()
    {
        fireballInstance = this;
    }

    void Start()
    {
        fireballs = new List<GameObject>();
    }

    public GameObject GetFireball()
    {
        for (int i = 0; i < fireballs.Count; i++)
        {
            if (!fireballs[i].activeInHierarchy)
            {
                return fireballs[i];
            }
        }

        if (notEnoughFireballsInPool)
        {
            GameObject fb = Instantiate(pooledFireball);
            fb.SetActive(false);
            fireballs.Add(fb);
            return fb;
        }

        return null;
    }

}
