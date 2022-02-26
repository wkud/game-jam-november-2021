using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tooltip : MonoBehaviour
{
    private static Tooltip instance;

    [SerializeField] private Camera uiCamera;

    [SerializeField] private TextMeshProUGUI _descriptionTextArea;
    [SerializeField] private TextMeshProUGUI _titleTextArea; 
    private RectTransform backgroundRectTransform;

    private void Awake()
    {
        instance = this;
        backgroundRectTransform = transform.Find("background").GetComponent<RectTransform>();
        //_descriptionTextArea = transform.Find("text").GetComponent<Text>();

        gameObject.SetActive(false);
    }

    private void Update()
    {
        //Vector2 localPoint;
        //RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, uiCamera, out localPoint);
        //transform.localPosition = localPoint;
    }

    private void ShowTooltip(string title, string description)
    {
        _descriptionTextArea.text = description;
        _titleTextArea.text = title;
        //float textPaddingSize = 4f;
        //Vector2 backgroundSize = new Vector2(tooltipText.preferredWidth + textPaddingSize * 2f, tooltipText.preferredHeight + textPaddingSize * 2f);

        //var rectTransform = tooltipText.GetComponent<RectTransform>().rect;
        //var backgroundSize = new Vector2(rectTransform.width, rectTransform.height);
        //backgroundRectTransform.sizeDelta = backgroundSize;

        gameObject.SetActive(true);
    }

    private void HideTooltip()
    {
        gameObject.SetActive(false);
    }

    public static void ShowTooltip_Static(string title, string description)
    {
        instance.ShowTooltip(title, description);
    }

    public static void HideTooltip_Static()
    {
        instance.HideTooltip();
    }
}
