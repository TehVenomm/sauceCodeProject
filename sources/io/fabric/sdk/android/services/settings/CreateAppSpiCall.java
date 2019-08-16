package p017io.fabric.sdk.android.services.settings;

import p017io.fabric.sdk.android.Kit;
import p017io.fabric.sdk.android.services.network.HttpMethod;
import p017io.fabric.sdk.android.services.network.HttpRequestFactory;

/* renamed from: io.fabric.sdk.android.services.settings.CreateAppSpiCall */
public class CreateAppSpiCall extends AbstractAppSpiCall {
    public CreateAppSpiCall(Kit kit, String str, String str2, HttpRequestFactory httpRequestFactory) {
        super(kit, str, str2, httpRequestFactory, HttpMethod.POST);
    }

    public /* bridge */ /* synthetic */ boolean invoke(AppRequestData appRequestData) {
        return super.invoke(appRequestData);
    }
}
