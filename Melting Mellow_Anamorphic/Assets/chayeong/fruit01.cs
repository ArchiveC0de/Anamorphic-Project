using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fruit01 : MonoBehaviour
{
    private GameObject hand;

    private Vector3 handPos;
    public Transform spawnPos;

    private bool handGrab;
    private bool dish;

    // Start is called before the first frame update
    void Start()
    {
        hand = GameObject.Find("Bone.011_end");

        StartCoroutine(strawberrySpawn());
    }

    // Update is called once per frame
    void Update()
    {
        handPos = hand.transform.position;

        if(handGrab)
        {
            gameObject.transform.position = new Vector3(handPos.x + 0.3f, handPos.y, handPos.z);
        }
        if(dish)
        {
            gameObject.transform.position = GameObject.Find("handGrab").transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("hand"))
        {
            handGrab = true;
        }
        if (other.gameObject.CompareTag("dish"))
        {
            handGrab = false;
            dish = true;
        }

    }

    IEnumerator strawberrySpawn()
    {
        yield return new WaitForSeconds(8f);

        dish = false;
        gameObject.transform.position = spawnPos.position;
    }


    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.CompareTag("hand"))
    //    {
    //        gameObject.transform.position = new Vector3(other.transform.position.x + 0.3f, transform.position.y, other.transform.position.z);
    //    }
    //}
}
