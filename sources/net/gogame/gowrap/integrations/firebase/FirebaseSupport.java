package net.gogame.gowrap.integrations.firebase;

import android.app.Activity;
import android.os.Bundle;
import com.google.firebase.analytics.FirebaseAnalytics;
import java.util.Map;
import java.util.Map.Entry;
import net.gogame.gowrap.integrations.AbstractIntegrationSupport;
import net.gogame.gowrap.integrations.CanSetGuid;
import net.gogame.gowrap.integrations.CanTrackEvent;
import net.gogame.gowrap.integrations.CanTrackPurchase;
import net.gogame.gowrap.integrations.Config;
import net.gogame.gowrap.integrations.IntegrationSupport.IntegrationContext;
import net.gogame.gowrap.support.ClassUtils;
import p017io.fabric.sdk.android.services.events.EventsFilesManager;

public class FirebaseSupport extends AbstractIntegrationSupport implements CanSetGuid, CanTrackEvent, CanTrackPurchase {
    public static final String CONFIG_EVENT_NAME_DELIMITER = "eventNameDelimiter";
    private String eventNameDelimiter = EventsFilesManager.ROLL_OVER_FILE_NAME_SEPARATOR;
    private IntegrationContext integrationContext;

    public FirebaseSupport() {
        super("firebase");
    }

    private String toEventName(String str, String str2) {
        if (str == null) {
            return str2;
        }
        return String.format("%s%s%s", new Object[]{str, this.eventNameDelimiter, str2}).replaceAll("\\W", this.eventNameDelimiter);
    }

    public boolean isIntegrated() {
        return ClassUtils.hasClass("com.google.firebase.analytics.FirebaseAnalytics");
    }

    /* access modifiers changed from: protected */
    public void doInit(Activity activity, Config config, IntegrationContext integrationContext2) {
        this.integrationContext = integrationContext2;
        this.eventNameDelimiter = config.getString("eventNameDelimiter", EventsFilesManager.ROLL_OVER_FILE_NAME_SEPARATOR);
    }

    public void setGuid(String str) {
        if (isIntegrated()) {
            FirebaseAnalytics.getInstance(this.integrationContext.getCurrentActivity()).setUserId(str);
        }
    }

    private void doTrackEvent(String str, Bundle bundle) {
        if (isIntegrated()) {
            FirebaseAnalytics.getInstance(this.integrationContext.getCurrentActivity()).logEvent(str, bundle);
        }
    }

    public void trackEvent(String str, String str2) {
        if (isIntegrated()) {
            doTrackEvent(toEventName(str, str2), null);
        }
    }

    public void trackEvent(String str, String str2, long j) {
        if (isIntegrated()) {
            doTrackEvent(toEventName(str, str2), createBundle(str2, Long.valueOf(j)));
        }
    }

    private Bundle createBundle(String str, Long l) {
        Bundle bundle = new Bundle();
        bundle.putLong(str, l.longValue());
        return bundle;
    }

    public void trackEvent(String str, String str2, Map<String, Object> map) {
        if (isIntegrated()) {
            doTrackEvent(toEventName(str, str2), createBundle(map));
        }
    }

    private Bundle createBundle(Map<String, Object> map) {
        Bundle bundle = new Bundle();
        for (Entry entry : map.entrySet()) {
            String str = (String) entry.getKey();
            Object value = entry.getValue();
            if (value instanceof String) {
                bundle.putString(str, (String) value);
            } else if (value instanceof Integer) {
                bundle.putInt(str, ((Integer) value).intValue());
            } else if (value instanceof Long) {
                bundle.putLong(str, ((Long) value).longValue());
            } else if (value instanceof Boolean) {
                bundle.putBoolean(str, ((Boolean) value).booleanValue());
            } else if (value instanceof Float) {
                bundle.putFloat(str, ((Float) value).floatValue());
            } else if (value instanceof Double) {
                bundle.putDouble(str, ((Double) value).doubleValue());
            }
        }
        return bundle;
    }

    public void trackPurchase(String str, String str2, double d) {
    }

    public void trackPurchase(String str, String str2, double d, String str3, String str4) {
    }
}
