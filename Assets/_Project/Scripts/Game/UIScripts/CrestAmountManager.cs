using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CrestAmountManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI summonCrest1, summonCrest2, summonCrest3, summonCrest4, 
               spellCrest, defenseCrest, attackCrest, movementCrest, trapCrest;

    int summonLevel1 = 0, summonLevel2 = 0, summonLevel3 = 0, summonLevel4 = 0,
        spell = 0, defense = 0, attack = 0, movement = 0, trap = 0;

    public void SetCrest(CrestType crestType , int crestLevel)
    {
        if(crestType == CrestType.Summoning)
        {
            if(crestLevel == 1)
            {
                summonLevel1++;
            }
            if (crestLevel == 2)
            {
                summonLevel2++;
            }
            if (crestLevel == 3)
            {
                summonLevel3++;
            }
            if (crestLevel == 4)
            {
                summonLevel4++;
            }
        }
        else if(crestType == CrestType.Spell)
        {
            spell += crestLevel;
        }
        else if (crestType == CrestType.Defense)
        {
            defense += crestLevel;
        }
        else if (crestType == CrestType.Attack)
        {
            attack += crestLevel;
        }
        else if (crestType == CrestType.Movement)
        {
            movement += crestLevel;
        }
        else if (crestType == CrestType.Trap)
        {
            trap += crestLevel;
        }
        SetUI();
    }

    public void SetUI()
    {
        summonCrest1.text = summonLevel1.ToString();
        summonCrest2.text = summonLevel2.ToString();
        summonCrest3.text = summonLevel3.ToString();
        summonCrest4.text = summonLevel4.ToString();
        spellCrest.text = spell.ToString();
        defenseCrest.text = defense.ToString();
        attackCrest.text = attack.ToString();
        movementCrest.text = movement.ToString();
        trapCrest.text = trap.ToString();
    }
}