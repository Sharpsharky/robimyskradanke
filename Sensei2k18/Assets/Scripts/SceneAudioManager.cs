using System.Collections;
using UnityEngine;

[RequireComponent( typeof( AudioSource ) )]
public class SceneAudioManager : MonoBehaviour
{

     public static SceneAudioManager instance;

    [SerializeField] AudioClip Intro_good;
    [SerializeField] AudioClip Intro_bad;

    [SerializeField] AudioClip Main_good;
    [SerializeField] AudioClip Main_bad;

    AudioSource _AudioSource;
    private bool isPlayingBad = false;

    public void Awake()
    {
        if (instance != null)
            return;
        instance = this;
        _AudioSource = GetComponent<AudioSource>();
    }

    bool _MainMode = false;

    public bool IsPlayingBad
    {
        get {
            return isPlayingBad;
        }

        set {
            isPlayingBad = value;
        }
    }

    private void OnEnable()
    {
        StartCoroutine( PlayIntroEnumerator() );
    }

    IEnumerator PlayIntroEnumerator()
    {
        _AudioSource.clip = Intro_good;
        _AudioSource.Play();
        yield return new WaitForSeconds( Intro_good.length );
        _AudioSource.clip = Main_good;
        _AudioSource.Play();
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
        _AudioSource.Play();
        isPlayingBad = true;
    }

    public void ChangeOnColissionExit()
    {
        float actTime = _AudioSource.time;
        if (_MainMode)
            _AudioSource.clip = Main_good;
        else
            _AudioSource.clip = Intro_good;
        _AudioSource.time = actTime;
        _AudioSource.Play();
        isPlayingBad = false;
    }
}
