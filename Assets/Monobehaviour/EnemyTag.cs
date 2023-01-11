using Scellecs.Morpeh;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTag : MonoBehaviour
{
    public Entity Entity;
    public EnemyType Type;
    public int Health;
}
public enum EnemyType { Weak, Medium, Strong }
