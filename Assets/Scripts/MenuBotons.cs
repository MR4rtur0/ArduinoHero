using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuBotons : MonoBehaviour
{
    public Image[] botones;
    public GameObject choose;
    public AudioSource audios;
    public AudioClip[] clip;
    public Animator anim;
    public Animator canv;
    public bool easy = true;
    void Start()
    {
        choose = GameObject.FindGameObjectWithTag("music");
    }

    
    public void crear()
    {
        SceneManager.LoadScene(1);
    }

    public void leer()
    {
        StartCoroutine(esperargame());
        canv.SetTrigger("trans");
        audios.pitch = 1;
        audios.clip = clip[1];
        audios.Play();
    }
    IEnumerator esperargame()
    {
        yield return new WaitForSeconds(4);
        if(easy)
        {
            SceneManager.LoadScene(3);
        }else
        {
            SceneManager.LoadScene(2);
        }
        
        
    }

     public void boton1()
    {
        botones[0].color = Color.yellow;
        botones[1].color = Color.white;
        botones[2].color = Color.white;
        botones[3].color = Color.white;
       choose.GetComponent<ChooseMusic>().music = 1;
       musicrandom();
    }

    public void boton2()
    {
        botones[1].color = Color.yellow;
        botones[0].color = Color.white;
        botones[2].color = Color.white;
        botones[3].color = Color.white;
        choose.GetComponent<ChooseMusic>().music = 2;
        musicrandom();
    }

    public void boton3()
    {
        botones[1].color = Color.white;
        botones[0].color = Color.white;
         botones[3].color = Color.white;
        botones[2].color = Color.yellow;
       choose.GetComponent<ChooseMusic>().music = 3;
       musicrandom();
    }

    public void boton4()
    {
        botones[1].color = Color.white;
        botones[0].color = Color.white;
         botones[2].color = Color.white;
        botones[3].color = Color.yellow;
        choose.GetComponent<ChooseMusic>().music = 4;
        musicrandom();
    }

    void musicrandom()
    {
        //audios.Stop();
        float r = Random.Range(1,4);
        audios.pitch = r;
        anim.speed = r;
        //audios.clip = clip[0];
        audios.PlayOneShot(clip[0]);
    }

    public void dificultad()
    {
        easy = !easy;
        musicrandom();
        if(!easy)
        {
             botones[4].color = Color.red;
             botones[4].GetComponentInChildren<TMP_Text>().text = "Dificil";
        }
        else
        {
            botones[4].color = Color.white;
            botones[4].GetComponentInChildren<TMP_Text>().text = "Facil";
        }
       

    }
}
