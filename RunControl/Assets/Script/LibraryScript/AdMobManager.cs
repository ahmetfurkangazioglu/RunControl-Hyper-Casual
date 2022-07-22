using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

namespace AdManager
{
     public class AdMobManager
    {
        private InterstitialAd interstitial;
        private RewardedAd rewarded;
        public  void RequestInterstitialAd()
        {
            string adUnityId;
#if UNITY_ANDROID

            adUnityId = "ca-app-pub-3940256099942544/1033173712";
#elif UNÝTY_IPHONE
            adUnityId = "ca-app-pub-3940256099942544/4411468910";
#else
                adUnityId="unexpected_platform";
#endif

            interstitial = new InterstitialAd(adUnityId);
            AdRequest request = new AdRequest.Builder().Build();
            interstitial.LoadAd(request);
            interstitial.OnAdClosed += InterstitialClosed;

        }
        void InterstitialClosed(object sender, EventArgs args)
        {
            interstitial.Destroy();
            RequestInterstitialAd();
        }
        public void ShowInterstitial()
        {
            if (PlayerPrefs.GetInt("CurrentAd")==1)
            {
                if (interstitial.IsLoaded())
                {
                    interstitial.Show();
                    PlayerPrefs.SetInt("CurrentAd", 0);
                }
                else
                {
                    interstitial.Destroy();
                    RequestInterstitialAd();
                }

            }
            else
            {
                PlayerPrefs.SetInt("CurrentAd", PlayerPrefs.GetInt("CurrentAd")+1);
            }
       
        }

        public void RequestRewardAd()
        {
            string adUnityId;
#if UNITY_ANDROID

            adUnityId = "ca-app-pub-3940256099942544/5224354917";
#elif UNÝTY_IPHONE
            adUnityId = "	ca-app-pub-3940256099942544/1712485313";
#else
                adUnityId="unexpected_platform";
#endif

            rewarded = new RewardedAd(adUnityId);
            AdRequest request = new AdRequest.Builder().Build();
            rewarded.LoadAd(request);
            rewarded.OnUserEarnedReward += UserEarnedReward;
            rewarded.OnAdClosed += RewardClosed;
            rewarded.OnAdLoaded+= EarnedLoaded;
        }
        public void ShowReward()
        {
            if (rewarded.IsLoaded())
            {
                rewarded.Show();
                GameObject.FindWithTag("MainControl").GetComponent<MainControl>().DisableReward();
            }
            else
            {
                GameObject.FindWithTag("MainControl").GetComponent<MainControl>().DisableReward();
            }                     
        }
        private void UserEarnedReward(object sender, Reward e)
        {
            string type = e.Type;
            double amount = e.Amount;
            PlayerPrefs.SetInt("TotalPoint", PlayerPrefs.GetInt("TotalPoint") + ((MainControl.LevelPoint * (int)amount) - MainControl.LevelPoint));          
        }
        private void RewardClosed(object sender, EventArgs e)
        {
            RequestRewardAd();
        }
        private void EarnedLoaded(object sender, EventArgs e)
        {
            Debug.Log("AdLoaded");
        }  
    }

}

