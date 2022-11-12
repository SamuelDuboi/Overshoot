using UnityEngine;
using UnityEngine.UI;

public class Gauge : MonoBehaviour
{
    public Slider slider;
    public int length;
    private float currentFill;

    public void FillGauge(float step)
    {
        currentFill += step;
        slider.value = currentFill / length;
        if (currentFill >= length)
        {
            GameManager.instance.GiveIdealWeapon(this);
            currentFill = 0;
        }
    }

    public void RemoveGauge(float step)
    {
        currentFill -= step;
    }

    public void ResetGauge()
    {
        currentFill = 0;
    }
}
