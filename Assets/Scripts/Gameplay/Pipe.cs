using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    [SerializeField] ParticleSystem _ParticleSystem;
    public bool IsSmoking;

    public void TriggerPipe()
    {
        if(_ParticleSystem != null)
        {
            if (IsSmoking)
                _ParticleSystem.Stop();
            else
                _ParticleSystem.Play();

            IsSmoking = !IsSmoking;
        }
    }
}
