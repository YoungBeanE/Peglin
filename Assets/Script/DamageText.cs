using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DamageText : MonoBehaviour
{
	[SerializeField] AnimationCurve OffsetCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0f), new Keyframe(1f, 0.3f) });
	[SerializeField] AnimationCurve AlphaCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0f, 1f), new Keyframe(1f, 50/255f) });
    [SerializeField] TextMeshProUGUI textMeshProUGUI = null;

    float time = 0f;
	Vector3 oriPos = Vector3.zero;
    Vector3 curOffset = Vector3.zero;

    Color oricolor = Color.yellow;
    Color curcolor = Color.yellow;

    public string Damage { get; set; }
    void Awake()
    {
        if(textMeshProUGUI == null) textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }
    private void OnEnable()
    {
        oriPos = transform.position;    // 시작할 때의 위치도 기록
        textMeshProUGUI.color = oricolor;
        time = 0;
        textMeshProUGUI.text = Damage;
        StartCoroutine(ShowDamageText());
    }

    IEnumerator ShowDamageText()
    {
        if (textMeshProUGUI.text != Damage) textMeshProUGUI.text = Damage;

        while (time < OffsetCurve.keys[OffsetCurve.keys.Length - 1].time)
        {
            // The value of the curve, at the point in time specified.
            curOffset.y = OffsetCurve.Evaluate(time);
            // Pos 변경
            transform.position = oriPos + curOffset;

            // AlphaCurve
            curcolor.a = AlphaCurve.Evaluate(time);
            // Alpha 변경
            textMeshProUGUI.color = curcolor;

            // 시간 누적
            time += Time.deltaTime;
            yield return null;
        }
        
        DamageTextMgr.Inst.DestroyText(this);
    }
    
}
