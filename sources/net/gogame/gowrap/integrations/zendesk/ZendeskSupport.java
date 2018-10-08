package net.gogame.gowrap.integrations.zendesk;

import android.app.Activity;
import android.util.Log;
import java.io.File;
import java.io.FileNotFoundException;
import java.net.MalformedURLException;
import java.net.URL;
import net.gogame.gowrap.Constants;
import net.gogame.gowrap.InternalConstants;
import net.gogame.gowrap.integrations.AbstractIntegrationSupport;
import net.gogame.gowrap.integrations.Config;
import net.gogame.gowrap.integrations.IntegrationSupport.IntegrationContext;
import net.gogame.gowrap.integrations.core.CoreSupport;
import net.gogame.gowrap.io.utils.FileUtils;
import net.gogame.gowrap.support.DownloadUtils;
import net.gogame.gowrap.support.DownloadUtils.FileTarget;

public class ZendeskSupport extends AbstractIntegrationSupport {
    public static final ZendeskSupport INSTANCE = new ZendeskSupport();

    private ZendeskSupport() {
        super("zendesk");
    }

    public boolean isIntegrated() {
        return true;
    }

    protected void doInit(Activity activity, Config config, IntegrationContext integrationContext) {
        if (CoreSupport.INSTANCE.getAppId() == null) {
            Log.w(Constants.TAG, "App ID not set");
        }
        File file = new File(activity.getFilesDir(), "net/gogame/gowrap/");
        file.mkdirs();
        File file2 = new File(file, InternalConstants.FAQ_FILENAME);
        if (CoreSupport.INSTANCE.getAppId() == null || !file2.exists()) {
            String str = "net/gogame/gowrap/faq.json";
            try {
                FileUtils.gzipCopyFromAsset(activity, "net/gogame/gowrap/faq.json", file2);
                Log.d(Constants.TAG, "Initialized faq.json.gz");
            } catch (FileNotFoundException e) {
                Log.w(Constants.TAG, "Could not copy pre-packaged FAQ file (not found): net/gogame/gowrap/faq.json");
            } catch (Throwable e2) {
                Log.e(Constants.TAG, "Could not copy pre-packaged FAQ file", e2);
            }
        } else {
            Log.d(Constants.TAG, "faq.json.gz already exists in internal storage");
        }
        if (CoreSupport.INSTANCE.getAppId() != null) {
            try {
                DownloadUtils.download(activity, new URL(String.format("%s/zendesk/%s/%s", new Object[]{InternalConstants.BASE_ENDPOINT_URL, CoreSupport.INSTANCE.getAppId(), InternalConstants.FAQ_FILENAME})), new FileTarget(file2), true, null);
            } catch (MalformedURLException e3) {
            }
        }
    }
}
