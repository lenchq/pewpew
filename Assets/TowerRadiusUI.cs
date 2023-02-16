using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRadiusUI : MonoBehaviour
{
    private SpriteRenderer _sprite;
    public static TowerRadiusUI current;
    
    public Color32 CircleColor = new Color32();
    void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();

        current = this;
    }
    

    public void Move(Vector2 position, float radius)
    {
        var trs = transform;
        Vector3 pos = new Vector3(position.x, position.y, trs.position.z);
        trs.position = pos;
        Vector3 scale = new Vector3(radius * 2f, radius * 2f, 1f);
        trs.localScale = scale;
    }

    public void Hide()
    {
        _sprite.enabled = false;
    }

    public void Show()
    {
        _sprite.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_sprite.color != CircleColor)
        {
            _sprite.color = CircleColor;
        }
    }
}
