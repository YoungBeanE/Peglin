using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DamageText : MonoBehaviour
{
	[SerializeField] AnimationCurve OffsetCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0f), new Keyframe(1f, 0.3f) });
	[SerializeField] AnimationCurve AlphaCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0f, 250/255f), new Keyframe(1f, 50/255f) });
    [SerializeField] TextMeshProUGUI textMeshProUGUI = null;

    float time = 0f;
	Vector3 oriPos = Vector3.zero;
    Vector3 curOffset = Vector3.zero;

    Color oricolor;
    Color curcolor;

    public string Damage { get; set; }
    void Awake()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }
    private void OnEnable()
    {
        oriPos = transform.position;    // ������ ���� ��ġ�� ���
        oricolor = new Color(250 / 255f, 250 / 255f, 10 / 255f, 250 / 255f);
        curcolor = new Color(250 / 255f, 250 / 255f, 10 / 255f, 250 / 255f);
        time = 0;
        textMeshProUGUI.text = (Damage);
        Debug.Log("��" + Damage);
    }


    // Update is called once per frame
    void Update()
    {
        if(textMeshProUGUI.text == Damage)
            Debug.Log("��" + Damage);
        // The value of the curve, at the point in time specified.
        curOffset.y = OffsetCurve.Evaluate(time);    
        // ��ġ ����
        transform.position = oriPos + curOffset;

        // AlphaCurve
        curcolor.a = AlphaCurve.Evaluate(time);
        // Alpha ����
        textMeshProUGUI.color = curcolor;


        // �ð� ����
        time += Time.deltaTime;

        // AnimationCurve ������ �ð�
        if (OffsetCurve.keys[OffsetCurve.keys.Length-1].time <= time)
		{
            // ��Ȱ��ȭ
            DamageTextMgr.Inst.DestroyText(this);
        }
    }
}
