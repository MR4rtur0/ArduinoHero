using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class SongControl : MonoBehaviour
{
    
    public AudioSource audiox;
    public AudioSource audi_rep;
    public  static SongControl scontrol;
    public float delay = 2f;
    private bool coroutina = false; 
    public bool crear = false;
    public AudioClip[] musics;
    private int numusic = 1;
    public SongLoader songLoader;
    public void Awake()
    {
        if(crear)
        {
                if (songLoader)
                {
                    songLoader.OnSongLoaded += HandleSongLoaded; // Suscríbete al evento
                    songLoader.StartLoadingSong();

                }
        }
        scontrol = this;


        if(crear)
        {
            StartCoroutine(corotina());
        }else
        {
                    numusic = GameObject.FindGameObjectWithTag("music").GetComponent<ChooseMusic>().music;
                audiox.clip = musics[numusic-1];
                    audiox.Play();
        }

    }
        private void HandleSongLoaded(AudioClip loadedClip)
        {

                audiox.clip = loadedClip;
                audiox.Play();
            
        }
    public void Update()
    {
        if(!coroutina&&GetAudioSourceTime()>1.96f&& !crear )
        {
            StartSong(musics[numusic-1]);
            coroutina = true;
        }
    }
   IEnumerator corotina()
    {
        yield return new WaitForSeconds(2);
        audiox.Play();
    }
        
    public void StartSong(AudioClip c)
    {
        if(audi_rep == null)
        {
            audiox.clip = c;
            audiox.Play();
        }
        else
        {
            audi_rep.clip = c;
            audi_rep.Play();
        }
        print(GetAudioSourceTime());
        
    }
    public static double GetAudioSourceTime()//Devuelve el tiempo actual de reproduccion
    {
        return (double)scontrol.audiox.timeSamples / scontrol.audiox.clip.frequency;
    }

    public static double GetAudioRepTime()//Devuelve el tiempo actual de reproduccion
    {
        return (double)scontrol.audi_rep.timeSamples / scontrol.audi_rep.clip.frequency;
    }
}
