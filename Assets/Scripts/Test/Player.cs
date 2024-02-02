using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : chract
{
    private Camera CameraMain;
    [SerializeField] private LayerMask CanMove;
    [SerializeField] private Interacted focus;
    [SerializeField] private Slider sliderHealth;
    [SerializeField] private Image HealPotion;
    [SerializeField] private int coinsCount;
    [SerializeField] private Text coinsText;
    [SerializeField] private Button HealButton;
    [SerializeField] private GameObject SpawnPosition;
    [SerializeField] private GameObject DialogScreen;
    [SerializeField] private GameObject HealEffect;
    [SerializeField] private Image NoDemonHead;
    [SerializeField] private Sprite DemonHead;
    [SerializeField] private Image NoCrystalArtifact;
    [SerializeField] private Sprite CrystalArtifact;
    [SerializeField] private Sprite CommonBackground;
    [SerializeField] private GameObject DieScreen;
    private bool die = false;
    private bool isHeal = false;

    public static Player instance;

    private void Awake()
    {
        instance = this;
        CameraMain = Camera.main;
        coinsCount = PlayerPrefs.GetInt("Coins");
        transform.position = new Vector3(SpawnPosition.transform.position.x, SpawnPosition.transform.position.y, SpawnPosition.transform.position.z);
        coinsText.text = coinsCount.ToString();
        if (PlayerPrefs.HasKey("DemonHead"))
            AddDemonHeadToStatusBar();
        if (PlayerPrefs.HasKey("Crystal"))
            AddArtifact();
    }


    protected override void Update()
    {
        if (!die)
        {
            base.Update();
            sliderHealth.value = currentHealth;
            sliderHealth.maxValue = maxHealth;
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = CameraMain.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100, CanMove))
                {
                    motor.MoveToPoint(hit.point);
                    DeleteFocus();
                }

            }

            else if (Input.GetMouseButtonDown(1))
            {
                Ray ray = CameraMain.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100))
                {
                    var interactable = hit.collider.GetComponent<Interacted>();
                    if (interactable != null)
                        SetFocus(interactable);

                }

            }
        }

    }

    public void SetFocus(Interacted Newfocus)
    {
        if (Newfocus != focus)
        {
            if (focus != null)
                focus.OnDeFocus();
        }
        focus = Newfocus;
        motor.FollowToObject(Newfocus);
        Newfocus.OnFocus(gameObject);
    }

    public void DeleteFocus()
    {
        if (focus != null)
            focus.OnDeFocus();
        focus = null;
        motor.StopFollowing();
    }



    public void Healing()
    {
        if (currentHealth >= 100)
            print("Максимальное здоровье");
        else
        {
            if ((currentHealth += 10) >= 100 && !isHeal)
                currentHealth = maxHealth;
            else if(!isHeal)
            {
                currentHealth += 20f;
                StartCoroutine(EffectHeal());
            }
        }
    }

    private IEnumerator EffectHeal()
    {
        HealPotion.color = new Color32(100,100, 100, 100);
        HealButton.interactable = false;
        isHeal = true;
        HealEffect.SetActive(true);
        yield return new WaitForSeconds(3f);
        HealEffect.SetActive(false);
        yield return new WaitForSeconds(2f);
        isHeal = false;
        HealButton.interactable = true;
        HealPotion.color = new Color32(255,255,255,255);
    }

    public void DialogOnPlayer()
    {
        quest.ChooseQuest(PlayerPrefs.GetInt("NumberQuest"));
        DialogScreen.SetActive(true);
    }

    protected override void Die()
    {
        Player.instance.enabled = false;
        DieEffect.SetActive(true);
        die = true;
        DeleteFocus();
        motor.Die();
        OnDie();
        DieScreen.SetActive(true);
    }

    public void AddCoins(int coins)
    {
        coinsCount += coins;
        PlayerPrefs.SetInt("Coins", coinsCount);
        coinsText.text = coinsCount.ToString();
    }

    public void AddDemonHeadToStatusBar()
    {
        NoDemonHead.sprite = DemonHead;
    }

    public void DeleteDemonHeadFromStatusBar()
    {
        NoDemonHead.sprite = CommonBackground;
    }

    public void AddArtifact()
    {
        NoCrystalArtifact.sprite = CrystalArtifact;
    }

   
}

