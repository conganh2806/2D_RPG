using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private HitPoints hitPoints;

    [HideInInspector] public Player character;

    [SerializeField] private Image meterImage;
    [SerializeField] private Text hpText;
    private float maxHitPoints;

    private void Start()
    {
        maxHitPoints = character.maxHitPoints;
    }

    private void Update()
    {
        AdjustFillAmount();
    }

    private void AdjustFillAmount()
    {
        if(character != null)
        {
            meterImage.fillAmount = hitPoints.value / maxHitPoints;
            hpText.text = "HP: " + (meterImage.fillAmount * 100);
        }
    }





}