using UnityEngine;
using TMPro;

class UI : MonoBehaviour
{
    public static UI Instance;
    public Player Player
    {
        get => _player;
        set
        {
            _player = value;
            playerHealth = _player.health;
            _player.OnHealthChangedUI.AddListener(ChangeHealthDisplay);
            _player.OnItemChanged.AddListener(ChangeItemHeld);
        }
    }

    private int playerHealth;

    private Player _player;

    [SerializeField] TextMeshProUGUI healthText = default;
    [SerializeField] TextMeshProUGUI itemText = default;
    [SerializeField] TextMeshProUGUI acornText = default;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        healthText.text = $"Health: {Player.health}";
        ChangeItemHeld(null);
    }

    private void ChangeHealthDisplay(int playerHealth)
    {
        healthText.text = $"Health: {playerHealth}";
        Debug.Log($"New Player Health: {playerHealth} (Love UI)");
    }

    private void ChangeItemHeld(string itemName)
    {
        itemName = itemName ?? "None";
        itemText.text = $"Current Item: {itemName}";
    }
}


