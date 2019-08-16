package com.amazon.device.iap.internal.p005b.p007b;

import android.app.Activity;
import android.content.Intent;
import android.os.RemoteException;
import com.amazon.android.framework.context.ContextManager;
import com.amazon.android.framework.exception.KiwiException;
import com.amazon.android.framework.resource.Resource;
import com.amazon.android.framework.task.Task;
import com.amazon.android.framework.task.TaskManager;
import com.amazon.android.framework.task.pipeline.TaskPipelineId;
import com.amazon.device.iap.internal.p005b.C0378e;
import com.amazon.device.iap.internal.p005b.C0393i;
import com.amazon.device.iap.internal.util.C0409e;
import com.amazon.venezia.command.SuccessResult;
import java.util.Map;

/* renamed from: com.amazon.device.iap.internal.b.b.a */
abstract class C0363a extends C0393i {
    /* access modifiers changed from: private */

    /* renamed from: d */
    public static final String f44d = C0363a.class.getSimpleName();
    @Resource

    /* renamed from: a */
    protected TaskManager f45a;
    @Resource

    /* renamed from: b */
    protected ContextManager f46b;

    /* renamed from: c */
    protected final String f47c;

    C0363a(C0378e eVar, String str, String str2) {
        super(eVar, "purchase_item", str);
        this.f47c = str2;
        mo6232a("sku", this.f47c);
    }

    /* access modifiers changed from: protected */
    /* renamed from: a */
    public boolean mo6206a(SuccessResult successResult) throws RemoteException, KiwiException {
        Map data = successResult.getData();
        C0409e.m168a(f44d, "data: " + data);
        if (!data.containsKey("purchaseItemIntent")) {
            C0409e.m170b(f44d, "did not find intent");
            return false;
        }
        C0409e.m168a(f44d, "found intent");
        final Intent intent = (Intent) data.remove("purchaseItemIntent");
        this.f45a.enqueueAtFront(TaskPipelineId.FOREGROUND, new Task() {
            public void execute() {
                try {
                    Activity visible = C0363a.this.f46b.getVisible();
                    if (visible == null) {
                        visible = C0363a.this.f46b.getRoot();
                    }
                    C0409e.m168a(C0363a.f44d, "About to fire intent with activity " + visible);
                    visible.startActivity(intent);
                } catch (Exception e) {
                    C0409e.m170b(C0363a.f44d, "Exception when attempting to fire intent: " + e);
                }
            }
        });
        return true;
    }
}
