using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using TMPro;

public class RegisterUI : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public TextMeshProUGUI resultText;
    public Button closeButton;
    public Button registerButton;
    public LoginUI loginUI;

    private void Awake()
    {
        closeButton.onClick.AddListener(BackToLogin);
        registerButton.onClick.AddListener(async () => await OnRegisterClick());
    }

    private void BackToLogin()
    {
        this.gameObject.SetActive(false);
        loginUI.Init();
    }

    private async Task OnRegisterClick()
    {
        string result = await LoginManager.Register(usernameInput.text, passwordInput.text);
        resultText.text = result;
        Debug.Log("result == " + result);
    }

    public void Init()
    {
        usernameInput.text = "";
        passwordInput.text = "";
        resultText.text = "";
    }
}
