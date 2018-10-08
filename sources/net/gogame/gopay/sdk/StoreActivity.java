package net.gogame.gopay.sdk;

import android.app.Activity;
import android.app.ProgressDialog;
import android.graphics.Color;
import android.graphics.Typeface;
import android.os.Bundle;
import android.os.Handler;
import android.view.View;
import android.view.ViewGroup.LayoutParams;
import android.widget.LinearLayout;
import android.widget.RelativeLayout;
import android.widget.Spinner;
import android.widget.TextView;
import net.gogame.gopay.sdk.support.C1400m;
import net.gogame.gopay.sdk.support.C1404s;
import net.gogame.gopay.sdk.support.DisplayUtils;
import org.onepf.oms.appstore.googleUtils.IabException;

public class StoreActivity extends Activity {
    /* renamed from: a */
    private Handler f3345a;
    /* renamed from: b */
    private ProgressDialog f3346b;
    /* renamed from: c */
    private C1381m f3347c;
    /* renamed from: d */
    private C1345d f3348d;
    /* renamed from: e */
    private C1410w f3349e;

    /* renamed from: a */
    private void m3774a() {
        new C1386r(this).execute(new Void[0]);
    }

    /* renamed from: b */
    private void m3775b() {
        this.f3345a.post(new C1407t(this));
    }

    /* renamed from: d */
    static /* synthetic */ void m3778d(StoreActivity storeActivity) {
        storeActivity.f3345a.post(new C1387s(storeActivity));
        try {
            C1350h a = C1378j.m3890a(storeActivity.f3347c.f3570a, storeActivity.f3347c.f3571b, C1378j.m3883a());
            C1378j.m3893a(a.f3371a);
            storeActivity.f3345a.post(new C1385q(storeActivity, a));
        } catch (IabException e) {
        } finally {
            storeActivity.m3775b();
        }
    }

    protected void onCreate(Bundle bundle) {
        super.onCreate(bundle);
        C1400m.m3948a(getFilesDir().getPath());
        requestWindowFeature(1);
        setTitle(C1404s.m3972a("store_title"));
        this.f3345a = new Handler();
        String str = null;
        String str2 = null;
        if (getIntent().getExtras().getString("appId") != null) {
            str = getIntent().getExtras().getString("appId");
        }
        if (getIntent().getExtras().getString("guid") != null) {
            str2 = getIntent().getExtras().getString("guid");
        }
        this.f3347c = new C1381m(str, str2);
        View linearLayout = new LinearLayout(this);
        linearLayout.setOrientation(1);
        View relativeLayout = new RelativeLayout(this);
        relativeLayout.setPadding(DisplayUtils.pxFromDp(this, 5.0f), DisplayUtils.pxFromDp(this, 0.0f), DisplayUtils.pxFromDp(this, 5.0f), DisplayUtils.pxFromDp(this, 0.0f));
        relativeLayout.setGravity(14);
        relativeLayout.setBackgroundColor(Color.parseColor("#87b856"));
        linearLayout.addView(relativeLayout);
        View textView = new TextView(this);
        textView.setTypeface(Typeface.DEFAULT_BOLD);
        textView.setTextSize(3, 10.0f);
        textView.setTextColor(-1);
        LayoutParams layoutParams = new RelativeLayout.LayoutParams(-2, -2);
        layoutParams.addRule(15);
        textView.setLayoutParams(layoutParams);
        textView.setText(C1404s.m3972a("store_title"));
        relativeLayout.addView(textView);
        this.f3348d = new C1345d(this, true);
        textView = new Spinner(this);
        textView.setBackgroundColor(0);
        textView.setAdapter(this.f3348d);
        textView.setPadding(0, 0, 0, 0);
        textView.setOnItemSelectedListener(new C1383o(this));
        layoutParams = new RelativeLayout.LayoutParams(-2, -2);
        layoutParams.addRule(11);
        textView.setLayoutParams(layoutParams);
        relativeLayout.addView(textView);
        if (C1351i.m3786a()) {
            linearLayout.setLongClickable(true);
            linearLayout.setOnLongClickListener(new C1384p(this));
        }
        this.f3349e = new C1410w(this, this.f3347c);
        linearLayout.addView(this.f3349e);
        setContentView(linearLayout);
    }

    protected void onResume() {
        super.onResume();
        m3774a();
    }

    protected void onStop() {
        super.onStop();
        this.f3346b.dismiss();
    }
}
