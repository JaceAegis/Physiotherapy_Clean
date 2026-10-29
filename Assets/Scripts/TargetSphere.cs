using UnityEngine;

public class TargetSphere : MonoBehaviour
{
    public int passesRemaining = 5;
    [HideInInspector] public ExerciseManager manager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PhysioTool"))
        {
            if (manager != null) manager.SpherePassed(this);
        }
    }
}