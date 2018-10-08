package net.gogame.gowrap.support;

import android.app.ActivityManager;
import android.app.ActivityManager.MemoryInfo;
import android.content.Context;
import android.os.Build;
import android.os.Build.VERSION;
import android.provider.Settings.Secure;
import android.text.format.Formatter;
import android.util.DisplayMetrics;
import android.util.Patterns;
import android.view.WindowManager;
import io.fabric.sdk.android.services.common.AbstractSpiCall;
import java.io.ByteArrayOutputStream;
import java.io.InputStream;
import java.io.OutputStream;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.Locale;
import jp.colopl.drapro.LocalNotificationAlarmReceiver;
import net.gogame.gowrap.C1110R;
import net.gogame.gowrap.GoWrapImpl;
import net.gogame.gowrap.InternalConstants;
import net.gogame.gowrap.integrations.core.CoreSupport;
import net.gogame.gowrap.integrations.core.Wrapper;
import net.gogame.gowrap.io.utils.IOUtils;
import org.apache.commons.lang3.StringUtils;
import org.json.JSONObject;

public final class SupportManager {
    private SupportManager() {
    }

    public static List<SupportCategory> getCategories() {
        List<SupportCategory> arrayList = new ArrayList();
        arrayList.add(new SupportCategory("billing", C1110R.string.net_gogame_gowrap_support_category_billing));
        arrayList.add(new SupportCategory("connection", C1110R.string.net_gogame_gowrap_support_category_connection));
        arrayList.add(new SupportCategory("game_play", C1110R.string.net_gogame_gowrap_support_category_gameplay));
        arrayList.add(new SupportCategory("feedback", C1110R.string.net_gogame_gowrap_support_category_feedback));
        arrayList.add(new SupportCategory("account_lost", C1110R.string.net_gogame_gowrap_support_category_account_lost));
        return arrayList;
    }

    public static boolean isValid(SupportRequest supportRequest) {
        if (StringUtils.trimToNull(supportRequest.getName()) == null || StringUtils.trimToNull(supportRequest.getEmail()) == null || supportRequest.getCategory() == null || StringUtils.trimToNull(supportRequest.getCategory().getId()) == null || StringUtils.trimToNull(supportRequest.getBody()) == null || !Patterns.EMAIL_ADDRESS.matcher(supportRequest.getEmail()).matches()) {
            return false;
        }
        return true;
    }

    public static Long send(Context context, SupportRequest supportRequest) throws SupportServiceException {
        OutputStream byteArrayOutputStream;
        Long l = null;
        try {
            String string;
            Integer valueOf;
            MultipartUtility multipartUtility = new MultipartUtility(InternalConstants.SUPPORT_ENDPOINT_URL, "UTF-8");
            if (CoreSupport.INSTANCE.getAppId() != null) {
                multipartUtility.addFormField("appId", CoreSupport.INSTANCE.getAppId());
            }
            if (supportRequest.getName() != null) {
                multipartUtility.addFormField("name", supportRequest.getName());
            }
            if (supportRequest.getEmail() != null) {
                multipartUtility.addFormField("email", supportRequest.getEmail());
            }
            if (supportRequest.getMobileNumber() != null) {
                multipartUtility.addFormField("mobileNumber", supportRequest.getMobileNumber());
            }
            if (supportRequest.getCategory() != null) {
                if (supportRequest.getCategory().getId() != null) {
                    string = context.getResources().getString(supportRequest.getCategory().getStringResourceId());
                } else {
                    string = null;
                }
                if (string == null) {
                    string = context.getResources().getString(C1110R.string.net_gogame_gowrap_support_category_default);
                }
                if (string != null) {
                    multipartUtility.addFormField("subject", string);
                }
                if (supportRequest.getCategory().getId() != null) {
                    multipartUtility.addFormField("category", supportRequest.getCategory().getId());
                }
            }
            if (supportRequest.getBody() != null) {
                multipartUtility.addFormField(LocalNotificationAlarmReceiver.EXTRA_BODY, supportRequest.getBody());
            }
            if (GoWrapImpl.INSTANCE.getGuid() != null) {
                multipartUtility.addFormField("guid", GoWrapImpl.INSTANCE.getGuid());
            }
            multipartUtility.addFormField("platform", AbstractSpiCall.ANDROID_CLIENT_TYPE);
            multipartUtility.addFormField("locale", Wrapper.INSTANCE.getCurrentLocale(context));
            multipartUtility.addFormField("extraData", getExtraData(context, supportRequest.getName(), supportRequest.getCategory()));
            if (supportRequest.getAttachment() != null) {
                InputStream openInputStream = context.getContentResolver().openInputStream(supportRequest.getAttachment());
                try {
                    byteArrayOutputStream = new ByteArrayOutputStream();
                    IOUtils.copy(openInputStream, byteArrayOutputStream);
                    multipartUtility.addFilePart("attachment", byteArrayOutputStream.toByteArray());
                    IOUtils.closeQuietly(byteArrayOutputStream);
                    IOUtils.closeQuietly(openInputStream);
                } catch (Throwable th) {
                    IOUtils.closeQuietly(openInputStream);
                }
            }
            JSONObject jSONObject = new JSONObject(multipartUtility.finish());
            if (jSONObject.has("code")) {
                valueOf = Integer.valueOf(jSONObject.getInt("code"));
            } else {
                valueOf = null;
            }
            if (jSONObject.has("message")) {
                string = jSONObject.getString("message");
            } else {
                string = null;
            }
            if (valueOf == null || valueOf.intValue() == 0) {
                if (jSONObject.has("ticketId")) {
                    l = Long.valueOf(jSONObject.getLong("ticketId"));
                }
                return l;
            }
            throw new SupportServiceException(valueOf.intValue(), string);
        } catch (Throwable e) {
            throw new SupportServiceException(e);
        } catch (Throwable e2) {
            throw new SupportServiceException(e2);
        }
    }

    public static String getExtraData(Context context, String str, SupportCategory supportCategory) {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.append("Date/time: " + new Date() + StringUtils.LF);
        if (str != null) {
            stringBuilder.append("Name: " + str + StringUtils.LF);
        }
        stringBuilder.append("Game: " + AppInfo.getAppLabel(context) + StringUtils.LF);
        stringBuilder.append("Version: " + AppInfo.getAppVersion(context) + StringUtils.LF);
        if (GoWrapImpl.INSTANCE.getGuid() != null) {
            stringBuilder.append("Game user ID: " + GoWrapImpl.INSTANCE.getGuid() + StringUtils.LF);
        }
        if (supportCategory != null) {
            stringBuilder.append("Category: " + supportCategory.getId() + StringUtils.LF);
        }
        if (Wrapper.INSTANCE.getCurrentLocale(context) != null) {
            stringBuilder.append("Locale: " + Wrapper.INSTANCE.getCurrentLocale(context) + StringUtils.LF);
        }
        stringBuilder.append("Device ID: " + getDeviceId(context) + StringUtils.LF);
        stringBuilder.append("Mem: " + getMemoryInfo(context) + StringUtils.LF);
        stringBuilder.append("Device Type: " + getDeviceInfo() + StringUtils.LF);
        stringBuilder.append("OS Version: " + getOsVersion() + StringUtils.LF);
        stringBuilder.append("Resolution: " + getScreenResolution(context) + StringUtils.LF);
        return stringBuilder.toString();
    }

    private static String getDeviceId(Context context) {
        return Secure.getString(context.getContentResolver(), "android_id");
    }

    private static String getDeviceInfo() {
        return String.format(Locale.getDefault(), "Brand=%s / Manufacturer=%s / Model=%s / Device=%s", new Object[]{Build.BRAND, Build.MANUFACTURER, Build.MODEL, Build.DEVICE});
    }

    private static String getOsVersion() {
        return String.format(Locale.getDefault(), "Android %s / SDK %d / %s", new Object[]{VERSION.RELEASE, Integer.valueOf(VERSION.SDK_INT), Build.FINGERPRINT});
    }

    private static String getMemoryInfo(Context context) {
        ((ActivityManager) context.getSystemService(LocalNotificationAlarmReceiver.EXTRA_ACTIVITY)).getMemoryInfo(new MemoryInfo());
        if (VERSION.SDK_INT >= 16) {
            return String.format(Locale.getDefault(), "Available=%s, Total=%s", new Object[]{Formatter.formatShortFileSize(context, r1.availMem), Formatter.formatShortFileSize(context, r1.totalMem)});
        }
        return String.format(Locale.getDefault(), "Available=%s", new Object[]{Formatter.formatShortFileSize(context, r1.availMem)});
    }

    private static String getScreenResolution(Context context) {
        WindowManager windowManager = (WindowManager) context.getSystemService("window");
        windowManager.getDefaultDisplay().getMetrics(new DisplayMetrics());
        return String.format(Locale.getDefault(), "%dx%d, DPI: %d, Density: %.2f", new Object[]{Integer.valueOf(r1.widthPixels), Integer.valueOf(r1.heightPixels), Integer.valueOf(r1.densityDpi), Float.valueOf(r1.density)});
    }

    private static String humanReadableByteCount(long j, boolean z) {
        int i = z ? 1000 : 1024;
        if (j < ((long) i)) {
            return j + " B";
        }
        String str = (z ? "kMGTPE" : "KMGTPE").charAt(((int) (Math.log((double) j) / Math.log((double) i))) - 1) + (z ? "" : "i");
        return String.format(Locale.getDefault(), "%.1f %sB", new Object[]{Double.valueOf(((double) j) / Math.pow((double) i, (double) ((int) (Math.log((double) j) / Math.log((double) i))))), str});
    }
}
