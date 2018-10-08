package com.amazon.device.iap.internal.p005b;

import android.app.Activity;
import android.content.Intent;
import android.net.Uri;
import com.amazon.android.framework.context.ContextManager;
import com.amazon.android.framework.prompt.PromptContent;
import com.amazon.android.framework.prompt.SimplePrompt;
import com.amazon.android.framework.resource.Resource;
import com.amazon.device.iap.internal.util.C0244e;

/* renamed from: com.amazon.device.iap.internal.b.b */
public class C0205b extends SimplePrompt {
    /* renamed from: a */
    private static final String f43a = C0205b.class.getSimpleName();
    @Resource
    /* renamed from: b */
    private ContextManager f44b;
    /* renamed from: c */
    private final PromptContent f45c;

    public C0205b(PromptContent promptContent) {
        super(promptContent);
        this.f45c = promptContent;
    }

    protected void doAction() {
        C0244e.m173a(f43a, "doAction");
        if ("Amazon Appstore required".equalsIgnoreCase(this.f45c.getTitle()) || "Amazon Appstore Update Required".equalsIgnoreCase(this.f45c.getTitle())) {
            try {
                Activity visible = this.f44b.getVisible();
                if (visible == null) {
                    visible = this.f44b.getRoot();
                }
                visible.startActivity(new Intent("android.intent.action.VIEW", Uri.parse("http://www.amazon.com/gp/mas/get-appstore/android/ref=mas_mx_mba_iap_dl")));
            } catch (Exception e) {
                C0244e.m175b(f43a, "Exception in PurchaseItemCommandTask.OnSuccess: " + e);
            }
        }
    }

    protected long getExpirationDurationInSeconds() {
        return 31536000;
    }

    public String toString() {
        return f43a;
    }
}
