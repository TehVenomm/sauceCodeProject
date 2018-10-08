package com.amazon.device.iap.internal;

import android.content.Context;
import android.content.Intent;
import com.amazon.device.iap.model.FulfillmentResult;
import com.amazon.device.iap.model.RequestId;
import java.util.Set;

/* renamed from: com.amazon.device.iap.internal.c */
public interface C0189c {
    /* renamed from: a */
    void mo1180a(Context context, Intent intent);

    /* renamed from: a */
    void mo1181a(RequestId requestId);

    /* renamed from: a */
    void mo1182a(RequestId requestId, String str);

    /* renamed from: a */
    void mo1183a(RequestId requestId, String str, FulfillmentResult fulfillmentResult);

    /* renamed from: a */
    void mo1184a(RequestId requestId, Set<String> set);

    /* renamed from: a */
    void mo1185a(RequestId requestId, boolean z);
}
