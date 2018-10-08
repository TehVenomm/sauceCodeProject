package im.getsocial.p026c.p027a;

/* renamed from: im.getsocial.c.a.ztWNWCuZiM */
public abstract class ztWNWCuZiM {
    /* renamed from: a */
    private int[] f1097a = new int[]{500, 1000, 2000, 3000};

    /* renamed from: a */
    protected abstract void mo4399a();

    /* renamed from: a */
    public final void m909a(int i) {
        this.f1097a = new int[2];
        for (int i2 = 0; i2 < 2; i2++) {
            this.f1097a[i2] = (i2 + 1) * 500;
        }
    }

    /* JADX WARNING: inconsistent code. */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    /* renamed from: b */
    public final boolean m910b() {
        /*
        r4 = this;
        r0 = -1;
    L_0x0001:
        r0 = r0 + 1;
        r4.mo4399a();	 Catch:{ cjrhisSQCL -> 0x0008, IOException -> 0x0016 }
        r0 = 1;
    L_0x0007:
        return r0;
    L_0x0008:
        r1 = move-exception;
        r2 = r1.m882b();
        if (r2 != 0) goto L_0x0010;
    L_0x000f:
        throw r1;
    L_0x0010:
        r2 = r4.f1097a;
        r2 = r2.length;
        if (r0 < r2) goto L_0x001d;
    L_0x0015:
        throw r1;
    L_0x0016:
        r1 = move-exception;
        r2 = r4.f1097a;
        r2 = r2.length;
        if (r0 < r2) goto L_0x001d;
    L_0x001c:
        throw r1;
    L_0x001d:
        r1 = r4.f1097a;	 Catch:{ InterruptedException -> 0x0026 }
        r1 = r1[r0];	 Catch:{ InterruptedException -> 0x0026 }
        r2 = (long) r1;	 Catch:{ InterruptedException -> 0x0026 }
        java.lang.Thread.sleep(r2);	 Catch:{ InterruptedException -> 0x0026 }
        goto L_0x0001;
    L_0x0026:
        r0 = move-exception;
        r0 = 0;
        goto L_0x0007;
        */
        throw new UnsupportedOperationException("Method not decompiled: im.getsocial.c.a.ztWNWCuZiM.b():boolean");
    }
}
