package jp.colopl.drapro;

import android.annotation.SuppressLint;
import android.app.Activity;
import android.app.AlertDialog;
import android.app.AlertDialog.Builder;
import android.content.ClipData;
import android.content.ClipboardManager;
import android.content.Context;
import android.content.DialogInterface;
import android.content.DialogInterface.OnCancelListener;
import android.content.DialogInterface.OnClickListener;
import android.content.Intent;
import android.content.SharedPreferences;
import android.content.SharedPreferences.Editor;
import android.content.pm.ApplicationInfo;
import android.net.Uri;
import android.os.Build.VERSION;
import android.os.Process;
import android.preference.PreferenceManager;
import android.provider.Settings.System;
import android.view.View;
import android.widget.EditText;
import android.widget.Toast;
import com.facebook.internal.ServerProtocol;
import com.google.android.gms.drive.DriveFile;
import java.io.UnsupportedEncodingException;
import java.net.URLEncoder;
import jp.colopl.libs.AssetService;
import jp.colopl.util.Util;

public class AppHelper {
    public static StartActivity activity;
    private static Config config;
    public static boolean isQuitDialogOpened = false;
    private static boolean isShowingDialog;
    private static boolean shopMode = false;

    /* renamed from: jp.colopl.drapro.AppHelper$1 */
    static final class C12761 implements Runnable {

        /* renamed from: jp.colopl.drapro.AppHelper$1$1 */
        class C12731 implements OnClickListener {
            C12731() {
            }

            public void onClick(DialogInterface dialogInterface, int i) {
                AppHelper.isQuitDialogOpened = false;
                Process.killProcess(Process.myPid());
            }
        }

        /* renamed from: jp.colopl.drapro.AppHelper$1$2 */
        class C12742 implements OnClickListener {
            C12742() {
            }

            public void onClick(DialogInterface dialogInterface, int i) {
                Util.dLog(null, "Cancel");
                AppHelper.isQuitDialogOpened = false;
            }
        }

        /* renamed from: jp.colopl.drapro.AppHelper$1$3 */
        class C12753 implements OnCancelListener {
            C12753() {
            }

            public void onCancel(DialogInterface dialogInterface) {
                Util.dLog(null, "Back key Cancel");
                AppHelper.isQuitDialogOpened = false;
            }
        }

        C12761() {
        }

        public void run() {
            int identifier = AppHelper.activity.getResources().getIdentifier("dialog_title_quit_fullname", "string", AppHelper.activity.getPackageName());
            int identifier2 = AppHelper.activity.getResources().getIdentifier("dialog_message_quit", "string", AppHelper.activity.getPackageName());
            int identifier3 = AppHelper.activity.getResources().getIdentifier("dialog_button_quit", "string", AppHelper.activity.getPackageName());
            Builder builder = new Builder(AppHelper.activity);
            builder.setTitle(AppHelper.activity.getString(identifier));
            builder.setMessage(AppHelper.activity.getString(identifier2));
            builder.setPositiveButton(AppHelper.activity.getString(identifier3), new C12731());
            builder.setNegativeButton(AppHelper.activity.getString(AppHelper.activity.getResources().getIdentifier("dialog_button_cancel", "string", AppHelper.activity.getPackageName())), new C12742());
            builder.setOnCancelListener(new C12753());
            builder.create().show();
        }
    }

    /* renamed from: jp.colopl.drapro.AppHelper$3 */
    static final class C12813 implements Runnable {
        C12813() {
        }

        public void run() {
            throw new RuntimeException("Forced runtime exception");
        }
    }

    public static boolean BootAffiliateBrowser(boolean z) {
        SharedPreferences defaultSharedPreferences = PreferenceManager.getDefaultSharedPreferences(activity.getApplicationContext());
        if (defaultSharedPreferences.getBoolean("ALREADY_REFERRER_SENT", false)) {
            return false;
        }
        String encode;
        String str = "";
        try {
            encode = URLEncoder.encode(GetInstallReferrerAtInstall(), "utf-8");
        } catch (UnsupportedEncodingException e) {
            encode = str;
        }
        str = "http://s.colo.pl/ad/" + (z ? "cnt2_drapro.php" : "cnt_drapro.php") + "?referrer=" + encode;
        if (str == null) {
            return false;
        }
        defaultSharedPreferences.edit().putBoolean("ALREADY_REFERRER_SENT", true).commit();
        Intent intent = new Intent("android.intent.action.VIEW", Uri.parse(str));
        intent.addFlags(DriveFile.MODE_READ_ONLY);
        activity.startActivityForResult(intent, StartActivity.AFFILIATE_BROWSER_REQUEST_CODE);
        return true;
    }

    public static boolean CheckReferrerSendToAppBrowser() {
        return false;
    }

    public static int GetDeviceAutoRotateSetting() {
        int i = 0;
        try {
            i = System.getInt(activity.getContentResolver(), "accelerometer_rotation");
        } catch (Exception e) {
        }
        return i;
    }

    public static String GetInstallReferrerAtInstall() {
        return ((ColoplApplication) activity.getApplicationContext()).getConfig().getReferrerAtInstalled();
    }

    public static void GoGooglePlayMyself() {
        activity.startActivity(new Intent("android.intent.action.VIEW", Uri.parse("market://details?id=" + activity.getPackageName())));
    }

    public static void OpenURL(String str) {
        try {
            activity.startActivity(new Intent("android.intent.action.VIEW", Uri.parse(str)));
        } catch (Exception e) {
        }
    }

    public static void ProcessKillCommit() {
        activity.finish();
        Process.killProcess(Process.myPid());
    }

    public static void ShowInvitationCodeView(final String str, final String str2, final String str3) {
        if (!isShowingDialog) {
            isShowingDialog = true;
            activity.runOnUiThread(new Runnable() {

                /* renamed from: jp.colopl.drapro.AppHelper$2$1 */
                class C12771 implements OnClickListener {
                    C12771() {
                    }

                    public void onClick(DialogInterface dialogInterface, int i) {
                        AppHelper.isShowingDialog = false;
                    }
                }

                /* renamed from: jp.colopl.drapro.AppHelper$2$2 */
                class C12782 implements OnClickListener {
                    C12782() {
                    }

                    @SuppressLint({"NewApi"})
                    public void onClick(DialogInterface dialogInterface, int i) {
                        if (VERSION.SDK_INT >= 11) {
                            ((ClipboardManager) AppHelper.activity.getSystemService("clipboard")).setPrimaryClip(ClipData.newPlainText("InvitationCode", str3));
                        } else {
                            ((android.text.ClipboardManager) AppHelper.activity.getSystemService("clipboard")).setText(str3);
                        }
                        AppHelper.isShowingDialog = false;
                        Toast.makeText(AppHelper.activity.getApplicationContext(), AppHelper.activity.getResources().getIdentifier("copy_to_clipboard", "string", AppHelper.activity.getPackageName()), 0).show();
                    }
                }

                /* renamed from: jp.colopl.drapro.AppHelper$2$3 */
                class C12793 implements OnClickListener {
                    C12793() {
                    }

                    public void onClick(DialogInterface dialogInterface, int i) {
                        int identifier = AppHelper.activity.getResources().getIdentifier("invite_mail_title", "string", AppHelper.activity.getPackageName());
                        int identifier2 = AppHelper.activity.getResources().getIdentifier("invite_mail_body", "string", AppHelper.activity.getPackageName());
                        Intent intent = new Intent("android.intent.action.SEND");
                        intent.setType("plain/text");
                        intent.putExtra("android.intent.extra.SUBJECT", identifier);
                        intent.putExtra("android.intent.extra.TEXT", String.format(AppHelper.activity.getString(identifier2), new Object[]{str3}));
                        AppHelper.activity.startActivity(Intent.createChooser(intent, "Mail"));
                        AppHelper.isShowingDialog = false;
                    }
                }

                @SuppressLint({"NewApi"})
                public void run() {
                    View editText = new EditText(AppHelper.activity);
                    editText.setText(str3);
                    editText.setKeyListener(null);
                    Builder builder = new Builder(AppHelper.activity);
                    builder.setTitle(str);
                    builder.setMessage(str2);
                    builder.setView(editText);
                    builder.setNegativeButton(AppHelper.activity.getResources().getIdentifier("dialog_button_cancel", "string", AppHelper.activity.getPackageName()), new C12771());
                    builder.setNeutralButton(AppHelper.activity.getResources().getIdentifier("dialog_button_copy", "string", AppHelper.activity.getPackageName()), new C12782());
                    builder.setPositiveButton(AppHelper.activity.getResources().getIdentifier("invite_mail_button", "string", AppHelper.activity.getPackageName()), new C12793());
                    AlertDialog create = builder.create();
                    create.setCanceledOnTouchOutside(false);
                    create.show();
                }
            });
        }
    }

    public static int checkInstallPackage(String str) {
        for (ApplicationInfo applicationInfo : activity.getPackageManager().getInstalledApplications(128)) {
            if ((applicationInfo.flags & 1) != 1 && str.equals(applicationInfo.packageName)) {
                return 1;
            }
        }
        return 0;
    }

    public static String getLocale() {
        String country = activity.getResources().getConfiguration().locale.getCountry();
        Util.dLog("CLIENT LOCALE", country);
        return country;
    }

    public static int getPurchaseType() {
        return 0;
    }

    public static String getScreenLockMode() {
        return config.getScreenLockMode() ? ServerProtocol.DIALOG_RETURN_SCOPES_TRUE : "false";
    }

    public static boolean getShopMode() {
        return shopMode;
    }

    public static void init(Activity activity) {
        activity = (StartActivity) activity;
    }

    public static int isAdsRemoved() {
        return config.getEnableAdView() ? 0 : 1;
    }

    public static void quit() {
        if (!isQuitDialogOpened) {
            isQuitDialogOpened = true;
            activity.runOnUiThread(new C12761());
        }
    }

    public static void resetPackagePreferences() {
        Context applicationContext = activity.getApplicationContext();
        Editor edit = applicationContext.getSharedPreferences(applicationContext.getPackageName(), 0).edit();
        edit.clear();
        edit.commit();
    }

    public static void setConfig(Config config) {
        config = config;
    }

    public static void setScreenLockMode(String str) {
        boolean equals = str.equals(ServerProtocol.DIALOG_RETURN_SCOPES_TRUE);
        config.setScreenLockMode(equals);
        StartActivity startActivity = activity;
        if (equals) {
            startActivity.ReleaseWakeLock();
        } else {
            startActivity.AcquireWakeLock();
        }
    }

    public static void setShopMode(boolean z) {
        shopMode = z;
    }

    public static int showList(String str) {
        Intent intent = new Intent(AppConsts.appContext, AssetService.class);
        intent.putExtra("asset", str);
        AppConsts.appContext.startService(intent);
        return 0;
    }

    public static void testCrash() {
        new Thread(new C12813()).start();
    }

    public static void trackUserRegEventAppsFlyer(String str) {
    }
}
