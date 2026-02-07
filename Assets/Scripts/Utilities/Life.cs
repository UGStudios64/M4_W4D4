using UnityEngine;
using UnityEngine.Events;

public class Life : MonoBehaviour
{
    [SerializeField] private int maxHP;
    private int HP;
    private Collider col;

    [SerializeField] private UnityEvent<int, int> OnHPChanged;
    [SerializeField] private UnityEvent OnDeath;

    #region// GET -----------------------------------------------------------------------------------------------------------------------
    public int GetHP() => HP;
    #endregion


    // GAME //-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-
    private void Awake()
    {
        HP = maxHP;
        OnHPChanged.Invoke(HP, maxHP);
        col = GetComponentInChildren<Collider>();
    }


    // FUNCTIONS //-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-
    public void TakeDamage(int damage)
    {
        HP -= damage;

        if (HP <= 0)
        {
            // SetUp the Death sequence ----------
            OnHPChanged.Invoke(HP, maxHP);
            OnDeath.Invoke();

            this.tag = "DEAD";
            col.tag = "DEAD";

            Debug.Log($"{gameObject.name} is dead");
        }
        else
        {
            OnHPChanged.Invoke(HP, maxHP);
            Debug.Log($"{gameObject.name} has {HP}/{maxHP}");
        }
    }


    public void TakeHeal(int amout)
    {
        HP += amout;

        if (HP > maxHP) HP = maxHP;
        OnHPChanged.Invoke(HP, maxHP);

        Debug.Log($"{gameObject.name} ha {HP}/{maxHP}");
    }
}