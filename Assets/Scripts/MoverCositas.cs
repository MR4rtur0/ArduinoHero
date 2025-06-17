using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverCositas : MonoBehaviour
{
    private Transform trans;
    public float velocidad = 23;
    public Rigidbody rb;

    public ReadSong valordeMusic;
    // Start is called before the first frame update
    void Start()
    {
        trans = GetComponent<Transform>();
        StartCoroutine(destroy_());
        valordeMusic = GetComponentInParent<ReadSong>();
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.forward * -velocidad;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        //trans.Translate(Vector3.up * -velocidad* Time.deltaTime);
    }

   public  IEnumerator destroy_()
    {
        yield return new WaitForSeconds(2.1f);
         valordeMusic.atinar++;
         valordeMusic.audioerror();
        
        yield return new WaitForSeconds(0.2f);
         Destroy(this.gameObject);
    }

    public void accion()
    {
                valordeMusic.atinar++;
              Destroy(this.gameObject);
    }
}
