package com.zopim.android.sdk.data;

import android.os.AsyncTask;
import android.util.Log;

/* renamed from: com.zopim.android.sdk.data.i */
class C1240i extends AsyncTask<String, Void, C1239h> {

    /* renamed from: a */
    private static final String f912a = C1240i.class.getSimpleName();

    C1240i() {
    }

    /* renamed from: a */
    private C1239h m712a(String str) {
        String c = m714c(str);
        C1239h b = m713b(str);
        switch (C1241j.f913a[b.ordinal()]) {
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
    private C1239h m713b(String str) {
        if (str == null) {
            return C1239h.UNKNOWN;
        }
        try {
            return C1239h.m711a(str.substring(0, (str.indexOf(";") + ";".length()) - 1));
        } catch (IndexOutOfBoundsException e) {
            Log.w(f912a, "Failed to parse the json message in order to retrieve path name. " + e.getMessage());
            return C1239h.UNKNOWN;
        }
    }

    /* renamed from: c */
    private String m714c(String str) {
        if (str == null) {
            return "";
        }
        try {
            return str.substring(str.indexOf(";") + ";".length());
        } catch (IndexOutOfBoundsException e) {
            Log.w(f912a, "Failed to parse the json message in order to retrieve message body. " + e.getMessage());
            return "";
        }
    }

    /* access modifiers changed from: protected */
    /* renamed from: a */
    public C1239h doInBackground(String... strArr) {
        return m712a(strArr[0]);
    }

    /* access modifiers changed from: protected */
    /* renamed from: a */
    public void onPostExecute(C1239h hVar) {
    }
}
