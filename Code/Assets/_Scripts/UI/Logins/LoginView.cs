using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginView : MonoBehaviour
{
    [SerializeField] private TMP_InputField usernameField;
    [SerializeField] private TMP_InputField passwordField;

    [SerializeField] private Button loginButton;

    private static string PPUsernameKey = "saved_username";
    private static string PPPasswordKey = "saved_password";

    private void Start()
    {
        loginButton.GetComponentInChildren<TMP_Text>().text = LanguageTranslation.GetText(
            LanguageTranslation.TextType.Login, LanguageTranslation.ReturnTextOption.Sentence_case);
        loginButton.onClick.AddListener(TryLogin);

        if (PlayerPrefs.HasKey(PPUsernameKey))
        {
            usernameField.text = PlayerPrefs.GetString(PPUsernameKey);
            passwordField.text = PlayerPrefs.GetString(PPPasswordKey);
        }
    }

    private void TryLogin()
    {
        if (CheckFormFulfillment() == false)
        {
            NotificationData fieldsNotFilled = new(NotificationType.Warning, GetFieldsNotFilledText());
            NotificationManager.Instance.EnqueueNotification(fieldsNotFilled);
            return;
        }

        BackendCommunicator.Instance.RequestLogin(usernameField.text, passwordField.text,
            (success, token) =>
            {
                if (success)
                {
                    PlayerPrefs.SetString(PPUsernameKey, usernameField.text);

                    // Bug: Remove this in final build.
                    PlayerPrefs.SetString(PPPasswordKey, passwordField.text);

                    SceneManager.LoadSceneAsync("Main");
                    AccountManager.Instance.SaveLoginCredentials(token);

                    BackendCommunicator.Instance.StaffDatabaseCommunicator.GetEmployeeByUsername(
                        usernameField.text, (isSucceeded, data) =>
                        {
                            if (isSucceeded) AccountManager.Instance.AccountID = data.ID;
                        });
                }
                else
                {
                    NotificationData loginFailed = new(NotificationType.Warning, GetLoginFailedText());
                    NotificationManager.Instance.EnqueueNotification(loginFailed);
                }
            });
    }

    private bool CheckFormFulfillment()
    {
        return usernameField.text != String.Empty && passwordField.text != String.Empty;
    }

    private string GetFieldsNotFilledText()
    {
        return LanguageTranslation.GetText(LanguageTranslation.TextType.Login_Unfilled_Fields,
            LanguageTranslation.ReturnTextOption.Sentence_case) + ".";
    }

    private string GetLoginFailedText()
    {
        return LanguageTranslation.GetText(LanguageTranslation.TextType.Login_Failed,
            LanguageTranslation.ReturnTextOption.Sentence_case) + ".";
    }
}