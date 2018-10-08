package net.gogame.gowrap;

import android.app.Activity;
import android.net.Uri;
import android.util.Log;
import com.google.firebase.analytics.FirebaseAnalytics.Param;
import io.fabric.sdk.android.services.events.EventsFilesManager;
import java.util.ArrayList;
import java.util.Date;
import java.util.HashMap;
import java.util.HashSet;
import java.util.List;
import java.util.Map;
import java.util.Set;
import java.util.UUID;
import net.gogame.gowrap.GoWrap.BannerAdSize;
import net.gogame.gowrap.GoWrap.InterstitialAdSize;
import net.gogame.gowrap.integrations.AbstractIntegrationSupport;
import net.gogame.gowrap.integrations.CanChat;
import net.gogame.gowrap.integrations.CanCheckVipStatus;
import net.gogame.gowrap.integrations.CanGetUid;
import net.gogame.gowrap.integrations.CanSetGuid;
import net.gogame.gowrap.integrations.CanShowBannerAd;
import net.gogame.gowrap.integrations.CanShowInAppNotifications;
import net.gogame.gowrap.integrations.CanShowInterstitialAd;
import net.gogame.gowrap.integrations.CanShowOffers;
import net.gogame.gowrap.integrations.CanShowRewardedAd;
import net.gogame.gowrap.integrations.CanTrackEvent;
import net.gogame.gowrap.integrations.CanTrackPurchase;
import net.gogame.gowrap.integrations.CanTrackPurchaseDetails;
import net.gogame.gowrap.integrations.CanTrackSandboxPurchaseDetails;
import net.gogame.gowrap.integrations.Config;
import net.gogame.gowrap.integrations.IntegrationSupport;
import net.gogame.gowrap.integrations.IntegrationSupport.IntegrationContext;
import net.gogame.gowrap.integrations.PurchaseDetails;
import net.gogame.gowrap.integrations.PurchaseDetails.VerificationStatus;
import net.gogame.gowrap.integrations.core.CoreSupport;
import net.gogame.gowrap.integrations.core.Wrapper;
import net.gogame.gowrap.support.InAppPurchaseData;
import net.gogame.gowrap.support.StringUtils;
import net.gogame.gowrap.ui.ActivityHelper;
import net.gogame.gowrap.ui.MainActivity;
import net.gogame.gowrap.ui.fab.FabManager;
import org.json.JSONException;
import org.json.JSONObject;

public class GoWrapImpl implements GoWrap {
    public static final GoWrapImpl INSTANCE = new GoWrapImpl();
    private final AdManager<CanShowBannerAd, BannerAdSize> bannerAdManager = new AdManager<CanShowBannerAd, BannerAdSize>("banner", this.canShowBannerAdList) {
        protected boolean hasAds(CanShowBannerAd canShowBannerAd, BannerAdSize bannerAdSize) {
            return canShowBannerAd.hasBannerAds(bannerAdSize);
        }

        protected boolean showAd(CanShowBannerAd canShowBannerAd, BannerAdSize bannerAdSize) {
            return canShowBannerAd.showBannerAd(bannerAdSize);
        }

        protected void hideAd(CanShowBannerAd canShowBannerAd) {
            canShowBannerAd.hideBannerAd();
        }
    };
    private CanChat canChat = null;
    private CanCheckVipStatus canCheckVipStatus = null;
    private final List<CanGetUid> canGetUidList = new ArrayList();
    private final List<CanSetGuid> canSetGuidList = new ArrayList();
    private final List<CanShowBannerAd> canShowBannerAdList = new ArrayList();
    private final List<CanShowInAppNotifications> canShowInAppNotificationsList = new ArrayList();
    private final List<CanShowInterstitialAd> canShowInterstitialAdList = new ArrayList();
    private CanShowOffers canShowOffers = null;
    private final List<CanShowRewardedAd> canShowRewardedAdList = new ArrayList();
    private final List<CanTrackEvent> canTrackEventList = new ArrayList();
    private final List<CanTrackPurchaseDetails> canTrackPurchaseDetailsList = new ArrayList();
    private final List<CanTrackPurchase> canTrackPurchaseList = new ArrayList();
    private final List<CanTrackSandboxPurchaseDetails> canTrackSandboxPurchaseDetailsList = new ArrayList();
    private List<String> customUrlSchemes = null;
    private GoWrapDelegate delegate = null;
    private String guid = null;
    private final IntegrationContext integrationContext = new C14221();
    private final List<IntegrationSupport> integrationSupportList = new ArrayList();
    private final AdManager<CanShowInterstitialAd, InterstitialAdSize> interstitialAdManager = new AdManager<CanShowInterstitialAd, InterstitialAdSize>("interstitial", this.canShowInterstitialAdList) {
        protected boolean hasAds(CanShowInterstitialAd canShowInterstitialAd, InterstitialAdSize interstitialAdSize) {
            return canShowInterstitialAd.hasInterstitialAds(interstitialAdSize);
        }

        protected boolean showAd(CanShowInterstitialAd canShowInterstitialAd, InterstitialAdSize interstitialAdSize) {
            return canShowInterstitialAd.showInterstitialAd(interstitialAdSize);
        }

        protected void hideAd(CanShowInterstitialAd canShowInterstitialAd) {
        }
    };
    private final Set<Listener> listeners = new HashSet();
    private Class<? extends Activity> mainActivity = MainActivity.class;
    private final AdManager<CanShowRewardedAd, InterstitialAdSize> rewardedAdManager = new AdManager<CanShowRewardedAd, InterstitialAdSize>("rewarded", this.canShowRewardedAdList) {
        protected boolean hasAds(CanShowRewardedAd canShowRewardedAd, InterstitialAdSize interstitialAdSize) {
            return canShowRewardedAd.hasRewardedAds(interstitialAdSize);
        }

        protected boolean showAd(CanShowRewardedAd canShowRewardedAd, InterstitialAdSize interstitialAdSize) {
            return canShowRewardedAd.showRewardedAd(interstitialAdSize);
        }

        protected void hideAd(CanShowRewardedAd canShowRewardedAd) {
        }
    };
    private VipStatus vipStatus = null;

    /* renamed from: net.gogame.gowrap.GoWrapImpl$1 */
    class C14221 implements IntegrationContext {
        C14221() {
        }

        public String getAppId() {
            return CoreSupport.INSTANCE.getAppId();
        }

        public String getGuid() {
            return GoWrapImpl.this.guid;
        }

        public Map<String, String> getUids() {
            Map<String, String> hashMap = new HashMap();
            for (CanGetUid canGetUid : GoWrapImpl.this.canGetUidList) {
                try {
                    hashMap.put(((IntegrationSupport) canGetUid).getId(), canGetUid.getUid());
                } catch (Exception e) {
                }
            }
            return hashMap;
        }

        public boolean isVip() {
            return (GoWrapImpl.this.vipStatus == null || !GoWrapImpl.this.vipStatus.isVip() || GoWrapImpl.this.vipStatus.isSuspended()) ? false : true;
        }

        public boolean isChatBotEnabled() {
            return Wrapper.INSTANCE.isChatBotEnabled();
        }

        public boolean isForceEnableChat() {
            return CoreSupport.INSTANCE.isForceEnableChat();
        }

        public Activity getCurrentActivity() {
            return ActivityHelper.INSTANCE.getCurrentActivity();
        }

        public void didCompleteRewardedAd(String str, int i) {
            GoWrapImpl.this.didCompleteRewardedAd(str, i);
        }

        public void onVipStatusUpdated(VipStatus vipStatus) {
            GoWrapImpl.this.setVipStatus(vipStatus);
        }

        public void onOffersAvailable() {
            GoWrapImpl.this.onOffersAvailable();
            GoWrapImpl.this.fireOnOffersAvailableEvent();
        }

        public boolean handleCustomUrl(String str) {
            return GoWrapImpl.this.handleCustomUri(str);
        }

        public boolean handleCustomUrl(Uri uri) {
            return GoWrapImpl.this.handleCustomUri(uri);
        }
    }

    private static abstract class AdManager<INTEGRATION, SIZE> {
        private INTEGRATION currentIntegration;
        private int index;
        private final List<INTEGRATION> integrations;
        private final String name;

        protected abstract boolean hasAds(INTEGRATION integration, SIZE size);

        protected abstract void hideAd(INTEGRATION integration);

        protected abstract boolean showAd(INTEGRATION integration, SIZE size);

        private AdManager(String str, List<INTEGRATION> list) {
            this.index = 0;
            this.currentIntegration = null;
            this.name = str;
            this.integrations = list;
        }

        private void incrementAdProviderIndex() {
            this.index++;
            if (this.index >= this.integrations.size()) {
                this.index = 0;
            }
        }

        public boolean hasAds(SIZE size) {
            if (this.integrations == null || this.integrations.isEmpty()) {
                Log.v(Constants.TAG, String.format("%s.hasAds()=false, no integrations available", new Object[]{this.name}));
                return false;
            }
            int i = 0;
            while (i < this.integrations.size()) {
                Object obj = this.integrations.get(this.index);
                String access$500 = GoWrapImpl.getId(obj);
                try {
                    if (hasAds(obj, size)) {
                        Log.v(Constants.TAG, String.format("%s.hasdAds(): %s has ads", new Object[]{this.name, access$500}));
                        return true;
                    }
                    Log.v(Constants.TAG, String.format("%s.hasAds(): %s has no ads", new Object[]{this.name, access$500}));
                    incrementAdProviderIndex();
                    i++;
                } catch (Throwable e) {
                    Log.e(Constants.TAG, "Exception", e);
                }
            }
            Log.v(Constants.TAG, String.format("%s.hasAds()=false", new Object[]{this.name}));
            return false;
        }

        public void showAd(SIZE size) {
            if (this.integrations == null || this.integrations.isEmpty()) {
                Log.v(Constants.TAG, String.format("%s.showAd(): no integrations available", new Object[]{this.name}));
                return;
            }
            int i = 0;
            while (i < this.integrations.size()) {
                Object obj = this.integrations.get(this.index);
                String access$500 = GoWrapImpl.getId(obj);
                try {
                    if (showAd(obj, size)) {
                        this.currentIntegration = obj;
                        Log.v(Constants.TAG, String.format("%s.showAd(): %s shown", new Object[]{this.name, access$500}));
                        incrementAdProviderIndex();
                        return;
                    }
                    Log.v(Constants.TAG, String.format("%s.showAd(): %s has no ads", new Object[]{this.name, access$500}));
                    incrementAdProviderIndex();
                    i++;
                } catch (Throwable e) {
                    Log.e(Constants.TAG, "Exception", e);
                }
            }
            Log.v(Constants.TAG, String.format("%s.showAd() no ads", new Object[]{this.name}));
        }

        public void hideAd() {
            if (this.currentIntegration != null) {
                hideAd(this.currentIntegration);
            }
        }
    }

    public interface Listener {
        void onOffersAvailable();

        void onVipStatusUpdated(VipStatus vipStatus);
    }

    private GoWrapImpl() {
    }

    public Class<? extends Activity> getMainActivity() {
        return this.mainActivity;
    }

    public void setMainActivity(Class<? extends Activity> cls) {
        this.mainActivity = cls;
    }

    private static String getId(Object obj) {
        if (obj == null) {
            return null;
        }
        if (obj instanceof IntegrationSupport) {
            return ((IntegrationSupport) obj).getId();
        }
        return obj.getClass().getSimpleName();
    }

    public void register(IntegrationSupport integrationSupport, Activity activity, Config config) {
        ActivityHelper.INSTANCE.setCurrentActivity(activity);
        if (integrationSupport.isIntegrated()) {
            this.integrationSupportList.add(integrationSupport);
            if (integrationSupport instanceof CanChat) {
                if (this.canChat == null) {
                    this.canChat = (CanChat) integrationSupport;
                } else {
                    Log.w(Constants.TAG, "Chat service already registered, skipping " + integrationSupport.getId());
                }
            }
            if (integrationSupport instanceof CanShowOffers) {
                if (this.canShowOffers == null) {
                    this.canShowOffers = (CanShowOffers) integrationSupport;
                } else {
                    Log.w(Constants.TAG, "Offers service already registered, skipping " + integrationSupport.getId());
                }
            }
            if (integrationSupport instanceof CanCheckVipStatus) {
                if (this.canCheckVipStatus == null) {
                    this.canCheckVipStatus = (CanCheckVipStatus) integrationSupport;
                } else {
                    Log.w(Constants.TAG, "VIP status check service already registered, skipping " + integrationSupport.getId());
                }
            }
            if (integrationSupport instanceof CanGetUid) {
                this.canGetUidList.add((CanGetUid) integrationSupport);
            }
            if (integrationSupport instanceof CanSetGuid) {
                this.canSetGuidList.add((CanSetGuid) integrationSupport);
            }
            if (integrationSupport instanceof CanTrackPurchase) {
                this.canTrackPurchaseList.add((CanTrackPurchase) integrationSupport);
            }
            if (integrationSupport instanceof CanTrackPurchaseDetails) {
                this.canTrackPurchaseDetailsList.add((CanTrackPurchaseDetails) integrationSupport);
            }
            if (integrationSupport instanceof CanTrackSandboxPurchaseDetails) {
                this.canTrackSandboxPurchaseDetailsList.add((CanTrackSandboxPurchaseDetails) integrationSupport);
            }
            if (integrationSupport instanceof CanTrackEvent) {
                this.canTrackEventList.add((CanTrackEvent) integrationSupport);
            }
            if (integrationSupport instanceof CanShowBannerAd) {
                this.canShowBannerAdList.add((CanShowBannerAd) integrationSupport);
            }
            if (integrationSupport instanceof CanShowInterstitialAd) {
                this.canShowInterstitialAdList.add((CanShowInterstitialAd) integrationSupport);
            }
            if (integrationSupport instanceof CanShowRewardedAd) {
                this.canShowRewardedAdList.add((CanShowRewardedAd) integrationSupport);
            }
            if (integrationSupport instanceof CanShowInAppNotifications) {
                this.canShowInAppNotificationsList.add((CanShowInAppNotifications) integrationSupport);
            }
            try {
                integrationSupport.init(activity, config, this.integrationContext);
            } catch (Throwable e) {
                Log.e(Constants.TAG, "Exception", e);
            }
        }
    }

    public boolean hasChat() {
        return this.canChat != null;
    }

    public void startChat() {
        if (hasChat()) {
            this.canChat.startChat();
        }
    }

    public List<IntegrationSupport> getIntegrationSupportList() {
        return this.integrationSupportList;
    }

    public void setDelegate(GoWrapDelegate goWrapDelegate) {
        this.delegate = goWrapDelegate;
    }

    public List<String> getCustomUrlSchemes() {
        return this.customUrlSchemes;
    }

    public void setCustomUrlSchemes(List<String> list) {
        this.customUrlSchemes = list;
    }

    public boolean handleCustomUri(String str) {
        if (str == null || this.customUrlSchemes == null) {
            return false;
        }
        return handleCustomUri(Uri.parse(str));
    }

    public boolean handleCustomUri(Uri uri) {
        if (uri == null || this.customUrlSchemes == null || !this.customUrlSchemes.contains(uri.getScheme())) {
            return false;
        }
        onCustomUrl(uri.toString());
        if (ActivityHelper.INSTANCE.getCurrentActivity() != null && ActivityHelper.INSTANCE.getCurrentActivity().getClass().getName().startsWith("net.gogame.gowrap.")) {
            ActivityHelper.INSTANCE.getCurrentActivity().finish();
        }
        return true;
    }

    public void addListener(Listener listener) {
        if (listener != null) {
            this.listeners.add(listener);
        }
    }

    public void removeListener(Listener listener) {
        if (listener != null) {
            this.listeners.remove(listener);
        }
    }

    private void fireOnVipStatusUpdatedEvent(VipStatus vipStatus) {
        if (this.listeners != null) {
            for (Listener listener : this.listeners) {
                if (listener != null) {
                    try {
                        listener.onVipStatusUpdated(vipStatus);
                    } catch (Throwable e) {
                        Log.e(Constants.TAG, "Exception", e);
                    }
                }
            }
        }
    }

    private void fireOnOffersAvailableEvent() {
        if (this.listeners != null) {
            for (Listener listener : this.listeners) {
                if (listener != null) {
                    try {
                        listener.onOffersAvailable();
                    } catch (Throwable e) {
                        Log.e(Constants.TAG, "Exception", e);
                    }
                }
            }
        }
    }

    public void showStartMenu() {
        if (ActivityHelper.INSTANCE.getCurrentActivity() != null) {
            FabManager.showMenu(ActivityHelper.INSTANCE.getCurrentActivity());
        }
    }

    public void showFab() {
        if (ActivityHelper.INSTANCE.getCurrentActivity() != null) {
            FabManager.showFab(ActivityHelper.INSTANCE.getCurrentActivity());
        }
    }

    public void hideFab() {
        if (ActivityHelper.INSTANCE.getCurrentActivity() != null) {
            FabManager.hideFab(ActivityHelper.INSTANCE.getCurrentActivity());
        }
    }

    public String getGuid() {
        return this.guid;
    }

    public void setGuid(String str) {
        if (!StringUtils.isEquals(str, this.guid)) {
            this.vipStatus = null;
            fireOnVipStatusUpdatedEvent(null);
        }
        this.guid = str;
        for (CanSetGuid guid : this.canSetGuidList) {
            try {
                guid.setGuid(str);
            } catch (Throwable e) {
                Log.e(Constants.TAG, "Exception", e);
            }
        }
    }

    public void checkVipStatus(boolean z) {
        if (this.canCheckVipStatus != null) {
            this.canCheckVipStatus.checkVipStatus(this.guid, z);
        }
    }

    public VipStatus getVipStatus() {
        return this.vipStatus;
    }

    public void setVipStatus(VipStatus vipStatus) {
        this.vipStatus = vipStatus;
        fireOnVipStatusUpdatedEvent(vipStatus);
    }

    private String generateReferenceId() {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.append(UUID.randomUUID().toString());
        stringBuilder.append(EventsFilesManager.ROLL_OVER_FILE_NAME_SEPARATOR);
        stringBuilder.append(System.currentTimeMillis());
        return stringBuilder.toString();
    }

    public void trackPurchase(String str, String str2, double d) {
        for (CanTrackPurchase trackPurchase : this.canTrackPurchaseList) {
            try {
                trackPurchase.trackPurchase(str, str2, d);
            } catch (Throwable e) {
                Log.e(Constants.TAG, "Exception", e);
            }
        }
        PurchaseDetails purchaseDetails = new PurchaseDetails();
        purchaseDetails.setReferenceId(generateReferenceId());
        purchaseDetails.setTimestamp(new Date());
        purchaseDetails.setProductId(str);
        purchaseDetails.setCurrencyCode(str2);
        purchaseDetails.setPrice(Double.valueOf(d));
        purchaseDetails.setOrderId(null);
        purchaseDetails.setVerificationStatus(VerificationStatus.NOT_VERIFIED);
        purchaseDetails.setSandbox(false);
        purchaseDetails.setComment("Legacy/deprecated");
        trackPurchase(purchaseDetails);
    }

    public void trackPurchase(String str, String str2, double d, String str3, String str4) {
        String str5;
        boolean z;
        Date date;
        Map hashMap;
        PurchaseDetails purchaseDetails = new PurchaseDetails();
        purchaseDetails.setReferenceId(generateReferenceId());
        purchaseDetails.setProductId(str);
        purchaseDetails.setCurrencyCode(str2);
        purchaseDetails.setPrice(Double.valueOf(d));
        purchaseDetails.setPurchaseData(str3);
        purchaseDetails.setSignature(str4);
        Date date2 = new Date();
        String str6 = null;
        if (str3 == null || str4 == null) {
            str5 = null;
            z = true;
        } else {
            try {
                JSONObject jSONObject = new JSONObject(str3);
                InAppPurchaseData inAppPurchaseData = new InAppPurchaseData(jSONObject);
                str5 = inAppPurchaseData.getOrderId();
                try {
                    if (inAppPurchaseData.getPurchaseTime() != null) {
                        date2 = new Date(inAppPurchaseData.getPurchaseTime().longValue());
                    }
                    try {
                        JSONObject optJSONObject = jSONObject.optJSONObject("goPay");
                        if (optJSONObject != null) {
                            z = optJSONObject.has("sandbox") ? optJSONObject.optBoolean("sandbox", false) : true;
                        } else {
                            if (str5 != null) {
                                if (!(str5.length() == 0 || str5.startsWith("goPay-sandbox-"))) {
                                    z = false;
                                }
                            }
                            z = true;
                        }
                    } catch (JSONException e) {
                        str6 = str5;
                        date = date2;
                        date2 = date;
                        str5 = str6;
                        z = true;
                        purchaseDetails.setTimestamp(date2);
                        purchaseDetails.setOrderId(str5);
                        purchaseDetails.setVerificationStatus(VerificationStatus.VERIFICATION_SUCCEEDED);
                        purchaseDetails.setSandbox(z);
                        if (z) {
                            Log.d(Constants.TAG, String.format("Sandbox purchase detected (%s, %s, %f, %s, %s)", new Object[]{str, str2, Double.valueOf(d), str3, str4}));
                            hashMap = new HashMap();
                            hashMap.put("product_id", str);
                            hashMap.put("currency_code", str2);
                            hashMap.put(Param.PRICE, Double.valueOf(d));
                            trackEvent("sandbox", AbstractIntegrationSupport.DEFAULT_PURCHASE_CATEGORY, hashMap);
                            trackSandboxPurchase(purchaseDetails);
                            return;
                        }
                        for (CanTrackPurchase trackPurchase : this.canTrackPurchaseList) {
                            try {
                                trackPurchase.trackPurchase(str, str2, d, str3, str4);
                            } catch (Throwable e2) {
                                Log.e(Constants.TAG, "Exception", e2);
                            }
                        }
                        trackPurchase(purchaseDetails);
                    }
                } catch (JSONException e3) {
                    str6 = str5;
                    date = date2;
                    date2 = date;
                    str5 = str6;
                    z = true;
                    purchaseDetails.setTimestamp(date2);
                    purchaseDetails.setOrderId(str5);
                    purchaseDetails.setVerificationStatus(VerificationStatus.VERIFICATION_SUCCEEDED);
                    purchaseDetails.setSandbox(z);
                    if (z) {
                        while (r0.hasNext()) {
                            trackPurchase.trackPurchase(str, str2, d, str3, str4);
                        }
                        trackPurchase(purchaseDetails);
                    }
                    Log.d(Constants.TAG, String.format("Sandbox purchase detected (%s, %s, %f, %s, %s)", new Object[]{str, str2, Double.valueOf(d), str3, str4}));
                    hashMap = new HashMap();
                    hashMap.put("product_id", str);
                    hashMap.put("currency_code", str2);
                    hashMap.put(Param.PRICE, Double.valueOf(d));
                    trackEvent("sandbox", AbstractIntegrationSupport.DEFAULT_PURCHASE_CATEGORY, hashMap);
                    trackSandboxPurchase(purchaseDetails);
                    return;
                }
            } catch (JSONException e4) {
                date = date2;
                date2 = date;
                str5 = str6;
                z = true;
                purchaseDetails.setTimestamp(date2);
                purchaseDetails.setOrderId(str5);
                purchaseDetails.setVerificationStatus(VerificationStatus.VERIFICATION_SUCCEEDED);
                purchaseDetails.setSandbox(z);
                if (z) {
                    Log.d(Constants.TAG, String.format("Sandbox purchase detected (%s, %s, %f, %s, %s)", new Object[]{str, str2, Double.valueOf(d), str3, str4}));
                    hashMap = new HashMap();
                    hashMap.put("product_id", str);
                    hashMap.put("currency_code", str2);
                    hashMap.put(Param.PRICE, Double.valueOf(d));
                    trackEvent("sandbox", AbstractIntegrationSupport.DEFAULT_PURCHASE_CATEGORY, hashMap);
                    trackSandboxPurchase(purchaseDetails);
                    return;
                }
                while (r0.hasNext()) {
                    trackPurchase.trackPurchase(str, str2, d, str3, str4);
                }
                trackPurchase(purchaseDetails);
            }
        }
        purchaseDetails.setTimestamp(date2);
        purchaseDetails.setOrderId(str5);
        purchaseDetails.setVerificationStatus(VerificationStatus.VERIFICATION_SUCCEEDED);
        purchaseDetails.setSandbox(z);
        if (z) {
            Log.d(Constants.TAG, String.format("Sandbox purchase detected (%s, %s, %f, %s, %s)", new Object[]{str, str2, Double.valueOf(d), str3, str4}));
            hashMap = new HashMap();
            hashMap.put("product_id", str);
            hashMap.put("currency_code", str2);
            hashMap.put(Param.PRICE, Double.valueOf(d));
            trackEvent("sandbox", AbstractIntegrationSupport.DEFAULT_PURCHASE_CATEGORY, hashMap);
            trackSandboxPurchase(purchaseDetails);
            return;
        }
        while (r0.hasNext()) {
            trackPurchase.trackPurchase(str, str2, d, str3, str4);
        }
        trackPurchase(purchaseDetails);
    }

    private void trackPurchase(PurchaseDetails purchaseDetails) {
        for (CanTrackPurchaseDetails trackPurchase : this.canTrackPurchaseDetailsList) {
            try {
                trackPurchase.trackPurchase(purchaseDetails);
            } catch (Throwable e) {
                Log.e(Constants.TAG, "Exception", e);
            }
        }
    }

    private void trackSandboxPurchase(PurchaseDetails purchaseDetails) {
        for (CanTrackSandboxPurchaseDetails trackSandboxPurchase : this.canTrackSandboxPurchaseDetailsList) {
            try {
                trackSandboxPurchase.trackSandboxPurchase(purchaseDetails);
            } catch (Throwable e) {
                Log.e(Constants.TAG, "Exception", e);
            }
        }
    }

    public void trackEvent(String str, String str2) {
        for (CanTrackEvent trackEvent : this.canTrackEventList) {
            try {
                trackEvent.trackEvent(str, str2);
            } catch (Throwable e) {
                Log.e(Constants.TAG, "Exception", e);
            }
        }
    }

    public void trackEvent(String str, String str2, long j) {
        for (CanTrackEvent trackEvent : this.canTrackEventList) {
            try {
                trackEvent.trackEvent(str, str2, j);
            } catch (Throwable e) {
                Log.e(Constants.TAG, "Exception", e);
            }
        }
    }

    public void trackEvent(String str, String str2, Map<String, Object> map) {
        for (CanTrackEvent trackEvent : this.canTrackEventList) {
            try {
                trackEvent.trackEvent(str, str2, (Map) map);
            } catch (Throwable e) {
                Log.e(Constants.TAG, "Exception", e);
            }
        }
    }

    public boolean hasBannerAds() {
        return hasBannerAds(BannerAdSize.BANNER_SIZE_AUTO);
    }

    public boolean hasBannerAds(BannerAdSize bannerAdSize) {
        try {
            return this.bannerAdManager.hasAds(bannerAdSize);
        } catch (Throwable e) {
            Log.e(Constants.TAG, "Exception", e);
            return false;
        }
    }

    public void showBannerAd() {
        showBannerAd(BannerAdSize.BANNER_SIZE_AUTO);
    }

    public void showBannerAd(BannerAdSize bannerAdSize) {
        try {
            if (hasBannerAds(bannerAdSize)) {
                this.bannerAdManager.showAd(bannerAdSize);
            }
        } catch (Throwable e) {
            Log.e(Constants.TAG, "Exception", e);
        }
    }

    public void hideBannerAd() {
        try {
            this.bannerAdManager.hideAd();
        } catch (Throwable e) {
            Log.e(Constants.TAG, "Exception", e);
        }
    }

    public boolean hasInterstitialAds() {
        try {
            return this.interstitialAdManager.hasAds(InterstitialAdSize.INTERSTITIAL_AD_SIZE_FULLSCREEN);
        } catch (Throwable e) {
            Log.e(Constants.TAG, "Exception", e);
            return false;
        }
    }

    public void showInterstitialAd() {
        try {
            if (hasInterstitialAds()) {
                this.interstitialAdManager.showAd(InterstitialAdSize.INTERSTITIAL_AD_SIZE_FULLSCREEN);
            }
        } catch (Throwable e) {
            Log.e(Constants.TAG, "Exception", e);
        }
    }

    public boolean hasRewardedAds() {
        try {
            return this.rewardedAdManager.hasAds(InterstitialAdSize.INTERSTITIAL_AD_SIZE_FULLSCREEN);
        } catch (Throwable e) {
            Log.e(Constants.TAG, "Exception", e);
            return false;
        }
    }

    public void showRewardedAd() {
        try {
            if (hasRewardedAds()) {
                this.rewardedAdManager.showAd(InterstitialAdSize.INTERSTITIAL_AD_SIZE_FULLSCREEN);
            }
        } catch (Throwable e) {
            Log.e(Constants.TAG, "Exception", e);
        }
    }

    public void onMenuOpened() {
        Log.d(Constants.TAG, "onMenuOpened()");
        if (this.delegate != null) {
            try {
                if (this.delegate instanceof GoWrapDelegateV2) {
                    ((GoWrapDelegateV2) this.delegate).onMenuOpened();
                }
            } catch (Throwable e) {
                Log.e(Constants.TAG, "Exception", e);
            }
        }
    }

    public void onMenuClosed() {
        Log.d(Constants.TAG, "onMenuClosed()");
        if (this.delegate != null) {
            try {
                if (this.delegate instanceof GoWrapDelegateV2) {
                    ((GoWrapDelegateV2) this.delegate).onMenuClosed();
                }
            } catch (Throwable e) {
                Log.e(Constants.TAG, "Exception", e);
            }
        }
    }

    public void onCustomUrl(String str) {
        Log.d(Constants.TAG, String.format("onCustomUrl(%s)", new Object[]{str}));
        if (this.delegate != null) {
            try {
                if (this.delegate instanceof GoWrapDelegateV2) {
                    ((GoWrapDelegateV2) this.delegate).onCustomUrl(str);
                }
            } catch (Throwable e) {
                Log.e(Constants.TAG, "Exception", e);
            }
        }
    }

    public void didCompleteRewardedAd(String str, int i) {
        if (this.delegate != null) {
            try {
                this.delegate.didCompleteRewardedAd(str, i);
            } catch (Throwable e) {
                Log.e(Constants.TAG, "Exception", e);
            }
        }
    }

    public void onOffersAvailable() {
        Log.d(Constants.TAG, "onOffersAvailable()");
        if (this.delegate != null) {
            try {
                if (this.delegate instanceof GoWrapDelegateV2) {
                    ((GoWrapDelegateV2) this.delegate).onOffersAvailable();
                }
            } catch (Throwable e) {
                Log.e(Constants.TAG, "Exception", e);
            }
        }
    }

    public void showInAppNotifications() {
        Log.d(Constants.TAG, "showInAppNotifications()");
        for (CanShowInAppNotifications showInAppNotifications : this.canShowInAppNotificationsList) {
            try {
                showInAppNotifications.showInAppNotifications();
                return;
            } catch (Throwable e) {
                Log.e(Constants.TAG, "Exception", e);
            }
        }
    }

    public boolean hasOffers() {
        return this.canShowOffers != null && this.canShowOffers.hasOffers();
    }

    public void showOffers() {
        if (hasOffers()) {
            this.canShowOffers.showOffers();
        }
    }
}
