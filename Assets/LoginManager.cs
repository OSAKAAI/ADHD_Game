using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LoginManager : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public TextMeshProUGUI errorText;

    public void Login()
    {
        string username = usernameInput.text.Trim();
        string password = passwordInput.text.Trim();

        if (username == "" || password == "")
        {
            errorText.text = "Please enter username and password!";
            return;
        }

        // Demo login
        if (username == "admin" && password == "1234")
        {
            errorText.text = "";
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            errorText.text = "Invalid Username or Password!";
        }
    }
}