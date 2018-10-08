package net.gogame.gowrap.integrations.core;

import android.app.Activity;
import android.content.Context;
import android.util.Log;
import java.io.File;
import java.io.FileNotFoundException;
import java.net.MalformedURLException;
import java.net.URL;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import net.gogame.gowrap.C1426R;
import net.gogame.gowrap.Constants;
import net.gogame.gowrap.InternalConstants;
import net.gogame.gowrap.integrations.core.ServerStatus.LocalizedStatus;
import net.gogame.gowrap.integrations.core.ServerStatus.Status;
import net.gogame.gowrap.integrations.core.ServerStatus.StatusFaqEntry;
import net.gogame.gowrap.io.utils.FileUtils;
import net.gogame.gowrap.model.configuration.Configuration;
import net.gogame.gowrap.model.configuration.Configuration.Integrations.Core.LocaleConfiguration;
import net.gogame.gowrap.support.DownloadUtils;
import net.gogame.gowrap.support.DownloadUtils.Callback;
import net.gogame.gowrap.support.DownloadUtils.FileTarget;
import net.gogame.gowrap.support.JSONUtils;
import net.gogame.gowrap.support.PreferenceUtils;
import net.gogame.gowrap.ui.fab.FabManager;
import org.json.JSONArray;
import org.json.JSONObject;

public class Wrapper {
    public static final Wrapper INSTANCE = new Wrapper();
    private Configuration configuration;
    private ServerStatus serverStatus = null;

    public Configuration getConfiguration() {
        return this.configuration;
    }

    private boolean toBoolean(Boolean bool) {
        return toBoolean(bool, false);
    }

    private boolean toBoolean(Boolean bool, boolean z) {
        return bool == null ? z : bool.booleanValue();
    }

    public LocaleConfiguration getLocaleConfiguration(Context context) {
        return getLocaleConfiguration(getCurrentLocale(context));
    }

    public LocaleConfiguration getLocaleConfiguration(String str) {
        if (this.configuration == null || this.configuration.getIntegrations() == null || this.configuration.getIntegrations().getCore() == null || this.configuration.getIntegrations().getCore().getLocales() == null) {
            return null;
        }
        LocaleConfiguration localeConfiguration = (LocaleConfiguration) this.configuration.getIntegrations().getCore().getLocales().get(str);
        if (localeConfiguration == null) {
            return (LocaleConfiguration) this.configuration.getIntegrations().getCore().getLocales().get("default");
        }
        return localeConfiguration;
    }

    public boolean isSlideOut() {
        return (this.configuration == null || this.configuration.getIntegrations() == null || this.configuration.getIntegrations().getCore() == null || !toBoolean(this.configuration.getIntegrations().getCore().getSlideOut())) ? false : true;
    }

    public boolean isSlideIn() {
        return (this.configuration == null || this.configuration.getIntegrations() == null || this.configuration.getIntegrations().getCore() == null || !toBoolean(this.configuration.getIntegrations().getCore().getSlideIn())) ? false : true;
    }

    public boolean isChatBotEnabled() {
        return (this.configuration == null || this.configuration.getSettings() == null || !toBoolean(this.configuration.getSettings().getChatBotEnabled())) ? false : true;
    }

    public List<String> getSupportedLocales() {
        if (this.configuration == null || this.configuration.getIntegrations() == null || this.configuration.getIntegrations().getCore() == null || this.configuration.getIntegrations().getCore().getSupportedLocales() == null) {
            return null;
        }
        return this.configuration.getIntegrations().getCore().getSupportedLocales();
    }

    public boolean isServerDown() {
        return (this.serverStatus == null || this.serverStatus.getStatus() == Status.OK) ? false : true;
    }

    public ServerStatus getServerStatus() {
        return this.serverStatus;
    }

    public String getCurrentLocale(Context context) {
        String preference = PreferenceUtils.getPreference(context, InternalConstants.LANGUAGE);
        if (preference == null) {
            return "default";
        }
        return preference;
    }

    public void setCurrentLocale(Context context, String str) {
        PreferenceUtils.setPreference(context, InternalConstants.LANGUAGE, str);
    }

    public void setup(final Context context) {
        if (CoreSupport.INSTANCE.getAppId() == null) {
            Log.w(Constants.TAG, "App ID not set");
        }
        File file = new File(context.getFilesDir(), "net/gogame/gowrap/");
        file.mkdirs();
        File file2 = new File(file, InternalConstants.CONFIG_FILENAME);
        if (CoreSupport.INSTANCE.getAppId() == null || !file2.exists()) {
            String str = "net/gogame/gowrap/config.json";
            try {
                FileUtils.gzipCopyFromAsset(context, "net/gogame/gowrap/config.json", file2);
                Log.d(Constants.TAG, "Initialized config.json.gz");
            } catch (FileNotFoundException e) {
                Log.w(Constants.TAG, "Could not copy pre-packaged config file (not found): net/gogame/gowrap/config.json");
            } catch (Throwable e2) {
                Log.e(Constants.TAG, "Could not copy pre-packaged config file", e2);
            }
        } else {
            Log.d(Constants.TAG, "config.json.gz already exists in internal storage");
        }
        if (CoreSupport.INSTANCE.getAppId() != null) {
            try {
                DownloadUtils.download(context, new URL(String.format("%s/config/%s/%s", new Object[]{InternalConstants.BASE_ENDPOINT_URL, CoreSupport.INSTANCE.getAppId(), InternalConstants.CONFIG_FILENAME})), new FileTarget(file2), true, null);
            } catch (MalformedURLException e3) {
            }
        }
        readConfiguration(context);
        if (CoreSupport.INSTANCE.getAppId() != null) {
            try {
                final File file3 = new File(file, InternalConstants.STATUS_FILENAME);
                DownloadUtils.download(context, new URL(String.format("%s/status/%s/%s", new Object[]{InternalConstants.BASE_ENDPOINT_URL, CoreSupport.INSTANCE.getAppId(), InternalConstants.STATUS_FILENAME})), new FileTarget(file3), false, new Callback() {
                    public void onDownloadSucceeded() {
                        try {
                            Wrapper.this.serverStatus = Wrapper.this.parseServerStatus(JSONUtils.read(file3));
                            file3.delete();
                            FabManager.update((Activity) context);
                        } catch (Throwable e) {
                            Log.e(Constants.TAG, "Exception", e);
                        }
                    }

                    public void onDownloadFailed() {
                    }
                });
            } catch (MalformedURLException e4) {
            }
        }
    }

    private ServerStatus parseServerStatus(JSONObject jSONObject) {
        ServerStatus serverStatus = new ServerStatus();
        String optString = jSONObject.optString("status", null);
        if (optString != null) {
            serverStatus.setStatus(Status.valueOf(optString));
        }
        serverStatus.setLocales(new HashMap());
        JSONObject optJSONObject = jSONObject.optJSONObject("locales");
        if (optJSONObject != null) {
            Iterator keys = optJSONObject.keys();
            while (keys.hasNext()) {
                optString = (String) keys.next();
                JSONObject optJSONObject2 = optJSONObject.optJSONObject(optString);
                if (optJSONObject2 != null) {
                    LocalizedStatus localizedStatus = new LocalizedStatus();
                    serverStatus.getLocales().put(optString, localizedStatus);
                    localizedStatus.setTitle(optJSONObject2.optString("title", null));
                    localizedStatus.setMessage(optJSONObject2.optString("message", null));
                    localizedStatus.setUrl(optJSONObject2.optString("url", null));
                    localizedStatus.setFaq(new ArrayList());
                    JSONArray optJSONArray = optJSONObject2.optJSONArray("faq");
                    if (optJSONArray != null) {
                        for (int i = 0; i < optJSONArray.length(); i++) {
                            JSONObject optJSONObject3 = optJSONArray.optJSONObject(i);
                            if (optJSONObject3 != null) {
                                StatusFaqEntry statusFaqEntry = new StatusFaqEntry();
                                statusFaqEntry.setQuestion(optJSONObject3.optString("question", null));
                                statusFaqEntry.setAnswer(optJSONObject3.optString("answer", null));
                                localizedStatus.getFaq().add(statusFaqEntry);
                            }
                        }
                    }
                }
            }
        }
        return serverStatus;
    }

    public void readConfiguration(Context context) {
        try {
            this.configuration = new Configuration(readJson(context, InternalConstants.CONFIG_FILENAME));
            if (this.configuration != null && this.configuration.getIntegrations() != null && this.configuration.getIntegrations().getCore() != null && this.configuration.getIntegrations().getCore().getSupportedLocales() != null) {
                List asList = Arrays.asList(context.getResources().getStringArray(C1426R.array.language_values));
                List arrayList = new ArrayList();
                arrayList.add("default");
                for (String str : this.configuration.getIntegrations().getCore().getSupportedLocales()) {
                    if (asList.contains(str) && !arrayList.contains(str)) {
                        arrayList.add(str);
                    }
                }
                this.configuration.getIntegrations().getCore().setSupportedLocales(arrayList);
            }
        } catch (Throwable e) {
            Log.e(Constants.TAG, "Exception", e);
        }
    }

    private JSONObject readJson(Context context, String str) {
        File file = new File(new File(context.getFilesDir(), "net/gogame/gowrap/"), str);
        if (file.exists() && file.isFile()) {
            try {
                Log.d(Constants.TAG, String.format("Reading %s from internal storage", new Object[]{str}));
                return JSONUtils.read(file);
            } catch (FileNotFoundException e) {
                Log.w(Constants.TAG, "File not found in internal storage:" + e.getMessage());
            } catch (Throwable e2) {
                Log.e(Constants.TAG, "I/O exception", e2);
            } catch (Throwable e22) {
                Log.e(Constants.TAG, "JSON exception", e22);
            }
        }
        try {
            Log.d(Constants.TAG, String.format("Reading %s from assets", new Object[]{str}));
            return JSONUtils.assetRead(context, "net/gogame/gowrap/" + str);
        } catch (FileNotFoundException e3) {
            Log.w(Constants.TAG, "File not found in assets:" + e3.getMessage());
            return null;
        } catch (Throwable e222) {
            Log.e(Constants.TAG, "I/O exception", e222);
            return null;
        } catch (Throwable e2222) {
            Log.e(Constants.TAG, "JSON exception", e2222);
            return null;
        }
    }
}
