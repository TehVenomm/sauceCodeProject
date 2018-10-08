package net.gogame.gowrap.support;

import android.app.AlertDialog;
import android.app.AlertDialog.Builder;
import android.content.ClipData;
import android.content.ClipboardManager;
import android.content.Context;
import android.os.Build.VERSION;
import android.util.Log;
import android.view.View;
import android.view.View.OnLongClickListener;
import android.widget.Toast;
import net.gogame.gowrap.Constants;
import net.gogame.gowrap.GoWrapImpl;
import net.gogame.gowrap.integrations.core.CoreSupport;
import org.apache.commons.lang3.StringUtils;

public final class BuildInfo {
    private BuildInfo() {
    }

    private static String getProperty(Class cls, String str) {
        String str2 = null;
        try {
            Object obj = cls.getField(str).get(null);
            if (obj != null) {
                str2 = obj.toString();
            }
        } catch (NoSuchFieldException e) {
        } catch (IllegalAccessException e2) {
        }
        return str2;
    }

    public static void showBuildInfoDialog(final Context context) {
        try {
            Class cls = Class.forName("net.gogame.gowrap.BuildProperties");
            StringBuilder append = new StringBuilder().append("App package name: ").append(AppInfo.getPackageName(context)).append(StringUtils.LF).append("App version: ").append(AppInfo.getAppVersion(context)).append(StringUtils.LF).append(StringUtils.LF).append("Variant: ").append(CoreSupport.INSTANCE.getVariantId()).append(StringUtils.LF).append("Version: ").append(getProperty(cls, "VERSION")).append(StringUtils.LF).append("Build date: ").append(getProperty(cls, "BUILD_DATE")).append(StringUtils.LF).append(StringUtils.LF).append("Branch: ").append(getProperty(cls, "GIT_BRANCH")).append(StringUtils.LF).append("Commit ID: ").append(getProperty(cls, "GIT_COMMIT_ID_ABBREV")).append(StringUtils.LF).append("Commit date: ").append(getProperty(cls, "GIT_COMMIT_DATE")).append(StringUtils.LF);
            if (GoWrapImpl.INSTANCE.getGuid() != null) {
                append.append(StringUtils.LF).append("GUID: ").append(GoWrapImpl.INSTANCE.getGuid()).append(StringUtils.LF);
            }
            final Object stringBuilder = append.toString();
            AlertDialog show = new Builder(context).setTitle("Info").setMessage(stringBuilder).show();
            if (VERSION.SDK_INT >= 11) {
                show.findViewById(16908290).setOnLongClickListener(new OnLongClickListener() {
                    public boolean onLongClick(View view) {
                        if (VERSION.SDK_INT >= 11) {
                            ((ClipboardManager) context.getSystemService("clipboard")).setPrimaryClip(ClipData.newPlainText("Build info", stringBuilder));
                            Toast.makeText(context, "Build info copied to clipboard", 0).show();
                        }
                        return false;
                    }
                });
            }
        } catch (ClassNotFoundException e) {
            Log.w(Constants.TAG, "Build info not found");
        }
    }
}
