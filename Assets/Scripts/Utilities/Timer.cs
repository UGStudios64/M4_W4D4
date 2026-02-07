using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private Life playerLife;
    [SerializeField] private float timer;

    [SerializeField] private bool InMinutes;
    private bool endTimer;

    #region// GET -----------------------------------------------------------------------------------------------------------------------
    public float GetTimeSeconds() => timer;
    #endregion


    // GAME //-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-
    void Start()
    {
        if (!playerLife) Debug.LogWarning($"Missing Life in Timer");
        if (InMinutes) timer = TransformInMinutes(timer);
    }

    void Update()
    {
        if (endTimer) return;
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            timer = 0;
            endTimer = true;
            playerLife.TakeDamage(playerLife.GetHP());

            Debug.Log("TIME'S UP");
        }
    }

    // FUNCTIONS //-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-
    private float TransformInMinutes(float time)
    { return time * 60f; }

    public void AddTime(float extraTime, bool convertInMinutes)
    {
        if (endTimer) return;

        if (convertInMinutes)
        {
            timer += TransformInMinutes(extraTime);
            Debug.Log($"+{TransformInMinutes(extraTime)} => timer");
        }
        else
        {
            timer += extraTime;
            Debug.Log($"+{extraTime} => timer");
        }  
    }
}