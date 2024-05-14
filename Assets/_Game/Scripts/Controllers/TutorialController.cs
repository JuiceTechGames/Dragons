using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    [SerializeField] private Transform _start, _end;
    [SerializeField] private Image _handImage;

    private void Start()
    {
        StartAnim();
    }

    private void OnEnable()
    {
        GameManager.GameStartedEvent += StopAnim;
        GameManager.GameStartedEvent += StartTapAnim;
    }

    private void OnDisable()
    {
        GameManager.GameStartedEvent -= StopAnim;
        GameManager.GameStartedEvent -= StartTapAnim;
    }

    private void StopAnim()
    {
        _handImage.DOFade(0, 0.25f).SetDelay(0.25f);
        _handImage.transform.DOKill();
    }

    private void StartTapAnim()
    {
        //_handImage.DOFade(1, 0.25f).SetDelay(0.25f);
        //MoveBetween();

    }

    private void MoveToEnd()
    {
        _handImage.transform.DOMove(_end.position, 1).OnComplete(MoveToStart).SetEase(Ease.InQuad);
    }

    private void MoveToStart()
    {
        _handImage.transform.DOMove(_start.position, 1).OnComplete(MoveToEnd).SetEase(Ease.InQuad);
    }

    private void MoveBetween()
    {
        Vector3 between = (_end.position + _start.position) / 2;
        _handImage.transform.DOMove(between, 0);
        _handImage.transform.DOScale(_handImage.transform.localScale * 1.2f,0.5f).SetLoops(-1, LoopType.Yoyo);


    }
    private void StartAnim()
    {
        _handImage.transform.DOMove(_start.position, 0);
        _handImage.DOFade(1, 0.1f);
        MoveToEnd();
    }
}