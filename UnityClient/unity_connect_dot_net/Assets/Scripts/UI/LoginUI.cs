using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using TMPro;

public class LoginUI : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public TextMeshProUGUI resultText;
    public Button loginButton;
    public Button registerButton;
    public RegisterUI registerUI;

    private void Awake()
    {
        loginButton.onClick.AddListener(async () => await OnLoginClick());
        registerButton.onClick.AddListener(OpenRegisterPanel);
        Init();
    }

    private async Task OnLoginClick()
    {
        string result = await LoginManager.Login(usernameInput.text, passwordInput.text);
        resultText.text = result;
        Debug.Log("result == " + result);
    }

    private void OpenRegisterPanel()
    {
        registerUI.gameObject.SetActive(true);
        registerUI.Init();
    }

    public void Init()
    {
        usernameInput.text = "";
        passwordInput.text = "";
        resultText.text = "";
    }
}
