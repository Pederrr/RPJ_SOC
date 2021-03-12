using UnityEngine;

public class SquareHighlighter : MonoBehaviour
{
    private GameObject selectedObject;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        selectedObject = GameManager.GetSelectedObject();

        if (selectedObject != null)
        {
            if (selectedObject.CompareTag("square"))
            {
                if (transform.position != selectedObject.transform.position)
                {
                    transform.position = selectedObject.transform.position;
                }

                if (!spriteRenderer.enabled)
                {
                    spriteRenderer.enabled = true;
                }
            }
            else if (spriteRenderer.enabled)
            {
                spriteRenderer.enabled = false;
            }
        }
        else if (spriteRenderer.enabled)
        {
            spriteRenderer.enabled = false;
        }
    }
}
