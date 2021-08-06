using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorPuzzle : MonoBehaviour
{
    [SerializeField] private DataSprites _dataSprites;
    [SerializeField] private SpriteRenderer _puzzlePlace;
    private SpriteRenderer _sprite;
    
    void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        gameObject.AddComponent<SpriteRenderer>();
        _sprite = GetComponent<SpriteRenderer>();
        _sprite.sprite = _dataSprites.Image[0];
        _puzzlePlace.sprite = _dataSprites.ImagePlace[0];
        StartCoroutine(AutomaticCutting());
    }
    
    private IEnumerator AutomaticCutting()
    {
        gameObject.AddComponent<PolygonCollider2D>();
        gameObject.AddComponent<SpliceSpriteController>().orderInLayer = 2;
        gameObject.GetComponent<SpliceSpriteController>().fragmentInEditor();
        yield return new WaitForSeconds(1.5f);
        gameObject.AddComponent<Picture>();
    }

   
}
