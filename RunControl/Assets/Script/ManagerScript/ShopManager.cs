using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyLibrary;
using TMPro;
using UnityEngine.Purchasing;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour,IStoreListener
{
    private static IStoreController m_StoreController;
    private static IExtensionProvider m_ExtensionProvider;
    private static string Point_250 = "";
    private static string Point_500 = "";
    private static string Point_750 = "";
    private static string Point_1000 = "";
    [Header("Language Operation")]
    [SerializeField] TextMeshProUGUI[] AllText;

    DataManager dataManager = new DataManager();
    MemoryManager memory = new MemoryManager();
    List<LanguageSet> languageMainData = new List<LanguageSet>();
    List<LanguageSet> languageText = new List<LanguageSet>();
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Application.persistentDataPath);
        StartOperation();
        SetLanguage(memory.Get_String("Language"));
        if (m_StoreController == null)
            initializePurchasing();
    }
    public void initializePurchasing()
    {
        if (IsInitalized())
        {
            return;
        }
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        builder.AddProduct(Point_250, ProductType.Consumable);
        builder.AddProduct(Point_500, ProductType.Consumable);
        builder.AddProduct(Point_750, ProductType.Consumable);
        builder.AddProduct(Point_1000, ProductType.Consumable);
        UnityPurchasing.Initialize(this, builder);
    }
    public void BuyProduct(string Product)
    {
        switch (Product)
        {
            case"250":
                BuyProductID(Point_250);       
                break;
            case "500":
                BuyProductID(Point_500);
                break;
            case "750":
                BuyProductID(Point_750);
                break;
            case "1000":
                BuyProductID(Point_1000);
                break;
        }
       
    }
    void BuyProductID(string productId)
    {
        if (IsInitalized())
        {
            Product product = m_StoreController.products.WithID(productId);
            if (product !=null&& product.availableToPurchase)
            {
                m_StoreController.InitiatePurchase(product);
            }
            else
            {
                Debug.Log("Satýn alýrken hata oluþtu");
            }
        }
        else
        {
            Debug.Log("ürün çaðýrýlmýyor");
        }
    }
    private bool IsInitalized()
    {
        return m_StoreController != null && m_ExtensionProvider != null;
    }
    public void OnInitializeFailed(InitializationFailureReason error)
    {
        throw new System.NotImplementedException();
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        if (string.Equals(purchaseEvent.purchasedProduct.definition.id,Point_250,System.StringComparison.Ordinal))
        {
            memory.Save_int("TotalPoint",memory.Get_int("TotalPoint") + 250);
        }
        else if (string.Equals(purchaseEvent.purchasedProduct.definition.id, Point_500, System.StringComparison.Ordinal))
        {
            memory.Save_int("TotalPoint", memory.Get_int("TotalPoint") + 500);
        }
        else if (string.Equals(purchaseEvent.purchasedProduct.definition.id, Point_750, System.StringComparison.Ordinal))
        {
            memory.Save_int("TotalPoint", memory.Get_int("TotalPoint") + 750);
        }
        else if (string.Equals(purchaseEvent.purchasedProduct.definition.id, Point_1000, System.StringComparison.Ordinal))
        {
            memory.Save_int("TotalPoint", memory.Get_int("TotalPoint") +1000);
        }
        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        throw new System.NotImplementedException();
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        m_ExtensionProvider = extensions;
        m_StoreController = controller;
    }
    void SetLanguage(string Value)
    {
        if (Value == "TR")
        {
            for (int i = 0; i < AllText.Length; i++)
            {
                AllText[i].text = languageText[0].Language_TR[i].Text;
            }
        }
        else if (Value == "EN")
        {
            for (int i = 0; i < AllText.Length; i++)
            {
                AllText[i].text = languageText[0].Language_EN[i].Text;
            }
        }
    }
    void StartOperation()
    {
        languageMainData = dataManager.LoadLanguageList();
        languageText.Add(languageMainData[4]);
    }
    public void BackMenu()
    {
        SceneManager.LoadScene(0);
    }
}
