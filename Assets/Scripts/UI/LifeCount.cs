using UnityEngine;
using UnityEngine.UI;
public class LifeCount : MonoBehaviour
{
    public Text life;

    public void setLife(int life)
    {
        this.life.text = life.ToString();
    }
}
