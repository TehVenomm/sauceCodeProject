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
import com.facebook.share.internal.MessengerShareContentUtility;
import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.Locale;
import net.gogame.gowrap.C1423R;
import net.gogame.gowrap.GoWrapImpl;
import net.gogame.gowrap.InternalConstants;
import net.gogame.gowrap.integrations.core.CoreSupport;
import net.gogame.gowrap.integrations.core.Wrapper;
import net.gogame.gowrap.p021io.utils.IOUtils;
import org.apache.commons.lang3.StringUtils;
import org.json.JSONException;
import org.json.JSONObject;
import p018jp.colopl.drapro.LocalNotificationAlarmReceiver;

public final class SupportManager {
    private SupportManager() {
    }

    public static List<SupportCategory> getCategories() {
        ArrayList arrayList = new ArrayList();
        arrayList.add(new SupportCategory("billing", C1423R.string.net_gogame_gowrap_support_category_billing));
        arrayList.add(new SupportCategory("connection", C1423R.string.net_gogame_gowrap_support_category_connection));
        arrayList.add(new SupportCategory("game_play", C1423R.string.net_gogame_gowrap_support_category_gameplay));
        arrayList.add(new SupportCategory("feedback", C1423R.string.net_gogame_gowrap_support_category_feedback));
        arrayList.add(new SupportCategory("account_lost", C1423R.string.net_gogame_gowrap_support_category_account_lost));
        return arrayList;
    }

    public static boolean isValid(SupportRequest supportRequest) {
        if (StringUtils.trimToNull(supportRequest.getName()) == null || StringUtils.trimToNull(supportRequest.getEmail()) == null || supportRequest.getCategory() == null || StringUtils.trimToNull(supportRequest.getCategory().getId()) == null || StringUtils.trimToNull(supportRequest.getBody()) == null || !Patterns.EMAIL_ADDRESS.matcher(supportRequest.getEmail()).matches()) {
            return false;
        }
        return true;
    }

    public static Long send(Context context, SupportRequest supportRequest) throws SupportServiceException {
        Integer num;
        String str;
        ByteArrayOutputStream byteArrayOutputStream;
        String str2;
        try {
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
                    str2 = context.getResources().getString(supportRequest.getCategory().getStringResourceId());
                } else {
                    str2 = null;
                }
                if (str2 == null) {
                    str2 = context.getResources().getString(C1423R.string.net_gogame_gowrap_support_category_default);
                }
                if (str2 != null) {
                    multipartUtility.addFormField("subject", str2);
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
            multipartUtility.addFormField("platform", "android");
            multipartUtility.addFormField("locale", Wrapper.INSTANCE.getCurrentLocale(context));
            multipartUtility.addFormField("extraData", getExtraData(context, supportRequest.getName(), supportRequest.getCategory()));
            if (supportRequest.getAttachment() != null) {
                InputStream openInputStream = context.getContentResolver().openInputStream(supportRequest.getAttachment());
                try {
                    byteArrayOutputStream = new ByteArrayOutputStream();
                    IOUtils.copy(openInputStream, byteArrayOutputStream);
                    multipartUtility.addFilePart(MessengerShareContentUtility.ATTACHMENT, byteArrayOutputStream.toByteArray());
                    IOUtils.closeQuietly((OutputStream) byteArrayOutputStream);
                    IOUtils.closeQuietly(openInputStream);
                } catch (Throwable th) {
                    IOUtils.closeQuietly(openInputStream);
                    throw th;
                }
            }
            JSONObject jSONObject = new JSONObject(multipartUtility.finish());
            if (jSONObject.has("code")) {
                num = Integer.valueOf(jSONObject.getInt("code"));
            } else {
                num = null;
            }
            if (jSONObject.has("message")) {
                str = jSONObject.getString("message");
            } else {
                str = null;
            }
            if (num != null && num.intValue() != 0) {
                throw new SupportServiceException(num.intValue(), str);
            } else if (jSONObject.has("ticketId")) {
                return Long.valueOf(jSONObject.getLong("ticketId"));
            } else {
                return null;
            }
        } catch (IOException e) {
            throw new SupportServiceException(e);
        } catch (JSONException e2) {
            throw new SupportServiceException(e2);
        }
    }

    public static String getExtraData(Context context, String str, SupportCategory supportCategory) {
        StringBuilder sb = new StringBuilder();
        sb.append("Date/time: " + new Date() + StringUtils.f1189LF);
        if (str != null) {
            sb.append("Name: " + str + StringUtils.f1189LF);
        }
        sb.append("Game: " + AppInfo.getAppLabel(context) + StringUtils.f1189LF);
        sb.append("Version: " + AppInfo.getAppVersion(context) + StringUtils.f1189LF);
        if (GoWrapImpl.INSTANCE.getGuid() != null) {
            sb.append("Game user ID: " + GoWrapImpl.INSTANCE.getGuid() + StringUtils.f1189LF);
        }
        if (supportCategory != null) {
            sb.append("Category: " + supportCategory.getId() + StringUtils.f1189LF);
        }
        if (Wrapper.INSTANCE.getCurrentLocale(context) != null) {
            sb.append("Locale: " + Wrapper.INSTANCE.getCurrentLocale(context) + StringUtils.f1189LF);
        }
        sb.append("Device ID: " + getDeviceId(context) + StringUtils.f1189LF);
        sb.append("Mem: " + getMemoryInfo(context) + StringUtils.f1189LF);
        sb.append("Device Type: " + getDeviceInfo() + StringUtils.f1189LF);
        sb.append("OS Version: " + getOsVersion() + StringUtils.f1189LF);
        sb.append("Resolution: " + getScreenResolution(context) + StringUtils.f1189LF);
        return sb.toString();
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
        MemoryInfo memoryInfo = new MemoryInfo();
        ((ActivityManager) context.getSystemService(LocalNotificationAlarmReceiver.EXTRA_ACTIVITY)).getMemoryInfo(memoryInfo);
        if (VERSION.SDK_INT >= 16) {
            return String.format(Locale.getDefault(), "Available=%s, Total=%s", new Object[]{Formatter.formatShortFileSize(context, memoryInfo.availMem), Formatter.formatShortFileSize(context, memoryInfo.totalMem)});
        }
        return String.format(Locale.getDefault(), "Available=%s", new Object[]{Formatter.formatShortFileSize(context, memoryInfo.availMem)});
    }

    private static String getScreenResolution(Context context) {
        WindowManager windowManager = (WindowManager) context.getSystemService("window");
        DisplayMetrics displayMetrics = new DisplayMetrics();
        windowManager.getDefaultDisplay().getMetrics(displayMetrics);
        return String.format(Locale.getDefault(), "%dx%d, DPI: %d, Density: %.2f", new Object[]{Integer.valueOf(displayMetrics.widthPixels), Integer.valueOf(displayMetrics.heightPixels), Integer.valueOf(displayMetrics.densityDpi), Float.valueOf(displayMetrics.density)});
    }

    private static String humanReadableByteCount(long j, boolean z) {
        int i = z ? 1000 : 1024;
        if (j < ((long) i)) {
            return j + " B";
        }
        int log = (int) (Math.log((double) j) / Math.log((double) i));
        return String.format(Locale.getDefault(), "%.1f %sB", new Object[]{Double.valueOf(((double) j) / Math.pow((double) i, (double) log)), (z ? "kMGTPE" : "KMGTPE").charAt(log - 1) + (z ? "" : "i")});
    }
}
