using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSettings : MonoBehaviour
{
    [SerializeField] private AudioSource keySound;
    [SerializeField] private AudioSource bubbleSound;
    [SerializeField] private AudioSource heartBeatSound;
    

    public void PlayKeySound() {
        keySound.Play();
    }

    public void StartBubbleSound() {
        bubbleSound.Play();
    }


    public void StopBubbleSound() {
        bubbleSound.Stop();
    }

    public void StartHeartBeat() {
        heartBeatSound.Play();
    }

    public void StopHeartBeat() {
        heartBeatSound.Stop();
    }
}
