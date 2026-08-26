using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;
using TMPro;
public class Wheel_Vision : MonoBehaviour
{
    [SerializeField] private RectTransform wheel_rotator;
    [SerializeField] private Button wheel_button;
    [SerializeField] private Image[] reward_image;
    [SerializeField] private Text[] reward_amount;

    [SerializeField] private GameObject ui_gameover_panel;
    [SerializeField] private Image ui_gameover_image;
    [SerializeField] private Button restart_button;
    [SerializeField] private Button leave_button;
    [SerializeField] private TMP_Text ui_gameover_title;
    [SerializeField] private TMP_Text ui_gameover_subtitle;


    [SerializeField] private RectTransform ui_reward;
    [SerializeField] private Image ui_rewardimage;

    [SerializeField] private RectTransform ui_flytarget;
    [SerializeField] private Transform ui_content;
    [SerializeField] private CollectedReward collected_reward_prefab;

   
    [SerializeField] private TMPro.TextMeshProUGUI ui_zone_value;
    [SerializeField] private TMPro.TextMeshProUGUI ui_zone_type_value;

    [SerializeField] private Image ui_wheel_base_image;
    [SerializeField] private Sprite wheel_base_bronze_sprite;
    [SerializeField] private Sprite wheel_base_silver_sprite;
    [SerializeField] private Sprite wheel_base_gold_sprite;

    [SerializeField] private Image ui_wheel_indicator;
    [SerializeField] private Sprite wheel_indicator_bronze_sprite;
    [SerializeField] private Sprite wheel_indicator_silver_sprite;
    [SerializeField] private Sprite wheel_indicator_gold_sprite;

    [SerializeField] private ZoneBar zone_bar;

    private readonly System.Collections.Generic.Dictionary<Reward, CollectedReward> collected_reward_views = new System.Collections.Generic.Dictionary<Reward, CollectedReward>();


    public Ease wheel_rotator_ease;
    private bool wheel_isSpin;
    private int reward_index;
  
    private Vector2 reward_result_startposition;
    private Vector3 reward_result_startscale;


    public event Action<int> OnSpinCompleted;
    public event Action OnRestartRequested;
    public event Action OnStopRequested;
    public event Action OnRewardAnimationCompleted;
    private void Awake()
    {
        reward_result_startposition = ui_reward.anchoredPosition;
        reward_result_startscale = ui_reward.localScale;
        ui_reward.gameObject.SetActive(false);

        wheel_button.onClick.AddListener(OnSpinButtonClicked);
        restart_button.onClick.AddListener(OnRestartButtonClicked);
        leave_button.onClick.AddListener(OnLeaveButtonClicked);
        ui_gameover_panel.SetActive(false);
    }

    private void OnDestroy()
    {
        wheel_button.onClick.RemoveListener(OnSpinButtonClicked);
        restart_button.onClick.RemoveListener(OnRestartButtonClicked);
        leave_button.onClick.RemoveListener(OnLeaveButtonClicked);

        wheel_rotator.DOKill();
    }
    private void OnSpinButtonClicked()
    {
        
        if (wheel_isSpin)
        {
            
            return;
        }
        wheel_isSpin = true;
        wheel_button.interactable = false;
        leave_button.interactable = false;
        
        int randomNumber = Random.Range(0, 8) * 45; //        360/slotnumber = 45
        reward_index = (randomNumber / 45);
        
        wheel_rotator.DOLocalRotate(new Vector3(0, 0, -1440 + randomNumber), 4f, RotateMode.FastBeyond360).SetEase(wheel_rotator_ease)
        .OnComplete(() =>
        {
            wheel_isSpin = false;

          
            OnSpinCompleted?.Invoke(reward_index);

        });





    }
    public void SetWheelButtonInteractable(bool interactable)
    {
        wheel_button.interactable = interactable;
        leave_button.interactable = interactable;
    }
    public void SetRewardImages(Reward[] rewards)
    {

        for (int i = 0; i < reward_image.Length; i++)
        {
            reward_image[i].sprite = rewards[i].Icon;

            reward_amount[i].text= rewards[i].Amount.ToString();

        }


    }
    private void OnRestartButtonClicked()
    {
       
        OnRestartRequested?.Invoke();
    }

    private void OnLeaveButtonClicked()
    {
        
        OnStopRequested?.Invoke();
    }
    public void ShowGameOverPanel(Reward reward)
    {
        ui_gameover_panel.SetActive(true);
        ui_gameover_image.sprite = reward.Icon;
        ui_gameover_panel.transform.SetAsLastSibling();
        ui_gameover_image.preserveAspect = true;

        ui_gameover_title.text = "OH NO, A BOMB EXPLODED RIGHT IN YOUR HANDS!";
        ui_gameover_subtitle.text = "Revive yourself to keep your rewards.";
        leave_button.interactable = false;


        wheel_button.interactable = false;
    }
    public void HideGameOverPanel()
    {
        ui_gameover_panel.SetActive(false);
        wheel_button.interactable = true;
        leave_button.interactable = false;
    }

    public void ShowRewardAnimation(Reward reward, int collectedCount)
    {
        ui_reward.DOKill();

        ui_reward.gameObject.SetActive(true);

        ui_rewardimage.sprite = reward.Icon;

        ui_reward.anchoredPosition = reward_result_startposition;
        ui_reward.localScale = reward_result_startscale;

        Sequence sequence = DOTween.Sequence();

        sequence.Append(ui_reward.DOScale(1.2f, 0.2f).SetEase(Ease.OutBack));

        sequence.AppendInterval(0.7f);
        sequence.Append(ui_reward.DOMove(ui_flytarget.position, 0.6f).SetEase(Ease.InOutQuad));

        sequence.Join(ui_reward.DOScale(0.2f, 0.6f));

        sequence.OnComplete(() =>
        {
            AddRewardCollectedPanel(reward, collectedCount);

           

            ui_reward.gameObject.SetActive(false);
            ui_reward.anchoredPosition = reward_result_startposition;
            ui_reward.localScale = reward_result_startscale;

            wheel_button.interactable = true;
            leave_button.interactable = true;

            OnRewardAnimationCompleted?.Invoke();




        });
    }

    private void AddRewardCollectedPanel(Reward reward, int collectedCount)
    {
        if (collected_reward_views.TryGetValue(reward, out CollectedReward existingItem))
        {
            existingItem.AddCount();
            return;
        }


        CollectedReward newItem = Instantiate(collected_reward_prefab, ui_content);
        newItem.SetReward(reward);
        collected_reward_views.Add(reward, newItem);
    }

    public void DeathAnimation(Reward reward)
    {
        ui_reward.DOKill();

        ui_reward.gameObject.SetActive(true);

        ui_rewardimage.sprite = reward.Icon;


        ui_reward.anchoredPosition = reward_result_startposition;
        ui_reward.localScale = reward_result_startscale;

        Sequence sequence = DOTween.Sequence();

        sequence.Append(ui_reward.DOScale(1.4f, 0.25f).SetEase(Ease.OutBack));
        sequence.AppendInterval(1f);

        sequence.OnComplete(() =>
        {
            ui_reward.gameObject.SetActive(false);
            ui_reward.anchoredPosition = reward_result_startposition;
            ui_reward.localScale = reward_result_startscale;
            ShowGameOverPanel(reward);
        });





    }

    public void ClearCollectedRewards()
    {
        foreach (Transform child in ui_content)
        {
            Destroy(child.gameObject);
        }
        collected_reward_views.Clear();

    }
    public void SetZoneInfo(int zoneNumber, WheelType wheelType)
    {
        ui_zone_value.text = zoneNumber.ToString();
        switch (wheelType)
        {
            case WheelType.Bronze:
                ui_zone_type_value.text = "NORMAL ZONE";
                break;
            case WheelType.Silver:
                ui_zone_type_value.text = "SAFE ZONE";
                break;
            case WheelType.Gold:
                ui_zone_type_value.text = "SUPER ZONE";
                break;
        }
    }

    public void SetWheelBaseImage(WheelType wheelType)
    {
        switch (wheelType)
        {
            case WheelType.Bronze:
                ui_wheel_base_image.sprite = wheel_base_bronze_sprite;
                ui_wheel_indicator.sprite = wheel_indicator_bronze_sprite;
                break;
            case WheelType.Silver:
                ui_wheel_base_image.sprite = wheel_base_silver_sprite;
                ui_wheel_indicator.sprite = wheel_indicator_silver_sprite;
                break;
            case WheelType.Gold:
                ui_wheel_base_image.sprite = wheel_base_gold_sprite;
                ui_wheel_indicator.sprite = wheel_indicator_gold_sprite;
                break;
        }

    }

    public void SetZone(int currentZone)
    {
        zone_bar.SetZone(currentZone);




    }
}

