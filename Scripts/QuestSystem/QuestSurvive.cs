using System.Collections.Generic;
using UnityEngine;

public class QuestSurvive : Quest
{
    [SerializeField] private int surviveTime;
    private float currentTime;

    private void Update()
    {
        if (!controller.IsAlive)
        {
            return;
        }

        currentTime += Time.deltaTime;

        if (currentTime >= surviveTime)
        {
            Complete();
        }
    }

    public override string GetDescription()
    {
        return Mathf.RoundToInt(surviveTime - currentTime).ToString();
    }

    public override string GetHeader()
    {
        return "Проживите " + surviveTime.ToString() + " секунд";
    }

    public override List<Vector3> GetTargets()
    {
        return new List<Vector3>();
    }
}
