using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ColorMySprite : MonoBehaviour {

    private SpriteRenderer spriteRenderer;
	// Use this for initialization
	void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
        {
            float r= Random.Range(0f, 1f);
            float b = Random.Range(0f, 1f);
            float c = Random.Range(0f, 1f);
            spriteRenderer.color = new Color(r, b, c);
        }
	}
}
