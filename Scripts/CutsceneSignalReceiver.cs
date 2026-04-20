using UnityEngine;
using UnityEngine.Playables;

public class CutsceneSignalReceiver : MonoBehaviour
{
    [SerializeField] private PlayableDirector _director;
    [SerializeField] private Animator[] _animatorsToDisable;

    private void Awake()
    {
        G.Register(this);
        _director.stopped += OnTimelineFinished;
    }

    private void OnDestroy()
    {
        _director.stopped -= OnTimelineFinished;
    }

    private void OnTimelineFinished(PlayableDirector director)
    { 
        //foreach (var anim in _animatorsToDisable)
        //{
        //    if (anim != null)
        //        anim.enabled = false;
        //}
    }

    public void PlayNewTimeline(PlayableAsset newTimeline)
    {
        foreach (var anim in _animatorsToDisable)
        {
            if (anim != null)
                anim.enabled = true;
        }

        _director.Stop();            
        _director.playableAsset = null;
        _director.RebuildGraph();
        _director.playableAsset = newTimeline;
        _director.time = 0;
        _director.Evaluate();
        _director.Play();
    }
}