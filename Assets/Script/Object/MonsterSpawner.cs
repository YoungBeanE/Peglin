using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] ResourceDataObj ResDataObj = null; // 리소스 데이터
    [SerializeField] Monster[] prefabmob = null;
    [SerializeField] List<Monster> Monsters1 = new List<Monster>(); // Pmob, Smob
    //[SerializeField] List<Monster> Monsters2 = new List<Monster>(); // Bmob, Mmob

    // Start is called before the first frame update
    void Awake()
    {
        ResDataObj = Resources.Load<ResourceDataObj>("MyResourceDataObj");
        prefabmob = new Monster[ResDataObj.mob.Length];
        //리소스데이터에서 mob 가져오기
        for (int i = 0; i < prefabmob.Length; i++)
        {
            prefabmob[i] = ResDataObj.mob[i]; // Pmob, Smob, Bmob, Mmob
        }
    }
    void Start()
    {
        Monster pmob = Instantiate<Monster>(prefabmob[0], new Vector2(3f, 1.7f), Quaternion.identity, this.transform);
        Monsters1.Add(pmob);
        Monster smob = Instantiate<Monster>(prefabmob[1], new Vector2(4f, 1.7f), Quaternion.identity, this.transform);
        Monsters1.Add(smob);
        /*
        if (Monsters2 == null)
        {
            Monster bmob = Instantiate<Monster>(prefabmob[2], new Vector2(3f, 2.4f), Quaternion.identity, this.transform);
            Monsters2.Add(bmob);
            Monster mmob = Instantiate<Monster>(prefabmob[3], new Vector2(4f, 1.7f), Quaternion.identity, this.transform);
            Monsters2.Add(mmob);
        }
        */
    }
}
