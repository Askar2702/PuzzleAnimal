using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectShow : MonoBehaviour
{
    [SerializeField] private Vector2[] _points;
    [SerializeField] private GameObject[] _balloon;
    [SerializeField] private ParticleSystem _fireballExplode;



    public void ShowEffect()
    {
        for(int i = 0; i < 20; i++)
        {
            Vector2 pos = new Vector2(Random.Range(_points[0].x, _points[1].x), _points[0].y);
            var index = Random.Range(0, _balloon.Length);
            var balloon = Instantiate(_balloon[index], pos, Quaternion.identity);
        }
    }

    public void ShowExplode(Vector2 vector)
    {
        _fireballExplode.transform.position = vector;
        _fireballExplode.Play();
    }
}
