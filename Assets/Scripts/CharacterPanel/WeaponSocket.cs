using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class WeaponSocket : GearSocket
{
    private float currentX, currentY;

    [SerializeField]
    private SpriteRenderer parentRenderer;

    public override void SetXAndY(float x, float y)
    {
        base.SetXAndY(x, y);

        if (currentY != y)
        {
            if (y == 1)
            {
                //nazad
                //spriteRenderer.sortingOrder = parentRenderer.sortingOrder - 1;
                spriteRenderer.sortingOrder = 6;
            }
            else
            {
                //naprijed
                //spriteRenderer.sortingOrder = parentRenderer.sortingOrder + 1;
                spriteRenderer.sortingOrder = 7;
            }
        }
        else if (currentX != x)
        {
            if (x == 1)
            {
                spriteRenderer.sortingOrder = 8;
            }
            else
            {
                spriteRenderer.sortingOrder = 4;
            }
        }
    }
}
