using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fruit02 : MonoBehaviour
{
    private GameObject hand;

    private Vector3 handPos;
    public Transform spawnPos;

    private bool handGrab;
    private bool dish;

    void Start()
    {
        hand = GameObject.Find("Bone.011_end");

        StartCoroutine(strawberryActive());
    }

    void Update()
    {
        handPos = hand.transform.position;

        if(handGrab)
        {
            gameObject.transform.position = new Vector3(handPos.x + 0.3f, handPos.y, handPos.z);
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
            dish = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("dish") && dish)
        {
            handGrab = false;
            gameObject.transform.position = other.transform.position;
        }

    }

    IEnumerator strawberryActive()
    {
        yield return new WaitForSeconds(4f);

        gameObject.GetComponent<MeshRenderer>().enabled = true;
        gameObject.GetComponent<Collider>().enabled = true;

        StartCoroutine(strawberrySpawn());
        StopCoroutine(strawberryActive());
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
