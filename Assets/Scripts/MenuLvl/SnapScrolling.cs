using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnapScrolling : MonoBehaviour
{
    [SerializeField] private RectTransform _panelScroll;
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private int _count;
    [SerializeField] private int _panelOffset;
    [SerializeField] private GameObject _panel;
    [SerializeField] private float _snapSpeed;
    [SerializeField] private float _scaleSpeed;
    [SerializeField] private float _scaleOffset;
    private GameObject[] _instPans;
    private Vector2[] _panPos;
    private Vector2[] _panScale;
    private Vector2 _contentVector;
    private bool _isScrolling;
    private int selectId;

    private void Start()
    {
        _instPans = new GameObject[_count];
        _panPos = new Vector2[_count];
        _panScale = new Vector2[_count];
        for(int i = 0; i < _count; i++)
        {
            _instPans[i] = Instantiate(_panel, _panelScroll, false);
            if (i == 0) continue;
            _instPans[i].transform.localPosition = new Vector2(_instPans[i - 1].transform.localPosition.x +
                _panel.GetComponent<RectTransform>().sizeDelta.x + _panelOffset, 0f);
            _panPos[i] = -_instPans[i].transform.localPosition;
        }
    }

    private void FixedUpdate()
    {
        if(_panelScroll.anchoredPosition.x >= _panPos[0].x && !_isScrolling || _panelScroll.anchoredPosition.x <= _panPos[_panPos.Length - 1].x && !_isScrolling)
        {
            _scrollRect.inertia = false;
        }
            float nearestPos = float.MaxValue;
        for(int i = 0; i < _count; i++) 
        {
            float dist = Mathf.Abs(_panelScroll.anchoredPosition.x - _panPos[i].x);
            if(dist < nearestPos)
            {
                nearestPos = dist;
                selectId = i;
            }
            float scale = Mathf.Clamp(1 / (dist / _panelOffset) * _scaleOffset, 0.5f, 1f);
            _panScale[i].x = Mathf.SmoothStep(_instPans[i].transform.localScale.x, scale, _scaleSpeed * Time.fixedDeltaTime);
            _panScale[i].y = Mathf.SmoothStep(_instPans[i].transform.localScale.y, scale, _scaleSpeed * Time.fixedDeltaTime);
            _instPans[i].transform.localScale = _panScale[i];
        }
        float scrollVelocity = Mathf.Abs(_scrollRect.velocity.x);
        if (scrollVelocity < 400 || !_isScrolling) _scrollRect.inertia = false;
        if (_isScrolling || scrollVelocity > 400) return;
        _contentVector.x = Mathf.SmoothStep(_panelScroll.anchoredPosition.x, _panPos[selectId].x, _snapSpeed
            * Time.fixedDeltaTime);
        _panelScroll.anchoredPosition = _contentVector;
    }

    public void Scrolling(bool isScroll)
    {
        _isScrolling = isScroll;
        if (isScroll) _scrollRect.inertia = true;
    }
}
