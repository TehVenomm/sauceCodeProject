package net.gogame.gowrap;

import android.app.Activity;
import android.net.Uri;
import android.util.Log;
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
import net.gogame.gowrap.p019ui.ActivityHelper;
import net.gogame.gowrap.p019ui.MainActivity;
import net.gogame.gowrap.p019ui.fab.FabManager;
import net.gogame.gowrap.support.StringUtils;
import p017io.fabric.sdk.android.services.events.EventsFilesManager;

public class GoWrapImpl implements GoWrap {
    public static final GoWrapImpl INSTANCE = new GoWrapImpl();
    private final AdManager<CanShowBannerAd, BannerAdSize> bannerAdManager = new AdManager<CanShowBannerAd, BannerAdSize>("banner", this.canShowBannerAdList) {
        /* access modifiers changed from: protected */
        public boolean hasAds(CanShowBannerAd canShowBannerAd, BannerAdSize bannerAdSize) {
            return canShowBannerAd.hasBannerAds(bannerAdSize);
        }

        /* access modifiers changed from: protected */
        public boolean showAd(CanShowBannerAd canShowBannerAd, BannerAdSize bannerAdSize) {
            return canShowBannerAd.showBannerAd(bannerAdSize);
        }

        /* access modifiers changed from: protected */
        public void hideAd(CanShowBannerAd canShowBannerAd) {
            canShowBannerAd.hideBannerAd();
        }
    };
    private CanChat canChat = null;
    private CanCheckVipStatus canCheckVipStatus = null;
    /* access modifiers changed from: private */
    public final List<CanGetUid> canGetUidList = new ArrayList();
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
    /* access modifiers changed from: private */
    public String guid = null;
    private final IntegrationContext integrationContext = new IntegrationContext() {
        public String getAppId() {
            return CoreSupport.INSTANCE.getAppId();
        }

        public String getGuid() {
            return GoWrapImpl.this.guid;
        }

        public Map<String, String> getUids() {
            HashMap hashMap = new HashMap();
            for (CanGetUid canGetUid : GoWrapImpl.this.canGetUidList) {
                try {
                    hashMap.put(((IntegrationSupport) canGetUid).getId(), canGetUid.getUid());
                } catch (Exception e) {
                }
            }
            return hashMap;
        }

        public boolean isVip() {
            return GoWrapImpl.this.vipStatus != null && GoWrapImpl.this.vipStatus.isVip() && !GoWrapImpl.this.vipStatus.isSuspended();
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
    };
    private final List<IntegrationSupport> integrationSupportList = new ArrayList();
    private final AdManager<CanShowInterstitialAd, InterstitialAdSize> interstitialAdManager = new AdManager<CanShowInterstitialAd, InterstitialAdSize>("interstitial", this.canShowInterstitialAdList) {
        /* access modifiers changed from: protected */
        public boolean hasAds(CanShowInterstitialAd canShowInterstitialAd, InterstitialAdSize interstitialAdSize) {
            return canShowInterstitialAd.hasInterstitialAds(interstitialAdSize);
        }

        /* access modifiers changed from: protected */
        public boolean showAd(CanShowInterstitialAd canShowInterstitialAd, InterstitialAdSize interstitialAdSize) {
            return canShowInterstitialAd.showInterstitialAd(interstitialAdSize);
        }

        /* access modifiers changed from: protected */
        public void hideAd(CanShowInterstitialAd canShowInterstitialAd) {
        }
    };
    private final Set<Listener> listeners = new HashSet();
    private Class<? extends Activity> mainActivity = MainActivity.class;
    private final AdManager<CanShowRewardedAd, InterstitialAdSize> rewardedAdManager = new AdManager<CanShowRewardedAd, InterstitialAdSize>("rewarded", this.canShowRewardedAdList) {
        /* access modifiers changed from: protected */
        public boolean hasAds(CanShowRewardedAd canShowRewardedAd, InterstitialAdSize interstitialAdSize) {
            return canShowRewardedAd.hasRewardedAds(interstitialAdSize);
        }

        /* access modifiers changed from: protected */
        public boolean showAd(CanShowRewardedAd canShowRewardedAd, InterstitialAdSize interstitialAdSize) {
            return canShowRewardedAd.showRewardedAd(interstitialAdSize);
        }

        /* access modifiers changed from: protected */
        public void hideAd(CanShowRewardedAd canShowRewardedAd) {
        }
    };
    /* access modifiers changed from: private */
    public VipStatus vipStatus = null;

    private static abstract class AdManager<INTEGRATION, SIZE> {
        private INTEGRATION currentIntegration;
        private int index;
        private final List<INTEGRATION> integrations;
        private final String name;

        /* access modifiers changed from: protected */
        public abstract boolean hasAds(INTEGRATION integration, SIZE size);

        /* access modifiers changed from: protected */
        public abstract void hideAd(INTEGRATION integration);

        /* access modifiers changed from: protected */
        public abstract boolean showAd(INTEGRATION integration, SIZE size);

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
                } catch (Exception e) {
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
                INTEGRATION integration = this.integrations.get(this.index);
                String access$500 = GoWrapImpl.getId(integration);
                try {
                    if (showAd(integration, size)) {
                        this.currentIntegration = integration;
                        Log.v(Constants.TAG, String.format("%s.showAd(): %s shown", new Object[]{this.name, access$500}));
                        incrementAdProviderIndex();
                        return;
                    }
                    Log.v(Constants.TAG, String.format("%s.showAd(): %s has no ads", new Object[]{this.name, access$500}));
                    incrementAdProviderIndex();
                    i++;
                } catch (Exception e) {
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

    /* access modifiers changed from: private */
    public static String getId(Object obj) {
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
            } catch (Exception e) {
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

    private void fireOnVipStatusUpdatedEvent(VipStatus vipStatus2) {
        if (this.listeners != null) {
            for (Listener listener : this.listeners) {
                if (listener != null) {
                    try {
                        listener.onVipStatusUpdated(vipStatus2);
                    } catch (Exception e) {
                        Log.e(Constants.TAG, "Exception", e);
                    }
                }
            }
        }
    }

    /* access modifiers changed from: private */
    public void fireOnOffersAvailableEvent() {
        if (this.listeners != null) {
            for (Listener listener : this.listeners) {
                if (listener != null) {
                    try {
                        listener.onOffersAvailable();
                    } catch (Exception e) {
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
        for (CanSetGuid guid2 : this.canSetGuidList) {
            try {
                guid2.setGuid(str);
            } catch (Exception e) {
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

    public void setVipStatus(VipStatus vipStatus2) {
        this.vipStatus = vipStatus2;
        fireOnVipStatusUpdatedEvent(vipStatus2);
    }

    private String generateReferenceId() {
        StringBuilder sb = new StringBuilder();
        sb.append(UUID.randomUUID().toString());
        sb.append(EventsFilesManager.ROLL_OVER_FILE_NAME_SEPARATOR);
        sb.append(System.currentTimeMillis());
        return sb.toString();
    }

    public void trackPurchase(String str, String str2, double d) {
        for (CanTrackPurchase trackPurchase : this.canTrackPurchaseList) {
            try {
                trackPurchase.trackPurchase(str, str2, d);
            } catch (Exception e) {
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

    /* JADX WARNING: Removed duplicated region for block: B:14:0x0074  */
    /* JADX WARNING: Removed duplicated region for block: B:25:0x00cd  */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public void trackPurchase(java.lang.String r10, java.lang.String r11, double r12, java.lang.String r14, java.lang.String r15) {
        /*
            r9 = this;
            r1 = 0
            r2 = 1
            net.gogame.gowrap.integrations.PurchaseDetails r8 = new net.gogame.gowrap.integrations.PurchaseDetails
            r8.<init>()
            java.lang.String r0 = r9.generateReferenceId()
            r8.setReferenceId(r0)
            r8.setProductId(r10)
            r8.setCurrencyCode(r11)
            java.lang.Double r0 = java.lang.Double.valueOf(r12)
            r8.setPrice(r0)
            r8.setPurchaseData(r14)
            r8.setSignature(r15)
            java.util.Date r4 = new java.util.Date
            r4.<init>()
            r3 = 0
            if (r14 == 0) goto L_0x00f5
            if (r15 == 0) goto L_0x00f5
            org.json.JSONObject r5 = new org.json.JSONObject     // Catch:{ JSONException -> 0x00ca }
            r5.<init>(r14)     // Catch:{ JSONException -> 0x00ca }
            net.gogame.gowrap.support.InAppPurchaseData r6 = new net.gogame.gowrap.support.InAppPurchaseData     // Catch:{ JSONException -> 0x00ca }
            r6.<init>(r5)     // Catch:{ JSONException -> 0x00ca }
            java.lang.String r3 = r6.getOrderId()     // Catch:{ JSONException -> 0x00ca }
            java.lang.Long r0 = r6.getPurchaseTime()     // Catch:{ JSONException -> 0x00ca }
            if (r0 == 0) goto L_0x004d
            java.util.Date r0 = new java.util.Date     // Catch:{ JSONException -> 0x00ca }
            java.lang.Long r6 = r6.getPurchaseTime()     // Catch:{ JSONException -> 0x00ca }
            long r6 = r6.longValue()     // Catch:{ JSONException -> 0x00ca }
            r0.<init>(r6)     // Catch:{ JSONException -> 0x00ca }
            r4 = r0
        L_0x004d:
            java.lang.String r0 = "goPay"
            org.json.JSONObject r0 = r5.optJSONObject(r0)     // Catch:{ JSONException -> 0x00ca }
            if (r0 == 0) goto L_0x00b6
            java.lang.String r5 = "sandbox"
            boolean r5 = r0.has(r5)     // Catch:{ JSONException -> 0x00ca }
            if (r5 == 0) goto L_0x00f5
            java.lang.String r5 = "sandbox"
            r6 = 0
            boolean r0 = r0.optBoolean(r5, r6)     // Catch:{ JSONException -> 0x00ca }
        L_0x0064:
            r8.setTimestamp(r4)
            r8.setOrderId(r3)
            net.gogame.gowrap.integrations.PurchaseDetails$VerificationStatus r3 = net.gogame.gowrap.integrations.PurchaseDetails.VerificationStatus.VERIFICATION_SUCCEEDED
            r8.setVerificationStatus(r3)
            r8.setSandbox(r0)
            if (r0 == 0) goto L_0x00cd
            java.lang.String r0 = "goWrap"
            java.lang.String r3 = "Sandbox purchase detected (%s, %s, %f, %s, %s)"
            r4 = 5
            java.lang.Object[] r4 = new java.lang.Object[r4]
            r4[r1] = r10
            r4[r2] = r11
            r1 = 2
            java.lang.Double r2 = java.lang.Double.valueOf(r12)
            r4[r1] = r2
            r1 = 3
            r4[r1] = r14
            r1 = 4
            r4[r1] = r15
            java.lang.String r1 = java.lang.String.format(r3, r4)
            android.util.Log.d(r0, r1)
            java.util.HashMap r0 = new java.util.HashMap
            r0.<init>()
            java.lang.String r1 = "product_id"
            r0.put(r1, r10)
            java.lang.String r1 = "currency_code"
            r0.put(r1, r11)
            java.lang.String r1 = "price"
            java.lang.Double r2 = java.lang.Double.valueOf(r12)
            r0.put(r1, r2)
            java.lang.String r1 = "sandbox"
            java.lang.String r2 = "purchase"
            r9.trackEvent(r1, r2, r0)
            r9.trackSandboxPurchase(r8)
        L_0x00b5:
            return
        L_0x00b6:
            if (r3 == 0) goto L_0x00c6
            int r0 = r3.length()     // Catch:{ JSONException -> 0x00ca }
            if (r0 == 0) goto L_0x00c6
            java.lang.String r0 = "goPay-sandbox-"
            boolean r0 = r3.startsWith(r0)     // Catch:{ JSONException -> 0x00ca }
            if (r0 == 0) goto L_0x00c8
        L_0x00c6:
            r0 = r2
            goto L_0x0064
        L_0x00c8:
            r0 = r1
            goto L_0x0064
        L_0x00ca:
            r0 = move-exception
            r0 = r2
            goto L_0x0064
        L_0x00cd:
            java.util.List<net.gogame.gowrap.integrations.CanTrackPurchase> r0 = r9.canTrackPurchaseList
            java.util.Iterator r0 = r0.iterator()
        L_0x00d3:
            boolean r1 = r0.hasNext()
            if (r1 == 0) goto L_0x00f1
            java.lang.Object r1 = r0.next()
            net.gogame.gowrap.integrations.CanTrackPurchase r1 = (net.gogame.gowrap.integrations.CanTrackPurchase) r1
            r2 = r10
            r3 = r11
            r4 = r12
            r6 = r14
            r7 = r15
            r1.trackPurchase(r2, r3, r4, r6, r7)     // Catch:{ Exception -> 0x00e8 }
            goto L_0x00d3
        L_0x00e8:
            r1 = move-exception
            java.lang.String r2 = "goWrap"
            java.lang.String r3 = "Exception"
            android.util.Log.e(r2, r3, r1)
            goto L_0x00d3
        L_0x00f1:
            r9.trackPurchase(r8)
            goto L_0x00b5
        L_0x00f5:
            r0 = r2
            goto L_0x0064
        */
        throw new UnsupportedOperationException("Method not decompiled: net.gogame.gowrap.GoWrapImpl.trackPurchase(java.lang.String, java.lang.String, double, java.lang.String, java.lang.String):void");
    }

    private void trackPurchase(PurchaseDetails purchaseDetails) {
        for (CanTrackPurchaseDetails trackPurchase : this.canTrackPurchaseDetailsList) {
            try {
                trackPurchase.trackPurchase(purchaseDetails);
            } catch (Exception e) {
                Log.e(Constants.TAG, "Exception", e);
            }
        }
    }

    private void trackSandboxPurchase(PurchaseDetails purchaseDetails) {
        for (CanTrackSandboxPurchaseDetails trackSandboxPurchase : this.canTrackSandboxPurchaseDetailsList) {
            try {
                trackSandboxPurchase.trackSandboxPurchase(purchaseDetails);
            } catch (Exception e) {
                Log.e(Constants.TAG, "Exception", e);
            }
        }
    }

    public void trackEvent(String str, String str2) {
        for (CanTrackEvent trackEvent : this.canTrackEventList) {
            try {
                trackEvent.trackEvent(str, str2);
            } catch (Exception e) {
                Log.e(Constants.TAG, "Exception", e);
            }
        }
    }

    public void trackEvent(String str, String str2, long j) {
        for (CanTrackEvent trackEvent : this.canTrackEventList) {
            try {
                trackEvent.trackEvent(str, str2, j);
            } catch (Exception e) {
                Log.e(Constants.TAG, "Exception", e);
            }
        }
    }

    public void trackEvent(String str, String str2, Map<String, Object> map) {
        for (CanTrackEvent trackEvent : this.canTrackEventList) {
            try {
                trackEvent.trackEvent(str, str2, map);
            } catch (Exception e) {
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
        } catch (Exception e) {
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
        } catch (Exception e) {
            Log.e(Constants.TAG, "Exception", e);
        }
    }

    public void hideBannerAd() {
        try {
            this.bannerAdManager.hideAd();
        } catch (Exception e) {
            Log.e(Constants.TAG, "Exception", e);
        }
    }

    public boolean hasInterstitialAds() {
        try {
            return this.interstitialAdManager.hasAds(InterstitialAdSize.INTERSTITIAL_AD_SIZE_FULLSCREEN);
        } catch (Exception e) {
            Log.e(Constants.TAG, "Exception", e);
            return false;
        }
    }

    public void showInterstitialAd() {
        try {
            if (hasInterstitialAds()) {
                this.interstitialAdManager.showAd(InterstitialAdSize.INTERSTITIAL_AD_SIZE_FULLSCREEN);
            }
        } catch (Exception e) {
            Log.e(Constants.TAG, "Exception", e);
        }
    }

    public boolean hasRewardedAds() {
        try {
            return this.rewardedAdManager.hasAds(InterstitialAdSize.INTERSTITIAL_AD_SIZE_FULLSCREEN);
        } catch (Exception e) {
            Log.e(Constants.TAG, "Exception", e);
            return false;
        }
    }

    public void showRewardedAd() {
        try {
            if (hasRewardedAds()) {
                this.rewardedAdManager.showAd(InterstitialAdSize.INTERSTITIAL_AD_SIZE_FULLSCREEN);
            }
        } catch (Exception e) {
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
            } catch (Exception e) {
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
            } catch (Exception e) {
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
            } catch (Exception e) {
                Log.e(Constants.TAG, "Exception", e);
            }
        }
    }

    public void didCompleteRewardedAd(String str, int i) {
        if (this.delegate != null) {
            try {
                this.delegate.didCompleteRewardedAd(str, i);
            } catch (Exception e) {
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
            } catch (Exception e) {
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
            } catch (Exception e) {
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
