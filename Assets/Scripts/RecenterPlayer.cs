using UnityEngine;
using UnityEngine.XR;
using System.Collections.Generic;

public class RecenterPlayer : MonoBehaviour
{
    public void Recenter()
    {
        List<XRInputSubsystem> subsystems = new List<XRInputSubsystem>();
        SubsystemManager.GetSubsystems(subsystems);

        foreach (var subsystem in subsystems)
        {
            if (subsystem != null)
            {
                subsystem.TryRecenter();
            }
        }
    }
}