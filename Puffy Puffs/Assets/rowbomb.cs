using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rowbomb : MonoBehaviour
{
     Bomb bomb;
    SpriteRenderer sr;
    public Sprite[] sprites;
    public int spriteDeger;
    private void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        bomb = gameObject.GetComponent<Bomb>();



    }
    private void Update()
    {
        sr.sprite = sprites[spriteDeger];
        if (bomb.matchValue == MatchValue.SiseMorSoda)
        {
            spriteDeger = 0;
        }
        if (bomb.matchValue == MatchValue.kartonSoda)
        {
            spriteDeger = 1;
        }
        if (bomb.matchValue == MatchValue.KirmiziSoda)
        {
            spriteDeger = 2;
        }
        if (bomb.matchValue == MatchValue.PembeSoda)
        {
            spriteDeger = 3;
        }
        if (bomb.matchValue == MatchValue.SariSoda)
        {
            spriteDeger = 4;
        }
        if (bomb.matchValue == MatchValue.MaviSodaS)
        {
            spriteDeger = 5;
        }
        if (bomb.matchValue == MatchValue.SiyahSoda)
        {
            spriteDeger = 6;
        }
        if (bomb.matchValue == MatchValue.YesilSoda)
        {
            spriteDeger = 7;
        }
    }

    // Update is called once per frame
    
}
