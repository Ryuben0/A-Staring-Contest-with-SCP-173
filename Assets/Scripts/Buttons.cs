using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour
{

    public int buttonNumber;

    private GameManager _gameManager;

    private AudioSource buttonBeep;

    private SpriteRenderer _sprite;

    private Collider2D _collider;

    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _gameManager = FindObjectOfType<GameManager>();
        buttonBeep = GetComponent<AudioSource>();
        _collider = GetComponent<Collider2D>();
    }

    public void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

            if(touch.phase == TouchPhase.Began)
            {
                Collider2D touchedCollider = Physics2D.OverlapPoint(touchPosition);
                if (_collider == touchedCollider)
                {
                    _sprite.color = new Color(_sprite.color.r, _sprite.color.g, _sprite.color.b, 1f);
                    buttonBeep.Play();
                }
            }

            if (touch.phase == TouchPhase.Ended)
            {
                Collider2D touchedCollider = Physics2D.OverlapPoint(touchPosition);
                if (_collider == touchedCollider)
                {
                    _sprite.color = new Color(_sprite.color.r, _sprite.color.g, _sprite.color.b, .5f);
                    buttonBeep.Stop();
                    _gameManager.colorInput(buttonNumber);
                }
            }
        }
    }

    public void OnMouseDown()
    {
        _sprite.color = new Color(_sprite.color.r, _sprite.color.g, _sprite.color.b, 1f);
        buttonBeep.Play();
    }

    public void OnMouseUp()
    {
        _sprite.color = new Color(_sprite.color.r, _sprite.color.g, _sprite.color.b, .5f);
        buttonBeep.Stop();
        _gameManager.colorInput(buttonNumber);
    }
}
