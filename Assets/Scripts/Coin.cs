
using UnityEngine;

public class Coin : MonoBehaviour
{
    public AudioSource coinAudio;
    void playCoinSound()
    {
        coinAudio.Play();
    }
}
