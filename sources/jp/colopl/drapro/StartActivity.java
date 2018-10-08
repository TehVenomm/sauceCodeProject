package jp.colopl.drapro;

import android.app.Activity;
import android.app.AlertDialog.Builder;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.DialogInterface.OnCancelListener;
import android.content.DialogInterface.OnClickListener;
import android.content.Intent;
import android.content.IntentFilter;
import android.content.SharedPreferences;
import android.content.SharedPreferences.Editor;
import android.content.pm.PackageManager.NameNotFoundException;
import android.content.res.Configuration;
import android.graphics.Color;
import android.net.Uri;
import android.os.Bundle;
import android.os.Handler;
import android.os.PowerManager;
import android.os.PowerManager.WakeLock;
import android.util.Log;
import android.view.KeyEvent;
import android.view.MotionEvent;
import android.view.ViewGroup.LayoutParams;
import android.widget.FrameLayout;
import android.widget.TextView;
import android.widget.Toast;
import com.google.firebase.messaging.MessageForwardingService;
import com.unity3d.player.UnityPlayer;
import java.util.ArrayList;
import java.util.Collections;
import java.util.Iterator;
import java.util.List;
import jp.colopl.config.Config;
import jp.colopl.drapro.ColoplDepositHelper.PostDepositFinishedListener;
import jp.colopl.drapro.ColoplDepositHelper.PostDepositResult;
import jp.colopl.drapro.ColoplDepositHelper.PrepareDepositFinishedListener;
import jp.colopl.drapro.ColoplDepositHelper.PrepareResult;
import jp.colopl.gcm.RegistrarHelper;
import jp.colopl.iab.IabBroadcastReceiver;
import jp.colopl.iab.IabBroadcastReceiver.IabBroadcastListener;
import jp.colopl.iab.IabException;
import jp.colopl.iab.IabHelper;
import jp.colopl.iab.IabHelper.OnConsumeFinishedListener;
import jp.colopl.iab.IabHelper.OnIabPurchaseFinishedListener;
import jp.colopl.iab.IabHelper.OnIabSetupFinishedListener;
import jp.colopl.iab.IabHelper.QueryInventoryFinishedListener;
import jp.colopl.iab.IabResult;
import jp.colopl.iab.Inventory;
import jp.colopl.iab.Purchase;
import jp.colopl.util.Util;
import net.gogame.gowrap.Bootstrap;
import net.gogame.gowrap.Constants;

public class StartActivity extends Activity {
    public static final int AFFILIATE_BROWSER_REQUEST_CODE = 556677;
    private static final String HOST = "gogame.net";
    private static int IABV3_API_RETRY_LIMIT = 3;
    private static final long LONG_PRESS_TIME = 200;
    private static final String SCHEME = "gogamedrapro";
    private static final List<String> SCHEME_ALLOWED_PREFS_KEY_LIST;
    private static final String SCHEME_PLAYER_IS_OPEN_URL = "o";
    private static final String SCHEME_PLAYER_PREFS_KEY_FOR_KEY = "k";
    private static final String SCHEME_PLAYER_PREFS_KEY_FOR_VALUE = "v";
    private static final int SHOW_ACHIEVEMENTLIST_CODE = 1000;
    private static final String TAG = "StartActivity";
    private static long timer = 0;
    private final String ALREADY_SIGNIN_KEY = "have_ever_signin";
    private Config config;
    private int consumptionRetry = 0;
    private int depositRetry = 0;
    private Handler handler = new Handler();
    private IabHelper mBillingHelper = null;
    private boolean mBillingRunning = false;
    IabBroadcastReceiver mBroadcastReceiver;
    private ColoplDepositHelper mColoDepositHelper = null;
    OnConsumeFinishedListener mConsumeFinishedListener = new C09877();
    PostDepositFinishedListener mDepositPostListener = new PostDepositFinishedListener() {
        public void onPostDepositFinished(final PostDepositResult postDepositResult) {
            Util.dLog("StartActivity", "Resultcode:" + postDepositResult.getStatusCode());
            if (postDepositResult.getSuccess() && postDepositResult.isValidStatusCode()) {
                CharSequence format;
                StartActivity.this.depositRetry = 0;
                if (AppConsts.getProductNameById(postDepositResult.getPurchasedSku(), StartActivity.this) != null) {
                    format = String.format(StartActivity.this.getString(StartActivity.this.getResources().getIdentifier("notification_purchase_item", "string", StartActivity.this.getPackageName())), new Object[]{r0});
                } else {
                    format = StartActivity.this.getString(StartActivity.this.getResources().getIdentifier("notification_purchase_item_default", "string", StartActivity.this.getPackageName()));
                }
                Toast.makeText(StartActivity.this, format, 1).show();
                UnityPlayer.UnitySendMessage("ShopReceiver", "buyItem", postDepositResult.getResultData());
                try {
                    InAppBillingHelper.trackPurtraceData(postDepositResult.getPurchasedSku(), postDepositResult.getPurchase().getOriginalJson(), postDepositResult.getPurchase().getSignature());
                } catch (IabException e) {
                    e.printStackTrace();
                }
                StartActivity.this.finishBillingRunning();
            } else if (postDepositResult.getSuccess() && postDepositResult.isAlreadyCancelled()) {
                AppConsts.getProductNameById(postDepositResult.getPurchasedSku(), StartActivity.this);
                Toast.makeText(StartActivity.this, StartActivity.this.getString(StartActivity.this.getResources().getIdentifier("notification_purchase_already_cancelled", "string", StartActivity.this.getPackageName())), 1).show();
                StartActivity.this.finishBillingRunning();
            } else {
                StartActivity.this.depositRetry = StartActivity.this.depositRetry + 1;
                Util.eLog("StartActivity", "[IABV3] Post deposit failed. retrying... : " + StartActivity.this.depositRetry);
                if (StartActivity.this.depositRetry >= StartActivity.IABV3_API_RETRY_LIMIT || postDepositResult.getPurchase() == null) {
                    StartActivity.this.showSimpleDialog(StartActivity.this.getResources().getIdentifier("network_error", "string", StartActivity.this.getPackageName()), StartActivity.this.getResources().getIdentifier("network_error_occurred", "string", StartActivity.this.getPackageName()), StartActivity.this.getResources().getIdentifier("dialog_button_ok", "string", StartActivity.this.getPackageName()));
                    StartActivity.this.finishBillingRunning();
                    return;
                }
                new Handler().post(new Runnable() {
                    public void run() {
                        StartActivity.this.mColoDepositHelper.postDepositAsync(postDepositResult.getPurchase(), StartActivity.this.mDepositPostListener);
                    }
                });
            }
        }
    };
    PrepareDepositFinishedListener mDepositPrepareListener = new C09909();
    QueryInventoryFinishedListener mGotInventoryListener = new C09846();
    ProgressDialog mProgressDialog = null;
    OnIabPurchaseFinishedListener mPurchaseFinishedListener = new C09898();
    ArrayList<Purchase> mPurchaseList = new ArrayList();
    ArrayList<Purchase> mUndepositedPurcahseList = new ArrayList();
    protected UnityPlayer mUnityPlayer;
    private WakeLock mWakeLock;
    private TextView purchaseStatusText;

    /* renamed from: jp.colopl.drapro.StartActivity$1 */
    class C09791 implements IabBroadcastListener {
        C09791() {
        }

        public void receivedBroadcast() {
            Util.eLog("PromoCode", "Receive API Code !!!!!!!!!!");
            UnityPlayer.UnitySendMessage("ShopReceiver", "promoteCheck", "");
        }
    }

    /* renamed from: jp.colopl.drapro.StartActivity$2 */
    class C09802 implements OnIabSetupFinishedListener {
        C09802() {
        }

        public void onIabSetupFinished(IabResult iabResult) {
            if (!iabResult.isSuccess()) {
                Util.eLog("StartActivity", "[IABV3] initBilling problem : " + iabResult);
            }
        }
    }

    /* renamed from: jp.colopl.drapro.StartActivity$3 */
    class C09813 implements Runnable {
        C09813() {
        }

        public void run() {
            StartActivity.this.checkInventory();
        }
    }

    /* renamed from: jp.colopl.drapro.StartActivity$6 */
    class C09846 implements QueryInventoryFinishedListener {
        C09846() {
        }

        public void onQueryInventoryFinished(IabResult iabResult, Inventory inventory) {
            Util.dLog("StartActivity", "[IABV3] onQueryInventoryFinished");
            if (iabResult.isFailure()) {
                Util.eLog("StartActivity", "[IABV3] onQueryInventoryFinished problem : " + iabResult);
                StartActivity.this.finishBillingRunning();
                return;
            }
            int i;
            Purchase purchase;
            Util.dLog("StartActivity", "[IABV3] onQueryInventoryFinished success");
            StartActivity.this.mPurchaseList.clear();
            List<Purchase> allPurchases = inventory.getAllPurchases();
            if (allPurchases == null || allPurchases.size() <= 0) {
                i = 0;
            } else {
                String string = StartActivity.this.getResources().getString(StartActivity.this.getResources().getIdentifier("promo_tag", "string", StartActivity.this.getPackageName()));
                i = 0;
                for (Purchase purchase2 : allPurchases) {
                    if (purchase2.getSku().indexOf(string) != -1) {
                        Util.eLog("StartActivity", "Skip Promotion Item");
                    } else if (StartActivity.verifyDeveloperPayload(purchase2)) {
                        Util.dLog("StartActivity", "[IABV3] onQueryInventoryFinished verifyDeveloperPayload inventoryList success " + purchase2.toLog());
                        i = 1;
                        StartActivity.this.mPurchaseList.add(purchase2);
                    }
                }
            }
            StartActivity.this.mUndepositedPurcahseList.clear();
            ArrayList undepositedPurchase = StartActivity.this.mColoDepositHelper.getUndepositedPurchase();
            if (undepositedPurchase != null && undepositedPurchase.size() > 0) {
                Iterator it = undepositedPurchase.iterator();
                while (it.hasNext()) {
                    purchase2 = (Purchase) it.next();
                    if (StartActivity.verifyDeveloperPayload(purchase2)) {
                        Util.dLog("StartActivity", "[IABV3] onQueryInventoryFinished verifyDeveloperPayload mUndepositedPurcahseList success " + purchase2.toLog());
                        StartActivity.this.mUndepositedPurcahseList.add(purchase2);
                    }
                }
            }
            String productNameById;
            if (StartActivity.this.mUndepositedPurcahseList.size() > 0) {
                productNameById = AppConsts.getProductNameById(((Purchase) StartActivity.this.mUndepositedPurcahseList.get(0)).getSku(), StartActivity.this);
                Util.dLog("StartActivity", "[IABV3] onQueryInventoryFinished mUndepositedPurcahseList productName " + productNameById);
                StartActivity.this.showDepositDialog(productNameById);
            } else if (i != 0) {
                productNameById = inventory.getSkuDetails(((Purchase) StartActivity.this.mPurchaseList.get(0)).getSku()).getTitle();
                Util.dLog("StartActivity", "[IABV3] onQueryInventoryFinished mPurchaseList productName " + productNameById);
                StartActivity.this.showConsumeDialog(productNameById);
            } else {
                StartActivity.this.mColoDepositHelper.prepareDepositAsync(InAppBillingHelper.getProductId(), StartActivity.this.mDepositPrepareListener);
            }
        }
    }

    /* renamed from: jp.colopl.drapro.StartActivity$7 */
    class C09877 implements OnConsumeFinishedListener {
        C09877() {
        }

        public void onConsumeFinished(final Purchase purchase, IabResult iabResult) {
            if (iabResult.isSuccess()) {
                StartActivity.this.consumptionRetry = 0;
                new Handler().post(new Runnable() {
                    public void run() {
                        StartActivity.this.mColoDepositHelper.addUndepositedPurchase(purchase);
                        StartActivity.this.mColoDepositHelper.postDepositAsync(purchase, StartActivity.this.mDepositPostListener);
                    }
                });
                return;
            }
            Util.eLog("StartActivity", "[IABV3] Consumption failed. consume item " + iabResult);
            if (iabResult.getResponse() == 8) {
                StartActivity.this.showSimpleDialog(StartActivity.this.getResources().getIdentifier("network_error", "string", StartActivity.this.getPackageName()), StartActivity.this.getResources().getIdentifier("network_purchase_delay", "string", StartActivity.this.getPackageName()), StartActivity.this.getResources().getIdentifier("ok", "string", StartActivity.this.getPackageName()));
                StartActivity.this.finishBillingRunning();
                return;
            }
            StartActivity.this.consumptionRetry = StartActivity.this.consumptionRetry + 1;
            if (StartActivity.this.consumptionRetry >= StartActivity.IABV3_API_RETRY_LIMIT || purchase == null) {
                StartActivity.this.showSimpleDialog(StartActivity.this.getResources().getIdentifier("network_error", "string", StartActivity.this.getPackageName()), StartActivity.this.getResources().getIdentifier("network_error_occurred", "string", StartActivity.this.getPackageName()), StartActivity.this.getResources().getIdentifier("ok", "string", StartActivity.this.getPackageName()));
                StartActivity.this.finishBillingRunning();
                return;
            }
            Util.eLog("StartActivity", "[IABV3] Consumption finished. retry " + purchase + "  retry=" + StartActivity.this.consumptionRetry);
            new Handler().post(new Runnable() {
                public void run() {
                    StartActivity.this.mBillingHelper.consumeAsync(purchase, StartActivity.this.mConsumeFinishedListener);
                }
            });
        }
    }

    /* renamed from: jp.colopl.drapro.StartActivity$8 */
    class C09898 implements OnIabPurchaseFinishedListener {
        C09898() {
        }

        public void onIabPurchaseFinished(IabResult iabResult, final Purchase purchase) {
            Util.dLog("StartActivity", "[IABV3] Purchase finished: " + iabResult + ", purchase: " + purchase);
            if (iabResult.isCancel()) {
                StartActivity.this.showSimpleDialog(StartActivity.this.getResources().getIdentifier("payment_cancel_title", "string", StartActivity.this.getPackageName()), StartActivity.this.getResources().getIdentifier("payment_cancel_message", "string", StartActivity.this.getPackageName()), StartActivity.this.getResources().getIdentifier("dialog_button_ok", "string", StartActivity.this.getPackageName()));
                StartActivity.this.finishBillingRunning();
            } else if (iabResult.isFailure()) {
                StartActivity.this.showSimpleDialog(StartActivity.this.getResources().getIdentifier("network_error", "string", StartActivity.this.getPackageName()), StartActivity.this.getResources().getIdentifier("network_purchase_error", "string", StartActivity.this.getPackageName()), StartActivity.this.getResources().getIdentifier("dialog_button_ok", "string", StartActivity.this.getPackageName()));
                StartActivity.this.finishBillingRunning();
            } else if (StartActivity.verifyDeveloperPayload(purchase)) {
                new Handler().post(new Runnable() {
                    public void run() {
                        StartActivity.this.mBillingHelper.consumeAsync(purchase, StartActivity.this.mConsumeFinishedListener);
                    }
                });
            } else {
                StartActivity.this.showSimpleDialog(StartActivity.this.getResources().getIdentifier("network_error", "string", StartActivity.this.getPackageName()), StartActivity.this.getResources().getIdentifier("network_purchase_error", "string", StartActivity.this.getPackageName()), StartActivity.this.getResources().getIdentifier("dialog_button_ok", "string", StartActivity.this.getPackageName()));
                StartActivity.this.finishBillingRunning();
            }
        }
    }

    /* renamed from: jp.colopl.drapro.StartActivity$9 */
    class C09909 implements PrepareDepositFinishedListener {
        C09909() {
        }

        public void onPrepareDepositFinished(PrepareResult prepareResult) {
            if (prepareResult.getSuccess()) {
                StartActivity.this.mBillingHelper.launchPurchaseFlow(StartActivity.this, prepareResult.getItemId(), "inapp", 10001, StartActivity.this.mPurchaseFinishedListener, prepareResult.getPayload(), InAppBillingHelper.userIdHash);
                return;
            }
            Util.eLog("StartActivity", "[IABV3] Prepare deposit failed. Show Error.");
            StartActivity.this.showSimpleDialog(prepareResult.getErrorTitle(), prepareResult.getErrorMessage(), StartActivity.this.getResources().getIdentifier("ok", "string", StartActivity.this.getPackageName()));
            StartActivity.this.finishBillingRunning();
        }
    }

    static {
        List arrayList = new ArrayList();
        arrayList.add("im");
        arrayList.add("fc");
        arrayList.add("il");
        arrayList.add("lt");
        arrayList.add("gc");
        arrayList.add("ic");
        SCHEME_ALLOWED_PREFS_KEY_LIST = Collections.unmodifiableList(arrayList);
    }

    private void disposeBilling() {
        if (this.mBillingHelper != null) {
            this.mBillingHelper.dispose();
            this.mBillingHelper = null;
        }
        if (this.mColoDepositHelper != null) {
            this.mColoDepositHelper = null;
        }
    }

    private void finishBillingRunning() {
        dismissProgressDialog2();
        UnityPlayer.UnitySendMessage("ShopReceiver", "buyItem", "");
        this.mBillingRunning = false;
    }

    private void initBilling() {
        Util.dLog("StartActivity", "[IABV3] initBilling start");
        Util.dLog("StartActivity", NetworkHelper.getHost());
        this.mColoDepositHelper = new ColoplDepositHelper(this);
        String appLicenseKey = AppConsts.getAppLicenseKey();
        Util.dLog("StartActivity", "[IABV3] appKey from AppConsts: " + appLicenseKey);
        this.mBillingHelper = new IabHelper(this, appLicenseKey);
        this.mBillingHelper.enableDebugLogging(true);
        InAppBillingHelper.init(this.mBillingHelper, this);
        this.consumptionRetry = 0;
        this.depositRetry = 0;
        this.mBillingRunning = false;
        this.mBillingHelper.startSetup(new C09802());
    }

    private void initPromoCodeReceiver() {
        Util.eLog("PromoCode", "Called initPromoCodeReceiver");
        Intent intent = new Intent("com.android.vending.billing.PURCHASES_UPDATED");
        intent.setPackage(getPackageName());
        sendBroadcast(intent);
        this.mBroadcastReceiver = new IabBroadcastReceiver(new C09791());
        registerReceiver(this.mBroadcastReceiver, new IntentFilter("com.android.vending.billing.PURCHASES_UPDATED"));
    }

    private String loadPlayerPrefs(String str) {
        Context applicationContext = getApplicationContext();
        SharedPreferences sharedPreferences = applicationContext.getSharedPreferences(applicationContext.getPackageName(), 0);
        return sharedPreferences == null ? "" : sharedPreferences.getString(str, "false");
    }

    private void savePlayerPrefs(String str, String str2) {
        Util.dLog("StartActivity", "savePlayerPrefs Key:" + str + " Value:" + str2);
        Context applicationContext = getApplicationContext();
        Editor edit = applicationContext.getSharedPreferences(applicationContext.getPackageName(), 0).edit();
        edit.putString(str, str2);
        edit.commit();
        Editor edit2 = applicationContext.getSharedPreferences(applicationContext.getPackageName() + ".v2.playerprefs", 0).edit();
        edit2.putString(str, str2);
        edit2.commit();
    }

    private void setSchemeParameterToPlayerPrefs() {
        Util.dLog("StartActivity", "intent action name: " + getIntent().getAction());
        if ("android.intent.action.VIEW".equals(getIntent().getAction())) {
            Uri data = getIntent().getData();
            if (data == null) {
                Util.dLog("StartActivity", "intent data is null");
                return;
            }
            Util.dLog("StartActivity", "url=" + data.toString());
            String scheme = data.getScheme();
            String host = data.getHost();
            if (scheme.equals(SCHEME) && host.endsWith(HOST)) {
                Util.dLog("StartActivity", "Start SetSchemeParameter");
                scheme = data.getQueryParameter(SCHEME_PLAYER_PREFS_KEY_FOR_KEY);
                host = data.getQueryParameter(SCHEME_PLAYER_PREFS_KEY_FOR_VALUE);
                Bundle extras = getIntent().getExtras();
                if (extras == null) {
                    extras = new Bundle();
                } else if (extras.getInt(host, 0) == 1) {
                    Util.dLog("StartActivity", "Stop SetSchemeParameter : Used Key");
                    return;
                }
                extras.putInt(host, 1);
                getIntent().putExtras(extras);
                if (scheme != null && !scheme.isEmpty() && host != null && !host.isEmpty() && SCHEME_ALLOWED_PREFS_KEY_LIST.contains(scheme)) {
                    savePlayerPrefs(scheme, host);
                    return;
                }
                return;
            }
            return;
        }
        Util.dLog("StartActivity", "intent action is null");
    }

    private void showSimpleDialog(int i, int i2, int i3) {
        new Builder(this).setTitle(i).setMessage(i2).setPositiveButton(i3, null).create().show();
    }

    public static boolean verifyDeveloperPayload(Purchase purchase) {
        if (purchase == null) {
            return false;
        }
        String developerPayload = purchase.getDeveloperPayload();
        return (developerPayload == null || developerPayload == "") ? false : true;
    }

    public void AcquireWakeLock() {
        if (!this.config.getScreenLockMode() && this.mWakeLock != null) {
            this.mWakeLock.acquire();
        }
    }

    public boolean ReleaseWakeLock() {
        if (this.mWakeLock == null || !this.mWakeLock.isHeld()) {
            return false;
        }
        this.mWakeLock.release();
        return true;
    }

    public void checkInventory() {
        Util.dLog("StartActivity", "[IABV3] checkInventory start querying inventory");
        this.mBillingHelper.queryInventoryAsync(this.mGotInventoryListener);
    }

    public void connect() {
    }

    public boolean dismissProgressDialog2() {
        if (this.mProgressDialog == null) {
            return false;
        }
        this.mProgressDialog.dismiss();
        this.mProgressDialog = null;
        return true;
    }

    public boolean dispatchKeyEvent(KeyEvent keyEvent) {
        return keyEvent.getAction() == 2 ? this.mUnityPlayer.injectEvent(keyEvent) : super.dispatchKeyEvent(keyEvent);
    }

    protected Config getConfig() {
        return ((ColoplApplication) getApplication()).getConfig();
    }

    public void inappbillingStart(String str) {
        if (!this.mBillingRunning) {
            this.mBillingRunning = true;
            this.consumptionRetry = 0;
            this.depositRetry = 0;
            runOnUiThread(new C09813());
        }
    }

    public boolean isConnected() {
        return false;
    }

    protected void onActivityResult(int i, int i2, Intent intent) {
        if (this.mBillingHelper.handleActivityResult(i, i2, intent)) {
            Util.dLog("StartActivity", "onActivityResult handled by IABUtil.");
        } else {
            super.onActivityResult(i, i2, intent);
        }
    }

    public void onConfigurationChanged(Configuration configuration) {
        super.onConfigurationChanged(configuration);
        this.mUnityPlayer.configurationChanged(configuration);
    }

    protected void onCreate(Bundle bundle) {
        requestWindowFeature(1);
        if (this.mUnityPlayer != null) {
            this.mUnityPlayer.quit();
            this.mUnityPlayer = null;
        }
        super.onCreate(bundle);
        getWindow().takeSurface(null);
        getWindow().setFormat(2);
        setContentView(getResources().getIdentifier("main", "layout", getPackageName()));
        this.config = new Config(getApplicationContext());
        this.mWakeLock = ((PowerManager) getSystemService("power")).newWakeLock(10, getClass().getName());
        AppHelper.init(this);
        AppHelper.setConfig(this.config);
        AppConsts.appContext = getApplicationContext();
        AppConsts.versionName = String.valueOf(getConfig().getVersionCode());
        try {
            AppConsts.versionName = getPackageManager().getPackageInfo(getPackageName(), 0).versionName;
            Util.dLog("StartActivity", "VersionName:" + AppConsts.versionName);
        } catch (NameNotFoundException e) {
            e.printStackTrace();
        }
        NetworkHelper.init(this);
        setSchemeParameterToPlayerPrefs();
        FrameLayout frameLayout = (FrameLayout) findViewById(getResources().getIdentifier("UnityLayout", "id", getPackageName()));
        frameLayout.setBackgroundColor(Color.rgb(1, 1, 1));
        this.mUnityPlayer = new UnityPlayer(this);
        if (this.mUnityPlayer.getSettings().getBoolean("hide_status_bar", true)) {
            getWindow().setFlags(1024, 1024);
        }
        try {
            frameLayout.addView(this.mUnityPlayer.getView(), new LayoutParams(-1, -1));
        } catch (Exception e2) {
            Util.dLog("StartActivity", "onCreate");
            e2.printStackTrace();
        }
        initPromoCodeReceiver();
        initBilling();
        this.purchaseStatusText = (TextView) findViewById(getResources().getIdentifier("text_purchase_status", "id", getPackageName()));
        this.purchaseStatusText.setVisibility(8);
        AnalyticsHelper.init(this);
        RegistrarHelper.init(this);
        try {
            Bootstrap.init(this);
        } catch (Throwable e3) {
            Log.e(Constants.TAG, "Exception", e3);
        }
    }

    protected void onDestroy() {
        unregisterReceiver(this.mBroadcastReceiver);
        ReleaseWakeLock();
        disposeBilling();
        this.mUnityPlayer.quit();
        super.onDestroy();
    }

    public boolean onGenericMotionEvent(MotionEvent motionEvent) {
        return this.mUnityPlayer.injectEvent(motionEvent);
    }

    public boolean onKeyDown(int i, KeyEvent keyEvent) {
        if (i == 4) {
            return this.mUnityPlayer.onKeyDown(i, keyEvent);
        }
        if (i != 82) {
            return super.onKeyDown(i, keyEvent);
        }
        if (timer == 0) {
            timer = System.currentTimeMillis();
            return false;
        } else if (System.currentTimeMillis() - timer > LONG_PRESS_TIME) {
            return true;
        } else {
            timer = System.currentTimeMillis();
            return false;
        }
    }

    public boolean onKeyUp(int i, KeyEvent keyEvent) {
        if (i == 82) {
            timer = 0;
        }
        return i == 4 ? this.mUnityPlayer.onKeyUp(i, keyEvent) : super.onKeyUp(i, keyEvent);
    }

    protected void onNewIntent(Intent intent) {
        super.onNewIntent(intent);
        Intent intent2 = new Intent(this, MessageForwardingService.class);
        intent2.setAction(MessageForwardingService.ACTION_REMOTE_INTENT);
        intent2.putExtras(intent);
        startService(intent2);
        setIntent(intent);
    }

    protected void onPause() {
        ReleaseWakeLock();
        super.onPause();
        this.mUnityPlayer.pause();
        AnalyticsHelper.dispatch();
    }

    protected void onResume() {
        super.onResume();
        setSchemeParameterToPlayerPrefs();
        try {
            this.mUnityPlayer.resume();
        } catch (Exception e) {
            Util.dLog("StartActivity", "onStart");
            e.printStackTrace();
        }
        AcquireWakeLock();
    }

    protected void onStart() {
        super.onStart();
        try {
            this.mUnityPlayer.resume();
        } catch (Exception e) {
            Util.dLog("StartActivity", "onStart");
            e.printStackTrace();
        }
        AcquireWakeLock();
    }

    protected void onStop() {
        ReleaseWakeLock();
        super.onStop();
    }

    public boolean onTouchEvent(MotionEvent motionEvent) {
        return this.mUnityPlayer.injectEvent(motionEvent);
    }

    public void onWindowFocusChanged(boolean z) {
        super.onWindowFocusChanged(z);
        this.mUnityPlayer.windowFocusChanged(z);
    }

    public void restoreInventory(final boolean z) {
        if (this.mBillingRunning) {
            Util.eLog("StartActivity", "[IABV3] Billing Process is already running");
            return;
        }
        this.mBillingRunning = true;
        this.consumptionRetry = 0;
        this.depositRetry = 0;
        final QueryInventoryFinishedListener c09824 = new QueryInventoryFinishedListener() {
            public void onQueryInventoryFinished(IabResult iabResult, Inventory inventory) {
                Util.dLog("StartActivity", "[IABV3] onQueryInventoryFinished");
                if (iabResult.isFailure()) {
                    if (z) {
                        StartActivity.this.showSimpleDialog(StartActivity.this.getResources().getIdentifier("payment_check_inventory_error_title", "string", StartActivity.this.getPackageName()), StartActivity.this.getResources().getIdentifier("payment_check_inventory_error_message", "string", StartActivity.this.getPackageName()), StartActivity.this.getResources().getIdentifier("dialog_button_ok", "string", StartActivity.this.getPackageName()));
                    }
                    StartActivity.this.finishBillingRunning();
                    return;
                }
                boolean z;
                Iterator it;
                Purchase purchase;
                StartActivity.this.mPurchaseList.clear();
                List<Purchase> allPurchases = inventory.getAllPurchases();
                if (allPurchases == null || allPurchases.size() <= 0) {
                    z = false;
                } else {
                    z = false;
                    for (Purchase purchase2 : allPurchases) {
                        if (StartActivity.verifyDeveloperPayload(purchase2)) {
                            z = true;
                            StartActivity.this.mPurchaseList.add(purchase2);
                        }
                    }
                }
                StartActivity.this.mUndepositedPurcahseList.clear();
                ArrayList undepositedPurchase = StartActivity.this.mColoDepositHelper.getUndepositedPurchase();
                if (undepositedPurchase != null && undepositedPurchase.size() > 0) {
                    it = undepositedPurchase.iterator();
                    while (it.hasNext()) {
                        purchase2 = (Purchase) it.next();
                        if (StartActivity.verifyDeveloperPayload(purchase2)) {
                            StartActivity.this.mUndepositedPurcahseList.add(purchase2);
                        }
                    }
                }
                Util.dLog("StartActivity", "[IABV3] onQueryInventoryFinished inventory check : has=" + z + " size=" + StartActivity.this.mPurchaseList.size());
                if (StartActivity.this.mUndepositedPurcahseList.size() > 0) {
                    StartActivity.this.showDepositDialog(AppConsts.getProductNameById(((Purchase) StartActivity.this.mUndepositedPurcahseList.get(0)).getSku(), StartActivity.this));
                } else if (z) {
                    StartActivity.this.showConsumeDialog(inventory.getSkuDetails(((Purchase) StartActivity.this.mPurchaseList.get(0)).getSku()).getTitle());
                } else {
                    if (z) {
                        StartActivity.this.showSimpleDialog(StartActivity.this.getResources().getIdentifier("payment_no_purchased_item_title", "string", StartActivity.this.getPackageName()), StartActivity.this.getResources().getIdentifier("payment_no_purchased_item_message", "string", StartActivity.this.getPackageName()), StartActivity.this.getResources().getIdentifier("dialog_button_ok", "string", StartActivity.this.getPackageName()));
                    }
                    StartActivity.this.finishBillingRunning();
                }
                Util.dLog("StartActivity", "[IABV3] enabling dialog to select action.");
            }
        };
        if (z) {
            this.handler.post(new Runnable() {
                public void run() {
                    StartActivity.this.showProgressDialog2(0, StartActivity.this.getResources().getIdentifier("payment_checking_inventory", "string", StartActivity.this.getPackageName()));
                    StartActivity.this.mBillingHelper.queryInventoryAsync(c09824);
                }
            });
            return;
        }
        Util.dLog("StartActivity", "[IABV3] restoreInventory start querying inventory");
        this.mBillingHelper.queryInventoryAsync(c09824);
    }

    public void showAchievementsList() {
    }

    public void showConsumeDialog(String str) {
        int identifier = getResources().getIdentifier("dialog_message_terms_recovery", "string", getPackageName());
        int identifier2 = getResources().getIdentifier("dialog_title_terms_recovery", "string", getPackageName());
        CharSequence format = String.format(getString(identifier), new Object[]{str});
        Builder builder = new Builder(this);
        builder.setTitle(identifier2);
        builder.setMessage(format);
        builder.setNegativeButton(getResources().getIdentifier("dialog_button_close", "string", getPackageName()), new OnClickListener() {
            public void onClick(DialogInterface dialogInterface, int i) {
                StartActivity.this.finishBillingRunning();
            }
        });
        builder.setNeutralButton(getResources().getIdentifier("dialog_button_terms_conditions_check_contract", "string", getPackageName()), new OnClickListener() {
            public void onClick(DialogInterface dialogInterface, int i) {
                StartActivity.this.startActivity(new Intent("android.intent.action.VIEW", Uri.parse(StartActivity.this.getString(StartActivity.this.getResources().getIdentifier("url_terms", "string", StartActivity.this.getPackageName())))));
                StartActivity.this.finishBillingRunning();
            }
        });
        builder.setPositiveButton(getResources().getIdentifier("dialog_button_terms_conditions_exec", "string", getPackageName()), new OnClickListener() {
            public void onClick(DialogInterface dialogInterface, int i) {
                Iterator it = StartActivity.this.mPurchaseList.iterator();
                while (it.hasNext()) {
                    final Purchase purchase = (Purchase) it.next();
                    new Handler().post(new Runnable() {
                        public void run() {
                            StartActivity.this.mBillingHelper.consumeAsync(purchase, StartActivity.this.mConsumeFinishedListener);
                        }
                    });
                }
                StartActivity.this.mPurchaseList.clear();
            }
        });
        builder.setOnCancelListener(new OnCancelListener() {
            public void onCancel(DialogInterface dialogInterface) {
                StartActivity.this.finishBillingRunning();
            }
        });
        builder.create().show();
    }

    public void showDepositDialog(String str) {
        int identifier = getResources().getIdentifier("dialog_message_terms_recovery", "string", getPackageName());
        int identifier2 = getResources().getIdentifier("dialog_title_terms_recovery", "string", getPackageName());
        CharSequence format = String.format(getString(identifier), new Object[]{str});
        Builder builder = new Builder(this);
        builder.setTitle(identifier2);
        builder.setMessage(format);
        builder.setNegativeButton(getResources().getIdentifier("dialog_button_close", "string", getPackageName()), new OnClickListener() {
            public void onClick(DialogInterface dialogInterface, int i) {
                StartActivity.this.finishBillingRunning();
            }
        });
        builder.setNeutralButton(getResources().getIdentifier("dialog_button_terms_conditions_check_contract", "string", getPackageName()), new OnClickListener() {
            public void onClick(DialogInterface dialogInterface, int i) {
                StartActivity.this.startActivity(new Intent("android.intent.action.VIEW", Uri.parse(StartActivity.this.getString(StartActivity.this.getResources().getIdentifier("url_terms", "string", StartActivity.this.getPackageName())))));
                StartActivity.this.finishBillingRunning();
            }
        });
        builder.setPositiveButton(getResources().getIdentifier("dialog_button_terms_conditions_exec", "string", getPackageName()), new OnClickListener() {
            public void onClick(DialogInterface dialogInterface, int i) {
                Iterator it = StartActivity.this.mUndepositedPurcahseList.iterator();
                while (it.hasNext()) {
                    final Purchase purchase = (Purchase) it.next();
                    new Handler().post(new Runnable() {
                        public void run() {
                            StartActivity.this.mColoDepositHelper.postDepositAsync(purchase, StartActivity.this.mDepositPostListener);
                        }
                    });
                }
                StartActivity.this.mUndepositedPurcahseList.clear();
            }
        });
        builder.setOnCancelListener(new OnCancelListener() {
            public void onCancel(DialogInterface dialogInterface) {
                StartActivity.this.finishBillingRunning();
            }
        });
        builder.create().show();
    }

    public void showProgressDialog2(int i, int i2) {
        if (this.mProgressDialog != null) {
            this.mProgressDialog.dismiss();
        }
        this.mProgressDialog = new ProgressDialog(this);
        if (i != 0) {
            this.mProgressDialog.setTitle(i);
        }
        if (i2 != 0) {
            this.mProgressDialog.setMessage(getString(i2));
        }
        this.mProgressDialog.setIndeterminate(true);
        this.mProgressDialog.setProgressStyle(0);
        this.mProgressDialog.setCancelable(false);
        this.mProgressDialog.show();
    }

    public void signin() {
    }

    public void syncUnlockedAchievements(List<String> list) {
    }

    public void unlockAchievement(String str) {
    }
}
