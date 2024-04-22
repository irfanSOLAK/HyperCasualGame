using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaintState : MonoBehaviour, IPlayerState
{
    PlayerController _playerController;

    Camera _mainCamera;

    Texture2D _objectToPaintMaterialTexture;
    Texture2D _brushPaintedCursor;
    Texture2D _brushUnpaintedCursor;

    float _brushCursorSize;
    float _paintedPercent;

    Vector2Int _paintingSize;

    Color _paintingColor;
    Color[] _objectToPaintTextureColors;

    bool _isPercentIncreased;

    public void Handle(PlayerController playerController)
    {
        if (!_playerController)
            _playerController = playerController;

        SetPaintParameters();
        SetCursorAs(_brushUnpaintedCursor);
        SetObjectToPaintMaterialTexture();
    }

    void SetPaintParameters()
    {
        _brushPaintedCursor = GameBehaviour.Instance.GameParameters.brushPainted;
        _brushUnpaintedCursor = GameBehaviour.Instance.GameParameters.brushUnpainted;
        _brushCursorSize = GameBehaviour.Instance.GameParameters.brushCursorSize;
        _paintingSize = GameBehaviour.Instance.GameParameters.paintingSize;
        _paintingColor = GameBehaviour.Instance.GameParameters.paintingColor;
        _mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    private void SetCursorAs(Texture2D brush)
    {
        Cursor.SetCursor(brush, new Vector2(_brushCursorSize * brush.width, 0), CursorMode.Auto);
    }

    private void SetObjectToPaintMaterialTexture()
    {
        _objectToPaintMaterialTexture = new Texture2D(_paintingSize.x, _paintingSize.y, TextureFormat.ARGB32, false);
        GameObject.FindGameObjectWithTag("ObjectToPaint").GetComponent<Renderer>().material.mainTexture = _objectToPaintMaterialTexture;
    }

    public void DeactivateState()
    {
        _playerController = null;
        SetCursorAsDefault();
    }

    private static void SetCursorAsDefault()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerController)
        {
            if (Input.GetMouseButton(0) && (Physics.Raycast(_mainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit)))
            {
                _playerController.IsPainting = true;
                PaintMaterial(hit.textureCoord);
                SetCursorAs(_brushPaintedCursor);
                ConvertPaintedMaterialToTexture2D();
                CalculatePaintedPercent();
                StartCoroutine(WritePercentWithAnimation(_isPercentIncreased));
            }
            else
            {
                _playerController.IsPainting = false;
                SetCursorAs(_brushUnpaintedCursor);
            }
        }
    }

    private void CalculatePaintedPercent()
    {
        float _paintedPercentTemp = 0;

        for (int i = 0; i < _objectToPaintTextureColors.Length; i++)
        {
            if (_objectToPaintTextureColors[i] == _paintingColor)
            {
                _paintedPercentTemp++;
            }
        }

        _paintedPercentTemp = (_paintedPercentTemp / _objectToPaintTextureColors.Length) * 100;

        if (_paintedPercent < _paintedPercentTemp)
        {
            _paintedPercent = _paintedPercentTemp;
            _isPercentIncreased = true;
        }
    }

    private void PaintMaterial(Vector2 coordinate)
    {
        coordinate.x *= _objectToPaintMaterialTexture.width;
        coordinate.y *= _objectToPaintMaterialTexture.height;

        Color32[] _texturePixels = _objectToPaintMaterialTexture.GetPixels32();
        int _texturePoint = (int)coordinate.x + ((int)coordinate.y * _objectToPaintMaterialTexture.width);

        _texturePixels[_texturePoint] = _paintingColor;
        _objectToPaintMaterialTexture.SetPixels32(_texturePixels);
        _objectToPaintMaterialTexture.Apply();
    }

    void ConvertPaintedMaterialToTexture2D()
    {
        Texture mainTexture = GameObject.FindGameObjectWithTag("ObjectToPaint").GetComponent<Renderer>().material.mainTexture;
        Texture2D texture2D = new Texture2D(mainTexture.width, mainTexture.height, TextureFormat.RGBA32, false);
        RenderTexture currentRT = RenderTexture.active;
        RenderTexture renderTexture = new RenderTexture(mainTexture.width, mainTexture.height, 32);
        Graphics.Blit(mainTexture, renderTexture);
        RenderTexture.active = renderTexture;
        texture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture2D.Apply();

        _objectToPaintTextureColors = texture2D.GetPixels();
        RenderTexture.active = currentRT;
    }

    private IEnumerator WritePercentWithAnimation(bool isPlayingAnimation)
    {
        float startScale = 1f;
        float endScale = 1.2f;
        float scaleIncrement = 0.05f;
        float scaleSpeed = 0.01f;

        if (isPlayingAnimation)
        {
            Text percentText = GameObject.FindGameObjectWithTag("PercentText").GetComponent<Text>();

            for (float i = startScale; i < endScale; i += scaleIncrement)
            {
                percentText.rectTransform.localScale = new Vector3(i, i, i);
                yield return new WaitForSeconds(scaleSpeed);
            }

            percentText.rectTransform.localScale = new Vector3(1f, 1f, 1f);
            percentText.text = _paintedPercent.ToString("0.00") + "%";

            _isPercentIncreased = false;
        }
    }
}