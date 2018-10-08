package im.getsocial.sdk.internal.p033c;

import android.os.Build;
import im.getsocial.sdk.internal.p033c.p041b.cjrhisSQCL;
import im.getsocial.sdk.internal.p033c.p041b.pdwpUtZXDT;
import java.util.HashMap;
import java.util.Map;

/* renamed from: im.getsocial.sdk.internal.c.HptYHntaqF */
public final class HptYHntaqF implements cjrhisSQCL<Map> {
    /* renamed from: a */
    public final /* synthetic */ Object mo4357a(pdwpUtZXDT pdwputzxdt) {
        Map hashMap = new HashMap();
        hashMap.put("PRODUCT", Build.PRODUCT);
        hashMap.put("MANUFACTURER", Build.MANUFACTURER);
        hashMap.put("DEVICE", Build.DEVICE);
        hashMap.put("MODEL", Build.MODEL);
        hashMap.put("HARDWARE", Build.HARDWARE);
        hashMap.put("FINGERPRINT", Build.FINGERPRINT);
        return hashMap;
    }
}
