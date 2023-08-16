using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour
{
    public RectTransform background;
    public RectTransform foreground;

    [SerializeField]
    private float _maxWidth;

    void OnEnable()
    {
        _maxWidth = background.rect.width;
    }

    public void SetBar(float currentValue, float maxValue)
    {
        float percentage = currentValue / maxValue;

        foreground.sizeDelta = new Vector2(percentage * _maxWidth, foreground.sizeDelta.y);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
