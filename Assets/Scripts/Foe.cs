using System;
using System.Collections;
using UnityEngine;

public class Foe : MonoBehaviour
{
    public Action<Foe> Died;

    [SerializeField] private float _lifeTime = 10f;
    [SerializeField] private float _speed = 5f;
    private void OnEnable()
    {
        StartCoroutine(LifeTimer());
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }

    private IEnumerator LifeTimer()
    {
        yield return new WaitForSeconds(_lifeTime);

        Died?.Invoke(this);
    }
}
