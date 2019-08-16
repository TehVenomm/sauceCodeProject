package com.amazon.device.iap.internal.p005b;

import android.os.RemoteException;
import com.amazon.android.Kiwi;
import com.amazon.android.framework.exception.KiwiException;
import com.amazon.android.framework.prompt.PromptContent;
import com.amazon.android.framework.task.command.AbstractCommandTask;
import com.amazon.android.licensing.LicenseFailurePromptContentMapper;
import com.amazon.device.iap.PurchasingService;
import com.amazon.device.iap.internal.util.C0408d;
import com.amazon.device.iap.internal.util.C0409e;
import com.amazon.venezia.command.SuccessResult;
import java.util.HashMap;
import java.util.Map;

/* renamed from: com.amazon.device.iap.internal.b.i */
public abstract class C0393i extends AbstractCommandTask {

    /* renamed from: a */
    private static final String f80a = C0393i.class.getSimpleName();

    /* renamed from: b */
    private final C0378e f81b;

    /* renamed from: c */
    private final String f82c;

    /* renamed from: d */
    private final String f83d;

    /* renamed from: e */
    private final String f84e;

    /* renamed from: f */
    private final Map<String, Object> f85f;

    /* renamed from: g */
    private final LicenseFailurePromptContentMapper f86g = new LicenseFailurePromptContentMapper();

    /* renamed from: h */
    private boolean f87h;

    /* renamed from: i */
    private C0393i f88i;

    /* renamed from: j */
    private C0393i f89j;

    /* renamed from: k */
    private boolean f90k = false;

    public C0393i(C0378e eVar, String str, String str2) {
        this.f81b = eVar;
        this.f82c = eVar.mo6220c().toString();
        this.f83d = str;
        this.f84e = str2;
        this.f85f = new HashMap();
        this.f85f.put("requestId", this.f82c);
        this.f85f.put("sdkVersion", PurchasingService.SDK_VERSION);
        this.f87h = true;
        this.f88i = null;
        this.f89j = null;
    }

    /* renamed from: a */
    private void m99a(PromptContent promptContent) {
        if (promptContent != null) {
            Kiwi.getPromptManager().present(new C0362b(promptContent));
        }
    }

    /* renamed from: a */
    public C0393i mo6230a(boolean z) {
        this.f90k = z;
        return this;
    }

    /* renamed from: a */
    public void mo6231a(C0393i iVar) {
        this.f88i = iVar;
    }

    /* access modifiers changed from: protected */
    /* renamed from: a */
    public void mo6232a(String str, Object obj) {
        this.f85f.put(str, obj);
    }

    /* access modifiers changed from: protected */
    /* renamed from: a */
    public abstract boolean mo6206a(SuccessResult successResult) throws Exception;

    /* renamed from: a_ */
    public void mo6224a_() {
        Kiwi.addCommandToCommandTaskPipeline(this);
    }

    /* access modifiers changed from: protected */
    /* renamed from: b */
    public C0378e mo6233b() {
        return this.f81b;
    }

    /* renamed from: b */
    public void mo6234b(C0393i iVar) {
        this.f89j = iVar;
    }

    /* access modifiers changed from: protected */
    /* renamed from: b */
    public void mo6235b(boolean z) {
        this.f87h = z;
    }

    /* access modifiers changed from: protected */
    /* renamed from: c */
    public String mo6236c() {
        return this.f82c;
    }

    /* access modifiers changed from: protected */
    public Map<String, Object> getCommandData() {
        return this.f85f;
    }

    /* access modifiers changed from: protected */
    public String getCommandName() {
        return this.f83d;
    }

    /* access modifiers changed from: protected */
    public String getCommandVersion() {
        return this.f84e;
    }

    /* access modifiers changed from: protected */
    public boolean isExecutionNeeded() {
        return true;
    }

    /* access modifiers changed from: protected */
    public final void onException(KiwiException kiwiException) {
        C0409e.m168a(f80a, "onException: exception = " + kiwiException.getMessage());
        if (!"UNHANDLED_EXCEPTION".equals(kiwiException.getType()) || !"2.0".equals(this.f84e) || this.f89j == null) {
            if (this.f87h) {
                m99a(this.f86g.map(kiwiException));
            }
            if (!this.f90k) {
                this.f81b.mo6209b();
                return;
            }
            return;
        }
        this.f89j.mo6230a(this.f90k);
        this.f89j.mo6224a_();
    }

    /* access modifiers changed from: protected */
    /* JADX WARNING: Removed duplicated region for block: B:13:0x0048  */
    /* JADX WARNING: Removed duplicated region for block: B:16:0x0064  */
    /* JADX WARNING: Removed duplicated region for block: B:19:? A[RETURN, SYNTHETIC] */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public final void onFailure(com.amazon.venezia.command.FailureResult r6) throws android.os.RemoteException, com.amazon.android.framework.exception.KiwiException {
        /*
            r5 = this;
            java.lang.String r0 = f80a
            java.lang.StringBuilder r1 = new java.lang.StringBuilder
            r1.<init>()
            java.lang.String r2 = "onFailure: result = "
            java.lang.StringBuilder r1 = r1.append(r2)
            java.lang.StringBuilder r1 = r1.append(r6)
            java.lang.String r1 = r1.toString()
            com.amazon.device.iap.internal.util.C0409e.m168a(r0, r1)
            if (r6 == 0) goto L_0x006a
            java.util.Map r0 = r6.getExtensionData()
            java.lang.String r1 = "maxVersion"
            java.lang.Object r0 = r0.get(r1)
            java.lang.String r0 = (java.lang.String) r0
            if (r0 == 0) goto L_0x006a
            java.lang.String r1 = "1.0"
            boolean r0 = r0.equalsIgnoreCase(r1)
            if (r0 == 0) goto L_0x006a
            r0 = 1
        L_0x0031:
            if (r0 == 0) goto L_0x0044
            com.amazon.device.iap.internal.b.i r0 = r5.f89j
            if (r0 == 0) goto L_0x0044
            com.amazon.device.iap.internal.b.i r0 = r5.f89j
            boolean r1 = r5.f90k
            r0.mo6230a(r1)
            com.amazon.device.iap.internal.b.i r0 = r5.f89j
            r0.mo6224a_()
        L_0x0043:
            return
        L_0x0044:
            boolean r0 = r5.f87h
            if (r0 == 0) goto L_0x0060
            com.amazon.android.framework.prompt.PromptContent r0 = new com.amazon.android.framework.prompt.PromptContent
            java.lang.String r1 = r6.getDisplayableName()
            java.lang.String r2 = r6.getDisplayableMessage()
            java.lang.String r3 = r6.getButtonLabel()
            boolean r4 = r6.show()
            r0.<init>(r1, r2, r3, r4)
            r5.m99a(r0)
        L_0x0060:
            boolean r0 = r5.f90k
            if (r0 != 0) goto L_0x0043
            com.amazon.device.iap.internal.b.e r0 = r5.f81b
            r0.mo6209b()
            goto L_0x0043
        L_0x006a:
            r0 = 0
            goto L_0x0031
        */
        throw new UnsupportedOperationException("Method not decompiled: com.amazon.device.iap.internal.p005b.C0393i.onFailure(com.amazon.venezia.command.FailureResult):void");
    }

    /* access modifiers changed from: protected */
    public final void onSuccess(SuccessResult successResult) throws RemoteException {
        String str = (String) successResult.getData().get("errorMessage");
        C0409e.m168a(f80a, "onSuccess: result = " + successResult + ", errorMessage: " + str);
        if (C0408d.m167a(str)) {
            boolean z = false;
            try {
                z = mo6206a(successResult);
            } catch (Exception e) {
                C0409e.m170b(f80a, "Error calling onResult: " + e);
            }
            if (z && this.f88i != null) {
                this.f88i.mo6224a_();
            } else if (this.f90k) {
            } else {
                if (z) {
                    this.f81b.mo6208a();
                } else {
                    this.f81b.mo6209b();
                }
            }
        } else if (!this.f90k) {
            this.f81b.mo6209b();
        }
    }
}
