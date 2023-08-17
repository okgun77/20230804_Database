using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_HUD_HP : MonoBehaviour
{
    [SerializeField] private RectTransform heartHolderTr = null;
    [SerializeField] private GameObject heartPrefab = null;

    private List<UI_HUD_HP_Heart> heartList = new List<UI_HUD_HP_Heart>();
    private float heartOffset = 50f;

    //private void Start()
    //{
    //    Init(3);
    //}

    public void Init(int _maxHp)
    {
        if (_maxHp < 1) _maxHp = 1;

        for (int i = 0; i < _maxHp; ++i)
        {
            GameObject go = Instantiate(heartPrefab, heartHolderTr);
            UI_HUD_HP_Heart heart = go.GetComponent<UI_HUD_HP_Heart>();
            heart.SetLocalPosition(new Vector3(i * heartOffset, 0f, 0f));
            heartList.Add(heart);
        }
    }

    public void FullHp()
    {
        foreach (UI_HUD_HP_Heart heart in heartList)
            heart.Fill();
    }

    public void UpdateHp(int _hp)
    {
        if (_hp >= heartList.Count) return;
        heartList[_hp].Empty();
    }
}