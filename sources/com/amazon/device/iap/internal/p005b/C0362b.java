package com.amazon.device.iap.internal.p005b;

import android.app.Activity;
import android.content.Intent;
import android.net.Uri;
import com.amazon.android.framework.context.ContextManager;
import com.amazon.android.framework.prompt.PromptContent;
import com.amazon.android.framework.prompt.SimplePrompt;
import com.amazon.android.framework.resource.Resource;
import com.amazon.device.iap.internal.util.C0409e;

/* renamed from: com.amazon.device.iap.internal.b.b */
public class C0362b extends SimplePrompt {

    /* renamed from: a */
    private static final String f41a = C0362b.class.getSimpleName();
    @Resource

    /* renamed from: b */
    private ContextManager f42b;

    /* renamed from: c */
    private final PromptContent f43c;

    public C0362b(PromptContent promptContent) {
        super(promptContent);
        this.f43c = promptContent;
    }

    /* access modifiers changed from: protected */
    public void doAction() {
        C0409e.m168a(f41a, "doAction");
        if ("Amazon Appstore required".equalsIgnoreCase(this.f43c.getTitle()) || "Amazon Appstore Update Required".equalsIgnoreCase(this.f43c.getTitle())) {
            try {
                Activity visible = this.f42b.getVisible();
                if (visible == null) {
                    visible = this.f42b.getRoot();
                }
                visible.startActivity(new Intent("android.intent.action.VIEW", Uri.parse("http://www.amazon.com/gp/mas/get-appstore/android/ref=mas_mx_mba_iap_dl")));
            } catch (Exception e) {
                C0409e.m170b(f41a, "Exception in PurchaseItemCommandTask.OnSuccess: " + e);
            }
        }
    }

    /* access modifiers changed from: protected */
    public long getExpirationDurationInSeconds() {
        return 31536000;
    }

    public String toString() {
        return f41a;
    }
}
