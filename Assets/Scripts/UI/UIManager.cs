using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using static System.String;
using Button = UnityEngine.UI.Button;
using Object = UnityEngine.Object;

[RequireComponent(typeof(TextMeshProUGUI))]
[RequireComponent(typeof(EventTrigger))]
public class UIManager : MonoBehaviour
{
    // private TouchControls _touchControls;
    // private BoxCollider2D _collider;
    // private InputAction leftClick;
    // private InputAction tap;
    private GameControls _controls;
    private GameObject _sidemenu;
    private GameObject _upgradePanel;
    private GameObject _buyPanel;
    private GameObject _canvas;
    private GameObject _behaviorSelectorObj;
    
    //todo maybe move it somewhere else?
    private const string PistolTowerUpgradeText = 
        "Вышка, {0} + {1} уровень\n" +
        "Урон: {2} + {3:F1}\n" +
        "Скорострельность: {4}с - {5:F1}с\n" +
        "Дальность: {6} + {7:F1}\n" +
        "Пробитие: {8} + {9:F1}";
    private const string ElectricTowerUpgradeText =
        "Тесла-пушка, {0} + {1} уровень\n" +
        "Урон (за тик):{2} + {3:F1}\n" +
        "Дальность: {4} + {5:F1}\n" +
        "Замедление: {6:P1} + {7:P1}";

    private TextMeshProUGUI _coins;
    private TextMeshProUGUI _health;
    private MouseDetector _mouseDetector;
    private AudioClip _buildSound;
    private AudioClip _upgradeSound;

    [Header("Towers")]
    [SerializeField]
    private GameObject towerPistolPrefab;
    [SerializeField]
    private GameObject towerTeslaPrefab;
    [SerializeField]
    private GameObject towerPlasmaPrefab;
    //public bool isMouseOverUI = false;

    private EventTrigger _eventTrigger;
    private AudioSource _audioPlayer;
    private WaveController _waveController;
    private Button _skipWaveButton;
    private TextMeshProUGUI _waveText;
    private TextMeshProUGUI _timeRemainsText;
    private TextMeshProUGUI _skipRewardText;
    
    private TMP_Dropdown _behaviorSelector;
    private GameObject _winWindow;
    private Button _winBackTomenuBtn;
    private Button _loseBackToMenuBtn;
    private TextMeshProUGUI _winStatText;
    private TextMeshProUGUI _loseStatText;
    private Button _loseRetryBtn;
    private GameObject _loseWindow;
    private GameObject _accessBlocker;

    private const string _statFormat = 
        "Заработано денег: {0}\n" +
        "Из них потрачено на улучшения: {1}\n" +
        "Потрачено жизней: {2}\n" + 
        "Башен построено: {3}\n" +
        "Врагов убито: {4}";


    void Awake()
    {
        // _eventTrigger = GetComponent<EventTrigger>();
        // if (_eventTrigger is not null)
        // {
        //     EventTrigger.Entry enterUIEntry = new EventTrigger.Entry();
        //
        //     enterUIEntry.eventID = EventTriggerType.PointerEnter;
        //     enterUIEntry.callback.AddListener(ev => EnterUI());
        //     _eventTrigger.triggers.Add(enterUIEntry);
        //
        //     EventTrigger.Entry exitUIEntry = new EventTrigger.Entry();
        //     exitUIEntry.eventID = EventTriggerType.PointerExit;
        //     exitUIEntry.callback.AddListener(ev => ExitUI());
        //     _eventTrigger.triggers.Add(exitUIEntry);
        //
        //     
        // }
        _buildSound = Resources.Load("Audio/Build") as AudioClip;
        _upgradeSound = Resources.Load("Audio/Powerup6") as AudioClip;
        _canvas = GameObject.Find("Canvas");
        _sidemenu = GameObject.Find("sidemenu");
        _behaviorSelectorObj = GameObject.Find("BehaviorSelector");
        _behaviorSelector = _behaviorSelectorObj.GetComponent<TMP_Dropdown>();
        _winWindow = GameObject.Find("WinWindow");
        _loseWindow = GameObject.Find("LoseWindow");
        _accessBlocker = GameObject.Find("AccessBlocker");
        
        
        _winStatText = GameObject.Find("WinStatText").GetComponent<TextMeshProUGUI>();
        _loseStatText = GameObject.Find("LoseStatText").GetComponent<TextMeshProUGUI>();
        _waveText = GameObject.Find("WaveCounter").GetComponent<TextMeshProUGUI>();
        _timeRemainsText = GameObject.Find("TimeRemainsText").GetComponent<TextMeshProUGUI>();
        _skipRewardText = GameObject.Find("SkipRewardText").GetComponent<TextMeshProUGUI>();
        _winStatText = GameObject.Find("WinStatText").GetComponent<TextMeshProUGUI>();

        _coins = GameObject.Find("coins-txt").GetComponent<TextMeshProUGUI>();
        _health = GameObject.Find("health-txt").GetComponent<TextMeshProUGUI>();
        
        _mouseDetector = GameObject.Find("vovo").GetComponent<MouseDetector>();
        _controls = new GameControls();


        _skipWaveButton = GameObject.Find("SkipButton").GetComponent< UnityEngine.UI.Button >();
        _winBackTomenuBtn = GameObject.Find("WinBackToMenuBtn").GetComponent<Button>();
        _loseBackToMenuBtn = GameObject.Find("LoseBackToMenuBtn").GetComponent<Button>();
        _loseRetryBtn = GameObject.Find("LoseRetryBtn").GetComponent<Button>();
        _waveController = GameObject.Find("WaveController").GetComponent<WaveController>();
        
        
        
        _buyPanel = GameObject.Find("BuyPanel");
        _upgradePanel = GameObject.Find("UpgradePanel");
        _audioPlayer = GetComponent<AudioSource>();
        
        _winWindow.SetActive(false);
        _loseWindow.SetActive(false);
        _accessBlocker.SetActive(false);

    }

    void Start()
    {
        // leftClick = new InputAction(binding: "<mouse>/leftbutton");
        // leftClick.performed += ctx => OnClick(ctx);
        // leftClick.Enable();
        
        
        

        _controls.Touchscreen.Tap.performed += OnTap;
        _controls.Mouse.Click.performed += OnClick;
        _controls.Keyboard.EscapeKey.performed += (InputAction.CallbackContext ctx) =>
            StartCoroutine(BackToMenu());
        
        
        _controls.Keyboard.Enable();
        _controls.Touchscreen.Tap.Enable();
        _controls.Mouse.Click.Enable();

        
        _waveText.SetText(_waveController.WaveCounter.ToString());
        _skipWaveButton.onClick.AddListener(SkipWave);
        _winBackTomenuBtn.onClick.AddListener(() => StartCoroutine(BackToMenu()) );
        _loseBackToMenuBtn.onClick.AddListener(() => StartCoroutine(BackToMenu()) );
        _loseRetryBtn.onClick.AddListener(() => StartCoroutine(Retry()) );
        
        GameEvents.current.OnWin += Win;
        GameEvents.current.OnLose += Lose;
        
        _sidemenu.SetActive(false);

        // tap = new InputAction(binding: "<Touchscreen>/Press", type: InputActionType.PassThrough, expectedControlType: "Touch");
        // tap.performed += ctx => OnClick(ctx);
        // tap.Enable();
    }

    public void HideSide()
    {
        _sidemenu.SetActive(false);
    }

    private IEnumerator Retry()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(currentScene.path, LoadSceneMode.Single);
        
        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    private void Lose()
    {
        _loseWindow.SetActive(true);
        _accessBlocker.SetActive(true);
        
        _loseStatText.SetText(Format(_statFormat, GlobalVars.current.GetStats()));
    }
    
    private IEnumerator BackToMenu()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Scenes/MainMenu", LoadSceneMode.Single);
        
        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    private void Win()
    {
        _accessBlocker.SetActive(true);
        _winWindow.SetActive(true);

        _winStatText.SetText(Format(_statFormat, GlobalVars.current.GetStats()));
    }

    #region controls

    private void SkipWave()
    {
        //skip if not started
        if (_waveController.isStarted)
        {
            _waveController.SkipWave();
            
        }
        else _waveController.StartWaves();

        StartCoroutine(SuspendSkipWaveButton());
    }

    private IEnumerator SuspendSkipWaveButton()
    {
        _skipWaveButton.interactable = false;
        yield return new WaitForSeconds(4);
        _skipWaveButton.interactable = true;
    }
    
    private bool IsPointerOverUIObject() {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    void OnTap(InputAction.CallbackContext ctx)
    {
        if (IsPointerOverUIObject()) {
            return;
        }
        //if (_mouseDetector.mouseOver) return;
        Vector2 rawPos;
        var touch = Touchscreen.current;
        rawPos = touch.primaryTouch.position.ReadValue();

        var pos = Camera.main.ScreenToWorldPoint(rawPos);
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

        

        if (hit.collider is not null)
            OpenUI(hit.collider);
        else {
            _sidemenu.SetActive(false);
            TowerRadiusUI.current.Hide();
        }
    }
    void OnClick(InputAction.CallbackContext ctx)
    {
        if (_mouseDetector.mouseOver) {
            return;
        }
        // var isTouch = ctx.action.expectedControlType.IndexOf("Touch", StringComparison.Ordinal) >= 0;
        Vector2 rawPos;
        var mouse = Mouse.current;
        rawPos = mouse.position.ReadValue();
        var pos = Camera.main.ScreenToWorldPoint(rawPos);
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

        //var e = GameObject.Find("UIEventSystem").GetComponent<EventSystem>();
        EventSystem.current.IsPointerOverGameObject();
        
        if (hit.collider is not null)
            OpenUI(hit.collider);
        else {
            _sidemenu.SetActive(false);
            TowerRadiusUI.current.Hide();
        }
        
    }
    #endregion
    void OpenUI(Collider2D target)
    {
        var obj = target.gameObject;
        var gunSlot = obj.GetComponent<GunSlotScript>() ?? obj.GetComponentInParent<GunSlotScript>();
        if (gunSlot is not null)
        {
            if (!_sidemenu.activeSelf) _sidemenu.SetActive(true);

            OpenBuyMenu(gunSlot);
            return;
        }
        var towerController = obj.GetComponent<TowerController>() ?? obj.GetComponentInParent<TowerController>();
        if (towerController is not null)
        {
            if (!_sidemenu.activeSelf) _sidemenu.SetActive(true);
            
            OpenUpgradeMenu(towerController);
        }
        else
        {
            _sidemenu.SetActive(false);
            TowerRadiusUI.current.Hide();
        }
    }

    #region Upgrade Menu
    void OpenUpgradeMenu(TowerController tc)
    {
        _upgradePanel.SetActive(true);
        _buyPanel.SetActive(false);
        

        _behaviorSelector.onValueChanged.RemoveAllListeners();
        _behaviorSelector.value = (int) tc.attackBehaviour;
        var txtObj = GameObject.Find("menu-type"); 
        var txt = txtObj.GetComponent<TextMeshProUGUI>();
        var upgradeText = GameObject.Find("upg-txt").GetComponent<TextMeshProUGUI>();
        var upgradeBtn = GameObject.Find("upg-btn");
        var upgradeBtnTxt = upgradeBtn.GetComponentInChildren<TextMeshProUGUI>();
        txt.SetText("Upgrade");
        TowerRadiusUI.current.Move(tc.gameObject.transform.position, tc.attackRadius);
        TowerRadiusUI.current.Show();

        var upgradeCost = tc.GetUpgradeCost();
        //todo maybe localization strings?
        upgradeBtnTxt.SetText($"{upgradeCost}$");
        string upgradeTextFormatted = tc.GetUpgradeTextFormat();
        // var penetrationPlus = tc.level > 3 ? tc.level * (tc.level / 100f) : 0;
        
        // upgradeText.SetText(Format(PistolTowerUpgradeText, tc.level, 1,
        //     tc.attackDamage, tc.attackDamage* tc.attackDamageUpgradeScale,
        //     tc.attackFrequency, tc.attackFrequency * tc.attackSpeedUpgradeScale,
        //     tc.attackRadius, tc.attackRadius* tc.attackRadiusUpgradeScale,
        //     tc.penetrationScale, penetrationPlus));
        upgradeText.SetText(upgradeTextFormatted);

        upgradeBtn.GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
        upgradeBtn.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() =>
        {
            if (GlobalVars.current.Money < upgradeCost) return;
            
            // tc.level += 1;
            // tc.attackDamage += Percent(tc.attackDamage, tc.attackDamageUpgradeScale);
            // tc.attackFrequency -= Percent(tc.attackFrequency, tc.attackSpeedUpgradeScale);
            // tc.attackRadius += Percent(tc.attackRadius, tc.attackRadiusUpgradeScale);
            // tc.bulletSpeed += Percent(tc.bulletSpeed, tc.bulletSpeedUpgradeScale);
            // tc.penetrationScale += penetrationPlus;
            tc.Upgrade();

            _audioPlayer.PlayOneShot(_upgradeSound);
                
            GlobalVars.current.DecreaseMoney(upgradeCost);
            GameEvents.current.Upgraded(upgradeCost);    
            
            OpenUpgradeMenu(tc);
        });
        
        _behaviorSelector.onValueChanged.AddListener((arg0) =>
        {
            var selectedBehavior = (TowerController.AttackBehaviour) arg0;
            tc.SetAttackBehavior(selectedBehavior);
        });
    }
    // void OpenUpgradeMenu(PistolTowerController tc)
    // {
    //     _upgradePanel.SetActive(true);
    //     _buyPanel.SetActive(false);
    //     
    //
    //     _behaviorSelector.onValueChanged.RemoveAllListeners();
    //     _behaviorSelector.value = (int) tc.attackBehaviour;
    //     var txtObj = GameObject.Find("menu-type"); 
    //     var txt = txtObj.GetComponent<TextMeshProUGUI>();
    //     var upgradeText = GameObject.Find("upg-txt").GetComponent<TextMeshProUGUI>();
    //     var upgradeBtn = GameObject.Find("upg-btn");
    //     var upgradeBtnTxt = upgradeBtn.GetComponentInChildren<TextMeshProUGUI>();
    //     txt.SetText("Upgrade");
    //     TowerRadiusUI.current.Show();
    //     TowerRadiusUI.current.Move(tc.gameObject.transform.position, tc.attackRadius);
    //
    //     var upgradeCost = tc.GetUpgradeCost();
    //     //todo maybe localization strings?
    //     upgradeBtnTxt.SetText($"{upgradeCost}$");
    //     var penetrationPlus = tc.level > 3 ? tc.level * (tc.level / 100f) : 0;
    //     
    //     upgradeText.SetText(Format(PistolTowerUpgradeText, tc.level, 1,
    //         tc.attackDamage, tc.attackDamage* tc.attackDamageUpgradeScale,
    //         tc.attackFrequency, tc.attackFrequency * tc.attackSpeedUpgradeScale,
    //         tc.attackRadius, tc.attackRadius* tc.attackRadiusUpgradeScale,
    //         tc.penetrationScale, penetrationPlus));
    //
    //     upgradeBtn.GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
    //     upgradeBtn.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() =>
    //     {
    //         if (GlobalVars.current.Money < upgradeCost) return;
    //         
    //         tc.level += 1;
    //         tc.attackDamage += Percent(tc.attackDamage, tc.attackDamageUpgradeScale);
    //         tc.attackFrequency -= Percent(tc.attackFrequency, tc.attackSpeedUpgradeScale);
    //         tc.attackRadius += Percent(tc.attackRadius, tc.attackRadiusUpgradeScale);
    //         tc.bulletSpeed += Percent(tc.bulletSpeed, tc.bulletSpeedUpgradeScale);
    //         tc.penetrationScale += penetrationPlus;
    //
    //         _audioPlayer.PlayOneShot(_upgradeSound);
    //             
    //         GlobalVars.current.DecreaseMoney(upgradeCost);
    //         GameEvents.current.Upgraded(upgradeCost);    
    //         
    //         OpenUpgradeMenu(tc);
    //     });
    //     
    //     _behaviorSelector.onValueChanged.AddListener((int arg0) =>
    //     {
    //         var selectedBehavior = (TowerController.AttackBehaviour) arg0;
    //         tc.SetAttackBehavior(selectedBehavior);
    //     });
    // }
    
    // void OpenUpgradeMenu(TeslaTowerController tc)
    // {
    //     _upgradePanel.SetActive(true);
    //     _buyPanel.SetActive(false);
    //     
    //     
    //     _behaviorSelector.onValueChanged.RemoveAllListeners();
    //     _behaviorSelector.value = (int) tc.attackBehaviour;
    //     var txtObj = GameObject.Find("menu-type"); 
    //     var txt = txtObj.GetComponent<TextMeshProUGUI>();
    //     var upgradeText = GameObject.Find("upg-txt").GetComponent<TextMeshProUGUI>();
    //     var upgradeBtn = GameObject.Find("upg-btn");
    //     var upgradeBtnTxt = upgradeBtn.GetComponentInChildren<TextMeshProUGUI>();
    //     txt.SetText("Upgrade");
    //     TowerRadiusUI.current.Show();
    //     TowerRadiusUI.current.Move(tc.gameObject.transform.position, tc.attackRadius);
    //
    //     var upgradeCost = tc.GetUpgradeCost();
    //     //todo maybe localization strings?
    //     upgradeBtnTxt.SetText($"{upgradeCost}$");
    //     
    //     upgradeText.SetText(Format(ElectricTowerUpgradeText,
    //         tc.level, 1, // plus one level
    //         tc.attackDamage, tc.attackDamage * tc.attackDamageUpgradeScale,
    //         tc.attackRadius, tc.attackRadius*tc.attackRadiusUpgradeScale,
    //         tc.electricFreezeScale, tc.electricFreezeScale + tc.electricFreezeUpgradeDifference));
    //
    //     upgradeBtn.GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
    //     upgradeBtn.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() =>
    //     {
    //         if (GlobalVars.current.Money < upgradeCost) return;
    //         
    //         tc.level += 1;
    //         tc.attackDamage += Percent(tc.attackDamage, tc.attackDamageUpgradeScale);
    //         tc.attackRadius += Percent(tc.attackRadius, tc.attackRadiusUpgradeScale);
    //         tc.electricFreezeScale += tc.electricFreezeScale + tc.electricFreezeUpgradeDifference;
    //
    //         _audioPlayer.PlayOneShot(_upgradeSound);
    //             
    //         GlobalVars.current.DecreaseMoney(upgradeCost);
    //         GameEvents.current.Upgraded(upgradeCost);
    //             
    //         OpenUpgradeMenu(tc);
    //     });
    //     
    //     _behaviorSelector.onValueChanged.AddListener((int arg0) =>
    //     {
    //         var selectedBehavior = (TowerController.AttackBehaviour) arg0;
    //         tc.SetAttackBehavior(selectedBehavior);
    //     });
    // }
    

    private static float Percent(float initial, float percent)
    {
        return (float)Math.Round(initial * percent, 2);
    }
    
    #endregion
    
    private static int logCalc(int wave, int cost)
    {
        return (int)Math.Round(cost * (float)Math.Log(wave, 1.8f) + cost);
    }
    void OpenBuyMenu(GunSlotScript gunslot)
    {
        _upgradePanel.SetActive(false);
        _buyPanel.SetActive(true);
        
        var txtObj = GameObject.Find("menu-type"); 
        var txt = txtObj.GetComponent<TextMeshProUGUI>();
        txt.SetText("Buy");
        
        #region buy pistol button
        {
            var buyPistolButton = GameObject.Find("BuyPistolGunBtn").GetComponent<UnityEngine.UI.Button>();
            buyPistolButton.onClick.RemoveAllListeners();
            buyPistolButton.onClick.AddListener(() => BuyTower(gunslot, towerPistolPrefab));
        }
        #endregion
        
        #region buy tesla button

        {
            var buyTeslaButton = GameObject.Find("BuyTeslaGunBtn").GetComponent<UnityEngine.UI.Button>();
            buyTeslaButton.onClick.RemoveAllListeners();
            buyTeslaButton.onClick.AddListener(() => BuyTower(gunslot, towerTeslaPrefab));
        }

        #endregion
        
        #region buy plasma button
        
        {
            var buyPlasmaButton = GameObject.Find("BuyPlasmaGunBtn").GetComponent<UnityEngine.UI.Button>();
            buyPlasmaButton.onClick.RemoveAllListeners();
            buyPlasmaButton.onClick.AddListener(() => BuyTower(gunslot, towerPlasmaPrefab));
        }
        
        #endregion

    }

    // Update is called once per frame

    private void BuyTower(GunSlotScript gunslot,GameObject towerPrefab)
    {
        var tc = towerPrefab.GetComponent<TowerController>();
        if (tc is null) return;
        var cost = tc.towerCost;
        if (GlobalVars.current.Money < cost) return;
                
        var pos = gunslot.gameObject.GetComponentsInChildren<Transform>()[1];
        var gun = Instantiate(towerPrefab, pos);
        //delete gunSlot to prevent opening buy menu again
        Destroy(gunslot);
        _audioPlayer.PlayOneShot(_buildSound);

        GlobalVars.current.DecreaseMoney(cost);
        GameEvents.current.TowerBuilt();
    }

    private void Update()
    {
        if (_coins.text != GlobalVars.current.Money.ToString())
        {
            _coins.SetText(GlobalVars.current.Money.ToString());
        }
        if (_health.text != GlobalVars.current.Health.ToString())
        {
            _health.SetText(GlobalVars.current.Health.ToString());
        }

        if (_waveText.text != (_waveController.WaveCounter+1).ToString())
        {
            _waveText.SetText((_waveController.WaveCounter+1).ToString());
        }

        _timeRemainsText.SetText( _waveController.timeRemains.ToString());
        _skipRewardText.SetText( _waveController.GetSkipReward().ToString());
        
    }
}
