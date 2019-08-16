package net.gogame.gopay.sdk;

import android.content.ActivityNotFoundException;
import android.content.Intent;
import android.view.View;
import android.view.View.OnClickListener;
import java.util.UUID;
import net.gogame.gopay.sdk.iab.PurchaseActivity;
import org.onepf.oms.appstore.AmazonAppstoreBillingService;
import org.onepf.oms.appstore.googleUtils.SkuDetails;

/* renamed from: net.gogame.gopay.sdk.v */
final class C1658v implements OnClickListener {

    /* renamed from: a */
    final /* synthetic */ SkuDetails f1324a;

    /* renamed from: b */
    final /* synthetic */ C1657u f1325b;

    C1658v(C1657u uVar, SkuDetails skuDetails) {
        this.f1325b = uVar;
        this.f1324a = skuDetails;
    }

    public final void onClick(View view) {
        Intent intent = new Intent(this.f1325b.f1322b, PurchaseActivity.class);
        intent.putExtra("gid", this.f1325b.f1323c.f1129a);
        intent.putExtra("guid", this.f1325b.f1323c.f1130b);
        intent.putExtra("sku", this.f1324a.getSku());
        intent.putExtra(AmazonAppstoreBillingService.JSON_KEY_RECEIPT_ITEM_TYPE, this.f1324a.getItemType());
        intent.putExtra("referenceId", UUID.randomUUID().toString().replaceAll("-", ""));
        try {
            this.f1325b.f1322b.startActivity(intent);
        } catch (ActivityNotFoundException e) {
        }
    }
}
