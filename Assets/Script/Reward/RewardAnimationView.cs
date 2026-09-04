using DG.Tweening;
using System;

using TMPro;
using UnityEngine;
using UnityEngine.UI;









public class RewardAnimationView : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;

    [SerializeField] private Image rewardImage;

    [SerializeField] private TMP_Text rewardName;
    [SerializeField] private TMP_Text rewardAmount;

    [SerializeField] private Button collectButton;

    [SerializeField] private RectTransform flyTarget;

    private Tween rewardFloat;
    private Reward currentReward;

    private Vector2 rewardStartPosition;

    private Sequence sequenceAnimation;


    public event Action<Reward> CollectRequested;



    private void Awake()
    {
        rewardStartPosition = rewardImage.rectTransform.anchoredPosition;

        collectButton.onClick.AddListener(Collect);


        canvasGroup.alpha = 0;

        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }



    private void OnDestroy()
    {
        collectButton.onClick.RemoveListener(Collect);


        if (sequenceAnimation != null)
        {
            sequenceAnimation.Kill();
        }
    }




    public void Show(Reward reward)
    {

        if (sequenceAnimation != null)
        {
            sequenceAnimation.Kill();
        }


        currentReward = reward;


        Debug.Log("reward geldi : " + reward.RewardName);


        rewardImage.sprite = reward.Icon;

        rewardName.text = reward.RewardName.Replace("_", " ").ToUpper();

        rewardAmount.text = "x" + reward.Amount;



        rewardImage.rectTransform.anchoredPosition = rewardStartPosition;

        rewardImage.rectTransform.localScale = Vector3.zero;

        collectButton.transform.localScale = Vector3.zero;



        canvasGroup.alpha = 1;

        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;


        collectButton.interactable = false;



        sequenceAnimation = DOTween.Sequence();


        sequenceAnimation.Append(
            rewardImage.rectTransform.DOScale(Vector3.one, 0.35f)
            .SetEase(Ease.OutBack)
        );


        sequenceAnimation.Append(
            collectButton.transform.DOScale(Vector3.one, 0.2f)
            .SetEase(Ease.OutBack)
        );


        sequenceAnimation.OnComplete(() =>
        {
            collectButton.interactable = true;

            rewardFloat = rewardImage.rectTransform
                .DOAnchorPosY(rewardStartPosition.y + 10f, 0.8f)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo);
        });


    }



    private void Collect()
    {
        if (rewardFloat != null)
        {
            rewardFloat.Kill();
        }
        if (currentReward == null)
        {
            Debug.Log(" reward yok");

            return;
        }


        collectButton.interactable = false;


        Reward reward = currentReward;

        currentReward = null;


        Debug.Log("reward toplandi" + reward.RewardName);



        if (sequenceAnimation != null)
        {
            sequenceAnimation.Kill();
        }


        sequenceAnimation = DOTween.Sequence();



        sequenceAnimation.Append(
            collectButton.transform.DOScale(Vector3.zero, 0.1f)
        );



        sequenceAnimation.Join(
            rewardImage.rectTransform.DOMove(flyTarget.position, 0.4f)
            .SetEase(Ease.InQuad)
        );


        sequenceAnimation.Join(
            rewardImage.rectTransform.DOScale(Vector3.zero, 0.4f)
        );


        sequenceAnimation.Join(
            canvasGroup.DOFade(0, 0.4f)
        );



        sequenceAnimation.OnComplete(() =>
        {

            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;


            if (CollectRequested != null)
            {
                CollectRequested.Invoke(reward);
            }

        });

    }
}