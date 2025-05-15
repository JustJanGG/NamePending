using UnityEngine;

public interface IMelee
{
    Vector2 direction { get; set; }
    MeleeStats meleeStats { get; set; }

    public void InitiateMelee()
    {
       // no reduced stats
    }

    public void DefaultMeleeBehaviour(GameObject gameObject)
    {
        // Define the arc parameters
        float arcAngle = 45f; // Total angle of the swing (e.g., 90 degrees)
        float swingDuration = 0.2f; // Duration of the swing in seconds
        Vector3 pivotPoint = gameObject.transform.position; // Pivot point for the swing
        Vector3 swingAxis = Vector3.forward; // Axis of rotation (Z-axis for 2D)

        // Start the swing
        gameObject.transform.RotateAround(pivotPoint, swingAxis, -arcAngle / 2); // Start at the leftmost position
        float elapsedTime = 0f;

        // Perform the swing over time
        while (elapsedTime < swingDuration)
        {
            float step = (arcAngle / swingDuration) * Time.deltaTime; // Calculate the step for this frame
            gameObject.transform.RotateAround(pivotPoint, swingAxis, step); // Rotate the sword
            elapsedTime += Time.deltaTime;
        }

        // Ensure the sword ends at the correct position
        gameObject.transform.RotateAround(pivotPoint, swingAxis, arcAngle / 2);

    }

}
