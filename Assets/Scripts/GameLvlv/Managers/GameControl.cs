using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;
using System;

public class GameControl : MonoBehaviour
{
    public static GameControl Instance;
    [SerializeField] private EffectShow _effectFinish;
    
    #region Puzzle
    [SerializeField] private float _delayFinish;
    [SerializeField] private Transform[] _points;
    [Space(10)]
    [SerializeField] private ParticleSystem _finishPart;
    [Space(10)]
    [SerializeField] private RectTransform _panelFinish;
    [SerializeField] private Ease _ease;
    [SerializeField] private Transform _puzzle;
    [SerializeField] private GameObject _puzzleShape;
    #endregion

    #region ChangeLvl
    [Space(50)]
    [SerializeField] private Button _nextGame;
    [SerializeField] private Transform _pointEnd;
    [SerializeField] private Transform _pointStart;
    [SerializeField] private Transform _puzzleParent;
    [SerializeField] private Transform _background;
    private int _indexLvl;
    public event Action ChangePuzzle;
    #endregion

    private void Awake()
    {
        if (!Instance) Instance = this;
        _nextGame.onClick.AddListener(() => NextLvl());
    }
    
    public Vector2[] GetPoints()
    {
        var points = new Vector2[_points.Length];
        for(int i = 0; i < points.Length; i++)
        {
            points[i] = _points[i].position;
        }
        return points;
    }

    public void Finish()
    {
        _finishPart.Play();
        _effectFinish.ShowEffect();
        StartCoroutine(ShowEffect());
    }
    IEnumerator ShowEffect()
    {
        yield return new WaitForSeconds(_delayFinish / 2);
        _puzzleShape.SetActive(false);
        _puzzle.DOScale(1.1f, 1f).SetEase(_ease, 0.01f);
        yield return new WaitForSeconds(5f);
        _nextGame.gameObject.SetActive(true);
    }

   
    private void StartLvl()
    {
        _nextGame.gameObject.SetActive(false);
        ChangePuzzle?.Invoke();
        _background.position = _pointStart.position;
        _puzzleParent.position = _pointStart.position;
        _puzzleParent.DOMove(Vector2.zero, 2f).SetEase(Ease.InOutElastic, 0.0001f);
        _background.DOMove(Vector2.zero, 2f).SetEase(Ease.InOutElastic, 0.0001f);
    }
    private void NextLvl()
    {
        var seq = DOTween.Sequence();
        _puzzleParent.DOMove(_pointEnd.position, 1f).SetEase(Ease.InOutElastic, 0.0001f);
        seq.Append(_background.DOMove(_pointEnd.position, 1f).SetEase(Ease.InOutElastic, 0.0001f));
        StartCoroutine(SetSignal());
        seq.OnComplete(StartLvl);
    }

    IEnumerator SetSignal()
    {
        yield return new WaitForSeconds(1f);
        _indexLvl++;
        PlayerPrefs.SetInt("IndexLvl", _indexLvl);
    }

    

}
