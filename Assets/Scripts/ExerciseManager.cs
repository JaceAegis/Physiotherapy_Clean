using UnityEngine;
using System.Collections.Generic;

public class ExerciseManager : MonoBehaviour
{
    [Header("Exercise Sequences")]
    public List<TargetSphere> exercise1Spheres = new List<TargetSphere>();
    public List<TargetSphere> exercise2Spheres = new List<TargetSphere>();
    public List<TargetSphere> exercise3Spheres = new List<TargetSphere>();

    [Header("Sphere Materials")]
    public Material greenMat;
    public Material yellowMat;
    public Material blueMat;

    private List<TargetSphere> currentList = new List<TargetSphere>();
    private int activeExerciseNum = 1;
    private int currentTargetIdx = 0;
    private bool exerciseStarted = false;

    void Start()
    {
        SetSpheresActive(exercise1Spheres, false);
        SetSpheresActive(exercise2Spheres, false);
        SetSpheresActive(exercise3Spheres, false);

        RegisterManager(exercise1Spheres);
        RegisterManager(exercise2Spheres);
        RegisterManager(exercise3Spheres);
    }

    void Update()
    {
        if (!exerciseStarted && ToolTracker.heldCount >= 1)
        {
            StartExercise(1);
        }
    }

    void RegisterManager(List<TargetSphere> spheres)
    {
        foreach (var s in spheres)
        {
            if (s != null) s.manager = this;
        }
    }

    void StartExercise(int exerciseNum)
    {
        exerciseStarted = true;
        activeExerciseNum = exerciseNum;
        currentTargetIdx = 0;

        SetSpheresActive(exercise1Spheres, false);
        SetSpheresActive(exercise2Spheres, false);
        SetSpheresActive(exercise3Spheres, false);

        if (exerciseNum == 1)
        {
            ResetSpheres(exercise1Spheres);
            currentList = new List<TargetSphere>(exercise1Spheres);
        }
        else if (exerciseNum == 2)
        {
            ResetSpheres(exercise2Spheres);
            currentList = new List<TargetSphere>(exercise2Spheres);
        }
        else if (exerciseNum == 3)
        {
            ResetSpheres(exercise3Spheres);
            currentList = new List<TargetSphere>(exercise3Spheres);
        }

        UpdateColors();
    }

    public void SpherePassed(TargetSphere sphere)
    {
        if (!exerciseStarted || currentList.Count == 0) return;

        if (sphere == currentList[currentTargetIdx])
        {
            sphere.passesRemaining--;

            if (sphere.passesRemaining <= 0)
            {
                sphere.gameObject.SetActive(false);
                currentList.RemoveAt(currentTargetIdx);

                if (currentList.Count == 0)
                {
                    int nextNum = 1;
                    if (activeExerciseNum == 1) nextNum = 2;
                    else if (activeExerciseNum == 2) nextNum = 3;
                    else if (activeExerciseNum == 3) nextNum = 1;

                    StartExercise(nextNum);
                    return;
                }

                if (currentTargetIdx >= currentList.Count) currentTargetIdx = 0;
            }
            else
            {
                currentTargetIdx = (currentTargetIdx + 1) % currentList.Count;
            }

            UpdateColors();
        }
    }

    void UpdateColors()
    {
        if (currentList.Count == 0) return;

        for (int i = 0; i < currentList.Count; i++)
        {
            MeshRenderer rend = currentList[i].GetComponent<MeshRenderer>();
            if (rend == null) continue;

            if (i == currentTargetIdx) rend.material = greenMat;
            else if (i == (currentTargetIdx + 1) % currentList.Count) rend.material = yellowMat;
            else rend.material = blueMat;
        }
    }

    void SetSpheresActive(List<TargetSphere> spheres, bool active)
    {
        foreach (var s in spheres)
            if (s != null) s.gameObject.SetActive(active);
    }

    void ResetSpheres(List<TargetSphere> spheres)
    {
        foreach (var s in spheres)
        {
            if (s != null)
            {
                s.passesRemaining = 5;
                s.gameObject.SetActive(true);
            }
        }
    }
}