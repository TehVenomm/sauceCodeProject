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
final class C1093v implements OnClickListener {
    /* renamed from: a */
    final /* synthetic */ SkuDetails f1256a;
    /* renamed from: b */
    final /* synthetic */ C1092u f1257b;

    C1093v(C1092u c1092u, SkuDetails skuDetails) {
        this.f1257b = c1092u;
        this.f1256a = skuDetails;
    }

    public final void onClick(View view) {
        Intent intent = new Intent(this.f1257b.f1254b, PurchaseActivity.class);
        intent.putExtra("gid", this.f1257b.f1255c.f1182a);
        intent.putExtra("guid", this.f1257b.f1255c.f1183b);
        intent.putExtra("sku", this.f1256a.getSku());
        intent.putExtra(AmazonAppstoreBillingService.JSON_KEY_RECEIPT_ITEM_TYPE, this.f1256a.getItemType());
        intent.putExtra("referenceId", UUID.randomUUID().toString().replaceAll("-", ""));
        try {
            this.f1257b.f1254b.startActivity(intent);
        } catch (ActivityNotFoundException e) {
        }
    }
}
