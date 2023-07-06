using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    [SerializeField] ParticleSystem explosionPrefab = null;

    public void PlayExplosion(Vector3 position)
    {
        if (explosionPrefab != null)
        {
            ParticleSystem explosionInstance = Instantiate(explosionPrefab, position, Quaternion.identity);
            explosionInstance.Play();

        }
    }
}

