using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AttackText : MonoBehaviour
{
    [SerializeField] AnimationCurve OffsetCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0f), new Keyframe(1f, 40f) });
    [SerializeField] AnimationCurve ScaleCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0f), new Keyframe(1f, 1f) });
    [SerializeField] TextMeshProUGUI textMeshProUGUI = null;


    Vector3 oriPos = Vector3.zero;
    Vector3 oriScale = Vector3.one;
    Vector3 curOffset = Vector3.zero;
    Vector3 curScale = Vector3.one;
    

    float time = 0f;
 
    public string Attack { get; set; }
    void Awake()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();

    }
    private void OnEnable()
    {
        oriPos = transform.position;    // ���� ��ġ ���
        oriScale = transform.localScale;
        time = 0;
        textMeshProUGUI.text = Attack;
    }


    // Update is called once per frame
    void Update()
    {
        if (textMeshProUGUI.text != Attack)
        {
            textMeshProUGUI.text = Attack;
        }

        // The value of the curve, at the point in time specified.
        curOffset.y = OffsetCurve.Evaluate(time);
        // ��ġ ����
        transform.position = oriPos + curOffset;


        // ScaleCurve
        curScale = Vector3.one * ScaleCurve.Evaluate(time);
        // ������ ����
        transform.localScale = curScale;


        // �ð� ����
        time += Time.deltaTime;

        // AnimationCurve ������ �ð�
        if (OffsetCurve.keys[OffsetCurve.keys.Length - 1].time <= time)
        {
            // ��Ȱ��ȭ
            DamageTextMgr.Inst.DestroyAttackText(this);
        }
    }
    
}
