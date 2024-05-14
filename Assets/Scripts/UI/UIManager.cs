using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class UIManager : MonoBehaviour
{
    private static UIManager singleton;
    public static UIManager Singleton
    {

        get { return singleton; }
        private set
        {
            if (singleton == null)
            {
                singleton = value;
            }
            else if (singleton != value)
            {
                Destroy(value);
            }
        }
    }

    [Header("Connect")]
    [SerializeField] private TMP_InputField ipField;
    [SerializeField] private Button connectButton;
    [SerializeField] private TextMeshProUGUI connectText;
    private void Awake()
    {
        Singleton = this;
    }

    public void ConnectClicked()
    {
        if (ipField.text != "")
        {
            ipField.interactable = false;
            connectButton.gameObject.SetActive(false);

            NetworkManager.Instance.Connect(ipField.text);
        }
    }

    public void ConnectFailed()
    {
        ipField.interactable = true;
        connectButton.gameObject.SetActive(true);

        connectText.gameObject.SetActive(true);
        connectText.color = Color.red;
        connectText.text = "No hay conexión establecida";
    }
}
