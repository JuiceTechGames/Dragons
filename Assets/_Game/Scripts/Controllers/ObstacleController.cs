using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    [SerializeField] private DragonController _dragonController;
    [SerializeField] private TextMeshPro _numberText;
    [SerializeField] private GameObject _mesh;
    [SerializeField] private int _obstacleNumber;
    [SerializeField] private bool isDestroyed;

    private void Start()
    {
        _dragonController.GetCaged();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isDestroyed) return;
        if (other.TryGetComponent(out DragonController dragonController))
        {
            if (!dragonController.IsCaged())
            {
                dragonController.GetDestroyed();
            }
        }
    }

    public void DecreaseNumber(int amount)
    {
        transform.DOShakeScale(0.15f, 1, 5);
        if (isDestroyed) return;
        _obstacleNumber -= amount;
        if (_obstacleNumber <= 0)
        {
            _obstacleNumber = 0;
            isDestroyed = true;
            DestroyAnim();
            FreeDragon();
        }

        UpdateText();
    }

    private void DestroyAnim()
    {
        _mesh.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBounce).OnComplete(DestroyCage);
    }
    private void DestroyCage()
    {
        _mesh.SetActive(false);
    }

    public bool IsDestroyed()
    {
        return isDestroyed;
    }
    private void FreeDragon()
    {
        _dragonController.GetFreed();
        _dragonController = null;
    }

    private void UpdateText()
    {
        _numberText.text = _obstacleNumber.ToString();
        _numberText.gameObject.SetActive(_obstacleNumber > 0);
    }

    private void OnValidate()
    {
        UpdateText();
    }
}