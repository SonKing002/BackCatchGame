using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MaterialAlphaChanger : MonoBehaviour
{
    public bool alphaChange { get => _alphaChange; set { _alphaChange = value; } }
    Material _material;
    private bool _alphaChange;


    private void Awake()
    {
        _material = GetComponent<Material>();
    }

    private void Update()
    {
        Renderer _obstacleRenderer = transform.GetComponent<Renderer>();
        Debug.Log(_obstacleRenderer == null);

        if (_obstacleRenderer != null)
        {
            Material _material = _obstacleRenderer.material;

            Color _materialColor = _material.color;
            if (!_alphaChange)
            {
                _materialColor.a = 1f;
                _material.color = _materialColor;
                return;
            }
            // 3. Metrial¿« Aplha∏¶ πŸ≤€¥Ÿ.

            _materialColor.a = 0.5f;

            _material.color = _materialColor;
        }
       
    }
    private void FixedUpdate()
    {
        if (_alphaChange)
        {
            _alphaChange = false;
        }
    }
}
