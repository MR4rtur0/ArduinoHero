using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class CrearTXT : MonoBehaviour
{
   public string CrearTxt()
    {
        string path = Application.dataPath + "/MusicData.txt";

        if(!File.Exists(path))
        {
            File.WriteAllText(path,"");
        } 
        else
        {
            path = Application.dataPath + "/MusicData";
            int x = 1;
            while(File.Exists(path + "("+ x+").txt"))
            {
                x++;
            }
            path = path + "("+ x+").txt";
            File.WriteAllText(path,"");
        }

        return path;
    }
}
