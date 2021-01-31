using UnityEngine;

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
            _player.OnHealthChange.AddListener(ChangeHealthDisplay);
        }
    }

    private int playerHealth;

    private Player _player;

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
    }

    private void ChangeHealthDisplay()
    {
        playerHealth = _player.health;
        Debug.Log($"New Player Health: {playerHealth} (Love UI)");
    }
}


