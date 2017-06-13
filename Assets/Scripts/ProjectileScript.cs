using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour {

    public GameObject projectileTarget;
    public float projectileSpeed;
    Vector3 startPosition;
    ProjectileBezier curve;
    SpriteRenderer sRenderer;
    float position = 0f;
    int effect;

    void Awake()
    {
        curve = GetComponent<ProjectileBezier>();
        sRenderer = GetComponent<SpriteRenderer>();
    }


    public void Shoot(GameObject target, Sprite itemSprite, int damage)
    {
        projectileTarget = target;
        sRenderer.sprite = itemSprite;
        effect = damage;
        gameObject.SetActive(true);
        for (int i = 0; i < curve.points.Length; i++)
        {
            switch (i)
            {
                case 2:
                    curve.points[i] = target.transform.position;
                    break;
                case 1:
                    Vector3 midPoint = Vector3.Lerp(transform.position, target.transform.position, 0.8f);
                    midPoint = new Vector3(midPoint.x, midPoint.y + 10f, target.transform.position.z);
                    curve.points[i] = midPoint;
                    break;
                case 0:
                    curve.points[i] = transform.position;
                    break;
                default:
                    break;
            }
        }

        StartCoroutine(BombsAway());
    }

    IEnumerator BombsAway()
    {
        while (position < 1f)
        {
            position += Time.deltaTime * projectileSpeed;
            transform.position = curve.GetPoint(position);
            yield return null;
        }
        Deactivate();
    }

    void Deactivate()
    {
        gameObject.SetActive(false);
        position = 0f;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.GetInstanceID() == projectileTarget.transform.GetInstanceID())
        {
            StopAllCoroutines();
            collision.GetComponent<Stats>().ItemDamage(effect);
            Deactivate();
        }
    }


}
