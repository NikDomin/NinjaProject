using Agent.Player;
using Coins;
using DataPersistence;
using DataPersistence.Data;
using Services;
using TMPro;
using UnityEngine;
using UnityEngine.U2D.Animation;
using UnityEngine.UI;

namespace UI.SkinShowcase
{
    public class SkinShowcase : MonoBehaviour, IDataPersistence
    {
        // [Utils.ReadOnly, SerializeField] private string id = System.Guid.NewGuid().ToString();
        [SerializeField] private int id;
        [SerializeField] private bool isPurchased;
        [SerializeField] private bool isEquipped;
        [SerializeField] private int price;
        [SerializeField] private SpriteLibrary spriteLibrary;
        [SerializeField] private CoinsHandler coinsHandler;
        [SerializeField] private SkinShowcaseActionHandler skinShowcaseActionHandler;
        [SerializeField] private TextMeshProUGUI priceText;
        
        [Header("Buttons")] 
        [SerializeField] private Button BuyButton;
        [SerializeField] private Button EquipButton;

        private Shop skinShop;
        private SpriteLibraryAsset spriteLibrarySpriteLibraryAsset;
        private int EquipedSpriteLibraryID = -1;


        #region MONO

        private void Awake()
        {
            skinShop = new Shop(coinsHandler);
        }

        private void OnEnable()
        {
            BuyButton.onClick.AddListener(TryBuy);
            EquipButton.onClick.AddListener(EquipSkin);
            skinShop.OnSuccessPurchase += SuccessPurchase;
            skinShowcaseActionHandler.OnEquipSkin += PlayerEquipSomeSkin;
        }
        private void OnDisable()
        {
            BuyButton.onClick.RemoveListener(TryBuy);
            EquipButton.onClick.RemoveListener(EquipSkin);
            skinShop.OnSuccessPurchase -= SuccessPurchase;
            skinShowcaseActionHandler.OnEquipSkin -= PlayerEquipSomeSkin;
        }

        private void Start()
        {
            var spriteDictionary = SpriteLibraryHandler.LibraryHandler.SpriteLibraryDictionary;
            spriteDictionary.TryGetValue(id, out spriteLibrarySpriteLibraryAsset);
            spriteLibrary.spriteLibraryAsset = spriteLibrarySpriteLibraryAsset;
                
            if (isPurchased)
            {
                BuyButton.gameObject.SetActive(false);
                EquipButton.gameObject.SetActive(true);
            }
            else
            {
                BuyButton.gameObject.SetActive(true);
                EquipButton.gameObject.SetActive(false);
            }
        }

        private void OnValidate()
        {
            priceText.text = price.ToString();
        }

        #endregion

        #region SaveSystem

        public void LoadData(GameData data)
        {
            Debug.Log("Load Data SkinShowcase");
            data.PurchasedSkins.TryGetValue(id, out isPurchased);
            if (isPurchased)
            {
                EquipButton.gameObject.SetActive(true);
            }
            else 
            {
                BuyButton.gameObject.SetActive(true);
            }
        }

        public void SaveData(GameData data)
        {
            Debug.Log("SaveDataSkinShowcase");
            if (data.PurchasedSkins.ContainsKey(id))
            {
                data.PurchasedSkins.Remove(id);
            }
            data.PurchasedSkins.Add(id, isPurchased);

            if (EquipedSpriteLibraryID != -1)
                data.HeroSpriteLibraryID = EquipedSpriteLibraryID;
            
        }

        #endregion

        private void TryBuy()
        {
            skinShop.TryBuy(price);
        }

        private void SuccessPurchase()
        {
            skinShowcaseActionHandler.PurchasedSkinsCount++;
            if(skinShowcaseActionHandler.PurchasedSkinsCount >= 15)
                Achievement.Instance.NinjaModel();
            
            isPurchased = true;
            DataPersistenceManager.instance.SaveGame();
            BuyButton.gameObject.SetActive(false);
            EquipButton.gameObject.SetActive(true);
        }
        
        private void EquipSkin()
        {
            skinShowcaseActionHandler.InvokeOnEquipSkin(id);
            
            if(isEquipped)
                return;
            isEquipped = true;

            EquipButton.interactable = false;

            Image image = EquipButton.GetComponent<Image>();
            var imageColor = image.color;
            imageColor.a = 0.5f;
            image.color = imageColor;
                
            //need to set interactable to other button
            EquipedSpriteLibraryID = id;
            DataPersistenceManager.instance.SaveGame();
            EquipedSpriteLibraryID = -1;
        }

        private void PlayerEquipSomeSkin(int id)
        {
            if (this.id != id)
            {
                isEquipped = false;
                EquipButton.interactable = true;
                
                Image image = EquipButton.GetComponent<Image>();
                var imageColor = image.color;
                imageColor.a = 1f;
                image.color = imageColor;
            }
        }
        
        // [ContextMenu("Generate guid for id")]
        // private void GenerateID()
        // {
        //     id = System.Guid.NewGuid().ToString();
        // }

    }
}