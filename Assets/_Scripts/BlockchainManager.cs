using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Thirdweb;
using UnityEngine;
using UnityEngine.Events;

public class BlockchainManager : MonoBehaviour
{
    public UnityEvent<string> OnLoggedIn;

    public string Address { get; private set; }

    public static BlockchainManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public async void Login(string authProvider)
    {
        AuthProvider provider = AuthProvider.Google;
        switch (authProvider)
        {
            case "google":
                provider = AuthProvider.Google;
                break;
            case "apple":
                provider = AuthProvider.Apple;
                break;
            case "facebook":
                provider = AuthProvider.Facebook;
                break;
        }

        var connection = new WalletConnection(
            provider: WalletProvider.SmartWallet,
            chainId: 421614,
            personalWallet: WalletProvider.EmbeddedWallet,
            authOptions: new AuthOptions(authProvider: provider)
        );

        Address = await ThirdwebManager.Instance.SDK.wallet.Connect(connection);

        OnLoggedIn?.Invoke(Address);
    }

    internal async Task SubmitScore(float distanceTravelled)
    {
        Debug.Log($"Submitting score of {distanceTravelled} to blockchain for address {Address}");
        var contract = ThirdwebManager.Instance.SDK.GetContract(
            "0x9d9a1f4c1a685857a5666db45588aa3d5643af9f",
            "[{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"internalType\":\"address\",\"name\":\"player\",\"type\":\"address\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"score\",\"type\":\"uint256\"}],\"name\":\"ScoreAdded\",\"type\":\"event\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"player\",\"type\":\"address\"}],\"name\":\"getRank\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"rank\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"score\",\"type\":\"uint256\"}],\"name\":\"submitScore\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"}]"
        );
        await contract.Write("submitScore", (int)distanceTravelled);
    }

    internal async Task<int> GetRank()
    {
        var contract = ThirdwebManager.Instance.SDK.GetContract(
            "0x9d9a1f4c1a685857a5666db45588aa3d5643af9f",
            "[{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"internalType\":\"address\",\"name\":\"player\",\"type\":\"address\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"score\",\"type\":\"uint256\"}],\"name\":\"ScoreAdded\",\"type\":\"event\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"player\",\"type\":\"address\"}],\"name\":\"getRank\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"rank\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"score\",\"type\":\"uint256\"}],\"name\":\"submitScore\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"}]"
        );
        var rank = await contract.Read<int>("getRank", Address);
        Debug.Log($"Rank for address {Address} is {rank}");
        return rank;
    }
}
