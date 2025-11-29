using System.Collections.Generic;
using UnityEngine;

public class KillQuest : Quest
{
    [SerializeField] private List<Transform> targets;
    [SerializeField] private int targetCount;
    private int destroyed;

    private void Update()
    {
        int destroyedEnemies = 0;

        foreach (var item in targets)
        {
            if (item == null)
            {
                destroyedEnemies++;
            }
        }

        destroyed = destroyedEnemies;

        if (destroyed >= targetCount)
        {
            Complete();
        }
    }

    private void OnValidate()
    {
        if (targetCount < 0)
        {
            targetCount = 0;
        }

        if (targetCount > targets.Count)
        {
            targetCount = targets.Count;
        }
    }

    public override string GetDescription()
    {
        return destroyed.ToString() + "/" + targetCount.ToString();
    }

    public override string GetHeader()
    {
        return "Óםטקעמזüעו גנאדמג";
    }

    public override List<Vector3> GetTargets()
    {
        List<Vector3> res = new List<Vector3>();
        foreach (var item in targets)
        {
            if (item != null)
            {
                res.Add(item.position);
            }
        }
        return res;
    }
}
