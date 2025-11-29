using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public abstract class Quest : MonoBehaviour
{
    [SerializeField] private UnityEvent onComplete;
    private bool active => this.enabled;
    private QuestManager manager;
    protected PlayerController controller;

    public virtual void Init(QuestManager m, PlayerController p)
    {
        manager = m;
        controller = p;
    }

    public abstract string GetHeader();
    public abstract string GetDescription();

    public virtual void Activate()
    {
        this.enabled = true;
    }

    protected void Complete()
    {
        if (!active)
        {
            return;
        }

        this.enabled = false;
        onComplete.Invoke();
        manager.NextQuest();
    }

    public abstract List<Vector3> GetTargets();
}
