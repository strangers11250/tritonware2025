using UnityEngine;

public class PopUp : MonoBehaviour
{
    public GameObject popUp;
    public Color highlight = Color.yellow;
    private Color originalColor;
    private SpriteRenderer sr;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
    }

    void OnMouseEnter()
    {
        sr.color = highlight;
    }

    void OnMouseExit()
    {
        sr.color = originalColor;
    }

    void OnMouseDown()
    {
        RectTransform rt = popUp.GetComponent<RectTransform>();
        rt.anchoredPosition = Vector2.zero;
        popUp.SetActive(true);
    }

    public void ClosePopup()
    {
        popUp.SetActive(false);
    }
}
