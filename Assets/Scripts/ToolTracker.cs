using UnityEngine;

public class ToolTracker : MonoBehaviour
{
    public static int heldCount = 0;

    void Start()
    {
        heldCount = 0;
    }

    public void GrabbedTool()
    {
        heldCount++;
    }

    public void DroppedTool()
    {
        heldCount--;
    }
}