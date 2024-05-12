using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    public Slider slider;
    public Gradient sliderGradient;
    public Image fill;

    public void setMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        fill.color = sliderGradient.Evaluate(1f);
    }

    public void setHealth(int health)
    {
        slider.value = health;
        fill.color = sliderGradient.Evaluate(slider.normalizedValue);
    }

    public void setMaxMana(float mana)
    {
        slider.maxValue = mana;
        slider.value = 0;
        fill.color = sliderGradient.Evaluate(slider.normalizedValue);
    }

    public void setMana(float health)
    {
        slider.value = health;
        fill.color = sliderGradient.Evaluate(slider.normalizedValue);
    }
}
