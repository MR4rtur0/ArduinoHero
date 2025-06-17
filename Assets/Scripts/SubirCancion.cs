using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Collections;

public class SongLoader : MonoBehaviour
{
        public delegate void SongLoadedDelegate(AudioClip loadedClip);
    public event SongLoadedDelegate OnSongLoaded;
    public void StartLoadingSong()
    {
        string path = OpenFilePanel();
        if (!string.IsNullOrEmpty(path))
        {
            StartCoroutine(LoadSong(path));
        }
    }

    private string OpenFilePanel()
    {
        string gameDirectory = Path.GetDirectoryName(Application.dataPath);
        string audioDirectory = Path.Combine(gameDirectory, "Audios");
        string[] paths = Directory.GetFiles(audioDirectory, "*.mp3;*.wav", SearchOption.TopDirectoryOnly);
        Debug.Log(paths[0]);

        if (paths.Length > 0)
        {
            return paths[0]; // Para este ejemplo, simplemente tomamos el primer archivo encontrado. 
                             // En una aplicación real, querrás proporcionar al usuario una manera de seleccionar el archivo deseado.
        }
        return string.Empty;
    }

    IEnumerator LoadSong(string path)
    {
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(path, AudioType.WAV))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(www.error);
            }
            else
            {
                AudioClip clip = DownloadHandlerAudioClip.GetContent(www);
                OnSongLoaded?.Invoke(clip); // Notifica a los suscriptores
            }
        }
    }
}
