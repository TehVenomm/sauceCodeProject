package com.amazon.device.iap.internal.p005b;

import android.os.RemoteException;
import com.amazon.android.Kiwi;
import com.amazon.android.framework.exception.KiwiException;
import com.amazon.android.framework.prompt.PromptContent;
import com.amazon.android.framework.task.command.AbstractCommandTask;
import com.amazon.android.licensing.LicenseFailurePromptContentMapper;
import com.amazon.device.iap.PurchasingService;
import com.amazon.device.iap.internal.util.C0243d;
import com.amazon.device.iap.internal.util.C0244e;
import com.amazon.venezia.command.FailureResult;
import com.amazon.venezia.command.SuccessResult;
import java.util.HashMap;
import java.util.Map;

/* renamed from: com.amazon.device.iap.internal.b.i */
public abstract class C0193i extends AbstractCommandTask {
    /* renamed from: a */
    private static final String f18a = C0193i.class.getSimpleName();
    /* renamed from: b */
    private final C0197e f19b;
    /* renamed from: c */
    private final String f20c;
    /* renamed from: d */
    private final String f21d;
    /* renamed from: e */
    private final String f22e;
    /* renamed from: f */
    private final Map<String, Object> f23f;
    /* renamed from: g */
    private final LicenseFailurePromptContentMapper f24g = new LicenseFailurePromptContentMapper();
    /* renamed from: h */
    private boolean f25h;
    /* renamed from: i */
    private C0193i f26i;
    /* renamed from: j */
    private C0193i f27j;
    /* renamed from: k */
    private boolean f28k = false;

    public C0193i(C0197e c0197e, String str, String str2) {
        this.f19b = c0197e;
        this.f20c = c0197e.m72c().toString();
        this.f21d = str;
        this.f22e = str2;
        this.f23f = new HashMap();
        this.f23f.put("requestId", this.f20c);
        this.f23f.put("sdkVersion", PurchasingService.SDK_VERSION);
        this.f25h = true;
        this.f26i = null;
        this.f27j = null;
    }

    /* renamed from: a */
    private void m53a(PromptContent promptContent) {
        if (promptContent != null) {
            Kiwi.getPromptManager().present(new C0205b(promptContent));
        }
    }

    /* renamed from: a */
    public C0193i m54a(boolean z) {
        this.f28k = z;
        return this;
    }

    /* renamed from: a */
    public void m55a(C0193i c0193i) {
        this.f26i = c0193i;
    }

    /* renamed from: a */
    protected void m56a(String str, Object obj) {
        this.f23f.put(str, obj);
    }

    /* renamed from: a */
    protected abstract boolean mo1187a(SuccessResult successResult) throws Exception;

    public void a_() {
        Kiwi.addCommandToCommandTaskPipeline(this);
    }

    /* renamed from: b */
    protected C0197e m58b() {
        return this.f19b;
    }

    /* renamed from: b */
    public void m59b(C0193i c0193i) {
        this.f27j = c0193i;
    }

    /* renamed from: b */
    protected void m60b(boolean z) {
        this.f25h = z;
    }

    /* renamed from: c */
    protected String m61c() {
        return this.f20c;
    }

    protected Map<String, Object> getCommandData() {
        return this.f23f;
    }

    protected String getCommandName() {
        return this.f21d;
    }

    protected String getCommandVersion() {
        return this.f22e;
    }

    protected boolean isExecutionNeeded() {
        return true;
    }

    protected final void onException(KiwiException kiwiException) {
        C0244e.m173a(f18a, "onException: exception = " + kiwiException.getMessage());
        if ("UNHANDLED_EXCEPTION".equals(kiwiException.getType()) && "2.0".equals(this.f22e) && this.f27j != null) {
            this.f27j.m54a(this.f28k);
            this.f27j.a_();
            return;
        }
        if (this.f25h) {
            m53a(this.f24g.map(kiwiException));
        }
        if (!this.f28k) {
            this.f19b.mo1189b();
        }
    }

    protected final void onFailure(FailureResult failureResult) throws RemoteException, KiwiException {
        Object obj;
        C0244e.m173a(f18a, "onFailure: result = " + failureResult);
        if (failureResult != null) {
            String str = (String) failureResult.getExtensionData().get("maxVersion");
            if (str != null && str.equalsIgnoreCase("1.0")) {
                obj = 1;
                if (obj != null || this.f27j == null) {
                    if (this.f25h) {
                        m53a(new PromptContent(failureResult.getDisplayableName(), failureResult.getDisplayableMessage(), failureResult.getButtonLabel(), failureResult.show()));
                    }
                    if (!this.f28k) {
                        this.f19b.mo1189b();
                    }
                }
                this.f27j.m54a(this.f28k);
                this.f27j.a_();
                return;
            }
        }
        obj = null;
        if (obj != null) {
        }
        if (this.f25h) {
            m53a(new PromptContent(failureResult.getDisplayableName(), failureResult.getDisplayableMessage(), failureResult.getButtonLabel(), failureResult.show()));
        }
        if (!this.f28k) {
            this.f19b.mo1189b();
        }
    }

    protected final void onSuccess(SuccessResult successResult) throws RemoteException {
        String str = (String) successResult.getData().get("errorMessage");
        C0244e.m173a(f18a, "onSuccess: result = " + successResult + ", errorMessage: " + str);
        if (C0243d.m172a(str)) {
            boolean z = false;
            try {
                z = mo1187a(successResult);
            } catch (Exception e) {
                C0244e.m175b(f18a, "Error calling onResult: " + e);
            }
            if (z && this.f26i != null) {
                this.f26i.a_();
            } else if (!this.f28k) {
                if (z) {
                    this.f19b.mo1188a();
                } else {
                    this.f19b.mo1189b();
                }
            }
        } else if (!this.f28k) {
            this.f19b.mo1189b();
        }
    }
}
