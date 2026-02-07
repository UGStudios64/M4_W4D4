using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private LayerMask ground;
    [SerializeField] private float radius;
    private bool IsGrounded;

    #region// GET -----------------------------------------------------------------------------------------------------------------------
    public bool GetIsGrounded() => IsGrounded;
    #endregion


    // GAME //-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-
    private void Update()
    { IsGrounded = GroundChecking(); }


    // FUNCTIONS //-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-
    private bool GroundChecking()
    { return Physics.CheckSphere(transform.position, radius, ground); }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}