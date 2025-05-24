using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public GameObject shopPanel;
    public TextMeshProUGUI coinText;
    public int playerCoins = 1000;

    [System.Serializable]
    public class ShopItem
    {
        public string itemName;
        public int cost;
        public AudioClip musicClip;
        public Button buyButton;
        public TextMeshProUGUI buttonText;
    }

    public ShopItem[] shopItems;
    public AudioSource audioSource;

    void Start()
    {
        UpdateCoinText();

        foreach (var item in shopItems)
        {
            item.buyButton.onClick.AddListener(() => BuyItem(item));
        }
    }

    public void OpenShop()
    {
        shopPanel.SetActive(true);
    }

    public void CloseShop()
    {
        shopPanel.SetActive(false);
    }

    void BuyItem(ShopItem item)
    {
        if (playerCoins >= item.cost)
        {
            playerCoins -= item.cost;
            UpdateCoinText();
            Debug.Log("Purchased: " + item.itemName);

            if (item.musicClip != null)
            {
                audioSource.clip = item.musicClip;
                audioSource.Play();
            }

            item.buyButton.interactable = false;
            item.buttonText.text = "SOLD";
        }
        else
        {
            Debug.Log("Yetersiz bakiye!");
        }
    }

    void UpdateCoinText()
    {
        coinText.text = "Coins: " + playerCoins.ToString();
    }
}
