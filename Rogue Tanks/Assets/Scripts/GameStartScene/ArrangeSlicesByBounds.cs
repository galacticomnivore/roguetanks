using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrangeSlicesByBounds : MonoBehaviour
{
    [Tooltip("If true, arrange left to right; if false, arrange top to bottom.")]
    public bool arrangeHorizontally = true;

    [Tooltip("Optional spacing between slices.")]
    public float spacing = 0f;

    void Start()
    {
        Arrange();
    }

    public void Arrange()
    {
        int childCount = transform.childCount;
        Vector3 currentPosition = Vector3.zero;

        for (int i = 0; i < childCount; i++)
        {
            Transform child = transform.GetChild(i);
            SpriteRenderer sr = child.GetComponent<SpriteRenderer>();
            if (sr == null)
            {
                Debug.LogWarning("Child has no SpriteRenderer: " + child.name);
                continue;
            }

            // Get the sprite's width and height in world units
            Vector2 size = sr.bounds.size;

            // Set the child's local position
            child.localPosition = currentPosition;

            // Move to the next position
            if (arrangeHorizontally)
                currentPosition.x += size.x + spacing;
            else
                currentPosition.y -= size.y + spacing; // y negative for top to bottom
        }
    }
}

