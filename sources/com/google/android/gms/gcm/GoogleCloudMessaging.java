package com.google.android.gms.gcm;

import android.app.PendingIntent;
import android.content.Context;
import android.content.Intent;
import android.content.pm.PackageManager.NameNotFoundException;
import android.os.Bundle;
import android.os.Handler;
import android.os.Looper;
import android.os.Message;
import android.os.Messenger;
import android.util.Log;
import com.google.firebase.messaging.MessageForwardingService;
import java.io.IOException;
import java.util.Collections;
import java.util.HashMap;
import java.util.Map;
import java.util.concurrent.BlockingQueue;
import java.util.concurrent.LinkedBlockingQueue;
import java.util.concurrent.TimeUnit;
import java.util.concurrent.atomic.AtomicInteger;
import jp.colopl.gcm.RegistrarHelper;

public class GoogleCloudMessaging {
    public static final String ERROR_MAIN_THREAD = "MAIN_THREAD";
    public static final String ERROR_SERVICE_NOT_AVAILABLE = "SERVICE_NOT_AVAILABLE";
    public static final String MESSAGE_TYPE_DELETED = "deleted_messages";
    public static final String MESSAGE_TYPE_MESSAGE = "gcm";
    public static final String MESSAGE_TYPE_SEND_ERROR = "send_error";
    public static final String MESSAGE_TYPE_SEND_EVENT = "send_event";
    private static String zzaoN = null;
    public static int zzaoO = 5000000;
    public static int zzaoP = 6500000;
    public static int zzaoQ = 7000000;
    static GoogleCloudMessaging zzaoR;
    private static final AtomicInteger zzaoU = new AtomicInteger(1);
    private PendingIntent zzaoS;
    private Map<String, Handler> zzaoT = Collections.synchronizedMap(new HashMap());
    private final BlockingQueue<Intent> zzaoV = new LinkedBlockingQueue();
    final Messenger zzaoW = new Messenger(new Handler(this, Looper.getMainLooper()) {
        final /* synthetic */ GoogleCloudMessaging zzaoX;

        public void handleMessage(Message message) {
            if (message == null || !(message.obj instanceof Intent)) {
                Log.w("GCM", "Dropping invalid message");
            }
            Intent intent = (Intent) message.obj;
            if ("com.google.android.c2dm.intent.REGISTRATION".equals(intent.getAction())) {
                this.zzaoX.zzaoV.add(intent);
            } else if (!this.zzaoX.zzj(intent)) {
                intent.setPackage(this.zzaoX.zzmH.getPackageName());
                this.zzaoX.zzmH.sendBroadcast(intent);
            }
        }
    });
    private Context zzmH;

    public static GoogleCloudMessaging getInstance(Context context) {
        synchronized (GoogleCloudMessaging.class) {
            Class applicationContext;
            try {
                GoogleCloudMessaging googleCloudMessaging;
                if (zzaoR == null) {
                    zzaoR = new GoogleCloudMessaging();
                    googleCloudMessaging = zzaoR;
                    applicationContext = context.getApplicationContext();
                    googleCloudMessaging.zzmH = applicationContext;
                }
                googleCloudMessaging = zzaoR;
                return googleCloudMessaging;
            } finally {
                applicationContext = GoogleCloudMessaging.class;
            }
        }
    }

    private String zza(Intent intent, String str) throws IOException {
        if (intent == null) {
            throw new IOException(ERROR_SERVICE_NOT_AVAILABLE);
        }
        String stringExtra = intent.getStringExtra(str);
        if (stringExtra != null) {
            return stringExtra;
        }
        stringExtra = intent.getStringExtra("error");
        if (stringExtra != null) {
            throw new IOException(stringExtra);
        }
        throw new IOException(ERROR_SERVICE_NOT_AVAILABLE);
    }

    private void zza(String str, String str2, long j, int i, Bundle bundle) throws IOException {
        if (str == null) {
            throw new IllegalArgumentException("Missing 'to'");
        }
        Intent intent = new Intent("com.google.android.gcm.intent.SEND");
        if (bundle != null) {
            intent.putExtras(bundle);
        }
        zzk(intent);
        intent.setPackage(zzae(this.zzmH));
        intent.putExtra("google.to", str);
        intent.putExtra("google.message_id", str2);
        intent.putExtra("google.ttl", Long.toString(j));
        intent.putExtra("google.delay", Integer.toString(i));
        this.zzmH.sendOrderedBroadcast(intent, "com.google.android.gtalkservice.permission.GTALK_SERVICE");
    }

    public static String zzae(Context context) {
        if (zzaoN == null) {
            zzaoN = "com.google.android.gms";
        }
        return zzaoN;
    }

    public static int zzaf(Context context) {
        try {
            return context.getPackageManager().getPackageInfo(zzae(context), 0).versionCode;
        } catch (NameNotFoundException e) {
            return -1;
        }
    }

    private boolean zzj(Intent intent) {
        Object stringExtra = intent.getStringExtra("In-Reply-To");
        if (stringExtra == null && intent.hasExtra("error")) {
            stringExtra = intent.getStringExtra("google.message_id");
        }
        if (stringExtra != null) {
            Handler handler = (Handler) this.zzaoT.remove(stringExtra);
            if (handler != null) {
                Message obtain = Message.obtain();
                obtain.obj = intent;
                return handler.sendMessage(obtain);
            }
        }
        return false;
    }

    private Intent zzs(Bundle bundle) throws IOException {
        if (Looper.getMainLooper() == Looper.myLooper()) {
            throw new IOException(ERROR_MAIN_THREAD);
        } else if (zzaf(this.zzmH) < 0) {
            throw new IOException("Google Play Services missing");
        } else {
            if (bundle == null) {
                bundle = new Bundle();
            }
            Intent intent = new Intent("com.google.android.c2dm.intent.REGISTER");
            intent.setPackage(zzae(this.zzmH));
            zzk(intent);
            intent.putExtra("google.message_id", zzsn());
            intent.putExtras(bundle);
            intent.putExtra("google.messenger", this.zzaoW);
            this.zzmH.startService(intent);
            try {
                return (Intent) this.zzaoV.poll(30000, TimeUnit.MILLISECONDS);
            } catch (InterruptedException e) {
                throw new IOException(e.getMessage());
            }
        }
    }

    private String zzsn() {
        return "google.rpc" + String.valueOf(zzaoU.getAndIncrement());
    }

    public void close() {
        zzso();
    }

    public String getMessageType(Intent intent) {
        if (!MessageForwardingService.ACTION_REMOTE_INTENT.equals(intent.getAction())) {
            return null;
        }
        String stringExtra = intent.getStringExtra("message_type");
        return stringExtra == null ? MESSAGE_TYPE_MESSAGE : stringExtra;
    }

    public String register(String... strArr) throws IOException {
        String zzc;
        synchronized (this) {
            zzc = zzc(strArr);
            Bundle bundle = new Bundle();
            bundle.putString("sender", zzc);
            zzc = zza(zzs(bundle), RegistrarHelper.PROPERTY_REG_ID);
        }
        return zzc;
    }

    public void send(String str, String str2, long j, Bundle bundle) throws IOException {
        zza(str, str2, j, -1, bundle);
    }

    public void send(String str, String str2, Bundle bundle) throws IOException {
        send(str, str2, -1, bundle);
    }

    public void unregister() throws IOException {
        synchronized (this) {
            if (Looper.getMainLooper() == Looper.myLooper()) {
                throw new IOException(ERROR_MAIN_THREAD);
            }
            Intent intent = new Intent("com.google.android.c2dm.intent.UNREGISTER");
            intent.setPackage(zzae(this.zzmH));
            intent.putExtra("google.messenger", this.zzaoW);
            zzk(intent);
            this.zzmH.startService(intent);
            try {
                zza((Intent) this.zzaoV.poll(30000, TimeUnit.MILLISECONDS), "unregistered");
            } catch (InterruptedException e) {
                throw new IOException(e.getMessage());
            }
        }
    }

    String zzc(String... strArr) {
        if (strArr == null || strArr.length == 0) {
            throw new IllegalArgumentException("No senderIds");
        }
        StringBuilder stringBuilder = new StringBuilder(strArr[0]);
        for (int i = 1; i < strArr.length; i++) {
            stringBuilder.append(',').append(strArr[i]);
        }
        return stringBuilder.toString();
    }

    void zzk(Intent intent) {
        synchronized (this) {
            if (this.zzaoS == null) {
                Intent intent2 = new Intent();
                intent2.setPackage("com.google.example.invalidpackage");
                this.zzaoS = PendingIntent.getBroadcast(this.zzmH, 0, intent2, 0);
            }
            intent.putExtra("app", this.zzaoS);
        }
    }

    void zzso() {
        synchronized (this) {
            if (this.zzaoS != null) {
                this.zzaoS.cancel();
                this.zzaoS = null;
            }
        }
    }
}
