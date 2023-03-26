using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AttackText : MonoBehaviour
{
    [SerializeField] AnimationCurve OffsetCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0f), new Keyframe(1f, 2f) });
    [SerializeField] AnimationCurve ScaleCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0f), new Keyframe(1f, 1f) });
    [SerializeField] TextMeshProUGUI textMeshProUGUI = null;


    Vector3 oriPos = Vector3.zero;
    Vector3 curPos = Vector3.zero;
    Vector3 curScale = Vector3.one;
    

    float time = 0f;
 
    public string Attack { get; set; }
    void Awake()
    {
        if(textMeshProUGUI == null) textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }
    private void OnEnable()
    {
        oriPos = transform.position;    // ���� ��ġ ���
        time = 0;
        textMeshProUGUI.text = Attack;
        StartCoroutine(ShowAttackText());
    }

    IEnumerator ShowAttackText()
    {
        if (textMeshProUGUI.text != Attack) textMeshProUGUI.text = Attack;

        while (time < OffsetCurve.keys[OffsetCurve.keys.Length - 1].time)
        {
            // The value of the curve, at the point in time specified.
            curPos.x = OffsetCurve.Evaluate(time);
            // Pos ����
            if (oriPos.x < 0f) transform.position = oriPos + curPos;
            else transform.position = oriPos - curPos;

            // ScaleCurve
            curScale = Vector3.one * ScaleCurve.Evaluate(time);
            // scale ����
            transform.localScale = curScale;

            // �ð� ����
            time += Time.deltaTime;
            yield return null;
        }

        DamageTextMgr.Inst.DestroyAttackText(this);
    }
    
}
