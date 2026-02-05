using UnityEngine;
using UnityEngine.Events;

public class Life : MonoBehaviour
{
    [SerializeField] int maxHP;
    public int HP;
  
    [HideInInspector] public bool IsHit;
    [HideInInspector] public bool IsDeath;
    [SerializeField] private float destroyTime = 1f;

    [SerializeField] private UnityEvent<int, int> OnHPChanged;


    // GAME //-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-
    private void Awake()
    {
        HP = maxHP;
        OnHPChanged.Invoke(HP, maxHP);
    }


    // FUNCTIONS //-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-
    public void TakeDamage(int damage)
    {
        HP -= damage;

        if (HP <= 0)
        {
            // SetUp the Death sequence
            OnHPChanged.Invoke(HP, maxHP);
            this.tag = "DEATH";
            IsDeath = true;

            Debug.Log($"{gameObject.name} is dead");
            Destroy(gameObject, destroyTime);
        }
        else
        {
            OnHPChanged.Invoke(HP, maxHP);
            IsHit = true;
            Debug.Log($"{gameObject.name} has {HP}/{maxHP}");
        }
    }

    public void TakeHeal(int amout)
    {
        HP += amout;

        OnHPChanged.Invoke(HP, maxHP);
        if (HP > maxHP) HP = maxHP;
        Debug.Log($"{gameObject.name} ha {HP}/{maxHP}");
    }
}