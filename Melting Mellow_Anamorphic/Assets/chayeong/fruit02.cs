using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fruit02 : MonoBehaviour
{
    private GameObject hand;
    public GameObject topping;
    public GameObject owl;

    private Vector3 handPos;
    Vector3 startPos;

    private bool handGrab;
    private bool cheese;


    void Start()
    {
        hand = GameObject.Find("Bone.011_end");

        startPos = gameObject.transform.position;
    }

    void Update()
    {
        handPos = hand.transform.position;

        if(handGrab)
        {
            gameObject.transform.position = new Vector3(handPos.x + 0.3f, handPos.y, handPos.z);
        }
        if (cheese)
        {
            gameObject.transform.position = startPos;
            cheese = false;
        }


        //if (pos.x <= cubeCheeseColl.bounds.max.x && pos.x >= cubeCheeseColl.bounds.max.x
        //    && pos.y <= cubeCheeseColl.bounds.max.y && pos.y >= cubeCheeseColl.bounds.max.y
        //    && pos.z <= cubeCheeseColl.bounds.max.z && pos.z >= cubeCheeseColl.bounds.max.z)
        //{
        //    handGrab = false;   
        //    gameObject.transform.position = startPos;
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("hand"))
        {
            handGrab = true;
        }
        if(other.gameObject.CompareTag("cheese"))
        {
            Animator toppingAni = topping.GetComponent<Animator>();
            toppingAni.SetTrigger("toppingActive");

            Animator owlAni = owl.GetComponent<Animator>();
            owlAni.SetTrigger("toppingOwl");
            handGrab = false;
            cheese = true;
        }
    }


    //IEnumerator strawberryActive()
    //{
    //    yield return new WaitForSeconds(4f);

    //    gameObject.GetComponent<MeshRenderer>().enabled = true;
    //    gameObject.GetComponent<Collider>().enabled = true;

    //    StartCoroutine(strawberrySpawn());
    //    StopCoroutine(strawberryActive());
    //}

    //IEnumerator strawberrySpawn()
    //{
    //    yield return new WaitForSeconds(8f);

    //    handGrab = false;
    //    dish = false;
    //    gameObject.transform.position = spawnPos.position;
    //}


}
