using UnityEngine;

public class Health : MonoBehaviour
{

    //SerializedFiled allows to be seen in the editor
    [SerializeField] private float startingHealth;
    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private AudioClip deathSound;

    //holds the player's current health. get set/being public is there so it can only curently be grabbed from Healthbar.cs
    public float currentHealth {get; private set; }

    private void Awake()
    {
        //Sets the player health to the max health
        currentHealth = startingHealth;
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            SoundManager.instance.PlaySound(hurtSound);
            //damage taken, but player is still alive
        }
        else
        {
            SoundManager.instance.PlaySound(deathSound);
            GetComponent<PlayerMovement>().SetCanMove(false);
            GetComponent<PlayerMovement>().PlayerDeath();
            //Leave blank for now
        }
    }

    // Method used to increase the player's health when they pick up a HealthCollectable
    public void IncreaseHealth(float healthToGive)
    {
        currentHealth += healthToGive;
    }

    // Update is called once per frame
    /*void Update()
    {
        //Test input to take damage.
        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(1);
            GetComponent<PlayerMovement>().GetHit();
        }  
    }*/
}