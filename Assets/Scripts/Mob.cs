using UnityEngine;
using System.Collections;

public class Mob : MonoBehaviour
{
    public Spawner spawner;

    public int moneyDrop;
    public int experienceDrop;
    public int killsPlayerLive = 1;

    public float maxHealth;
    public float curHealth;

    public float maxShield;
    public float curShield;

    public float maxSpeed;
    public float curSpeed;

    public float maxArmor;
    public float curArmor;

    public bool isFlying = false;

    public float regenerationHealth;
    public float regenerationShield;
    public float regenerationArmor;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        curHealth = Mathf.Clamp(curHealth + regenerationHealth, 0, maxHealth);
        curShield = Mathf.Clamp(curShield + regenerationShield, 0, maxShield);
        curArmor = Mathf.Clamp(curArmor + regenerationArmor, 0, maxArmor);

    }


    public void TakeDamage(float damageAmount)
    {
        float damageToShield = Mathf.Clamp(damageAmount, 0, curShield);
        curShield = Mathf.Clamp(curShield - damageToShield, 0, curShield);
        damageAmount = Mathf.Clamp(damageAmount - damageToShield, 0, damageAmount);

        float damageToArmor = Mathf.Clamp(damageAmount, 0, curArmor);
        curArmor = Mathf.Clamp(curArmor - damageToArmor, 0, curArmor);
        damageAmount = Mathf.Clamp(damageAmount - damageToArmor, 0, damageAmount);

        curHealth = Mathf.Clamp(curHealth - damageAmount, 0, curHealth);

        if (curHealth <= 0)
        {


            spawner.MobsOfWave.Remove(gameObject);
            Destroy(gameObject);



        }

    }

    public void MobFinishedMace()
    {
        spawner.MobsOfWave.Remove(gameObject);
        Destroy(gameObject);
    }

}
