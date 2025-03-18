using System;
using UnityEngine;

public class NpcRandomizer : MonoBehaviour
{
    public PartList[] randomizableParts;

    private void Start()
    {
        RandomizeAllParts();
    }

    public void RandomizeAllParts()
    {
        foreach (var partList in randomizableParts)
        {
            partList.ActivateRandom();
        }
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
public class PartList
{
    public Part[] parts;

    public void HideAll()
    {
        foreach (var part in parts)
        {
            part.gameObject.SetActive(false);
        }
    }

    public void ActivateRandom()
    {
        HideAll();

        var randomPart = parts.Random();
        randomPart.gameObject.SetActive(true);
        randomPart.HideIncompatibleParts();
    }
}

[Serializable]
public class Part
{
    public GameObject gameObject;

    public GameObject[] incompatibleParts;

    public void HideIncompatibleParts()
    {
        foreach (var part in incompatibleParts)
        {
            part.SetActive(false);
        }
    }
}
