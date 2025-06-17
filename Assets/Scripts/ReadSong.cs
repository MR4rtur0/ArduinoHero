using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine.SceneManagement;


public class ReadSong : MonoBehaviour
{
    public string[] lines;
    public double[] times;
    public GameObject cosita;
    public botonsave botones;
    public Transform[] lugar;
    public AudioSource audio_s;
    public AudioClip fallo;
    public int[] id;
    private int act = 0;
    public int atinar = 0;
    public float tiempodecositas;
    public GameObject[] cositas;
    public int score = 0;
    private Color[] colors;
    public TMP_Text tex;
    public Animator anim;
    public int song;
    public bool babymode = false;
    private bool final = false;

    public TextAsset[] canciones;

    // Start is called before the first frame update
    void Start()
    {
        song = GameObject.FindGameObjectWithTag("music").GetComponent<ChooseMusic>().music;
        LeerDesdeAsset(song - 1);


        times = new double[lines.Count()/2];
        id = new int[lines.Count()/2];
        cositas = new GameObject[lines.Count()/2];
        int y = 0;
        for(int x = 0;x<lines.Count();x+=2)
        {
            id[y] = int.Parse(lines[x],System.Globalization.CultureInfo.InvariantCulture);
            while(babymode&&( id[y]<0 || id[y]>3))
            {
                if(id[y]<0)
                {
                    id[y] += 4;
                }
                else
                {
                    id[y] -=6;
                }
            }
            times[y] = double.Parse(lines[x+1],System.Globalization.CultureInfo.InvariantCulture);
            y++;
        }
        lugar = new Transform[botones.Mcolor.Count()];
        for(int x = 0;x<botones.Mcolor.Count();x++)
        {
            lugar[x] = botones.Mcolor[x].gameObject.transform;
        }

        colors = new Color[lugar.Count()];
        for(int x = 0; x<lugar.Count();x++)
        {
            colors[x] =  lugar[x].gameObject.GetComponent<MeshRenderer>().material.color;
        }

    }

    // Update is called once per frame
    void Update()
    {
        tex.text = "Score: " + score;
        if(act<times.Count())
        {
         if(times[act] <= SongControl.GetAudioSourceTime())
        {
            Vector3 aparecer = new Vector3(lugar[id[act]].position.x,lugar[id[act]].position.y,lugar[id[act]].position.z + tiempodecositas);
            cositas[act] = Instantiate(cosita, aparecer, lugar[id[act]].rotation,this.transform);
            cositas[act].GetComponent<MeshRenderer>().material.color = colors[id[act]];
             cositas[act].GetComponent<Light>().color = colors[id[act]];
            act++;
        }
        }
        if(act>=times.Count() && !final)
        {
            anim.SetTrigger("final");
            final = true;
            StartCoroutine(fin());
        }
    }
    IEnumerator fin()
    {
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene(0);
    }
   string[] LeerTxt(string s)
    {
        string path = Application.streamingAssetsPath + "/song" + s + ".txt";

        if(!File.Exists(path))
        {
            Application.Quit();
            Debug.Log("No encontrado");
        } 
        //lines = new string[File.ReadLines(path).Count()];
        string[] _lines = File.ReadAllLines(path);
        
        return _lines;
    }

    void LeerDesdeAsset(int index)
    {
        if (index < 0 || index >= canciones.Length)
        {
            Debug.LogError("Índice fuera de rango.");
            return;
        }

        lines = canciones[index].text.Split('\n');
    }

    public void comparar(int x)
    {
        while (atinar - 1 >= 0 && SongControl.GetAudioRepTime() < times[atinar - 1])
        {
            atinar -= 1;
        }


        while (atinar + 1 < times.Length && SongControl.GetAudioRepTime() > times[atinar + 1])
        {
            atinar += 1;
        }

        if (id[atinar] == x)
        {
            if (Mathf.Abs((float)(SongControl.GetAudioRepTime() - times[atinar])) <= 0.5f)
            {
                if (Mathf.Abs((float)(SongControl.GetAudioRepTime() - times[atinar])) <= 0.14f)
                {
                    cositas[atinar].GetComponent<MoverCositas>().accion();
                    score += 5;
                }
                else
                {
                    Debug.Log("Fallaste");
                    audioerror();
                    //cositas[atinar].GetComponent<MoverCositas>().accion();
                }
            }
            else
            {
                Debug.Log("Fallaste");
                audioerror();
            }

        }
        else
        {
            Debug.Log("Fallaste");
            audioerror();
        }



    }

    public void audioerror()
    {
                audio_s.clip = fallo;
                  audio_s.Play();
                  if(score - 3 < 0)
                  {
                    score = 0;
                  }
                  else
                  {
                    score -= 3;
                  }
    }
}
