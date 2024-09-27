using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBars : MonoBehaviour
{
    public Image healthbar;

    public void UpdateHealth(float fraction)
    {
        healthbar.fillAmount = fraction;
    }
}
