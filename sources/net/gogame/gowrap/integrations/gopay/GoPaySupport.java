package net.gogame.gowrap.integrations.gopay;

import android.app.Activity;
import android.util.Log;
import java.util.Map;
import java.util.Map.Entry;
import net.gogame.gopay.vip.IVipClient.Listener;
import net.gogame.gopay.vip.PurchaseEvent;
import net.gogame.gopay.vip.PurchaseEvent.VerificationStatus;
import net.gogame.gopay.vip.VipClient;
import net.gogame.gopay.vip.VipStatus;
import net.gogame.gowrap.Constants;
import net.gogame.gowrap.integrations.AbstractIntegrationSupport;
import net.gogame.gowrap.integrations.CanCheckVipStatus;
import net.gogame.gowrap.integrations.CanSetGuid;
import net.gogame.gowrap.integrations.CanTrackPurchaseDetails;
import net.gogame.gowrap.integrations.CanTrackSandboxPurchaseDetails;
import net.gogame.gowrap.integrations.Config;
import net.gogame.gowrap.integrations.IntegrationSupport.IntegrationContext;
import net.gogame.gowrap.integrations.PurchaseDetails;
import net.gogame.gowrap.support.ClassUtils;

public class GoPaySupport extends AbstractIntegrationSupport implements CanSetGuid, CanTrackPurchaseDetails, CanTrackSandboxPurchaseDetails, CanCheckVipStatus {
    public static final String CONFIG_APP_ID = "appId";
    public static final String CONFIG_GAME_MANAGED_VIP_STATUS = "gameManagedVipStatus";
    public static final String CONFIG_SECRET = "secret";
    public static final GoPaySupport INSTANCE = new GoPaySupport();
    public static final String METADATA_GAME_MANAGED_VIP_STATUS = "goWrap.goPay.gameManagedVipStatus";
    private String appId;
    private boolean gameManagedVipStatus = false;
    private final Listener goPayClientListener = new C11131();
    private IntegrationContext integrationContext;

    /* renamed from: net.gogame.gowrap.integrations.gopay.GoPaySupport$1 */
    class C11131 implements Listener {
        C11131() {
        }

        public void onVipStatus(VipStatus vipStatus) {
            if (!GoPaySupport.this.gameManagedVipStatus && GoPaySupport.this.integrationContext != null) {
                net.gogame.gowrap.VipStatus vipStatus2 = null;
                if (vipStatus != null) {
                    vipStatus2 = new net.gogame.gowrap.VipStatus();
                    vipStatus2.setVip(vipStatus.isVip());
                    vipStatus2.setSuspended(vipStatus.isSuspended());
                    vipStatus2.setSuspensionMessage(vipStatus.getSuspensionMessage());
                }
                GoPaySupport.this.integrationContext.onVipStatusUpdated(vipStatus2);
            }
        }
    }

    private GoPaySupport() {
        super("goPay");
    }

    public String getAppId() {
        return this.appId;
    }

    public String getGuid() {
        return this.integrationContext.getGuid();
    }

    public Activity getCurrentActivity() {
        return this.integrationContext.getCurrentActivity();
    }

    public boolean isGameManagedVipStatusEnabled() {
        return this.gameManagedVipStatus;
    }

    public boolean isIntegrated() {
        return ClassUtils.hasClass("net.gogame.gopay.vip.VipClient");
    }

    protected void doInit(Activity activity, Config config, IntegrationContext integrationContext) {
        boolean z = true;
        this.integrationContext = integrationContext;
        this.appId = config.getString("appId");
        String string = config.getString(CONFIG_SECRET);
        try {
            int i = activity.getPackageManager().getApplicationInfo(activity.getPackageName(), 128).metaData.getInt(METADATA_GAME_MANAGED_VIP_STATUS, -1);
            if (i != -1) {
                if (i == 1) {
                    this.gameManagedVipStatus = true;
                } else if (i == 0) {
                    this.gameManagedVipStatus = false;
                } else {
                    z = false;
                }
                if (z) {
                    try {
                        Log.d(Constants.TAG, "gameManagedVipStatus=" + this.gameManagedVipStatus);
                    } catch (Exception e) {
                    }
                }
            } else {
                z = false;
            }
        } catch (Exception e2) {
            z = false;
        }
        if (!z) {
            this.gameManagedVipStatus = config.getBoolean(CONFIG_GAME_MANAGED_VIP_STATUS, false);
        }
        VipClient.INSTANCE.init(activity, this.appId, string);
        VipClient.INSTANCE.addListener(this.goPayClientListener);
    }

    public void setGuid(String str) {
        checkVipStatus(str, false);
    }

    private void setExtraData() {
        Map uids = this.integrationContext.getUids();
        if (uids != null) {
            for (Entry entry : uids.entrySet()) {
                VipClient.INSTANCE.setExtraData(((String) entry.getKey()) + "_uid", (String) entry.getValue());
            }
        }
        VipClient.INSTANCE.setExtraData("gowrap_version", "2.3.24");
        VipClient.INSTANCE.setExtraHeader("X-goWrap-Version", "2.3.24");
    }

    public void checkVipStatus(String str, boolean z) {
        setExtraData();
        VipClient.INSTANCE.checkVipStatus(str, z);
    }

    public void trackPurchase(PurchaseDetails purchaseDetails) {
        setExtraData();
        PurchaseEvent purchaseEvent = new PurchaseEvent();
        purchaseEvent.setReferenceId(purchaseDetails.getReferenceId());
        purchaseEvent.setGuid(this.integrationContext.getGuid());
        purchaseEvent.setProductId(purchaseDetails.getProductId());
        purchaseEvent.setCurrencyCode(purchaseDetails.getCurrencyCode());
        if (purchaseDetails.getPrice() != null) {
            purchaseEvent.setPrice(purchaseDetails.getPrice().doubleValue());
        }
        if (purchaseDetails.getTimestamp() != null) {
            purchaseEvent.setTimestamp(purchaseDetails.getTimestamp().getTime());
        }
        purchaseEvent.setOrderId(purchaseDetails.getOrderId());
        if (purchaseDetails.getVerificationStatus() != null) {
            switch (purchaseDetails.getVerificationStatus()) {
                case NOT_VERIFIED:
                    purchaseEvent.setVerificationStatus(VerificationStatus.NOT_VERIFIED);
                    break;
                case VERIFICATION_SUCCEEDED:
                    purchaseEvent.setVerificationStatus(VerificationStatus.VERIFICATION_SUCCEEDED);
                    break;
                case VERIFICATION_FAILED:
                    purchaseEvent.setVerificationStatus(VerificationStatus.VERIFICATION_FAILED);
                    break;
            }
        }
        purchaseEvent.setSandbox(purchaseDetails.isSandbox());
        VipClient.INSTANCE.trackPurchase(purchaseEvent);
    }

    public void trackSandboxPurchase(PurchaseDetails purchaseDetails) {
        trackPurchase(purchaseDetails);
    }
}
