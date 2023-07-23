using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreparationBoomListener : EventListener<EndAnimationEvent>
{
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private GameObject explosionEffect;
    public override IEventHundle EventHundle { get { eventHundle = eventHundleObject;
            return eventHundle;
        } set => eventHundle = value; }

    protected override void EventListen()
    {
        _spriteRenderer.enabled = true;
        StartCoroutine(Explosion());
        _animator.Play("BoomAnimation");
    }

    void Start()
    {
        EventHundle._event += EventListen;
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.enabled = false;
    }

    private IEnumerator Explosion()
    {
        explosionEffect.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        explosionEffect.SetActive(false);
    }
}
