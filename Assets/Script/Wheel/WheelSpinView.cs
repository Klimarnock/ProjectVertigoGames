using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class WheelSpinView : MonoBehaviour
{
    [Header("Wheel Staff")]
    [SerializeField] private RectTransform wheelRotator;   
    [SerializeField] private Button spinButton;

    [Header("Spin Settings")]
    [SerializeField] private int slotCount = 8;
    [SerializeField] private int fullSpinCount = 4;
    [SerializeField] private float spinDuration = 4f;
    [SerializeField] private float idleRotationSpeed = 20f;
    [SerializeField] private Ease spinEase = Ease.OutBack;

    private bool isSpinning;

    public event Action<int> SpinCompleted;

    private void Awake()
    {
        spinButton.onClick.AddListener(Spin);
    }

    private void OnDestroy()
    {
        spinButton.onClick.RemoveListener(Spin);
        wheelRotator.DOKill();
    }
    
    
    
    public void SetSlotCount(int slotCount)
    {
        this.slotCount = slotCount;
    }


    public void SetInteractable(bool interactable)
    {
        if (isSpinning)
        {
            spinButton.interactable = false;
            return;
        }

        spinButton.interactable = interactable;

        if (interactable)
        {
            StartIdleRotation();
        }
        else
        {
            StopIdleRotation();
        }
    }


    public void StartIdleRotation()
    {
        if (isSpinning) { return; }

        wheelRotator.DOKill();

      
        wheelRotator.DOLocalRotate(new Vector3(0f, 0f, -360f),360f / idleRotationSpeed,RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental);
    }

    public void StopIdleRotation()
    {
        if (!isSpinning) { wheelRotator.DOKill(); }
    }

    private void Spin()
    {
        if (isSpinning) { return; }

        isSpinning = true;
        spinButton.interactable = false;
        wheelRotator.DOKill();

        
        float slotAngle = 360f / slotCount;
        
        
        int rewardIndex = Random.Range(0, slotCount);

        
        float targetAngle = -(360f * fullSpinCount) + rewardIndex * slotAngle;

        wheelRotator.DOLocalRotate(
           new Vector3(0f, 0f, targetAngle),
           spinDuration,
           RotateMode.FastBeyond360
       )
       .SetEase(spinEase)
       .OnComplete(() =>
       {
           isSpinning = false;

           if (SpinCompleted != null)
           {
               SpinCompleted.Invoke(rewardIndex);
           }
       });
    }
}
