using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioClip menuMusic;
    [SerializeField] AudioClip gameMusic;
    [SerializeField] List<AudioClip> gameMusicList;
    [SerializeField] List<AudioClip> menuMusicList;

    [SerializeField] AudioClip sceneSwitchSwoosh;
    [SerializeField] AudioClip pilotLaser;

    [Space(10)]
    [SerializeField] AudioSource musicChannel;
    [SerializeField] List<AudioSource> sfxChannels;
    [SerializeField] AudioSource voiceAudioChannel;

    int currentSFXChannel = 0;
    int highestSFXChannel = 0;

    private List<AudioClip> currentPlaylist;
    private AudioClip lastClip;

    public static AudioManager instance;
    void Awake()
    {
        if (instance == null)
            instance = this;

        if (musicChannel == null) Debug.LogError("AudioManager: Music Channel is null");
        foreach (AudioSource sfxChannel in sfxChannels)
        {
            if (sfxChannel == null) Debug.LogError("Audio Manager: One of the SFX Channels is null");
        }

        highestSFXChannel = sfxChannels.Count - 1;
        //highestSFXChannel = Mathf.Max(sfxChannels.Count - 1, 0); // debugging earlier
    }

    public void PlayPlaylist(List<AudioClip> playlist)
    {
        if (playlist ==  null || playlist.Count == 0)
        {
            Debug.LogWarning("no songs in playlist to play");
            return;
        }

        currentPlaylist = playlist;
        PlayNextTrack();
    }

    private void PlayNextTrack()
    {
        if (currentPlaylist == null || currentPlaylist.Count == 0) { return; }
        AudioClip nextClip = GetRandomClip();
        if (nextClip == null) { return; }
        PlayMusic(nextClip);
        StartCoroutine(WaitForTrackToEnd(nextClip.length));
    }

    private AudioClip GetRandomClip()
    {
        if (currentPlaylist.Count ==1)
            return currentPlaylist[0];

        AudioClip nextClip;
        do
        {
            nextClip = currentPlaylist[Random.Range(0, currentPlaylist.Count)];
        } while (nextClip == lastClip);
        lastClip = nextClip;

        return nextClip;
    }

    private IEnumerator WaitForTrackToEnd(float trackLength)
    {
        yield return new WaitForSeconds(trackLength);
        PlayNextTrack();
    }

    #region Music
    void PlayMusic(AudioClip music)
    {
        if (music == null)
        {
            Debug.LogWarning("Attempted to play a null music clip.  Not playing music");
            return;
        }
        // This conditional will only allow swapping of the music if the music is changing
        //  - This lets multiple areas with the same music feel contiguous
        if (musicChannel.clip != music)
        {
            musicChannel.Stop();
            musicChannel.clip = music;
            musicChannel.Play();
        }
    }
    public void PlayMenuMusic()
    {
        PlayMusic(menuMusic);
        //PlayPlaylist(menuMusicList);
    }
    public void PlayGameMusic()
    {
        //PlayMusic(gameMusic);
        //Debug.Log($"GameMusic ${gameMusicList} {gameMusic.length}");
            PlayPlaylist(gameMusicList);
    }

    public void StopMusic()
    {
        if(musicChannel.isPlaying) musicChannel.Stop();
    }
    #endregion

    #region Sound Effects
    void PlaySoundEffect(AudioClip soundEffect)
    {
        if (soundEffect == null)
        {
            Debug.LogWarning("Attempted to play a null sfx clip.  Not playing sfx");
            return;
        }
        // for debugging earlier
        //if (currentSFXChannel < 0 || currentSFXChannel >= sfxChannels.Count)
        //{
        //    Debug.LogError("AudioManager: currentSFXChannel is out of range.");
        //    return;
        //}
        NextSFXChannel();
        if (!sfxChannels[currentSFXChannel].isPlaying)
        {
            sfxChannels[currentSFXChannel].Stop();
            sfxChannels[currentSFXChannel].clip = soundEffect;
            sfxChannels[currentSFXChannel].Play();
        }
    }

    public void PlaySceneSwitchSwooshSFX()
    {
        PlaySoundEffect(sceneSwitchSwoosh);
    }
    public void PlayPilotLaserSFX()
    {
        PlaySoundEffect(pilotLaser);
    }

    // This cycles the indices of the sfx channel list and makes "currentSFXChannel" appropriate throughout the class
    // - This is called by PlayMusic() and PlaySoundEffect() before stopping the sound/music, replacing the clip, and playing the new clip
    void NextSFXChannel()
    {
        // getting index out of range here use in future to make more safe but works as is for now
        //if (sfxChannels.Count == 0) return;

        currentSFXChannel++;
        if (currentSFXChannel > highestSFXChannel)
            currentSFXChannel = 0;

    }
    #endregion

    #region Voice Acting
    public void PlayVoiceLine(AudioClip voiceline)
    {
        if (voiceline == null) return;

        if(voiceAudioChannel.clip != voiceline)
        {
            voiceAudioChannel.Stop();
            voiceAudioChannel.clip = voiceline;
            voiceAudioChannel.Play();
        }
    }
    #endregion
}
