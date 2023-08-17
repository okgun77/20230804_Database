using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_HUD_Missile : MonoBehaviour
{
    [SerializeField] private RectTransform missileHolderTr = null;
    [SerializeField] private GameObject missilePrefab = null;

    private List<UI_HUD_Missile_Missile> missileList = new List<UI_HUD_Missile_Missile>();
    private float missileOffset = 50f;

    //private void Start()
    //{
    //    Init(3);
    //}

    public void Init(int _maxMissileCnt)
    {
        if (_maxMissileCnt < 1) _maxMissileCnt = 1;

        for (int i = 0; i < _maxMissileCnt; ++i)
        {
            GameObject go = Instantiate(missilePrefab, missileHolderTr);
            UI_HUD_Missile_Missile missile = go.GetComponent<UI_HUD_Missile_Missile>();
            missile.SetLocalPosition(new Vector3(i * missileOffset, 0f, 0f));
            missileList.Add(missile);
        }
    }

    public void ReloadAll()
    {
        foreach (UI_HUD_Missile_Missile missile in missileList)
            missile.Fill();
    }

    //public void UpdateMissileCnt(int _missileCnt)
    //{
    //    if (_missileCnt >= missileList.Count) return;
    //    for( int i = 0; i <missileList.Count; ++i)
    //    {
    //        if (i < _missileCnt) missileList[i].Fill();
    //        else missileList[i].Empty();
    //    }    
    //}

    public void UpdateMissileState(int _idx, bool _isFill)
    {
        if (_idx < 0 && _idx >= missileList.Count) return;

        if (_isFill)
            missileList[_idx].Fill();
        else
            missileList[_idx].Empty();
    }
}
