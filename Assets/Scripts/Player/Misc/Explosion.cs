using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Explosion : MonoBehaviour
{
    public float explosionRadius = 2.5f;
    public int damage = 25;

    public GameObject explodeVisual;

    public UnityEvent explosionEndEvent;
    public void Explode(Vector3 pos)
    {
        transform.parent = null;
        transform.position = pos;
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider collider in colliders)
        {
            Health health = collider.GetComponent<Health>();
            if (health != null) health.TakeDamage(damage);
            Explodable explodable = collider.GetComponent<Explodable>();
            if (explodable != null) explodable.exploded.Invoke();
        }
        explodeVisual.SetActive(true);
        StartCoroutine(EndExplosion());
    }

    IEnumerator EndExplosion()
    {
        yield return new WaitForSeconds(.5f);
        explosionEndEvent.Invoke();
    }
}
