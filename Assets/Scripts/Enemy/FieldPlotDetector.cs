using UnityEngine;

public class FieldPlotDetector : MonoBehaviour
{
    private FieldPlot currentFieldPlot;

    public FieldPlot CurrentFieldPlot => currentFieldPlot;

    private void OnTriggerEnter(Collider other)
    {
        FieldPlot fieldPlot = other.GetComponent<FieldPlot>();

        if (fieldPlot != null)
        {
            currentFieldPlot = fieldPlot;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        FieldPlot fieldPlot = other.GetComponent<FieldPlot>();

        if (fieldPlot != null && currentFieldPlot == fieldPlot)
        {
            currentFieldPlot = null;
        }
    }
}