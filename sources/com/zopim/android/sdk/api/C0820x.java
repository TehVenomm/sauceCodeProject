package com.zopim.android.sdk.api;

import android.content.Context;
import android.os.Handler;
import android.os.Looper;
import android.support.annotation.NonNull;
import android.util.Log;
import android.webkit.WebChromeClient;
import android.webkit.WebView;
import android.webkit.WebViewClient;
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
final class C0820x extends C0796a {
    /* renamed from: b */
    private static final String f675b = C0820x.class.getSimpleName();
    /* renamed from: c */
    private static final long f676c = TimeUnit.SECONDS.toMillis(5);
    /* renamed from: a */
    Handler f677a = new Handler(Looper.getMainLooper());
    /* renamed from: d */
    private WebView f678d;
    /* renamed from: e */
    private String f679e;

    /* renamed from: com.zopim.android.sdk.api.x$a */
    class C0819a implements Runnable {
        /* renamed from: a */
        final /* synthetic */ C0820x f673a;
        /* renamed from: b */
        private final WebView f674b;

        C0819a(C0820x c0820x, WebView webView) {
            this.f673a = c0820x;
            this.f674b = webView;
        }

        public void run() {
            if (this.f674b != null) {
                this.f674b.stopLoading();
                this.f674b.destroy();
            }
        }
    }

    private C0820x() {
    }

    C0820x(Context context) {
        this.f678d = new WebView(context.getApplicationContext());
        this.f678d.getSettings().setJavaScriptEnabled(true);
        this.f678d.setWebChromeClient(new WebChromeClient());
        WebViewClient webWidgetListener = new WebWidgetListener();
        this.f678d.addJavascriptInterface(webWidgetListener, "JSInterface");
        this.f678d.setWebViewClient(webWidgetListener);
        String format = String.format(Locale.US, "%s/%s", new Object[]{AppInfo.getApplicationName(context).replaceAll("\\s+", ""), AppInfo.getApplicationVersionName(context)});
        String format2 = String.format(Locale.US, "%s/%s", new Object[]{AppInfo.getChatSdkName().replaceAll("\\s+", ""), AppInfo.getChatSdkVersionName()});
        this.f679e = String.format(Locale.US, "%s %s %s", new Object[]{this.f678d.getSettings().getUserAgentString(), format, format2});
    }

    /* renamed from: a */
    private void m629a(@NonNull File file) {
        if (m632b(file)) {
            String add = FileTransfers.INSTANCE.add(file);
            if (add == null || add.isEmpty()) {
                Log.w(f675b, "File name is invalid. Will not prepare attachment upload.");
                return;
            }
            add = StringEscapeUtils.escapeEcmaScript(add);
            String escapeEcmaScript = StringEscapeUtils.escapeEcmaScript(AbstractIntegrationSupport.DEFAULT_EVENT_NAME_DELIMITER + FileExtension.getExtension(file).getValue());
            String valueOf = String.valueOf(file.length());
            m630a(String.format(Locale.US, "javascript:__z_sdk.sendFile('%s', '%s', '%s');", new Object[]{add, escapeEcmaScript, valueOf}));
        }
    }

    /* renamed from: a */
    private synchronized void m630a(String str) {
        this.f677a.post(new C0821y(this, str));
    }

    /* renamed from: a */
    private void m631a(String str, String str2, String str3, String str4, String str5, String str6, String str7) {
        String format = String.format(Locale.US, "window.__z_sdk.initApp({bridge: '%s',register: {mID: '%s',ua: '%s',title: '%s',url: '%s',ref: '%s'}});", new Object[]{str2, str3, str4, str5, str6, str7});
        this.f678d.loadDataWithBaseURL("https://dashboard.zopim.com/bin/", String.format(Locale.US, "<html><head></head><body><script src='%s'></script><script type=\"text/javascript\">%s</script></body></html>", new Object[]{"mobile_sdk.js?" + str, format}), "text/html", "UTF-8", "");
    }

    /* renamed from: b */
    private boolean m632b(File file) {
        if (file == null || file.getName() == null || file.getName().isEmpty()) {
            Log.w(f675b, "File can not be null or empty");
            return false;
        } else if (file.isDirectory()) {
            Log.w(f675b, "Directory is not supported");
            return false;
        } else if (file.exists()) {
            return true;
        } else {
            Log.w(f675b, "File does not exist");
            return false;
        }
    }

    /* renamed from: a */
    void mo4233a() {
        m630a("javascript:__z_sdk.sendActive();");
    }

    /* renamed from: a */
    void mo4234a(String str, String str2, String str3, String str4) {
        if (str2 != null) {
            Log.v(f675b, "Reconnecting to previous chat id: " + str2);
        }
        m631a(str, "jsinterface", str2 != null ? str2 : "", this.f679e, str3, "", str4);
    }

    /* renamed from: a */
    void mo4235a(String... strArr) {
        if (strArr == null) {
            Log.w(f675b, "Tags must not be null. Will not set tags.");
            return;
        }
        String[] strArr2 = new String[strArr.length];
        for (int i = 0; i < strArr.length; i++) {
            strArr2[i] = StringEscapeUtils.escapeEcmaScript(strArr[i]);
        }
        String arrays = Arrays.toString(strArr2);
        arrays = arrays.substring(1, arrays.length() - 1);
        m630a(String.format(Locale.US, "javascript:$zopim.livechat.addTags('%s');", new Object[]{arrays}));
    }

    /* renamed from: b */
    public void mo4236b() {
        m630a("javascript:__z_sdk.sendButtonClicked();");
    }

    public boolean emailTranscript(String str) {
        if (str == null || str.isEmpty()) {
            Log.w(f675b, "Email address must not be null or empty. Will not email transcript.");
            return false;
        }
        m630a(String.format(Locale.US, "javascript:__z_sdk.sendEmailTranscript('%s');", new Object[]{str}));
        return true;
    }

    public void endChat() {
        m630a("javascript:$zopim.livechat.endChat();");
        m630a("javascript:__z_sdk.sendDisconnectTimeout(1);");
        this.f677a.postDelayed(new C0819a(this, this.f678d), f676c);
        this.f677a.post(new C0822z(this));
    }

    public void resend(String str) {
        if (str == null || str.isEmpty()) {
            Log.w(f675b, "Message ID must not be null or empty. Will not resend message.");
            return;
        }
        ChatLog chatLog = (ChatLog) LivechatChatLogPath.getInstance().getData().get(str);
        if (chatLog == null) {
            Log.i(f675b, "Could not resend the message. No message with message id = " + str);
        }
        chatLog.setFailed(false);
        String escapeEcmaScript = StringEscapeUtils.escapeEcmaScript(chatLog.getMessage());
        String escapeEcmaScript2 = StringEscapeUtils.escapeEcmaScript(str);
        m630a(String.format(Locale.US, "javascript:__z_sdk.sendChatMsg('%s', '%s');", new Object[]{escapeEcmaScript, escapeEcmaScript2}));
    }

    public void send(@NonNull File file) {
        if (m632b(file)) {
            m629a(file);
        } else {
            Log.w(f675b, "Could not send file");
        }
    }

    public void send(String str) {
        if (str == null) {
            Log.w(f675b, "Message must not be null. Will not send message.");
            return;
        }
        String escapeEcmaScript = StringEscapeUtils.escapeEcmaScript(str);
        m630a(String.format(Locale.US, "javascript:__z_sdk.sendChatMsg('%s');", new Object[]{escapeEcmaScript}));
    }

    public void sendChatComment(@NonNull String str) {
        if (str == null || str.isEmpty()) {
            Log.w(f675b, "Comment must not be null or empty. Will not comment on this chat.");
            return;
        }
        String escapeEcmaScript = StringEscapeUtils.escapeEcmaScript(str);
        m630a(String.format(Locale.US, "javascript:__z_sdk.sendChatComment('%s');", new Object[]{escapeEcmaScript}));
    }

    public void sendChatRating(@NonNull Rating rating) {
        if (rating == null) {
            Log.w(f675b, "Rating must not be null. Will not rate this chat.");
            return;
        }
        switch (aa.f624a[rating.ordinal()]) {
            case 1:
            case 2:
                String escapeEcmaScript = StringEscapeUtils.escapeEcmaScript(rating.getValue());
                m630a(String.format(Locale.US, "javascript:__z_sdk.sendChatRating('%s');", new Object[]{escapeEcmaScript}));
                return;
            case 3:
                m630a(String.format(Locale.US, "javascript:__z_sdk.sendChatRating(null);", new Object[0]));
                return;
            default:
                Log.v(f675b, "Unknown rating " + rating + " will not be sent");
                return;
        }
    }

    public boolean sendOfflineMessage(String str, String str2, String str3) {
        if (str2 == null || str2.isEmpty()) {
            Log.w(f675b, "Email address must not be null or empty. Will not send email.");
            return false;
        } else if (str3 == null || str3.isEmpty()) {
            Log.w(f675b, "Message must not be null or empty. Will not send email.");
            return false;
        } else {
            if (str == null) {
                str = "";
            }
            String escapeEcmaScript = StringEscapeUtils.escapeEcmaScript(str);
            String escapeEcmaScript2 = StringEscapeUtils.escapeEcmaScript(str2);
            String escapeEcmaScript3 = StringEscapeUtils.escapeEcmaScript(str3);
            m630a(String.format(Locale.US, "javascript:__z_sdk.sendOfflineMsg('%s', '%s', '%s');", new Object[]{escapeEcmaScript, escapeEcmaScript2, escapeEcmaScript3}));
            return true;
        }
    }

    public void setDepartment(String str) {
        if (str == null) {
            Log.w(f675b, "Department must not be null. Will not set department.");
            return;
        }
        String escapeEcmaScript = StringEscapeUtils.escapeEcmaScript(str);
        m630a(String.format(Locale.US, "javascript:$zopim.livechat.departments.setVisitorDepartment('%s');", new Object[]{escapeEcmaScript}));
    }

    public void setEmail(String str) {
        if (str == null) {
            Log.w(f675b, "Email must not be null. Will not set email.");
            return;
        }
        String escapeEcmaScript = StringEscapeUtils.escapeEcmaScript(str);
        m630a(String.format(Locale.US, "javascript:$zopim.livechat.setEmail('%s');", new Object[]{escapeEcmaScript}));
    }

    public void setName(String str) {
        if (str == null) {
            Log.w(f675b, "Name must not be null. Will not set name.");
            return;
        }
        String escapeEcmaScript = StringEscapeUtils.escapeEcmaScript(str);
        m630a(String.format(Locale.US, "javascript:$zopim.livechat.setName('%s');", new Object[]{escapeEcmaScript}));
    }

    public void setPhoneNumber(String str) {
        if (str == null) {
            Log.w(f675b, "Phone number must not be null. Will not set phone number.");
            return;
        }
        String escapeEcmaScript = StringEscapeUtils.escapeEcmaScript(str);
        m630a(String.format(Locale.US, "javascript:$zopim.livechat.setPhone('%s');", new Object[]{escapeEcmaScript}));
    }
}
