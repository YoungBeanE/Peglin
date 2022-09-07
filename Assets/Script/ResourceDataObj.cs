using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New ResourceDataObj", menuName ="ScriptableObjects/ResourceDataObj", order = 1)]
public class ResourceDataObj : ScriptableObject
{
    public int PlayerOrb = 5;

    public GameObject EffHit;
    public GameObject BombEff;
    public GameObject Bomb;
    public Orb[] orb;
    public Peg[] peg;
    public Monster[] mob;
    public DamageText damagetext;
    public AttackText attacktext;

}
