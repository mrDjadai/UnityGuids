using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestManager : MonoBehaviour
{
    [SerializeField] protected Quest[] quests;
    [SerializeField] private PlayerController player;
    [SerializeField] private TMP_Text header;
    [SerializeField] private TMP_Text description;

    [SerializeField] private Transform arrowPrefab;
    [SerializeField] private float arrowHeight;

    private List<Transform> arrows = new List<Transform>();

    private int current;

    private void Start()
    {
        foreach (var item in quests)
        {
            item.Init(this, player);
            item.enabled = false;
        }

        StartQuest(0);
    }

    public void NextQuest()
    {
        current++;

        if (current < quests.Length)
        {
            StartQuest(current);
        }
        else
        {
            EndGame();
        }
    }

    private void Update()
    {
        if (current < quests.Length)
        {
            description.text = quests[current].GetDescription();
        }
        DrawArrows();
    }

    private void EndGame()
    {
        Debug.Log("Game is ended");
    }

    private void StartQuest(int num)
    {
        quests[num].Activate();
        header.text = quests[num].GetHeader();
    }

    private void DrawArrows()
    {
        List<Vector3> targets = new List<Vector3>();

        if (current < quests.Length)
        {
            targets = quests[current].GetTargets();
        }

        if (targets.Count > arrows.Count)
        {
            int count = targets.Count - arrows.Count;
            for (int i = 0; i < count; i++)
            {
                arrows.Add(Instantiate(arrowPrefab));
            }
        }
        else if(targets.Count < arrows.Count)
        {
            for (int i = targets.Count; i < arrows.Count; i++)
            {
                arrows[i].gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < targets.Count; i++)
        {
            arrows[i].position = targets[i] + Vector3.up * arrowHeight;
            arrows[i].gameObject.SetActive(true);
        }
    }

}
