using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class SequenceController : MonoBehaviour
{
    public PlayableDirector[] cutscenes;

    private int _index;
    private bool _waitingGameplay;

    private void Awake()
    {
        G.Register(this);
    }

    private void Start()
    {
        StartCoroutine(Run());
    }

    IEnumerator Run()
    {
        while (_index < cutscenes.Length)
        {
            yield return PlayCutscene(cutscenes[_index]);
            yield return Gameplay();
            _index++;
        }
    }

    IEnumerator PlayCutscene(PlayableDirector d)
    {
        PlayerAction.Instance.Lock();
        PlayerAction.Instance.Clear();

        var player = G.Get<PlayerController>();
        if (player) player.enabled = false;

        bool done = false;

        d.stopped += OnStop;

        d.Stop();
        d.time = 0;
        d.Play();

        void OnStop(PlayableDirector _) => done = true;

        yield return new WaitUntil(() => done);

        d.stopped -= OnStop;
    }

    IEnumerator Gameplay()
    {
        PlayerAction.Instance.Unlock();

        var player = G.Get<PlayerController>();
        if (player) player.enabled = true;

        _waitingGameplay = true;

        yield return new WaitUntil(() => !_waitingGameplay);
    }

    public void Continue()
    {
        _waitingGameplay = false;
    }
}