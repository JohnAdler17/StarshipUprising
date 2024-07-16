using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    static AudioPlayer instance; //static variables persist through all instances of a class

    /*
    public AudioPlayer GetInstance() {
      return instance;
    }
    */

    void Awake() {
      ManageSingleton();
    }

    void ManageSingleton() {
      //int instanceCount = FindObjectsOfType(GetType()).Length; //GetType() returns the type of the current class, in this case it is AudioPlayer
      //if (instanceCount > 1)
      if (instance != null)
      {
        gameObject.SetActive(false); //by disabling the audio player before destroying it, you can make sure nothing else in the scene will try to access the instance that will be destroyed
        Destroy(gameObject);
      }
      else {
        instance = this;
        DontDestroyOnLoad(gameObject);
      }
    }

    [Header("Shooting")]
    [SerializeField] AudioSource shootingClipSrc;

    public void PlayShootingClip()
    {
      shootingClipSrc.Play();
    }

    [Header("Take Hit")]
    [SerializeField] AudioSource shipHitClipSrc;

    public void PlayShipHitClip() {
      shipHitClipSrc.Play();
    }

}
