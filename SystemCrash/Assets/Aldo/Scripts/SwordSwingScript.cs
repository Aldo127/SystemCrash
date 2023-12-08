using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSwingScript : MonoBehaviour
{

    public GameObject Sword;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(SwordSwing());
        }
    }

    IEnumerator SwordSwing()
    {
        Sword.GetComponent<Animator>().Play("SwordSwing1");
        yield return new WaitForEndOfFrame();
       
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

        Sword.GetComponent<Animator>().Play("SwordSwing2");
        yield return new WaitForEndOfFrame();

        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

        Sword.GetComponent<Animator>().Play("SwordSwing1");
        yield return new WaitForEndOfFrame();

        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

        Sword.GetComponent<Animator>().Play("SwordSwing2");
        yield return new WaitForEndOfFrame();

        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

        Sword.GetComponent<Animator>().Play("SwordSwing3");
        yield return new WaitForEndOfFrame();

        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

        Sword.GetComponent<Animator>().Play("SwordSwing1");

        yield return new WaitForEndOfFrame();

        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

        Sword.GetComponent<Animator>().Play("SwordSwing2");

        yield break;
       
    }
}