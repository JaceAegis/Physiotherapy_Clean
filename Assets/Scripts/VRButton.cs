using UnityEngine;

public class VRButton : MonoBehaviour
{
    public SceneLoader loader;
    private bool pressed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!pressed && other.CompareTag("VRHand"))
        {
            pressed = true;
            loader.LoadGame();
        }
    }
}