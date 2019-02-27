using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//delegati za evente
public delegate void ChangedHealth(float health);
public delegate void CharacterRemoved();

public class NPC : Character, IInteractable
{
    //eventi za delegate
    public event ChangedHealth healthChanged;
    public event CharacterRemoved removedCharacter;

    [SerializeField]
    private Sprite portrait;

    public Sprite MyPortrait
    {
        get { return portrait; }
    }

    public virtual Transform Select()
    {
        return hitBox;
    }
    public virtual void DeSelect()
    {
        healthChanged -= new ChangedHealth(UIManager.MyInstance.UpdateTargetFrame);
        removedCharacter -= new CharacterRemoved(UIManager.MyInstance.HideTargetFrame);

    }

    public void OnHealthChanged(float health)
    {
        if (healthChanged != null)
            healthChanged(health);
    }

    public void OnCharacterRemoved()
    {
        if (removedCharacter != null)
            removedCharacter();
        Destroy(gameObject);
    }

    public virtual void Interact()
    {
        Debug.Log("Otvara dialog s npc-evima.");
    }

    public virtual void StopInteract()
    {
        
    }
}
