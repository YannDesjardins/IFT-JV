using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemPool : MonoBehaviour
{
    public GameObject particleSystemPrefab;

    private List<GameObject> freeParticle = new List<GameObject>();
    private IEnumerator routine;

    public void PlayParticleSystem(Transform targetTransform)
    {
        GameObject particleSystemObject;
        if(freeParticle.Count == 0)
        {
            particleSystemObject = Instantiate(particleSystemPrefab, targetTransform.position, Quaternion.identity);
        }
        else
        {
            particleSystemObject = freeParticle[freeParticle.Count - 1];
            freeParticle.RemoveAt(freeParticle.Count - 1);
            particleSystemObject.transform.position = targetTransform.position;
        }
        routine = WaitForEndAndReturnFree(particleSystemObject);
        StartCoroutine(routine);
    }

    IEnumerator WaitForEndAndReturnFree(GameObject particleSystemObject)
    {
        ParticleSystem particleSystem = particleSystemObject.GetComponent<ParticleSystem>();
        particleSystem.Play();
        yield return new WaitForSeconds(particleSystem.duration);
        freeParticle.Add(particleSystemObject);
    }
}
