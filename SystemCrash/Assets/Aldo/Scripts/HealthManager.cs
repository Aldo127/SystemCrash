using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int health;
    public int attack;
    public GameObject character;

    [SerializeField] GameObject fireVFX;

    //color stuff
    MeshRenderer meshRenderer;
    Color origColor;
    float flashTime = .15f;

    //XPdrop
    public GameObject XPdrop;
    public Transform transform;

    public void TakeDamage(int amount)
    {
        health -= amount;
        FlashStart();
        
    }

    public void DealDamage(GameObject target)
    {
        var atm = target.GetComponent<HealthManager>();
        if(atm != null)
        {
            atm.TakeDamage(attack);
        }
    }

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        origColor = meshRenderer.material.color;
    }

    public void Update()
    {
        if (health < 0)
        {
            // StartCoroutine(Death());
            Destroy(gameObject);
            GameObject explosion = Instantiate(fireVFX, transform.position, transform.rotation);
            DropXP();
        }
    }

    void FlashStart()
    {
        meshRenderer.material.color = Color.red;
        Invoke("FlashStop", flashTime);
    }

    void FlashStop()
    {
        meshRenderer.material.color = origColor;
    }

    void DropXP()
    {
        Vector3 position = transform.position; //position of enemy
        GameObject XP = Instantiate(XPdrop, position, Quaternion.identity);

    }
    //IEnumerator Death()
    //{
    //    character.GetComponent<Animator>().Play("Death");
    //    yield return null;
    //}
}
