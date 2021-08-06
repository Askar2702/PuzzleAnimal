using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartPuzzle : MonoBehaviour
{
    [SerializeField] private GeneratorPuzzle _puzzle;
    private void Start()
    {
        GameControl.Instance.ChangePuzzle += CleanComponent;
    }
    private void CleanComponent()
    {
        Destroy(_puzzle.GetComponent<SpriteRenderer>());
        Destroy(_puzzle.GetComponent<PolygonCollider2D>());
        Destroy(_puzzle.GetComponent<SpliceSpriteController>());
        Destroy(_puzzle.GetComponent<Picture>());
        StartCoroutine(StartNewPuzzle());
        for(int i = 0;i< _puzzle.transform.childCount; i++)
        {
            Destroy(_puzzle.transform.GetChild(i).gameObject);
        }
    }
    IEnumerator StartNewPuzzle()
    {
        yield return new WaitForSeconds(0.5f);
        _puzzle.StartGame();
    }
   
}
