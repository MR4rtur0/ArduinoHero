using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class botonsave : MonoBehaviour
{
    public KeyCode[] teclas;
    public MeshRenderer[] Mcolor;
    private Color[] OriColors;
    private string dir;
    public CrearTXT scrtxt;
    public ReadSong compare;
    void Start()
    {
        OriColors = new Color[Mcolor.Length];
        for(int x=0;x<Mcolor.Length;x++)
        {
            OriColors[x] = Mcolor[x].material.color; 
        }
        if(scrtxt != null)
        {
            dir = scrtxt.CrearTxt();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        botones();
    }

    void botones()
    {
            for(int x=0;x<teclas.Length;x++)
                    {
                           if(Input.GetKeyDown(teclas[x]))
                                {
                                        Color ColorChange = Mcolor[x].material.color;
                                        Mcolor[x].material.color = new Color32(((byte)(ColorChange.r)) ,(byte)(ColorChange.g),(byte)(ColorChange.b),(byte)(ColorChange.a));
                                        if(scrtxt != null)
                                        {
                                               string contenido =  x + "\n"+ SongControl.GetAudioSourceTime()+"\n";
                                               escribir(contenido);
                                                
                                        }
                                        if(scrtxt == null)
                                        {
                                            compare.comparar(x);
                                        }
                                
                                 }
                            if(Input.GetKeyUp(teclas[x]))
                                 {
                                        Mcolor[x].material.color = OriColors[x];
                                
                                  }
                    }
    }

    public void escribir(string contenido)
    {
        File.AppendAllText(dir,contenido);
    }
    
    
}
