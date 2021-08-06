using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Picture : MonoBehaviour
{
    private Vector2[] _pointPos;
    public int _childCount;
    private int _delaySlice = 1;

    private void Start()
    {
        _childCount = transform.childCount;
        StartCoroutine(Slice());
    }

   

    IEnumerator Slice()
    {
        yield return new WaitForSeconds(_delaySlice);
        _pointPos = GameControl.Instance.GetPoints();
        for (int i = 0; i < _childCount; i++)
        {
            transform.GetChild(i).GetComponent<Piece>().MoveToTarget(_pointPos[i]);
        }
    }

    public void Finish()
    {
        _childCount--;
        if (_childCount <= 0) GameControl.Instance.Finish();

    }

}
