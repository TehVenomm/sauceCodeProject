package com.amazon.device.iap.internal.p001b.p003b;

import android.app.Activity;
import android.content.Intent;
import android.os.RemoteException;
import com.amazon.android.framework.context.ContextManager;
import com.amazon.android.framework.exception.KiwiException;
import com.amazon.android.framework.resource.Resource;
import com.amazon.android.framework.task.Task;
import com.amazon.android.framework.task.TaskManager;
import com.amazon.android.framework.task.pipeline.TaskPipelineId;
import com.amazon.device.iap.internal.p001b.C0193i;
import com.amazon.device.iap.internal.p001b.C0197e;
import com.amazon.device.iap.internal.util.C0244e;
import com.amazon.venezia.command.SuccessResult;
import java.util.Map;

/* renamed from: com.amazon.device.iap.internal.b.b.a */
abstract class C0201a extends C0193i {
    /* renamed from: d */
    private static final String f39d = C0201a.class.getSimpleName();
    @Resource
    /* renamed from: a */
    protected TaskManager f40a;
    @Resource
    /* renamed from: b */
    protected ContextManager f41b;
    /* renamed from: c */
    protected final String f42c;

    C0201a(C0197e c0197e, String str, String str2) {
        super(c0197e, "purchase_item", str);
        this.f42c = str2;
        m56a("sku", this.f42c);
    }

    /* renamed from: a */
    protected boolean mo1187a(SuccessResult successResult) throws RemoteException, KiwiException {
        Map data = successResult.getData();
        C0244e.m173a(f39d, "data: " + data);
        if (data.containsKey("purchaseItemIntent")) {
            C0244e.m173a(f39d, "found intent");
            final Intent intent = (Intent) data.remove("purchaseItemIntent");
            this.f40a.enqueueAtFront(TaskPipelineId.FOREGROUND, new Task(this) {
                /* renamed from: b */
                final /* synthetic */ C0201a f38b;

                public void execute() {
                    try {
                        Activity visible = this.f38b.f41b.getVisible();
                        if (visible == null) {
                            visible = this.f38b.f41b.getRoot();
                        }
                        C0244e.m173a(C0201a.f39d, "About to fire intent with activity " + visible);
                        visible.startActivity(intent);
                    } catch (Exception e) {
                        C0244e.m175b(C0201a.f39d, "Exception when attempting to fire intent: " + e);
                    }
                }
            });
            return true;
        }
        C0244e.m175b(f39d, "did not find intent");
        return false;
    }
}
