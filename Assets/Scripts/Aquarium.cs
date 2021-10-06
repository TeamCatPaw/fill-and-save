using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aquarium : MonoBehaviour
{
    private ParticleSystem _confetti;

    void Start() {
        _confetti = transform.GetChild(0).GetComponent<ParticleSystem>();
        EventManager.GetInstance().OnWin += PlayConfetti;
    }
    private void PlayConfetti() {
        _confetti.Play();
    }
    private void Update() {
        if (Input.GetKeyDown("p")) {
            PlayConfetti();
        }
    }
}
