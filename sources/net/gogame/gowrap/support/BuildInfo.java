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
        try {
            Object obj = cls.getField(str).get(null);
            if (obj == null) {
                return null;
            }
            return obj.toString();
        } catch (IllegalAccessException | NoSuchFieldException e) {
            return null;
        }
    }

    public static void showBuildInfoDialog(final Context context) {
        try {
            Class cls = Class.forName("net.gogame.gowrap.BuildProperties");
            StringBuilder append = new StringBuilder().append("App package name: ").append(AppInfo.getPackageName(context)).append(StringUtils.f1189LF).append("App version: ").append(AppInfo.getAppVersion(context)).append(StringUtils.f1189LF).append(StringUtils.f1189LF).append("Variant: ").append(CoreSupport.INSTANCE.getVariantId()).append(StringUtils.f1189LF).append("Version: ").append(getProperty(cls, "VERSION")).append(StringUtils.f1189LF).append("Build date: ").append(getProperty(cls, "BUILD_DATE")).append(StringUtils.f1189LF).append(StringUtils.f1189LF).append("Branch: ").append(getProperty(cls, "GIT_BRANCH")).append(StringUtils.f1189LF).append("Commit ID: ").append(getProperty(cls, "GIT_COMMIT_ID_ABBREV")).append(StringUtils.f1189LF).append("Commit date: ").append(getProperty(cls, "GIT_COMMIT_DATE")).append(StringUtils.f1189LF);
            if (GoWrapImpl.INSTANCE.getGuid() != null) {
                append.append(StringUtils.f1189LF).append("GUID: ").append(GoWrapImpl.INSTANCE.getGuid()).append(StringUtils.f1189LF);
            }
            final String sb = append.toString();
            AlertDialog show = new Builder(context).setTitle("Info").setMessage(sb).show();
            if (VERSION.SDK_INT >= 11) {
                show.findViewById(16908290).setOnLongClickListener(new OnLongClickListener() {
                    public boolean onLongClick(View view) {
                        if (VERSION.SDK_INT >= 11) {
                            ((ClipboardManager) context.getSystemService("clipboard")).setPrimaryClip(ClipData.newPlainText("Build info", sb));
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
