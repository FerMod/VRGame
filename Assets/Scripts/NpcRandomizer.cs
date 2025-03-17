using System;
using UnityEditor;
using UnityEngine;

public class NpcRandomizer : MonoBehaviour
{
    public Parts[] randomizableParts;

    private void Start()
    {
        RandomizeAllParts();
    }

    public void RandomizeAllParts()
    {
        foreach (var partArray in randomizableParts)
        {
            RandomizePart(partArray.parts);
        }
    }

    public void RandomizePart(GameObject[] array)
    {
        DeactivateAll(array);
        ActivateRandom(array);
    }

    private void DeactivateAll(GameObject[] array)
    {
        foreach (var part in array)
        {
            SetActive(part, false);
        }
    }

    public void ActivateRandom(GameObject[] array)
    {
        SetActive(array.Random(), true);
    }

    private void SetActive(GameObject gameObject, bool active)
    {
        if (gameObject == null) return;
        gameObject.SetActive(active);
    }
}

static class ArrayExtension
{
    public static T Random<T>(this T[] array)
    {
        return array[array.RandomIndex()];
    }

    public static int RandomIndex<T>(this T[] array)
    {
        return UnityEngine.Random.Range(0, array.Length);
    }
}

[Serializable]
public class Parts
{
    public GameObject[] parts;
}
