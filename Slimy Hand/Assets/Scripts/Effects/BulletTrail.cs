using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrail : MonoBehaviour
{
    [SerializeField] private LineRenderer line;
    private float speed;

    public void Init(Vector3 startPos, Vector3 endPos, float width, float damage, bool hit, float speed = 1)
    {
        this.speed = speed;
        line.SetPosition(0, startPos);
        line.SetPosition(1, endPos);
        line.widthMultiplier = width;
        //if (hit)
        //{
        //    GameObject particles = EffectMaterialManager.Instance.GetMatEffect(mat);
        //    ObjectPool.Instance.GetPooledObject(particles, endPos, Quaternion.identity);
        //}
    }
    private void Update()
    {
        if (gameObject.activeSelf && line.widthMultiplier <= 0)
        {
            gameObject.SetActive(false);
        }
        line.widthMultiplier -= Time.deltaTime * speed;
    }
}