using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class AnimatedButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private static readonly string BUTTON_TEXT_OBJ_NAME = "Text";
    private static readonly float TIME_TO_CHANGE_SIZE = 1f;
    private static readonly float SIZE_COEFFICIENT = 1.5f;

    private Text text;
    private Vector3 _defaultTextScale;
    private Coroutine _coroutine = null;

    private void Start()
    {
        Transform textObj = transform.Find(BUTTON_TEXT_OBJ_NAME);

        text = textObj.GetComponent<Text>();
        _defaultTextScale = text.rectTransform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(IncreaseRectSize());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(DecreaseRectSize());
    }

    public void ResetScale()
    {
        text.rectTransform.localScale = _defaultTextScale;
    }

    private IEnumerator IncreaseRectSize()
    {
        while (text.rectTransform.localScale.x < _defaultTextScale.x * SIZE_COEFFICIENT)
        {
            Vector3 newScale = GetNewIncreasedSize();

            if (newScale.x >= _defaultTextScale.x * SIZE_COEFFICIENT)
                newScale = _defaultTextScale * SIZE_COEFFICIENT;

            SetNewScale(newScale);
            yield return null;
        }

        yield break;
    }

    private IEnumerator DecreaseRectSize()
    {
        while (text.rectTransform.localScale.x > _defaultTextScale.x)
        {
            Vector3 newScale = GetNewDecreasedSize();

            if (newScale.x < _defaultTextScale.x)
                newScale = _defaultTextScale;

            SetNewScale(newScale);
            yield return null;
        }

        yield break;
    }

    private void SetNewScale(Vector3 newScale)
    {
        text.rectTransform.localScale = newScale;
    }

    private Vector3 GetNewIncreasedSize()
    {
        return text.rectTransform.localScale + _defaultTextScale * (SIZE_COEFFICIENT - 1) * Time.deltaTime / TIME_TO_CHANGE_SIZE;
    }

    private Vector3 GetNewDecreasedSize()
    {
        return text.rectTransform.localScale - _defaultTextScale * (SIZE_COEFFICIENT - 1) * Time.deltaTime / TIME_TO_CHANGE_SIZE;
    }

    public abstract void OnPointerClick(PointerEventData eventData);
}
