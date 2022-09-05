using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    int level;
    string nam;
    int damage;
    int criDamage;
    int healPower;
    int attackPower;
    string info;

    public void SetData(OrbData orb)
    {
        level = orb.Level;
        nam = orb.Name;
        damage = orb.Damage;
        criDamage = orb.CriDamage;
        healPower = orb.HealPower;
        attackPower = orb.AttackPower;
        info = orb.Info;
        Debug.Log($"{level}, {nam}, {damage}, {criDamage}, {healPower}, {attackPower}, {info}");
    }

    Player player;

    Rigidbody2D rigidbody;
    LineRenderer line;
    CircleCollider2D CircleCollider;
    RaycastHit2D hit;
    Vector3 MousePosition;
    Vector3 hitPos;
    Vector3 dir;

    //int layerMaskPeg;

    // Start is called before the first frame update
    void Awake()
    {
        player = FindObjectOfType<Player>();
        rigidbody = GetComponent<Rigidbody2D>();
        line = GetComponent<LineRenderer>();
        CircleCollider = GetComponent<CircleCollider2D>();
        rigidbody.gravityScale = 0f;
        CircleCollider.enabled = false;
        //layerMaskPeg = 1 << 10;
    }
    void onEnable()
    {
        
    }
    private void Update()
    {
        DrawLine();
        
        if (Input.GetMouseButtonDown(0))
        {
            line.enabled = false;
            CircleCollider.enabled = true;
            rigidbody.AddForceAtPosition(dir * 100f, hitPos);
            
        }
    }
    
    private void DrawLine()
    {
        line.enabled = true;
        line.SetPosition(0, this.transform.position); //Line Renderer의 포지션 인덱스 0은 Orb
        
        MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        MousePosition.z = 0f;
        dir = (MousePosition-this.transform.position).normalized;

        
        hit = Physics2D.Raycast(this.transform.position, dir, 2.5f);
        if (hit)
        {
            hitPos = hit.point;
            line.SetPosition(1, hitPos);
        }
        else
        {
            if(Vector3.Distance(MousePosition, this.transform.position) >= 2.5f)
            {
                hitPos = dir * 2f;
                line.SetPosition(1, hitPos);
            }
            else
            {
                hitPos = MousePosition;
                line.SetPosition(1, hitPos);
            }
            
        }
        
    }
    private void OnCollisionEnter2D(Collision2D peg)
    {
        rigidbody.gravityScale = 0.2f;
        switch (peg.transform.tag)
        {
            case "OriPeg":
                attackPower += damage;
                break;
            case "CriPeg":
                attackPower *= criDamage;
                break;
            case "BomPeg":
                attackPower += damage;
                break;
            case "RefPeg":
                attackPower += damage;
                break;
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Attack")
        {

            rigidbody.gravityScale = 0f;
            CircleCollider.enabled = false;
            OrbPool.Inst.DestroyOrb(this);
            player.AttackPower = attackPower;
            Debug.Log(attackPower);
            OrbPool.Inst.SetOrb();
        }
    }

}
