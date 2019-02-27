using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDmg : MonoBehaviour
{
    private static EnemyDmg instance;

    public static EnemyDmg MyInstance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<EnemyDmg>();

            return instance;
        }
    }

    [SerializeField]
    private Enemy enemy;

    public Enemy MyEnemy
    {
        get
        {
            return enemy;
        }
    }
}
