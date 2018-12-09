using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SceneAudioManager : MonoBehaviour {

    public static SceneAudioManager instance;

    [SerializeField] AudioClip Intro_good;
    [SerializeField] AudioClip Intro_bad;

    [SerializeField] AudioClip Main_good;
    [SerializeField] AudioClip Main_bad;

    AudioSource _AudioSource;

    public void Awake()
    {
        if (instance != null)
            return;
        instance = this;
    }

    bool _MainMode = false;
    private void Start()
    {
        _AudioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        StartCoroutine(PlayIntroEnumerator());
    }

    IEnumerator PlayIntroEnumerator()
    {
        _AudioSource.clip = Intro_good;
        _AudioSource.Play();
        yield return new WaitForSeconds(Intro_good.length);
        _AudioSource.clip = Main_good;
        _AudioSource.loop = true;
        _MainMode = true;
    }

    public void ChangeOnColissionenter()
    {
        float actTime = _AudioSource.time;
        if (_MainMode)
            _AudioSource.clip = Main_bad;
        else
            _AudioSource.clip = Intro_bad;
        _AudioSource.time = actTime;

    }

    public void ChangeOnColissionExit()
    {
        float actTime = _AudioSource.time;
        if (_MainMode)
            _AudioSource.clip = Main_good;
        else
            _AudioSource.clip = Intro_good;
        _AudioSource.time = actTime;

    }
}
