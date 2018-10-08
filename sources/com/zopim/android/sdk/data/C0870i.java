package com.zopim.android.sdk.data;

import android.os.AsyncTask;
import android.util.Log;

/* renamed from: com.zopim.android.sdk.data.i */
class C0870i extends AsyncTask<String, Void, C0869h> {
    /* renamed from: a */
    private static final String f868a = C0870i.class.getSimpleName();

    C0870i() {
    }

    /* renamed from: a */
    private com.zopim.android.sdk.data.C0869h m699a(java.lang.String r5) {
        /* JADX: method processing error */
/*
Error: java.util.ConcurrentModificationException
	at java.util.ArrayList$Itr.checkForComodification(ArrayList.java:901)
	at java.util.ArrayList$Itr.next(ArrayList.java:851)
	at jadx.core.dex.visitors.ReSugarCode.getEnumMap(ReSugarCode.java:172)
	at jadx.core.dex.visitors.ReSugarCode.processEnumSwitch(ReSugarCode.java:124)
	at jadx.core.dex.visitors.ReSugarCode.process(ReSugarCode.java:68)
	at jadx.core.dex.visitors.ReSugarCode.visit(ReSugarCode.java:52)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:31)
	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:17)
	at jadx.core.ProcessClass.process(ProcessClass.java:34)
	at jadx.core.ProcessClass.processDependencies(ProcessClass.java:56)
	at jadx.core.ProcessClass.process(ProcessClass.java:39)
	at jadx.api.JadxDecompiler.processClass(JadxDecompiler.java:282)
	at jadx.api.JavaClass.decompile(JavaClass.java:62)
	at jadx.api.JadxDecompiler.lambda$appendSourcesSave$0(JadxDecompiler.java:200)
	at jadx.api.JadxDecompiler$$Lambda$8/1659309731.run(Unknown Source)
*/
        /*
        r4 = this;
        r0 = r4.m701c(r5);
        r1 = r4.m700b(r5);
        r2 = com.zopim.android.sdk.data.C0871j.f869a;
        r3 = r1.ordinal();
        r2 = r2[r3];
        switch(r2) {
            case 1: goto L_0x0014;
            case 2: goto L_0x001c;
            case 3: goto L_0x0024;
            case 4: goto L_0x002c;
            case 5: goto L_0x0034;
            case 6: goto L_0x003c;
            case 7: goto L_0x0044;
            default: goto L_0x0013;
        };
    L_0x0013:
        return r1;
    L_0x0014:
        r2 = com.zopim.android.sdk.data.LivechatChatLogPath.getInstance();
        r2.update(r0);
        goto L_0x0013;
    L_0x001c:
        r2 = com.zopim.android.sdk.data.LivechatProfilePath.getInstance();
        r2.update(r0);
        goto L_0x0013;
    L_0x0024:
        r2 = com.zopim.android.sdk.data.LivechatAgentsPath.getInstance();
        r2.update(r0);
        goto L_0x0013;
    L_0x002c:
        r2 = com.zopim.android.sdk.data.LivechatDepartmentsPath.getInstance();
        r2.update(r0);
        goto L_0x0013;
    L_0x0034:
        r2 = com.zopim.android.sdk.data.LivechatAccountPath.getInstance();
        r2.update(r0);
        goto L_0x0013;
    L_0x003c:
        r2 = com.zopim.android.sdk.data.LivechatFormsPath.getInstance();
        r2.update(r0);
        goto L_0x0013;
    L_0x0044:
        r2 = com.zopim.android.sdk.data.ConnectionPath.getInstance();
        r2.update(r0);
        goto L_0x0013;
        */
        throw new UnsupportedOperationException("Method not decompiled: com.zopim.android.sdk.data.i.a(java.lang.String):com.zopim.android.sdk.data.h");
    }

    /* renamed from: b */
    private C0869h m700b(String str) {
        if (str == null) {
            return C0869h.UNKNOWN;
        }
        try {
            return C0869h.m698a(str.substring(0, (str.indexOf(";") + ";".length()) - 1));
        } catch (IndexOutOfBoundsException e) {
            Log.w(f868a, "Failed to parse the json message in order to retrieve path name. " + e.getMessage());
            return C0869h.UNKNOWN;
        }
    }

    /* renamed from: c */
    private String m701c(String str) {
        if (str == null) {
            return "";
        }
        try {
            return str.substring(str.indexOf(";") + ";".length());
        } catch (IndexOutOfBoundsException e) {
            Log.w(f868a, "Failed to parse the json message in order to retrieve message body. " + e.getMessage());
            return "";
        }
    }

    /* renamed from: a */
    protected C0869h m702a(String... strArr) {
        return m699a(strArr[0]);
    }

    /* renamed from: a */
    protected void m703a(C0869h c0869h) {
    }

    protected /* synthetic */ Object doInBackground(Object[] objArr) {
        return m702a((String[]) objArr);
    }

    protected /* synthetic */ void onPostExecute(Object obj) {
        m703a((C0869h) obj);
    }
}
