using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class SequenceStep
{
    public StepType type;

    public PlayableDirector director;
    public PlayableAsset timeline;

    public float gameplayDuration; 
}

public enum StepType
{
    Cutscene,
    Gameplay
}