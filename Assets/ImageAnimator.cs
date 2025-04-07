using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageAnimation : MonoBehaviour
{

	public Sprite[] sprites;
	public int FramesPerSprite = 6;
	public bool IsLooping = true;
	public bool ShouldDestroyOnEnd = false;

	private int index = 0;
	[SerializeField] private SpriteRenderer _sr = null;
	private int frame = 0;


	void Update()
	{
		if (!IsLooping && index == sprites.Length) return;
		frame++;
		if (frame < FramesPerSprite) return;
		_sr.sprite = sprites[index];
		frame = 0;
		index++;
		if (index >= sprites.Length)
		{
			if (IsLooping) index = 0;
			if (ShouldDestroyOnEnd) Destroy(gameObject);
		}
	}
}
