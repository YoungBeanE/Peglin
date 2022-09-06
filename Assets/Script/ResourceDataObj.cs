using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New ResourceDataObj", menuName ="ScriptableObjects/ResourceDataObj", order = 1)]
public class ResourceDataObj : ScriptableObject
{
    public float PlayerAttacPower = 1;
    public int PlayerOrb = 5;

    public GameObject EffHit;
    public Orb[] orb;
    public Peg[] peg;
    public DamageText damagetext;

    public int GetData()
	{
        return PlayerOrb;
    }
}
