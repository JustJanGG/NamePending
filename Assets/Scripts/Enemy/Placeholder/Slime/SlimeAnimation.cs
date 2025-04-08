using UnityEngine;

public class SlimeAnimation : MonoBehaviour
{
    [Header("Components")]
    private Animator animator;
    private Vector2 direction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        direction = GetComponent<SlimeMovement>().direction;

        animator.SetFloat("XSpeed", direction.x);
        animator.SetFloat("YSpeed", direction.y);
    }
}
