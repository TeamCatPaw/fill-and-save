using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cup : MonoBehaviour
{
    private ParticleSystem _stars;


    void Start()
    {
        _stars = transform.GetChild(3).GetComponent<ParticleSystem>();
    }

    public void PlayStars() {
        _stars.Play();
    }
}
