using UnityEngine;

public class Life : MonoBehaviour
{
    [SerializeField] int maxhp;
    public int hp;
  
    [HideInInspector] public bool IsHit;
    [HideInInspector] public bool IsDeath;
    [SerializeField] private float destroyTime = 1f;


    // GAME //-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-
    private void Awake()
    {
        hp = maxhp;
    }


    // FUNCTIONS //-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-
    public void TakeDamage(int damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            // SetUp the Death sequence
            GetComponentInChildren<Collider>().enabled = false;
            this.tag = "DEATH";
            IsDeath = true;

            Debug.Log($"{gameObject.name} is dead");
            Destroy(gameObject, destroyTime);
        }
        else
        {
            IsHit = true;
            Debug.Log($"{gameObject.name} has {hp}/{maxhp}");
        }
    }

    public void TakeHeal(int amout)
    {
        hp += amout;

        if (hp > maxhp) hp = maxhp;
        Debug.Log($"{gameObject.name} ha {hp}/{maxhp}");
    }
}