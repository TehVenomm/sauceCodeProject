package com.zopim.android.sdk.data;

import android.os.AsyncTask;
import android.util.Log;

/* renamed from: com.zopim.android.sdk.data.i */
class C0871i extends AsyncTask<String, Void, C0870h> {
    /* renamed from: a */
    private static final String f868a = C0871i.class.getSimpleName();

    C0871i() {
    }

    /* renamed from: a */
    private C0870h m699a(String str) {
        String c = m701c(str);
        C0870h b = m700b(str);
        switch (C0872j.f869a[b.ordinal()]) {
            case 1:
                LivechatChatLogPath.getInstance().update(c);
                break;
            case 2:
                LivechatProfilePath.getInstance().update(c);
                break;
            case 3:
                LivechatAgentsPath.getInstance().update(c);
                break;
            case 4:
                LivechatDepartmentsPath.getInstance().update(c);
                break;
            case 5:
                LivechatAccountPath.getInstance().update(c);
                break;
            case 6:
                LivechatFormsPath.getInstance().update(c);
                break;
            case 7:
                ConnectionPath.getInstance().update(c);
                break;
        }
        return b;
    }

    /* renamed from: b */
    private C0870h m700b(String str) {
        if (str == null) {
            return C0870h.UNKNOWN;
        }
        try {
            return C0870h.m698a(str.substring(0, (str.indexOf(";") + ";".length()) - 1));
        } catch (IndexOutOfBoundsException e) {
            Log.w(f868a, "Failed to parse the json message in order to retrieve path name. " + e.getMessage());
            return C0870h.UNKNOWN;
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
    protected C0870h m702a(String... strArr) {
        return m699a(strArr[0]);
    }

    /* renamed from: a */
    protected void m703a(C0870h c0870h) {
    }

    protected /* synthetic */ Object doInBackground(Object[] objArr) {
        return m702a((String[]) objArr);
    }

    protected /* synthetic */ void onPostExecute(Object obj) {
        m703a((C0870h) obj);
    }
}
