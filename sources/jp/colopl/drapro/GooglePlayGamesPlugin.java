package jp.colopl.drapro;

import android.app.Activity;
import android.app.Dialog;
import android.widget.Toast;
import com.google.android.gms.common.GooglePlayServicesUtil;
import java.util.ArrayList;
import java.util.Arrays;

public class GooglePlayGamesPlugin {
    public static void connect(StartActivity startActivity) {
        startActivity.connect();
    }

    public static boolean isConnected(StartActivity startActivity) {
        return startActivity.isConnected();
    }

    public static boolean isGooglePlayServicesAvailable(Activity activity) {
        int isGooglePlayServicesAvailable = GooglePlayServicesUtil.isGooglePlayServicesAvailable((StartActivity) activity);
        if (isGooglePlayServicesAvailable == 0) {
            return true;
        }
        if (GooglePlayServicesUtil.isUserRecoverableError(isGooglePlayServicesAvailable)) {
            Dialog errorDialog = GooglePlayServicesUtil.getErrorDialog(isGooglePlayServicesAvailable, activity, 0);
            if (errorDialog != null) {
                errorDialog.show();
            }
        } else {
            Toast.makeText(activity, "Google Play Services is not available.", 1).show();
        }
        return false;
    }

    public static void showAchievementsList(StartActivity startActivity) {
        startActivity.showAchievementsList();
    }

    public static void signin(StartActivity startActivity) {
        startActivity.signin();
    }

    public static void syncUnlockedAchievements(StartActivity startActivity, String[] strArr) {
        startActivity.syncUnlockedAchievements(new ArrayList(Arrays.asList(strArr)));
    }

    public static void unlockAchievement(StartActivity startActivity, String str) {
        startActivity.unlockAchievement(str);
    }
}
