using UnityEngine;
using UnityEngine.InputSystem;

public class ShootFireball : MonoBehaviour
{
    private FireballProto fireballProto;
    public GameObject fireballPrefab;

    private float lastFireballTime = 0.0f;

    private void Start()
    {
        fireballProto = fireballPrefab.GetComponent<FireballProto>();
    }

    public void Shoot(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && Time.time > fireballProto.castDelay + lastFireballTime)
        {
            Instantiate(fireballPrefab, transform.position, Quaternion.identity);
            lastFireballTime = Time.time;
        }
    }

}
