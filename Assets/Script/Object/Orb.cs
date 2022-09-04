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
    }


    Rigidbody2D Rigidbody;
    LineRenderer line;
    RaycastHit2D hit;
    Vector2 hitPos;
    Vector3 dir;

    int layerMaskPeg;

    // Start is called before the first frame update
    void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        line = GetComponent<LineRenderer>();
        Rigidbody.gravityScale = 0f;

        layerMaskPeg = 1 << 10;
    }
    void Start()
    {
        
    }
    private void Update()
    {
        DrawLine();
        
        if (Input.GetMouseButtonDown(0))
        {
            line.enabled = false;
            Rigidbody.gravityScale = 0.5f;
            Rigidbody.AddForceAtPosition(dir, hitPos);
           
        }
    }
    
    private void DrawLine()
    {
        line.SetPosition(0, this.transform.position); //Line Renderer의 포지션 인덱스 0은 Orb
        
        Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousepos.z = 0f;
        line.SetPosition(1, mousepos);
        /*
        //float d = Vector3.Distance(mouse, this.transform.position);
        dir = new Vector3(mouse.x - transform.position.x, mouse.y - transform.position.y, 0f);
        //Physics.Raycast(this.transform.position, dir);

        hit = Physics2D.Raycast(this.transform.position, dir.normalized, Mathf.Infinity, layerMaskPeg);
        

        //
        if (hit.collider != null)
        {
            //Vector3 aa = hit.point;
            
            line.SetPosition(1,hit.point);
            Debug.Log(hit.point);
            Debug.DrawRay(transform.position, dir, Color.green);
        }
        else
        {
            //line.SetPosition(1, mouse);
            Debug.Log("hi");
        }
        */
        line.enabled = true;
    }
    //Plane orbPlane = new Plane();
    //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    private void OnCollisionEnter2D(Collision2D peg)
    {
        
    }

    //Vector2 targetPoint = hit.point; // 끝점을 hit포인트로.
}
