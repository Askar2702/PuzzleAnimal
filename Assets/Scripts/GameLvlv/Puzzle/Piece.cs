using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
public class Piece : MonoBehaviour
{
    [SerializeField] private Ease _ease;
    private Vector2 _startPos;
    private Vector2 _targetPos;
    public bool IsLocked;// { get; private set; }
    private float _deltaX;
    private float _deltaY;
    private Camera _camera;
    private Vector2 _touchPos;

    private EffectShow _effectShow;
    void Start()
    {
        _camera = Camera.main;
        IsLocked = false;
        _ease = Ease.OutElastic;
        _effectShow = FindObjectOfType<EffectShow>();
    }

   
    public void MoveToTarget(Vector3 targetPos)
    {
        _startPos = transform.position;
        transform.DOMove(targetPos, 1f).SetEase(_ease, 0.005f);
        _targetPos = targetPos;
    }

   
    

    private void OnMouseDown()
    {
        if (IsLocked) return;
        _touchPos = _camera.ScreenToWorldPoint(Input.mousePosition);
        _deltaX = _touchPos.x - transform.position.x;
        _deltaY = _touchPos.y - transform.position.y;
    }

    private void OnMouseDrag()
    {
        if (IsLocked) return;
        _touchPos = _camera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(_touchPos.x - _deltaX, _touchPos.y - _deltaY);
    }
    private void OnMouseUp()
    {
        if (Vector2.Distance(transform.position, _startPos) <= 0.5f)
        {
            transform.DOMove(_startPos, 1f);
            GetComponent<Collider2D>().enabled = false;
            IsLocked = true;
            transform.parent.GetComponent<Picture>().Finish();
            _effectShow.ShowExplode(transform.position);
        }
        else
        {
            transform.DOMove(_targetPos, 1f).SetEase(_ease, 0.005f);
            _deltaX = 0;
            _deltaY = 0;
        }
    }
}
