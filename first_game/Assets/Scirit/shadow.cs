using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class shadow : MonoBehaviour
{
    private Transform player;
    private SpriteRenderer thisSprite;
    private SpriteRenderer playerSprite;
    private Color color;
    [Header("時間控制參數")]
    public float activeTime;//顯示時間
    public float activeStart;//開始顯示時間點

    [Header("不透明度控制")]
    private float alpha;
    public float alphaSet;
    public float alphaMultiplier;
    
    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        thisSprite = GetComponent<SpriteRenderer>();
        playerSprite =player.GetComponent<SpriteRenderer>();
        

        alpha = alphaSet;

        thisSprite.sprite = playerSprite.sprite;

        transform.position = player.position;
        transform.localScale = player.localScale;
        transform.rotation = player.rotation;

        activeStart = Time.time;
    }
    void Update()
    {
        alpha *= alphaMultiplier;
        color = new Color(0.5f, 0.5f, 1, alpha);
        thisSprite.color = color;

        if(Time.time >= activeStart + activeTime)
        {
            //返回對象持
            shadowpool.instance.ReturnPool(this.gameObject);
        }
    }
}
