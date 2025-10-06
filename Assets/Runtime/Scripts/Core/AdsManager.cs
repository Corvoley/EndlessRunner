using GoogleMobileAds.Api;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

public class AdsManager : MonoBehaviour
{

    // These ad units are configured to always serve test ads.
#if UNITY_ANDROID
    private string _adInterstitialUnitId = "ca-app-pub-4311989921598352/1918029395";
#elif UNITY_IPHONE
    private string _adInterstitialUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
    private string _adInterstitialUnitId = "unused";
#endif


    // These ad units are configured to always serve test ads.
#if UNITY_ANDROID
    private string _adRewardedUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IPHONE
    private string _adRewardedUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
    private string _adRewardedUnitId = "unused";
#endif


    // These ad units are configured to always serve test ads.
#if UNITY_ANDROID
    private string _adNativeUnitId = "ca-app-pub-3940256099942544/2247696110";
#elif UNITY_IPHONE
    private string _adNativeUnitId = "ca-app-pub-3940256099942544/3986624511";
#else
    private string _adNativeUnitId = "unused";
#endif



    public InterstitialAd CurrentInterstitialAd { get; private set; }
    public RewardedAd CurrentRewardedAd { get; private set; }
    public NativeOverlayAd CurrentNativeOverlayAd { get; private set; }


    public static AdsManager Instance;

    public Action OnIntertistialAdClosed;

    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(this);

        }
        else
        {
            Instance = this;
            // Initialize the Google Mobile Ads SDK.
            MobileAds.Initialize((InitializationStatus initStatus) =>
            {
                // This callback is called once the MobileAds SDK is initialized.

                //request an interstitial ad
                LoadInterstitialAd();                
            });
        }



    }


    private void Start()
    {
        MobileAds.RaiseAdEventsOnUnityMainThread = true;
    }


    /// <summary>
    /// Loads the interstitial ad.
    /// </summary>
    public void LoadInterstitialAd()
    {
        // Clean up the old ad before loading a new one.
        if (CurrentInterstitialAd != null)
        {
            CurrentInterstitialAd.Destroy();
            CurrentInterstitialAd = null;
        }

       // Debug.Log("Loading the interstitial ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();
        adRequest.Keywords.Add("unity-admob-sample");

        // send the request to load the ad.
        InterstitialAd.Load(_adInterstitialUnitId, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("interstitial ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Interstitial ad loaded with response : "
                          + ad.GetResponseInfo());

                CurrentInterstitialAd = ad;
                RegisterInterstitiveEventHandlers(CurrentInterstitialAd);
            });
    }
    /// <summary>
    /// Shows the interstitial ad.
    /// </summary>
    public void ShowInterstitialAd()
    {
        if (CurrentInterstitialAd != null && CurrentInterstitialAd.CanShowAd())
        {
            Debug.Log("Showing interstitial ad.");
            CurrentInterstitialAd.Show();
        }
        else
        {
            Debug.LogError("Interstitial ad is not ready yet.");
        }
    }
    private void RegisterInterstitiveEventHandlers(InterstitialAd interstitialAd)
    {
        // Raised when the ad is estimated to have earned money.
        interstitialAd.OnAdPaid += (AdValue adValue) =>
        {
            // Debug.Log(String.Format("Interstitial ad paid {0} {1}.", adValue.Value, adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        interstitialAd.OnAdImpressionRecorded += () =>
        {
            //Debug.Log("Interstitial ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        interstitialAd.OnAdClicked += () =>
        {
            // Debug.Log("Interstitial ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        interstitialAd.OnAdFullScreenContentOpened += () =>
        {
            //Debug.Log("Interstitial ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        interstitialAd.OnAdFullScreenContentClosed += () =>
        {
            // Debug.Log("Interstitial ad full screen content closed.");

            LoadInterstitialAd();
            OnIntertistialAdClosed?.Invoke();

        };
        // Raised when the ad failed to open full screen content.
        interstitialAd.OnAdFullScreenContentFailed += (AdError error) =>
        {
            // Debug.LogError("Interstitial ad failed to open full screen content " + "with error : " + error);
            LoadInterstitialAd();
            OnIntertistialAdClosed?.Invoke();
        };
    }


    /// <summary>
    /// Loads the rewarded ad.
    /// </summary>
    public void LoadRewardedAd()
    {
        // Clean up the old ad before loading a new one.
        if (CurrentRewardedAd != null)
        {
            CurrentRewardedAd.Destroy();
            CurrentRewardedAd = null;
        }

        //Debug.Log("Loading the rewarded ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        RewardedAd.Load(_adRewardedUnitId, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("Rewarded ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Rewarded ad loaded with response : "
                          + ad.GetResponseInfo());

                CurrentRewardedAd = ad;
            });
    }

    public void ShowRewardedAd()
    {
        const string rewardMsg =
            "Rewarded ad rewarded the user. Type: {0}, amount: {1}.";

        if (CurrentRewardedAd != null && CurrentRewardedAd.CanShowAd())
        {
            CurrentRewardedAd.Show((Reward reward) =>
            {
                // TODO: Reward the user.
                Debug.Log(String.Format(rewardMsg, reward.Type, reward.Amount));
                GiveReward(reward);
            });
        }
    }
    private void RegisterRewardedEventHandlers(RewardedAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            //Debug.Log(String.Format("Rewarded ad paid {0} {1}.", adValue.Value, adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
            // Debug.Log("Rewarded ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
            //Debug.Log("Rewarded ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
            // Debug.Log("Rewarded ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            // Debug.Log("Rewarded ad full screen content closed.");
            LoadRewardedAd();          
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            //Debug.LogError("Rewarded ad failed to open full screen content " + "with error : " + error);
            LoadRewardedAd();
        };
    }


    /// <summary>
    /// Loads the ad.
    /// </summary>
    public void LoadNativeAd()
    {
        // Clean up the old ad before loading a new one.
        if (CurrentNativeOverlayAd != null)
        {
            CurrentNativeOverlayAd.Destroy();
            CurrentNativeOverlayAd = null;
        }

        //Debug.Log("Loading native overlay ad.");

        // Create a request used to load the ad.
        var adRequest = new AdRequest();

        // Optional: Define native ad options.
        var options = new NativeAdOptions
        {
            AdChoicesPlacement = AdChoicesPlacement.TopRightCorner,
            MediaAspectRatio = MediaAspectRatio.Any,
        };

        // Send the request to load the ad.
        NativeOverlayAd.Load(_adNativeUnitId, adRequest, options,
            (NativeOverlayAd ad, LoadAdError error) =>
            {
                if (error != null)
                {
                    Debug.LogError("Native Overlay ad failed to load an ad " +
                               " with error: " + error);
                    return;
                }

                // The ad should always be non-null if the error is null, but
                // double-check to avoid a crash.
                if (ad == null)
                {
                    Debug.LogError("Unexpected error: Native Overlay ad load event " +
                               " fired with null ad and null error.");
                    return;
                }

                // The operation completed successfully.
                Debug.Log("Native Overlay ad loaded with response : " +
                       ad.GetResponseInfo());
                CurrentNativeOverlayAd = ad;

                // Register to ad events to extend functionality.
                RegisterNativeEventHandlers(ad);
            });
    }
    private void RegisterNativeEventHandlers(NativeOverlayAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            //Debug.Log(String.Format("Rewarded ad paid {0} {1}.", adValue.Value, adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
            // Debug.Log("Rewarded ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
            //Debug.Log("Rewarded ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
            // Debug.Log("Rewarded ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            // Debug.Log("Rewarded ad full screen content closed.");
            LoadRewardedAd();
        };       
        
    }

    /// <summary>
    /// Renders the ad.
    /// </summary>
    public void RenderNativeAd()
    {
        if (CurrentNativeOverlayAd != null)
        {
            Debug.Log("Rendering Native Overlay ad.");

            // Define a native template style with a custom style.
            var style = new NativeTemplateStyle
            {
                TemplateId = NativeTemplateId.Medium,
                MainBackgroundColor = Color.red,
                CallToActionText = new NativeTemplateTextStyle
                {
                    BackgroundColor = Color.green,
                    TextColor = Color.white,
                    FontSize = 9,
                    Style = NativeTemplateFontStyle.Bold
                }
            };

            // Renders a native overlay ad at the default size
            // and anchored to the bottom of the screne.
            CurrentNativeOverlayAd.RenderTemplate(style, AdPosition.Bottom);
        }
    }
    public void ShowNativeAd()
    {
        if (CurrentNativeOverlayAd != null)
        {
            Debug.Log("Showing Native Overlay ad.");
            CurrentNativeOverlayAd.Show();
        }
    }
    public void HideNativeAd()
    {
        if (CurrentNativeOverlayAd != null)
        {
            Debug.Log("Hiding Native Overlay ad.");
            CurrentNativeOverlayAd.Hide();
        }
    }
    private void GiveReward(Reward reward)
    {
       
    }


}
