using System;
using UnityEngine;

public class NpcRandomizer : MonoBehaviour
{
    public PartList[] randomizableParts;

    private void Start()
    {
        RandomizeAllParts();
        // TODO: Obtain material from active game objects and randomize colors
        // Should take into account the color of the skin and have it the same
    }

    public void RandomizeAllParts()
    {
        foreach (var partList in randomizableParts)
        {
            partList.ActivateRandom();
        }
    }
}

[Serializable]
public class PartList
{
    public PartGroup[] parts;

    public void HideAll()
    {
        foreach (var part in parts)
        {
            part.SetActive(false);
        }
    }

    public void ActivateRandom()
    {
        HideAll();

        var randomPart = parts.Random();
        randomPart.SetActive(true);
        //randomPart.HideIncompatibleParts();
    }
}

[Serializable]
public class PartGroup
{
    public GameObject[] parts;

    //public GameObject[] incompatibleParts;

    public void SetActive(bool value)
    {
        foreach (var part in parts)
        {
            part.SetActive(value);
        }
    }

    //public void HideIncompatibleParts()
    //{
    //    foreach (var part in incompatibleParts)
    //    {
    //        part.SetActive(false);
    //    }
    //}
}
