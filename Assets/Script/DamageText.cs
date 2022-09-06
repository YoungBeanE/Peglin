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
        oriPos = transform.position;    // 시작할 때의 위치도 기록
        oricolor = new Color(250 / 255f, 250 / 255f, 10 / 255f, 250 / 255f);
        curcolor = new Color(250 / 255f, 250 / 255f, 10 / 255f, 250 / 255f);
        time = 0;
        textMeshProUGUI.text = (Damage);
        Debug.Log("얼마" + Damage);
    }


    // Update is called once per frame
    void Update()
    {
        if(textMeshProUGUI.text == Damage)
            Debug.Log("음" + Damage);
        // The value of the curve, at the point in time specified.
        curOffset.y = OffsetCurve.Evaluate(time);    
        // 위치 변경
        transform.position = oriPos + curOffset;

        // AlphaCurve
        curcolor.a = AlphaCurve.Evaluate(time);
        // Alpha 변경
        textMeshProUGUI.color = curcolor;


        // 시간 누적
        time += Time.deltaTime;

        // AnimationCurve 마지막 시간
        if (OffsetCurve.keys[OffsetCurve.keys.Length-1].time <= time)
		{
            // 비활성화
            DamageTextMgr.Inst.DestroyText(this);
        }
    }
}
