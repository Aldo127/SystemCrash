using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeteorBar : MonoBehaviour
{

    private Timeee timeee;
    private Image barImage;

    private void Awake()
    {
            barImage = transform.Find("bar").GetComponent<Image>();

            timeee = new Timeee();
    }

    private void Update()
    {
        timeee.Update();

        barImage.fillAmount = timeee.GetTimeNormalized();
    }
}
    public class Timeee
    {
        public const int MANA_MAX = 100;
        private float timeAmount;
        private float timeRegenAmount;

        public Timeee()
        {
            timeAmount = 0;
            timeRegenAmount = 30f;
        }
        public void Update()
        {
            timeAmount += timeRegenAmount * Time.deltaTime;
        }
         public void TrySpendMana(int amount)
        {
            if (timeAmount >= amount)
            {
                timeAmount -= amount;
            }
        }

        public float GetTimeNormalized()
        {
            return timeAmount / MANA_MAX;
        }
    }

   

