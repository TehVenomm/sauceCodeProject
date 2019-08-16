package com.zopim.android.sdk.api;

import android.content.Context;
import android.os.Handler;
import android.os.Looper;
import android.support.annotation.NonNull;
import android.util.Log;
import android.webkit.WebChromeClient;
import android.webkit.WebView;
import com.zopim.android.sdk.attachment.FileExtension;
import com.zopim.android.sdk.data.LivechatChatLogPath;
import com.zopim.android.sdk.data.WebWidgetListener;
import com.zopim.android.sdk.model.ChatLog;
import com.zopim.android.sdk.model.ChatLog.Rating;
import com.zopim.android.sdk.util.AppInfo;
import java.io.File;
import java.util.Arrays;
import java.util.Locale;
import java.util.concurrent.TimeUnit;
import net.gogame.gowrap.integrations.AbstractIntegrationSupport;
import org.apache.commons.lang3.StringEscapeUtils;

/* renamed from: com.zopim.android.sdk.api.x */
final class C1164x extends C1137a {
    /* access modifiers changed from: private */

    /* renamed from: b */
    public static final String f717b = C1164x.class.getSimpleName();

    /* renamed from: c */
    private static final long f718c = TimeUnit.SECONDS.toMillis(5);

    /* renamed from: a */
    Handler f719a = new Handler(Looper.getMainLooper());
    /* access modifiers changed from: private */

    /* renamed from: d */
    public WebView f720d;

    /* renamed from: e */
    private String f721e;

    /* renamed from: com.zopim.android.sdk.api.x$a */
    class C1165a implements Runnable {

        /* renamed from: b */
        private final WebView f723b;

        C1165a(WebView webView) {
            this.f723b = webView;
        }

        public void run() {
            if (this.f723b != null) {
                this.f723b.stopLoading();
                this.f723b.destroy();
            }
        }
    }

    private C1164x() {
    }

    C1164x(Context context) {
        this.f720d = new WebView(context.getApplicationContext());
        this.f720d.getSettings().setJavaScriptEnabled(true);
        this.f720d.setWebChromeClient(new WebChromeClient());
        WebWidgetListener webWidgetListener = new WebWidgetListener();
        this.f720d.addJavascriptInterface(webWidgetListener, "JSInterface");
        this.f720d.setWebViewClient(webWidgetListener);
        this.f721e = String.format(Locale.US, "%s %s %s", new Object[]{this.f720d.getSettings().getUserAgentString(), String.format(Locale.US, "%s/%s", new Object[]{AppInfo.getApplicationName(context).replaceAll("\\s+", ""), AppInfo.getApplicationVersionName(context)}), String.format(Locale.US, "%s/%s", new Object[]{AppInfo.getChatSdkName().replaceAll("\\s+", ""), AppInfo.getChatSdkVersionName()})});
    }

    /* renamed from: a */
    private void m642a(@NonNull File file) {
        if (m645b(file)) {
            String add = FileTransfers.INSTANCE.add(file);
            if (add == null || add.isEmpty()) {
                Log.w(f717b, "File name is invalid. Will not prepare attachment upload.");
                return;
            }
            m643a(String.format(Locale.US, "javascript:__z_sdk.sendFile('%s', '%s', '%s');", new Object[]{StringEscapeUtils.escapeEcmaScript(add), StringEscapeUtils.escapeEcmaScript(AbstractIntegrationSupport.DEFAULT_EVENT_NAME_DELIMITER + FileExtension.getExtension(file).getValue()), String.valueOf(file.length())}));
        }
    }

    /* renamed from: a */
    private synchronized void m643a(String str) {
        this.f719a.post(new C1166y(this, str));
    }

    /* renamed from: a */
    private void m644a(String str, String str2, String str3, String str4, String str5, String str6, String str7) {
        this.f720d.loadDataWithBaseURL("https://dashboard.zopim.com/bin/", String.format(Locale.US, "<html><head></head><body><script src='%s'></script><script type=\"text/javascript\">%s</script></body></html>", new Object[]{"mobile_sdk.js?" + str, String.format(Locale.US, "window.__z_sdk.initApp({bridge: '%s',register: {mID: '%s',ua: '%s',title: '%s',url: '%s',ref: '%s'}});", new Object[]{str2, str3, str4, str5, str6, str7})}), "text/html", "UTF-8", "");
    }

    /* renamed from: b */
    private boolean m645b(File file) {
        if (file == null || file.getName() == null || file.getName().isEmpty()) {
            Log.w(f717b, "File can not be null or empty");
            return false;
        } else if (file.isDirectory()) {
            Log.w(f717b, "Directory is not supported");
            return false;
        } else if (file.exists()) {
            return true;
        } else {
            Log.w(f717b, "File does not exist");
            return false;
        }
    }

    /* access modifiers changed from: 0000 */
    /* renamed from: a */
    public void mo20635a() {
        m643a("javascript:__z_sdk.sendActive();");
    }

    /* access modifiers changed from: 0000 */
    /* renamed from: a */
    public void mo20636a(String str, String str2, String str3, String str4) {
        if (str2 != null) {
            Log.v(f717b, "Reconnecting to previous chat id: " + str2);
        }
        m644a(str, "jsinterface", str2 != null ? str2 : "", this.f721e, str3, "", str4);
    }

    /* access modifiers changed from: 0000 */
    /* renamed from: a */
    public void mo20637a(String... strArr) {
        if (strArr == null) {
            Log.w(f717b, "Tags must not be null. Will not set tags.");
            return;
        }
        String[] strArr2 = new String[strArr.length];
        for (int i = 0; i < strArr.length; i++) {
            strArr2[i] = StringEscapeUtils.escapeEcmaScript(strArr[i]);
        }
        String arrays = Arrays.toString(strArr2);
        m643a(String.format(Locale.US, "javascript:$zopim.livechat.addTags('%s');", new Object[]{arrays.substring(1, arrays.length() - 1)}));
    }

    /* renamed from: b */
    public void mo20638b() {
        m643a("javascript:__z_sdk.sendButtonClicked();");
    }

    public boolean emailTranscript(String str) {
        if (str == null || str.isEmpty()) {
            Log.w(f717b, "Email address must not be null or empty. Will not email transcript.");
            return false;
        }
        m643a(String.format(Locale.US, "javascript:__z_sdk.sendEmailTranscript('%s');", new Object[]{str}));
        return true;
    }

    public void endChat() {
        m643a("javascript:$zopim.livechat.endChat();");
        m643a("javascript:__z_sdk.sendDisconnectTimeout(1);");
        this.f719a.postDelayed(new C1165a(this.f720d), f718c);
        this.f719a.post(new C1167z(this));
    }

    public void resend(String str) {
        if (str == null || str.isEmpty()) {
            Log.w(f717b, "Message ID must not be null or empty. Will not resend message.");
            return;
        }
        ChatLog chatLog = (ChatLog) LivechatChatLogPath.getInstance().getData().get(str);
        if (chatLog == null) {
            Log.i(f717b, "Could not resend the message. No message with message id = " + str);
        }
        chatLog.setFailed(false);
        m643a(String.format(Locale.US, "javascript:__z_sdk.sendChatMsg('%s', '%s');", new Object[]{StringEscapeUtils.escapeEcmaScript(chatLog.getMessage()), StringEscapeUtils.escapeEcmaScript(str)}));
    }

    public void send(@NonNull File file) {
        if (!m645b(file)) {
            Log.w(f717b, "Could not send file");
        } else {
            m642a(file);
        }
    }

    public void send(String str) {
        if (str == null) {
            Log.w(f717b, "Message must not be null. Will not send message.");
            return;
        }
        m643a(String.format(Locale.US, "javascript:__z_sdk.sendChatMsg('%s');", new Object[]{StringEscapeUtils.escapeEcmaScript(str)}));
    }

    public void sendChatComment(@NonNull String str) {
        if (str == null || str.isEmpty()) {
            Log.w(f717b, "Comment must not be null or empty. Will not comment on this chat.");
            return;
        }
        m643a(String.format(Locale.US, "javascript:__z_sdk.sendChatComment('%s');", new Object[]{StringEscapeUtils.escapeEcmaScript(str)}));
    }

    public void sendChatRating(@NonNull Rating rating) {
        if (rating == null) {
            Log.w(f717b, "Rating must not be null. Will not rate this chat.");
            return;
        }
        switch (rating) {
            case GOOD:
            case BAD:
                m643a(String.format(Locale.US, "javascript:__z_sdk.sendChatRating('%s');", new Object[]{StringEscapeUtils.escapeEcmaScript(rating.getValue())}));
                return;
            case UNRATED:
                m643a(String.format(Locale.US, "javascript:__z_sdk.sendChatRating(null);", new Object[0]));
                return;
            default:
                Log.v(f717b, "Unknown rating " + rating + " will not be sent");
                return;
        }
    }

    public boolean sendOfflineMessage(String str, String str2, String str3) {
        if (str2 == null || str2.isEmpty()) {
            Log.w(f717b, "Email address must not be null or empty. Will not send email.");
            return false;
        } else if (str3 == null || str3.isEmpty()) {
            Log.w(f717b, "Message must not be null or empty. Will not send email.");
            return false;
        } else {
            if (str == null) {
                str = "";
            }
            m643a(String.format(Locale.US, "javascript:__z_sdk.sendOfflineMsg('%s', '%s', '%s');", new Object[]{StringEscapeUtils.escapeEcmaScript(str), StringEscapeUtils.escapeEcmaScript(str2), StringEscapeUtils.escapeEcmaScript(str3)}));
            return true;
        }
    }

    public void setDepartment(String str) {
        if (str == null) {
            Log.w(f717b, "Department must not be null. Will not set department.");
            return;
        }
        m643a(String.format(Locale.US, "javascript:$zopim.livechat.departments.setVisitorDepartment('%s');", new Object[]{StringEscapeUtils.escapeEcmaScript(str)}));
    }

    public void setEmail(String str) {
        if (str == null) {
            Log.w(f717b, "Email must not be null. Will not set email.");
            return;
        }
        m643a(String.format(Locale.US, "javascript:$zopim.livechat.setEmail('%s');", new Object[]{StringEscapeUtils.escapeEcmaScript(str)}));
    }

    public void setName(String str) {
        if (str == null) {
            Log.w(f717b, "Name must not be null. Will not set name.");
            return;
        }
        m643a(String.format(Locale.US, "javascript:$zopim.livechat.setName('%s');", new Object[]{StringEscapeUtils.escapeEcmaScript(str)}));
    }

    public void setPhoneNumber(String str) {
        if (str == null) {
            Log.w(f717b, "Phone number must not be null. Will not set phone number.");
            return;
        }
        m643a(String.format(Locale.US, "javascript:$zopim.livechat.setPhone('%s');", new Object[]{StringEscapeUtils.escapeEcmaScript(str)}));
    }
}
