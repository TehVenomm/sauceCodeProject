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
import net.gogame.gopay.sdk.support.C1084m;
import net.gogame.gopay.sdk.support.C1088s;
import net.gogame.gopay.sdk.support.DisplayUtils;
import org.onepf.oms.appstore.googleUtils.IabException;

public class StoreActivity extends Activity {
    /* renamed from: a */
    private Handler f957a;
    /* renamed from: b */
    private ProgressDialog f958b;
    /* renamed from: c */
    private C1065m f959c;
    /* renamed from: d */
    private C1029d f960d;
    /* renamed from: e */
    private C1094w f961e;

    /* renamed from: a */
    private void m749a() {
        new C1070r(this).execute(new Void[0]);
    }

    /* renamed from: b */
    private void m750b() {
        this.f957a.post(new C1091t(this));
    }

    /* renamed from: d */
    static /* synthetic */ void m753d(StoreActivity storeActivity) {
        storeActivity.f957a.post(new C1071s(storeActivity));
        try {
            C1034h a = C1062j.m865a(storeActivity.f959c.f1182a, storeActivity.f959c.f1183b, C1062j.m858a());
            C1062j.m868a(a.f983a);
            storeActivity.f957a.post(new C1069q(storeActivity, a));
        } catch (IabException e) {
        } finally {
            storeActivity.m750b();
        }
    }

    protected void onCreate(Bundle bundle) {
        super.onCreate(bundle);
        C1084m.m923a(getFilesDir().getPath());
        requestWindowFeature(1);
        setTitle(C1088s.m947a("store_title"));
        this.f957a = new Handler();
        String str = null;
        String str2 = null;
        if (getIntent().getExtras().getString("appId") != null) {
            str = getIntent().getExtras().getString("appId");
        }
        if (getIntent().getExtras().getString("guid") != null) {
            str2 = getIntent().getExtras().getString("guid");
        }
        this.f959c = new C1065m(str, str2);
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
        textView.setText(C1088s.m947a("store_title"));
        relativeLayout.addView(textView);
        this.f960d = new C1029d(this, true);
        textView = new Spinner(this);
        textView.setBackgroundColor(0);
        textView.setAdapter(this.f960d);
        textView.setPadding(0, 0, 0, 0);
        textView.setOnItemSelectedListener(new C1067o(this));
        layoutParams = new RelativeLayout.LayoutParams(-2, -2);
        layoutParams.addRule(11);
        textView.setLayoutParams(layoutParams);
        relativeLayout.addView(textView);
        if (C1035i.m761a()) {
            linearLayout.setLongClickable(true);
            linearLayout.setOnLongClickListener(new C1068p(this));
        }
        this.f961e = new C1094w(this, this.f959c);
        linearLayout.addView(this.f961e);
        setContentView(linearLayout);
    }

    protected void onResume() {
        super.onResume();
        m749a();
    }

    protected void onStop() {
        super.onStop();
        this.f958b.dismiss();
    }
}
