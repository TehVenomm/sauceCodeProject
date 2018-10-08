package org.onepf.oms.appstore.skubitUtils;

import android.content.Context;
import android.content.Intent;
import org.jetbrains.annotations.NotNull;
import org.onepf.oms.Appstore;
import org.onepf.oms.appstore.SkubitTestAppstore;

public class SkubitTestIabHelper extends SkubitIabHelper {
    public SkubitTestIabHelper(Context context, String str, Appstore appstore) {
        super(context, str, appstore);
    }

    @NotNull
    protected Intent getServiceIntent() {
        Intent intent = new Intent(SkubitTestAppstore.VENDING_ACTION);
        intent.setPackage("net.skubit.android");
        return intent;
    }
}
