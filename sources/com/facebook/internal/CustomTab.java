package com.facebook.internal;

import android.app.Activity;
import android.net.Uri;
import android.os.Bundle;
import android.support.customtabs.CustomTabsIntent;
import android.support.customtabs.CustomTabsIntent.Builder;
import com.appsflyer.share.Constants;
import com.facebook.FacebookSdk;

public class CustomTab {
    private Uri uri;

    public CustomTab(String str, Bundle bundle) {
        if (bundle == null) {
            bundle = new Bundle();
        }
        this.uri = Utility.buildUri(ServerProtocol.getDialogAuthority(), FacebookSdk.getGraphApiVersion() + Constants.URL_PATH_DELIMITER + ServerProtocol.DIALOG_PATH + str, bundle);
    }

    public void openCustomTab(Activity activity, String str) {
        CustomTabsIntent build = new Builder().build();
        build.intent.setPackage(str);
        build.intent.addFlags(1073741824);
        build.launchUrl(activity, this.uri);
    }
}
