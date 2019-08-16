package net.gogame.gopay.sdk;

import android.app.Activity;
import android.app.ProgressDialog;
import android.graphics.Color;
import android.graphics.Typeface;
import android.os.Bundle;
import android.os.Handler;
import android.widget.LinearLayout;
import android.widget.RelativeLayout;
import android.widget.RelativeLayout.LayoutParams;
import android.widget.Spinner;
import android.widget.TextView;
import net.gogame.gopay.sdk.support.C1415m;
import net.gogame.gopay.sdk.support.C1416s;
import net.gogame.gopay.sdk.support.DisplayUtils;
import org.onepf.oms.appstore.googleUtils.IabException;

public class StoreActivity extends Activity {

    /* renamed from: a */
    private Handler f988a;
    /* access modifiers changed from: private */

    /* renamed from: b */
    public ProgressDialog f989b;

    /* renamed from: c */
    private C1407m f990c;
    /* access modifiers changed from: private */

    /* renamed from: d */
    public C1360d f991d;
    /* access modifiers changed from: private */

    /* renamed from: e */
    public C1418w f992e;

    /* access modifiers changed from: private */
    /* renamed from: a */
    public void m762a() {
        new C1412r(this).execute(new Void[0]);
    }

    /* renamed from: b */
    private void m763b() {
        this.f988a.post(new C1417t(this));
    }

    /* renamed from: d */
    static /* synthetic */ void m766d(StoreActivity storeActivity) {
        storeActivity.f988a.post(new C1413s(storeActivity));
        try {
            C1363h a = C1406j.m865a(storeActivity.f990c.f1129a, storeActivity.f990c.f1130b, C1406j.m858a());
            C1406j.m868a(a.f1009a);
            storeActivity.f988a.post(new C1411q(storeActivity, a));
        } catch (IabException e) {
        } finally {
            storeActivity.m763b();
        }
    }

    /* access modifiers changed from: protected */
    public void onCreate(Bundle bundle) {
        super.onCreate(bundle);
        C1415m.m920a(getFilesDir().getPath());
        requestWindowFeature(1);
        setTitle(C1416s.m943a("store_title"));
        this.f988a = new Handler();
        String str = null;
        String str2 = null;
        if (getIntent().getExtras().getString("appId") != null) {
            str = getIntent().getExtras().getString("appId");
        }
        if (getIntent().getExtras().getString("guid") != null) {
            str2 = getIntent().getExtras().getString("guid");
        }
        this.f990c = new C1407m(str, str2);
        LinearLayout linearLayout = new LinearLayout(this);
        linearLayout.setOrientation(1);
        RelativeLayout relativeLayout = new RelativeLayout(this);
        relativeLayout.setPadding(DisplayUtils.pxFromDp(this, 5.0f), DisplayUtils.pxFromDp(this, 0.0f), DisplayUtils.pxFromDp(this, 5.0f), DisplayUtils.pxFromDp(this, 0.0f));
        relativeLayout.setGravity(14);
        relativeLayout.setBackgroundColor(Color.parseColor("#87b856"));
        linearLayout.addView(relativeLayout);
        TextView textView = new TextView(this);
        textView.setTypeface(Typeface.DEFAULT_BOLD);
        textView.setTextSize(3, 10.0f);
        textView.setTextColor(-1);
        LayoutParams layoutParams = new LayoutParams(-2, -2);
        layoutParams.addRule(15);
        textView.setLayoutParams(layoutParams);
        textView.setText(C1416s.m943a("store_title"));
        relativeLayout.addView(textView);
        this.f991d = new C1360d(this, true);
        Spinner spinner = new Spinner(this);
        spinner.setBackgroundColor(0);
        spinner.setAdapter(this.f991d);
        spinner.setPadding(0, 0, 0, 0);
        spinner.setOnItemSelectedListener(new C1409o(this));
        LayoutParams layoutParams2 = new LayoutParams(-2, -2);
        layoutParams2.addRule(11);
        spinner.setLayoutParams(layoutParams2);
        relativeLayout.addView(spinner);
        if (C1364i.m772a()) {
            linearLayout.setLongClickable(true);
            linearLayout.setOnLongClickListener(new C1410p(this));
        }
        this.f992e = new C1418w(this, this.f990c);
        linearLayout.addView(this.f992e);
        setContentView(linearLayout);
    }

    /* access modifiers changed from: protected */
    public void onResume() {
        super.onResume();
        m762a();
    }

    /* access modifiers changed from: protected */
    public void onStop() {
        super.onStop();
        this.f989b.dismiss();
    }
}
