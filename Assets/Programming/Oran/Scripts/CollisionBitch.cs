using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CollisionBitch : MonoBehaviour {

    public Action<Collision> OnCollisionEnter_fn = null;
    public Action<Collision> OnCollisionStay_fn = null; 
    public Action<Collision> OnCollisionExit_fn = null;


    public Action<Collision2D> OnCollisionEnter2D_fn = null;
    public Action<Collision2D> OnCollisionStay2D_fn = null;
    public Action<Collision2D> OnCollisionExit2D_fn = null;

    public Action<Collider> OnTriggerEnter_fn = null;
    public Action<Collider> OnTriggerStay_fn = null;
    public Action<Collider> OnTriggerExit_fn = null;

    public Action<Collider2D> OnTriggerEnter2D_fn = null;
    public Action<Collider2D> OnTriggerStay2D_fn = null;
    public Action<Collider2D> OnTriggerExit2D_fn = null;

    private void OnCollisionEnter(Collision other)
    {
        OnCollisionEnter_fn?.Invoke(other);
    }

	private void OnCollisionExit(Collision other)
    {
        OnCollisionExit_fn?.Invoke(other);

    }

    private void OnCollisionStay(Collision other)
    {
        OnCollisionStay_fn?.Invoke(other);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        OnCollisionEnter2D_fn?.Invoke(other);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        OnCollisionStay2D_fn?.Invoke(other);

    }

	void OnCollisionExit2D(Collision2D other)
	{
		OnCollisionExit2D_fn.Invoke(other);
	}

    private void OnTriggerEnter(Collider other)
    {
        OnTriggerEnter_fn?.Invoke(other);
    }

    private void OnTriggerExit(Collider other)
    {
        OnTriggerExit_fn?.Invoke(other);
    }

	private void OnTriggerStay(Collider other)
	{
        OnTriggerStay_fn?.Invoke(other);
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        OnTriggerEnter2D_fn?.Invoke(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        OnTriggerExit2D_fn?.Invoke(other);
    }
	
    private void OnTriggerStay2D(Collider2D other)
    {
        OnTriggerStay2D_fn?.Invoke(other);
    }
}
