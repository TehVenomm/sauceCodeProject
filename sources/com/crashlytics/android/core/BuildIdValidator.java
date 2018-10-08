package com.crashlytics.android.core;

import android.util.Log;
import io.fabric.sdk.android.Fabric;
import io.fabric.sdk.android.services.common.CommonUtils;
import net.gogame.gowrap.integrations.AbstractIntegrationSupport;

class BuildIdValidator {
    private static final String MESSAGE = "This app relies on Crashlytics. Please sign up for access at https://fabric.io/sign_up,\ninstall an Android build tool and ask a team member to invite you to this app's organization.";
    private final String buildId;
    private final boolean requiringBuildId;

    public BuildIdValidator(String str, boolean z) {
        this.buildId = str;
        this.requiringBuildId = z;
    }

    protected String getMessage(String str, String str2) {
        return MESSAGE;
    }

    public void validate(String str, String str2) {
        if (CommonUtils.isNullOrEmpty(this.buildId) && this.requiringBuildId) {
            String message = getMessage(str, str2);
            Log.e("Fabric", AbstractIntegrationSupport.DEFAULT_EVENT_NAME_DELIMITER);
            Log.e("Fabric", ".     |  | ");
            Log.e("Fabric", ".     |  |");
            Log.e("Fabric", ".     |  |");
            Log.e("Fabric", ".   \\ |  | /");
            Log.e("Fabric", ".    \\    /");
            Log.e("Fabric", ".     \\  /");
            Log.e("Fabric", ".      \\/");
            Log.e("Fabric", AbstractIntegrationSupport.DEFAULT_EVENT_NAME_DELIMITER);
            Log.e("Fabric", message);
            Log.e("Fabric", AbstractIntegrationSupport.DEFAULT_EVENT_NAME_DELIMITER);
            Log.e("Fabric", ".      /\\");
            Log.e("Fabric", ".     /  \\");
            Log.e("Fabric", ".    /    \\");
            Log.e("Fabric", ".   / |  | \\");
            Log.e("Fabric", ".     |  |");
            Log.e("Fabric", ".     |  |");
            Log.e("Fabric", ".     |  |");
            Log.e("Fabric", AbstractIntegrationSupport.DEFAULT_EVENT_NAME_DELIMITER);
            throw new CrashlyticsMissingDependencyException(message);
        } else if (!this.requiringBuildId) {
            Fabric.getLogger().mo4289d("Fabric", "Configured not to require a build ID.");
        }
    }
}
