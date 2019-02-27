using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearSocket : MonoBehaviour
{
    public Animator MyAnimator { get; set; }

    protected SpriteRenderer spriteRenderer;

    private Animator parentAnimator;

    private AnimatorOverrideController animatorOverrideController;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        parentAnimator = GetComponentInParent<Animator>();
        MyAnimator = GetComponent<Animator>();

        animatorOverrideController = new AnimatorOverrideController(MyAnimator.runtimeAnimatorController);
        MyAnimator.runtimeAnimatorController = animatorOverrideController;
    }

    public virtual void SetXAndY(float x, float y)
    {
        //podesava smijer animacija
        MyAnimator.SetFloat("MovingX", x);
        MyAnimator.SetFloat("MovingY", y);
    }

    public void ActivateLayer(string layerName)
    {
        for (int i = 0; i < MyAnimator.layerCount; i++)
        {
            MyAnimator.SetLayerWeight(i, 0);
        }

        MyAnimator.SetLayerWeight(MyAnimator.GetLayerIndex(layerName), 1);
    }

    public void Equip(AnimationClip[] animations)
    {
        spriteRenderer.color = Color.white;

        //napad
        animatorOverrideController["ABack"] = animations[0];
        animatorOverrideController["AFront"] = animations[1];
        animatorOverrideController["ALeft"] = animations[2];
        animatorOverrideController["ARight"] = animations[3];

        //idle
        animatorOverrideController["IBack"] = animations[4];
        animatorOverrideController["IFront"] = animations[5];
        animatorOverrideController["ILeft"] = animations[6];
        animatorOverrideController["IRight"] = animations[7];

        //walk
        animatorOverrideController["WBack"] = animations[8];
        animatorOverrideController["WFront"] = animations[9];
        animatorOverrideController["WLeft"] = animations[10];
        animatorOverrideController["WRight"] = animations[11];
    }

    public void Dequip()
    {
        //napad
        animatorOverrideController["ABack"] = null;
        animatorOverrideController["AFront"] = null;
        animatorOverrideController["ALeft"] = null;
        animatorOverrideController["ARight"] = null;

        //idle
        animatorOverrideController["IBack"] = null;
        animatorOverrideController["IFront"] = null;
        animatorOverrideController["ILeft"] = null;
        animatorOverrideController["IRight"] = null;

        //walk
        animatorOverrideController["WBack"] = null;
        animatorOverrideController["WFront"] = null;
        animatorOverrideController["WLeft"] = null;
        animatorOverrideController["WRight"] = null;

        Color c = spriteRenderer.color;
        c.a = 0;
        spriteRenderer.color = c;
    }
}
