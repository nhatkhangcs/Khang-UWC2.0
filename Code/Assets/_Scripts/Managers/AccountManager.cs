using System;
using UnityEngine;

public class AccountManager : PersistentSingleton<AccountManager>
{
    [HideInInspector] public string AccountID;

    private LoginToken loginToken;

    [SerializeField] private string token;
    
    public void SaveLoginCredentials(LoginToken loginToken)
    {
        this.loginToken = loginToken;
        token = loginToken.AccessToken;
    }

    public string GetAccessToken()
    {
        if (token != String.Empty) return token;
        return loginToken.AccessToken;
    }
}