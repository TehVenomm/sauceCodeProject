package com.amazon.device.iap.internal;

import android.content.Context;
import android.content.Intent;
import com.amazon.device.iap.model.FulfillmentResult;
import com.amazon.device.iap.model.RequestId;
import java.util.Set;

/* renamed from: com.amazon.device.iap.internal.c */
public interface C0394c {
    /* renamed from: a */
    void mo6196a(Context context, Intent intent);

    /* renamed from: a */
    void mo6197a(RequestId requestId);

    /* renamed from: a */
    void mo6198a(RequestId requestId, String str);

    /* renamed from: a */
    void mo6199a(RequestId requestId, String str, FulfillmentResult fulfillmentResult);

    /* renamed from: a */
    void mo6200a(RequestId requestId, Set<String> set);

    /* renamed from: a */
    void mo6201a(RequestId requestId, boolean z);
}
