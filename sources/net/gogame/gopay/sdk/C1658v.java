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
    final /* synthetic */ SkuDetails f1312a;

    /* renamed from: b */
    final /* synthetic */ C1657u f1313b;

    C1658v(C1657u uVar, SkuDetails skuDetails) {
        this.f1313b = uVar;
        this.f1312a = skuDetails;
    }

    public final void onClick(View view) {
        Intent intent = new Intent(this.f1313b.f1310b, PurchaseActivity.class);
        intent.putExtra("gid", this.f1313b.f1311c.f1123a);
        intent.putExtra("guid", this.f1313b.f1311c.f1124b);
        intent.putExtra("sku", this.f1312a.getSku());
        intent.putExtra(AmazonAppstoreBillingService.JSON_KEY_RECEIPT_ITEM_TYPE, this.f1312a.getItemType());
        intent.putExtra("referenceId", UUID.randomUUID().toString().replaceAll("-", ""));
        try {
            this.f1313b.f1310b.startActivity(intent);
        } catch (ActivityNotFoundException e) {
        }
    }
}
