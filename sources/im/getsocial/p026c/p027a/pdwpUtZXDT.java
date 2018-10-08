package im.getsocial.p026c.p027a;

import java.util.HashMap;
import java.util.Map;

/* renamed from: im.getsocial.c.a.pdwpUtZXDT */
public abstract class pdwpUtZXDT {
    /* renamed from: a */
    private final zoToeBNOjF f1086a;
    /* renamed from: b */
    private final String f1087b;
    /* renamed from: c */
    private final String f1088c;
    /* renamed from: d */
    private int f1089d = 5000;
    /* renamed from: e */
    private int f1090e = 5000;
    /* renamed from: f */
    private int f1091f = 1000;
    /* renamed from: g */
    private int f1092g = 8;

    /* renamed from: im.getsocial.c.a.pdwpUtZXDT$1 */
    class C09221 implements Runnable {
        /* renamed from: a */
        final /* synthetic */ pdwpUtZXDT f1085a;

        C09221(pdwpUtZXDT pdwputzxdt) {
            this.f1085a = pdwputzxdt;
        }

        /* JADX WARNING: inconsistent code. */
        /* Code decompiled incorrectly, please refer to instructions dump. */
        public void run() {
            /*
            r4 = this;
            r0 = 0;
            r1 = r0;
        L_0x0002:
            if (r1 != 0) goto L_0x000e;
        L_0x0004:
            r0 = r4.f1085a;	 Catch:{ IOException -> 0x00a2, InterruptedException -> 0x00d1 }
            r0 = r0.f1091f;	 Catch:{ IOException -> 0x00a2, InterruptedException -> 0x00d1 }
            r2 = (long) r0;	 Catch:{ IOException -> 0x00a2, InterruptedException -> 0x00d1 }
            java.lang.Thread.sleep(r2);	 Catch:{ IOException -> 0x00a2, InterruptedException -> 0x00d1 }
        L_0x000e:
            r0 = new java.net.URL;	 Catch:{ IOException -> 0x00a2, InterruptedException -> 0x00d1 }
            r2 = r4.f1085a;	 Catch:{ IOException -> 0x00a2, InterruptedException -> 0x00d1 }
            r2 = r2.f1087b;	 Catch:{ IOException -> 0x00a2, InterruptedException -> 0x00d1 }
            r0.<init>(r2);	 Catch:{ IOException -> 0x00a2, InterruptedException -> 0x00d1 }
            r0 = r0.openConnection();	 Catch:{ IOException -> 0x00a2, InterruptedException -> 0x00d1 }
            r0 = (java.net.HttpURLConnection) r0;	 Catch:{ IOException -> 0x00a2, InterruptedException -> 0x00d1 }
            r2 = "GET";
            r0.setRequestMethod(r2);	 Catch:{ IOException -> 0x00a2, InterruptedException -> 0x00d1 }
            r2 = r4.f1085a;	 Catch:{ IOException -> 0x00a2, InterruptedException -> 0x00d1 }
            r2 = r2.f1089d;	 Catch:{ IOException -> 0x00a2, InterruptedException -> 0x00d1 }
            r0.setConnectTimeout(r2);	 Catch:{ IOException -> 0x00a2, InterruptedException -> 0x00d1 }
            r2 = r4.f1085a;	 Catch:{ IOException -> 0x00a2, InterruptedException -> 0x00d1 }
            r2 = r2.f1086a;	 Catch:{ IOException -> 0x00a2, InterruptedException -> 0x00d1 }
            r3 = r4.f1085a;	 Catch:{ IOException -> 0x00a2, InterruptedException -> 0x00d1 }
            r3 = r3.f1088c;	 Catch:{ IOException -> 0x00a2, InterruptedException -> 0x00d1 }
            r2 = r2.m907b(r3);	 Catch:{ IOException -> 0x00a2, InterruptedException -> 0x00d1 }
            if (r2 == 0) goto L_0x0048;
        L_0x003f:
            r3 = "Cookie";
            r2 = r2.toString();	 Catch:{ IOException -> 0x00a2, InterruptedException -> 0x00d1 }
            r0.setRequestProperty(r3, r2);	 Catch:{ IOException -> 0x00a2, InterruptedException -> 0x00d1 }
        L_0x0048:
            r0.connect();	 Catch:{ IOException -> 0x00a2, InterruptedException -> 0x00d1 }
            r2 = r0.getResponseCode();	 Catch:{ IOException -> 0x00a2, InterruptedException -> 0x00d1 }
            r3 = 204; // 0xcc float:2.86E-43 double:1.01E-321;
            if (r2 != r3) goto L_0x007a;
        L_0x0053:
            r0.disconnect();	 Catch:{ IOException -> 0x00a2, InterruptedException -> 0x00d1 }
            r0 = r4.f1085a;	 Catch:{ IOException -> 0x00a2, InterruptedException -> 0x00d1 }
            r0 = r0.f1090e;	 Catch:{ IOException -> 0x00a2, InterruptedException -> 0x00d1 }
            r2 = (long) r0;	 Catch:{ IOException -> 0x00a2, InterruptedException -> 0x00d1 }
            java.lang.Thread.sleep(r2);	 Catch:{ IOException -> 0x00a2, InterruptedException -> 0x00d1 }
            r0 = r4.f1085a;
            r0 = r0.f1092g = r0.f1092g - 1;
            r2 = 1;
            if (r0 > r2) goto L_0x00d3;
        L_0x0069:
            r0 = r4.f1085a;
            r1 = new im.getsocial.c.a.upgqDBbsrL;
            r2 = r4.f1085a;
            r2 = r2.f1087b;
            r1.<init>(r2);
            r0.mo4397a(r1);
        L_0x0079:
            return;
        L_0x007a:
            r1 = 200; // 0xc8 float:2.8E-43 double:9.9E-322;
            if (r2 != r1) goto L_0x00ae;
        L_0x007e:
            r1 = r4.f1085a;	 Catch:{ IOException -> 0x00a2, InterruptedException -> 0x00d1 }
            r2 = r4.f1085a;	 Catch:{ IOException -> 0x00a2, InterruptedException -> 0x00d1 }
            r3 = "Getsocial-Resource";
            r3 = r0.getHeaderField(r3);	 Catch:{ IOException -> 0x00a2, InterruptedException -> 0x00d1 }
            r2 = im.getsocial.p026c.p027a.pdwpUtZXDT.m891a(r2, r3);	 Catch:{ IOException -> 0x00a2, InterruptedException -> 0x00d1 }
            r1.mo4398a(r2);	 Catch:{ IOException -> 0x00a2, InterruptedException -> 0x00d1 }
            r0.disconnect();	 Catch:{ IOException -> 0x00a2, InterruptedException -> 0x00d1 }
            r0 = r4.f1085a;	 Catch:{ IOException -> 0x00a2, InterruptedException -> 0x00d1 }
            r0 = r0.f1086a;	 Catch:{ IOException -> 0x00a2, InterruptedException -> 0x00d1 }
            r1 = r4.f1085a;	 Catch:{ IOException -> 0x00a2, InterruptedException -> 0x00d1 }
            r1 = r1.f1088c;	 Catch:{ IOException -> 0x00a2, InterruptedException -> 0x00d1 }
            r0.m905a(r1);	 Catch:{ IOException -> 0x00a2, InterruptedException -> 0x00d1 }
            goto L_0x0079;
        L_0x00a2:
            r0 = move-exception;
        L_0x00a3:
            r1 = r4.f1085a;
            r2 = new im.getsocial.c.a.upgqDBbsrL;
            r2.<init>(r0);
            r1.mo4397a(r2);
            goto L_0x0069;
        L_0x00ae:
            r0.disconnect();	 Catch:{ IOException -> 0x00a2, InterruptedException -> 0x00d1 }
            r0 = r4.f1085a;	 Catch:{ IOException -> 0x00a2, InterruptedException -> 0x00d1 }
            r0 = r0.f1086a;	 Catch:{ IOException -> 0x00a2, InterruptedException -> 0x00d1 }
            r1 = r4.f1085a;	 Catch:{ IOException -> 0x00a2, InterruptedException -> 0x00d1 }
            r1 = r1.f1088c;	 Catch:{ IOException -> 0x00a2, InterruptedException -> 0x00d1 }
            r0.m905a(r1);	 Catch:{ IOException -> 0x00a2, InterruptedException -> 0x00d1 }
            r0 = r4.f1085a;	 Catch:{ IOException -> 0x00a2, InterruptedException -> 0x00d1 }
            r1 = new im.getsocial.c.a.upgqDBbsrL;	 Catch:{ IOException -> 0x00a2, InterruptedException -> 0x00d1 }
            r3 = r4.f1085a;	 Catch:{ IOException -> 0x00a2, InterruptedException -> 0x00d1 }
            r3 = r3.f1087b;	 Catch:{ IOException -> 0x00a2, InterruptedException -> 0x00d1 }
            r1.<init>(r3, r2);	 Catch:{ IOException -> 0x00a2, InterruptedException -> 0x00d1 }
            r0.mo4397a(r1);	 Catch:{ IOException -> 0x00a2, InterruptedException -> 0x00d1 }
            goto L_0x0069;
        L_0x00d1:
            r0 = move-exception;
            goto L_0x00a3;
        L_0x00d3:
            r0 = r1 + 1;
            r1 = r0;
            goto L_0x0002;
            */
            throw new UnsupportedOperationException("Method not decompiled: im.getsocial.c.a.pdwpUtZXDT.1.run():void");
        }
    }

    public pdwpUtZXDT(zoToeBNOjF zotoebnojf, String str, int i, String str2) {
        this.f1086a = zotoebnojf;
        this.f1087b = str;
        this.f1092g = i;
        this.f1088c = str2;
        new Thread(new C09221(this)).start();
    }

    /* renamed from: a */
    static /* synthetic */ Map m891a(pdwpUtZXDT pdwputzxdt, String str) {
        if (str == null) {
            return null;
        }
        Map hashMap = new HashMap();
        for (String str2 : str.split("\\|")) {
            int indexOf = str2.indexOf("=");
            if (!(indexOf == -1 || indexOf == str2.length() - 1)) {
                hashMap.put(str2.substring(0, indexOf), str2.substring(indexOf + 1));
            }
        }
        return hashMap;
    }

    /* renamed from: a */
    protected abstract void mo4397a(upgqDBbsrL upgqdbbsrl);

    /* renamed from: a */
    protected abstract void mo4398a(Map<String, String> map);
}
