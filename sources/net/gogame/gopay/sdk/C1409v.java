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
final class C1409v implements OnClickListener {
    /* renamed from: a */
    final /* synthetic */ SkuDetails f3644a;
    /* renamed from: b */
    final /* synthetic */ C1408u f3645b;

    C1409v(C1408u c1408u, SkuDetails skuDetails) {
        this.f3645b = c1408u;
        this.f3644a = skuDetails;
    }

    public final void onClick(View view) {
        Intent intent = new Intent(this.f3645b.f3642b, PurchaseActivity.class);
        intent.putExtra("gid", this.f3645b.f3643c.f3570a);
        intent.putExtra("guid", this.f3645b.f3643c.f3571b);
        intent.putExtra("sku", this.f3644a.getSku());
        intent.putExtra(AmazonAppstoreBillingService.JSON_KEY_RECEIPT_ITEM_TYPE, this.f3644a.getItemType());
        intent.putExtra("referenceId", UUID.randomUUID().toString().replaceAll("-", ""));
        try {
            this.f3645b.f3642b.startActivity(intent);
        } catch (ActivityNotFoundException e) {
        }
    }
}
