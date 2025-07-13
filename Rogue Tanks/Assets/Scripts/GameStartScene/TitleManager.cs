using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TitleManager : MonoBehaviour
{
    [Tooltip("Assign all 36 slices here")]
    public GameObject[] columns;

    [Tooltip("Time in seconds over which all columns will appear")]
    public float revealDuration = 5f;

    private List<GameObject> hiddenColumns = new List<GameObject>();

    void Start()
    {
        // Fill the list with all slices
        hiddenColumns.AddRange(columns);

        // Start the reveal animation
        StartCoroutine(RevealColumnsCoroutine());
    }

    IEnumerator RevealColumnsCoroutine()
    {
        float interval = revealDuration / columns.Length;

        while (hiddenColumns.Count > 0)
        {
            // Pick a random column
            int index = Random.Range(0, hiddenColumns.Count);
            GameObject col = hiddenColumns[index];

            // Show it
            col.SetActive(true);

            // Remove from the list
            hiddenColumns.RemoveAt(index);

            // Wait before revealing the next
            yield return new WaitForSeconds(interval);
        }
    }
}