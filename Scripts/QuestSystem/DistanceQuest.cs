using System.Collections.Generic;
using UnityEngine;

public class DistanceQuest : Quest
{
    [SerializeField] private Transform target;
    private Transform playerTransform;
    [SerializeField] private float completeDistance = 1f;

    private float distance;

    public override void Init(QuestManager m, PlayerController p)
    {
        base.Init(m, p);
        playerTransform = p.transform;
    }

    private void Update()
    {
        distance = Vector3.Distance(target.position, playerTransform.position);

        if (distance < completeDistance)
        {
            Complete();
        }
    }

    public override string GetDescription()
    {
        return "Расстояние: " + Mathf.RoundToInt(distance).ToString() + "М";
    }

    public override string GetHeader()
    {
        return "Достигнуть точки";
    }

    public override List<Vector3> GetTargets()
    {
        List<Vector3> res = new List<Vector3>();
        res.Add(target.position);
        return res;
    }
}
