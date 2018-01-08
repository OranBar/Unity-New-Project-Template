using UnityEngine;
using Pixelplacement;
using Pixelplacement.TweenSystem;

public class SplineTweener : MonoBehaviour
{
	public bool startOnAwake;
	public Spline mySpline;
	public Transform targetTransform;
	public float duration = 3;
	public float dealy = 0;
	public bool faceDirection = true;
	public Tween.LoopType loopType = Tween.LoopType.PingPong;

	public TweenBase SplineTween { get; private set; }

	void Awake()
	{
		if (startOnAwake)
		{
			StartSplineTweening();
		}
	}

	public void StartSplineTweening()
	{
		SplineTween = Tween.Spline(mySpline, targetTransform, 0, 1, faceDirection, duration, dealy, Tween.EaseInOut, loopType);
	}
}