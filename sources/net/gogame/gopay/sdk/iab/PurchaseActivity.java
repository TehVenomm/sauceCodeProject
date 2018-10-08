package net.gogame.gopay.sdk.iab;

import android.app.Activity;
import android.app.AlertDialog.Builder;
import android.app.Dialog;
import android.content.DialogInterface.OnClickListener;
import android.content.Intent;
import android.content.SharedPreferences;
import android.graphics.Bitmap;
import android.graphics.Color;
import android.graphics.Point;
import android.graphics.Typeface;
import android.graphics.drawable.BitmapDrawable;
import android.graphics.drawable.Drawable;
import android.graphics.drawable.GradientDrawable;
import android.graphics.drawable.LayerDrawable;
import android.os.AsyncTask;
import android.os.Build.VERSION;
import android.os.Bundle;
import android.os.Handler;
import android.text.TextUtils.TruncateAt;
import android.view.View;
import android.view.ViewGroup.LayoutParams;
import android.view.WindowManager;
import android.webkit.WebChromeClient;
import android.webkit.WebView;
import android.widget.Button;
import android.widget.FrameLayout;
import android.widget.ImageButton;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.ListAdapter;
import android.widget.ListView;
import android.widget.ProgressBar;
import android.widget.RelativeLayout;
import android.widget.Spinner;
import android.widget.TextView;
import com.google.android.gms.measurement.AppMeasurement.Param;
import java.util.ArrayList;
import java.util.List;
import java.util.Map;
import net.gogame.gopay.sdk.C1342a;
import net.gogame.gopay.sdk.C1345d;
import net.gogame.gopay.sdk.C1348f;
import net.gogame.gopay.sdk.C1349g;
import net.gogame.gopay.sdk.C1378j;
import net.gogame.gopay.sdk.C1382n;
import net.gogame.gopay.sdk.Country;
import net.gogame.gopay.sdk.support.C1390c;
import net.gogame.gopay.sdk.support.C1400m;
import net.gogame.gopay.sdk.support.DisplayUtils;
import org.json.JSONException;
import org.json.JSONObject;
import org.onepf.oms.appstore.googleUtils.SkuDetails;

public class PurchaseActivity extends Activity {
    /* renamed from: A */
    private C1345d f3393A;
    /* renamed from: B */
    private C1353b f3394B;
    /* renamed from: C */
    private C1361i f3395C;
    /* renamed from: D */
    private bs f3396D;
    /* renamed from: E */
    private C1341a f3397E = null;
    /* renamed from: F */
    private Spinner f3398F;
    /* renamed from: G */
    private Spinner f3399G;
    /* renamed from: H */
    private C1360g f3400H = new C1360g();
    /* renamed from: I */
    private boolean f3401I = false;
    /* renamed from: J */
    private boolean f3402J;
    /* renamed from: K */
    private boolean f3403K = false;
    /* renamed from: L */
    private boolean f3404L = false;
    /* renamed from: M */
    private int f3405M = 0;
    /* renamed from: N */
    private int f3406N = 3;
    /* renamed from: O */
    private int f3407O;
    /* renamed from: P */
    private int f3408P;
    /* renamed from: Q */
    private int f3409Q;
    /* renamed from: a */
    RelativeLayout f3410a;
    /* renamed from: b */
    C1390c f3411b;
    /* renamed from: c */
    private boolean f3412c = false;
    /* renamed from: d */
    private boolean f3413d = false;
    /* renamed from: e */
    private String f3414e;
    /* renamed from: f */
    private String f3415f;
    /* renamed from: g */
    private String f3416g;
    /* renamed from: h */
    private String f3417h;
    /* renamed from: i */
    private br f3418i;
    /* renamed from: j */
    private String f3419j;
    /* renamed from: k */
    private String f3420k;
    /* renamed from: l */
    private String f3421l;
    /* renamed from: m */
    private String f3422m;
    /* renamed from: n */
    private String f3423n;
    /* renamed from: o */
    private String f3424o;
    /* renamed from: p */
    private Map f3425p;
    /* renamed from: q */
    private SkuDetails f3426q;
    /* renamed from: r */
    private SkuDetails f3427r;
    /* renamed from: s */
    private ProgressBar f3428s;
    /* renamed from: t */
    private SharedPreferences f3429t;
    /* renamed from: u */
    private AsyncTask f3430u;
    /* renamed from: v */
    private Handler f3431v;
    /* renamed from: w */
    private Runnable f3432w;
    /* renamed from: x */
    private Runnable f3433x = new C1364l(this);
    /* renamed from: y */
    private WebView f3434y;
    /* renamed from: z */
    private Button f3435z;

    /* renamed from: a */
    static /* synthetic */ Dialog m3804a(PurchaseActivity purchaseActivity, String str, C1342a c1342a, OnClickListener onClickListener) {
        Dialog dialog = new Dialog(purchaseActivity);
        dialog.setOnCancelListener(new bf(purchaseActivity));
        dialog.getWindow().requestFeature(1);
        View imageButton = new ImageButton(purchaseActivity);
        imageButton.setMinimumWidth(0);
        imageButton.setMinimumHeight(0);
        imageButton.setBackgroundColor(0);
        imageButton.setPadding(0, 0, 0, 0);
        imageButton.setOnClickListener(new bg(purchaseActivity, dialog));
        imageButton.setImageDrawable(purchaseActivity.m3806a(C1400m.m3961g()));
        LayoutParams layoutParams = new RelativeLayout.LayoutParams(-2, -2);
        layoutParams.setMargins(0, 0, DisplayUtils.pxFromDp(purchaseActivity, 7.0f), 0);
        layoutParams.addRule(11);
        layoutParams.addRule(15);
        View textView = new TextView(purchaseActivity);
        textView.setBackgroundColor(0);
        textView.setPadding(DisplayUtils.pxFromDp(purchaseActivity, 7.0f), 0, 0, 0);
        textView.setText(str);
        textView.setTypeface(null, 1);
        textView.setTextSize(18.0f);
        textView.setTextColor(-1);
        textView.setSingleLine();
        textView.setEllipsize(TruncateAt.END);
        LayoutParams layoutParams2 = new RelativeLayout.LayoutParams(-2, -2);
        layoutParams2.addRule(15);
        layoutParams2.addRule(9);
        View relativeLayout = new RelativeLayout(purchaseActivity);
        Drawable gradientDrawable = new GradientDrawable();
        gradientDrawable.setCornerRadii(new float[]{(float) DisplayUtils.pxFromDp(purchaseActivity, 10.0f), (float) DisplayUtils.pxFromDp(purchaseActivity, 10.0f), (float) DisplayUtils.pxFromDp(purchaseActivity, 10.0f), (float) DisplayUtils.pxFromDp(purchaseActivity, 10.0f), 0.0f, 0.0f, 0.0f, 0.0f});
        gradientDrawable.setColor(Color.rgb(92, 176, 59));
        if (VERSION.SDK_INT >= 16) {
            relativeLayout.setBackground(gradientDrawable);
        } else {
            relativeLayout.setBackgroundDrawable(gradientDrawable);
        }
        relativeLayout.addView(textView, layoutParams2);
        relativeLayout.addView(imageButton, layoutParams);
        imageButton = new ListView(purchaseActivity);
        imageButton.setBackgroundColor(0);
        imageButton.setAdapter(c1342a);
        imageButton.setSelection(purchaseActivity.f3395C.f3532e);
        imageButton.setOnItemClickListener(new bh(purchaseActivity, onClickListener, dialog));
        View linearLayout = new LinearLayout(purchaseActivity);
        linearLayout.setOrientation(1);
        LayoutParams layoutParams3 = new LinearLayout.LayoutParams(-1, DisplayUtils.pxFromDp(purchaseActivity, 40.0f));
        layoutParams2 = new LinearLayout.LayoutParams(-1, -1);
        linearLayout.addView(relativeLayout, layoutParams3);
        linearLayout.addView(imageButton, layoutParams2);
        dialog.setContentView(linearLayout);
        Drawable gradientDrawable2 = new GradientDrawable();
        gradientDrawable2.setCornerRadius((float) DisplayUtils.pxFromDp(purchaseActivity, 10.0f));
        gradientDrawable2.setColor(-1);
        if (VERSION.SDK_INT >= 16) {
            linearLayout.setBackground(gradientDrawable2);
        } else {
            linearLayout.setBackgroundDrawable(gradientDrawable2);
        }
        dialog.getWindow().setBackgroundDrawable(gradientDrawable2);
        WindowManager.LayoutParams attributes = dialog.getWindow().getAttributes();
        int i = (int) (((double) DisplayUtils.getScreenSize(purchaseActivity).y) / 1.5d);
        if (DisplayUtils.pxFromDp(purchaseActivity, 60.0f) * c1342a.getCount() >= i) {
            attributes.height = i;
        }
        return dialog;
    }

    /* renamed from: a */
    private static Intent m3805a(int i, br brVar, String str) {
        Intent intent = new Intent();
        intent.putExtra("RESPONSE_CODE", i);
        if (brVar != null) {
            intent.putExtra("INAPP_PURCHASE_DATA", brVar.toString());
            intent.putExtra("INAPP_DATA_SIGNATURE", brVar.f3502b);
        }
        if (str != null) {
            intent.putExtra("MESSAGE", str);
        }
        return intent;
    }

    /* renamed from: a */
    private BitmapDrawable m3806a(Bitmap bitmap) {
        return bitmap == null ? null : new BitmapDrawable(getResources(), Bitmap.createScaledBitmap(bitmap, DisplayUtils.pxFromDp(this, ((float) bitmap.getWidth()) * 0.5f), DisplayUtils.pxFromDp(this, ((float) bitmap.getHeight()) * 0.5f), false));
    }

    /* renamed from: a */
    private String m3807a(String str) {
        return this.f3425p.containsKey(str) ? (String) this.f3425p.get(str) : null;
    }

    /* renamed from: a */
    private void m3809a() {
        if (this.f3428s == null || this.f3428s.isShown()) {
            m3831c();
            Handler handler = this.f3431v;
            Runnable axVar = new ax(this);
            this.f3432w = axVar;
            handler.postDelayed(axVar, 10000);
            return;
        }
        this.f3431v.post(new bm(this));
    }

    /* renamed from: a */
    private void m3810a(int i, String str) {
        this.f3412c = true;
        m3827b();
        this.f3431v.post(new ay(this, i, str));
    }

    /* renamed from: a */
    static /* synthetic */ void m3812a(PurchaseActivity purchaseActivity, int i) {
        purchaseActivity.f3408P = i;
        purchaseActivity.f3401I = false;
        purchaseActivity.f3398F.setEnabled(false);
        purchaseActivity.f3399G.setEnabled(false);
        if (purchaseActivity.f3435z != null) {
            purchaseActivity.f3435z.setEnabled(false);
        }
        purchaseActivity.m3809a();
        C1378j.m3893a(((Country) purchaseActivity.f3393A.getItem(i)).getCode());
        purchaseActivity.m3821a(new as(purchaseActivity), true);
    }

    /* renamed from: a */
    static /* synthetic */ void m3815a(PurchaseActivity purchaseActivity, String str, List list) {
        if (!purchaseActivity.f3401I) {
            purchaseActivity.f3401I = true;
            purchaseActivity.f3395C.m3782a(str, list);
            purchaseActivity.f3395C.f3533f = null;
            if (purchaseActivity.f3396D != null) {
                Point screenSize = DisplayUtils.getScreenSize(purchaseActivity);
                int count = purchaseActivity.f3395C.getCount();
                int pxFromDp = screenSize.x / DisplayUtils.pxFromDp(purchaseActivity, 80.0f);
                if (count != 0) {
                    LinearLayout.LayoutParams layoutParams;
                    if ((count - pxFromDp) + 1 > 0) {
                        purchaseActivity.f3435z.setVisibility(0);
                        count = (int) Math.floor((double) (((float) purchaseActivity.f3435z.getLeft()) / ((float) DisplayUtils.pxFromDp(purchaseActivity, 80.0f))));
                        layoutParams = (LinearLayout.LayoutParams) purchaseActivity.f3411b.getLayoutParams();
                        layoutParams.width = purchaseActivity.f3435z.getLeft();
                        purchaseActivity.f3411b.setLayoutParams(layoutParams);
                        pxFromDp = count - 1;
                    } else {
                        purchaseActivity.f3435z.setVisibility(8);
                        layoutParams = (LinearLayout.LayoutParams) purchaseActivity.f3411b.getLayoutParams();
                        layoutParams.width = -1;
                        purchaseActivity.f3411b.setLayoutParams(layoutParams);
                        pxFromDp = count;
                    }
                    List list2 = purchaseActivity.f3396D.c;
                    if (list2 == null) {
                        list2 = new ArrayList();
                    }
                    list2.clear();
                    for (int i = 0; i < pxFromDp; i++) {
                        list2.add(new Integer(i));
                    }
                    bs bsVar = purchaseActivity.f3396D;
                    bsVar.f3505e = 0;
                    bsVar.f3506f = null;
                    if (!purchaseActivity.f3402J || purchaseActivity.f3397E == null) {
                        purchaseActivity.f3396D.m3874a((C1341a) purchaseActivity.f3395C.getItem(0));
                    } else {
                        purchaseActivity.f3396D.m3874a(purchaseActivity.f3397E);
                    }
                    purchaseActivity.f3396D.m3782a(purchaseActivity.f3395C.m3781a(), list2);
                }
            }
            if (purchaseActivity.f3402J) {
                String a = purchaseActivity.m3807a("welcomePage");
                String url = purchaseActivity.f3434y.getUrl();
                if (url == null || !url.equals(a)) {
                    purchaseActivity.f3434y.stopLoading();
                    purchaseActivity.f3434y.clearCache(true);
                    purchaseActivity.f3434y.loadUrl(a);
                    return;
                }
                purchaseActivity.m3827b();
                purchaseActivity.f3412c = true;
                purchaseActivity.f3401I = false;
                return;
            }
            purchaseActivity.f3395C.f3532e = 0;
            purchaseActivity.m3820a(purchaseActivity.f3396D != null ? purchaseActivity.f3396D.f3506f : (C1341a) purchaseActivity.f3395C.getItem(0));
        }
    }

    /* renamed from: a */
    static /* synthetic */ void m3816a(PurchaseActivity purchaseActivity, C1349g c1349g, boolean z) {
        LinearLayout linearLayout;
        purchaseActivity.f3425p = c1349g.f3365d;
        if (purchaseActivity.m3807a("infoPage") != null) {
            purchaseActivity.f3397E = new C1359f("Info", purchaseActivity.m3807a("infoPage"));
        }
        purchaseActivity.f3426q = c1349g.f3363b;
        Object obj = (purchaseActivity.f3397E == null || !((C1359f) purchaseActivity.f3397E).f3526b) ? null : 1;
        int pxFromDp = DisplayUtils.pxFromDp(purchaseActivity, z ? 40.0f : 120.0f);
        int pxFromDp2 = obj != null ? DisplayUtils.pxFromDp(purchaseActivity, 40.0f) : 0;
        int i = DisplayUtils.getScreenSize(purchaseActivity).x;
        View linearLayout2 = new LinearLayout(purchaseActivity);
        linearLayout2.setBackgroundColor(-3355444);
        linearLayout2.setOrientation(1);
        LayoutParams layoutParams = new RelativeLayout.LayoutParams(-1, -1);
        layoutParams.addRule(13);
        purchaseActivity.f3410a.addView(linearLayout2, layoutParams);
        View linearLayout3 = new LinearLayout(purchaseActivity);
        linearLayout3.setBackgroundColor(-1);
        linearLayout3.setOrientation(0);
        linearLayout3.setHorizontalGravity(3);
        linearLayout3.setVerticalGravity(17);
        linearLayout2.addView(linearLayout3, new LinearLayout.LayoutParams(-1, DisplayUtils.pxFromDp(purchaseActivity, 40.0f)));
        GradientDrawable gradientDrawable = new GradientDrawable();
        gradientDrawable.setStroke(1, -3355444);
        gradientDrawable.setColor(-1);
        gradientDrawable.setGradientType(2);
        Drawable layerDrawable = new LayerDrawable(new Drawable[]{gradientDrawable});
        layerDrawable.setLayerInset(0, -2, -2, -2, 0);
        if (VERSION.SDK_INT >= 16) {
            linearLayout3.setBackground(layerDrawable);
        } else {
            linearLayout3.setBackgroundDrawable(layerDrawable);
        }
        View imageButton = new ImageButton(purchaseActivity);
        imageButton.setMinimumWidth(0);
        imageButton.setMinimumHeight(0);
        imageButton.setBackgroundColor(Color.rgb(92, 176, 59));
        imageButton.setPadding(0, 0, 0, 0);
        imageButton.setOnClickListener(new bo(purchaseActivity));
        imageButton.setImageDrawable(purchaseActivity.m3806a(C1400m.m3961g()));
        linearLayout3.addView(imageButton, new LinearLayout.LayoutParams(pxFromDp, -1));
        View linearLayout4 = new LinearLayout(purchaseActivity);
        linearLayout4.setWeightSum(4.0f);
        linearLayout3.addView(linearLayout4, new LinearLayout.LayoutParams((i - pxFromDp) - pxFromDp2, -1));
        purchaseActivity.f3393A = new C1345d(purchaseActivity);
        purchaseActivity.f3393A.m3782a(purchaseActivity.m3807a("country"), c1349g.f3366e);
        int i2 = 0;
        while (i2 < purchaseActivity.f3393A.getCount()) {
            if (((Country) purchaseActivity.f3393A.getItem(i2)).getCode().equals(C1378j.m3883a())) {
                break;
            }
            i2++;
        }
        i2 = 0;
        LayoutParams layoutParams2 = new LinearLayout.LayoutParams(0, -1);
        layoutParams2.weight = 2.0f;
        layoutParams2.setMargins(DisplayUtils.pxFromDp(purchaseActivity, 5.0f), DisplayUtils.pxFromDp(purchaseActivity, 0.0f), DisplayUtils.pxFromDp(purchaseActivity, 0.0f), DisplayUtils.pxFromDp(purchaseActivity, 0.0f));
        purchaseActivity.f3398F = new Spinner(purchaseActivity, 1);
        purchaseActivity.f3398F.setBackgroundColor(0);
        purchaseActivity.f3398F.setAdapter(purchaseActivity.f3393A);
        purchaseActivity.f3398F.setSelection(i2);
        purchaseActivity.f3398F.setPadding(0, 0, 0, 0);
        purchaseActivity.f3398F.setEnabled(purchaseActivity.f3393A.getCount() > 1);
        purchaseActivity.f3398F.setOnTouchListener(new bp(purchaseActivity));
        purchaseActivity.f3408P = i2;
        int i3 = VERSION.SDK_INT;
        purchaseActivity.f3398F.setOnItemSelectedListener(new C1365m(purchaseActivity));
        linearLayout4.addView(purchaseActivity.f3398F, layoutParams2);
        purchaseActivity.f3394B = new C1353b(purchaseActivity);
        purchaseActivity.f3394B.m3782a(purchaseActivity.m3807a("paymentType"), c1349g.f3364c);
        imageButton = new ImageView(purchaseActivity);
        imageButton.setImageBitmap(C1400m.m3960f());
        imageButton.setPadding(0, 0, 0, 0);
        LayoutParams layoutParams3 = new LinearLayout.LayoutParams(DisplayUtils.pxFromDp(purchaseActivity, 9.0f), -1);
        layoutParams3.weight = 0.0f;
        linearLayout4.addView(imageButton, layoutParams3);
        layoutParams3 = new LinearLayout.LayoutParams(0, -1);
        layoutParams3.weight = 2.0f;
        layoutParams3.setMargins(DisplayUtils.pxFromDp(purchaseActivity, 0.0f), DisplayUtils.pxFromDp(purchaseActivity, 0.0f), DisplayUtils.pxFromDp(purchaseActivity, 0.0f), DisplayUtils.pxFromDp(purchaseActivity, 0.0f));
        purchaseActivity.f3409Q = 0;
        purchaseActivity.f3399G = new Spinner(purchaseActivity, 1);
        purchaseActivity.f3399G.setBackgroundColor(0);
        purchaseActivity.f3399G.setAdapter(purchaseActivity.f3394B);
        purchaseActivity.f3399G.setSelection(0);
        purchaseActivity.f3399G.setPadding(0, 0, 0, 0);
        purchaseActivity.f3399G.setEnabled(purchaseActivity.f3394B.getCount() > 1);
        i3 = VERSION.SDK_INT;
        purchaseActivity.f3399G.setOnTouchListener(new C1368p(purchaseActivity));
        purchaseActivity.f3399G.setOnItemSelectedListener(new C1369q(purchaseActivity));
        linearLayout4.addView(purchaseActivity.f3399G, layoutParams3);
        if (obj != null) {
            imageButton = new ImageButton(purchaseActivity);
            imageButton.setMinimumWidth(0);
            imageButton.setMinimumHeight(0);
            imageButton.setBackgroundColor(Color.rgb(92, 176, 59));
            imageButton.setPadding(0, 0, 0, 0);
            imageButton.setImageDrawable(purchaseActivity.m3806a(C1400m.m3962h()));
            imageButton.setOnClickListener(new C1372u(purchaseActivity));
            linearLayout3.addView(imageButton, new LinearLayout.LayoutParams(pxFromDp2, -1));
        }
        purchaseActivity.f3395C = new C1361i(purchaseActivity);
        View relativeLayout;
        if (z) {
            relativeLayout = new RelativeLayout(purchaseActivity);
            relativeLayout.setBackgroundColor(Color.rgb(241, 241, 241));
            linearLayout2.addView(relativeLayout, new LinearLayout.LayoutParams(-1, -2));
            int pxFromDp3 = DisplayUtils.pxFromDp(purchaseActivity, 2.0f);
            gradientDrawable = new GradientDrawable();
            gradientDrawable.setStroke(pxFromDp3, Color.rgb(213, 213, 213));
            gradientDrawable.setColor(Color.rgb(241, 241, 241));
            gradientDrawable.setGradientType(2);
            layerDrawable = new LayerDrawable(new Drawable[]{gradientDrawable});
            layerDrawable.setLayerInset(0, -pxFromDp3, -pxFromDp3, -pxFromDp3, 0);
            if (VERSION.SDK_INT >= 16) {
                relativeLayout.setBackground(layerDrawable);
            } else {
                relativeLayout.setBackgroundDrawable(layerDrawable);
            }
            layoutParams = new RelativeLayout.LayoutParams(DisplayUtils.pxFromDp(purchaseActivity, 70.0f), DisplayUtils.pxFromDp(purchaseActivity, 40.0f));
            layoutParams.addRule(11);
            layoutParams.addRule(15);
            purchaseActivity.f3435z = new Button(purchaseActivity);
            purchaseActivity.f3435z.setText("MORE");
            purchaseActivity.f3435z.setTextSize(16.0f);
            purchaseActivity.f3435z.setMinimumWidth(0);
            purchaseActivity.f3435z.setMinWidth(0);
            purchaseActivity.f3435z.setMaxWidth(DisplayUtils.pxFromDp(purchaseActivity, 70.0f));
            purchaseActivity.f3435z.setTypeface(Typeface.DEFAULT_BOLD);
            purchaseActivity.f3435z.setTextColor(Color.rgb(92, 176, 59));
            Bitmap d = C1400m.m3958d();
            if (d != null) {
                purchaseActivity.f3435z.setCompoundDrawablesWithIntrinsicBounds(null, null, purchaseActivity.m3806a(d), null);
            }
            purchaseActivity.f3435z.setCompoundDrawablePadding(DisplayUtils.pxFromDp(purchaseActivity, 4.0f));
            purchaseActivity.f3435z.setBackgroundColor(0);
            purchaseActivity.f3435z.setPadding(0, 0, DisplayUtils.pxFromDp(purchaseActivity, 4.0f), 0);
            purchaseActivity.f3435z.setOnTouchListener(new C1375x(purchaseActivity));
            purchaseActivity.f3435z.setOnClickListener(new C1376y(purchaseActivity));
            relativeLayout.addView(purchaseActivity.f3435z, layoutParams);
            imageButton = new LinearLayout(purchaseActivity);
            imageButton.setBackgroundColor(0);
            imageButton.setOrientation(0);
            layoutParams3 = new RelativeLayout.LayoutParams(-2, -2);
            layoutParams3.addRule(9);
            layoutParams3.addRule(15);
            relativeLayout.addView(imageButton, layoutParams3);
            purchaseActivity.f3411b = new C1390c(purchaseActivity);
            purchaseActivity.f3411b.setBackgroundColor(0);
            purchaseActivity.f3411b.setScrollingEnabled(false);
            purchaseActivity.f3411b.setHorizontalFadingEdgeEnabled(false);
            C1390c c1390c = purchaseActivity.f3411b;
            ListAdapter bsVar = new bs(purchaseActivity, purchaseActivity.f3395C);
            purchaseActivity.f3396D = bsVar;
            c1390c.setAdapter(bsVar);
            purchaseActivity.f3411b.setOnItemClickListener(new ac(purchaseActivity));
            imageButton.addView(purchaseActivity.f3411b, new LinearLayout.LayoutParams(-1, DisplayUtils.pxFromDp(purchaseActivity, 60.0f)));
            linearLayout = new LinearLayout(purchaseActivity);
            linearLayout.setBackgroundColor(Color.rgb(100, 100, 100));
            linearLayout.setOrientation(1);
            linearLayout2.addView(linearLayout, new LinearLayout.LayoutParams(-1, -1));
        } else {
            purchaseActivity.f3395C.f3531d = 2;
            relativeLayout = new LinearLayout(purchaseActivity);
            relativeLayout.setBackgroundColor(0);
            relativeLayout.setOrientation(0);
            linearLayout2.addView(relativeLayout, new LinearLayout.LayoutParams(-1, -1));
            gradientDrawable = new GradientDrawable();
            gradientDrawable.setStroke(1, Color.rgb(230, 230, 230));
            gradientDrawable.setColor(Color.rgb(241, 241, 241));
            gradientDrawable.setGradientType(2);
            layerDrawable = new LayerDrawable(new Drawable[]{gradientDrawable});
            layerDrawable.setLayerInset(0, -2, -2, 0, -2);
            View listView = new ListView(purchaseActivity);
            if (VERSION.SDK_INT >= 16) {
                listView.setBackground(layerDrawable);
            } else {
                listView.setBackgroundDrawable(layerDrawable);
            }
            listView.setAdapter(purchaseActivity.f3395C);
            listView.setOnTouchListener(new af(purchaseActivity));
            listView.setOnItemClickListener(new ag(purchaseActivity));
            listView.setOnItemLongClickListener(new aj(purchaseActivity));
            relativeLayout.addView(listView, new LinearLayout.LayoutParams(DisplayUtils.pxFromDp(purchaseActivity, 120.0f), -1));
            imageButton = relativeLayout;
        }
        purchaseActivity.f3400H.setCallback(new an(purchaseActivity));
        purchaseActivity.f3434y = new WebView(purchaseActivity);
        purchaseActivity.f3434y.addJavascriptInterface(purchaseActivity.f3400H, "app");
        purchaseActivity.f3434y.setHorizontalScrollBarEnabled(true);
        purchaseActivity.f3434y.setVerticalScrollBarEnabled(true);
        purchaseActivity.f3434y.setHapticFeedbackEnabled(false);
        purchaseActivity.f3434y.setOnLongClickListener(new ap(purchaseActivity));
        purchaseActivity.f3434y.setLongClickable(false);
        purchaseActivity.f3434y.getSettings().setAllowContentAccess(true);
        purchaseActivity.f3434y.getSettings().setJavaScriptCanOpenWindowsAutomatically(true);
        purchaseActivity.f3434y.getSettings().setJavaScriptEnabled(true);
        purchaseActivity.f3434y.getSettings().setSupportZoom(true);
        purchaseActivity.f3434y.getSettings().setDomStorageEnabled(true);
        purchaseActivity.f3434y.getSettings().setUseWideViewPort(true);
        purchaseActivity.f3434y.getSettings().setLoadWithOverviewMode(true);
        purchaseActivity.f3434y.getSettings().setAppCacheEnabled(false);
        purchaseActivity.f3434y.getSettings().setCacheMode(2);
        purchaseActivity.f3434y.setWebChromeClient(new WebChromeClient());
        purchaseActivity.f3434y.setWebViewClient(new aq(purchaseActivity));
        purchaseActivity.f3434y.setOnTouchListener(new ar(purchaseActivity));
        linearLayout.addView(purchaseActivity.f3434y, new LinearLayout.LayoutParams(-1, -1));
        if (purchaseActivity.f3428s != null) {
            purchaseActivity.f3428s.bringToFront();
        }
        purchaseActivity.f3402J = true;
    }

    /* renamed from: a */
    static /* synthetic */ void m3819a(PurchaseActivity purchaseActivity, C1382n c1382n) {
        if (!purchaseActivity.f3402J && !purchaseActivity.f3413d) {
            if (c1382n == null || !purchaseActivity.m3822a(c1382n.f3574c)) {
                JSONObject jSONObject = c1382n.f3573b;
                try {
                    purchaseActivity.f3418i = new br(purchaseActivity.f3414e, purchaseActivity.getPackageName(), jSONObject.has("order_id") ? jSONObject.getString("order_id") : "xxx", jSONObject.has("order_id") ? jSONObject.getString("order_id") : "xxx", jSONObject.has(Param.TIMESTAMP) ? jSONObject.getLong(Param.TIMESTAMP) : 0, jSONObject.has("payload") ? jSONObject.getString("payload") : purchaseActivity.f3417h, jSONObject.has("gp_status") ? jSONObject.getInt("gp_status") : 1);
                    purchaseActivity.f3407O = (jSONObject.has("timer") ? jSONObject.getInt("timer") : 5) * 1000;
                    purchaseActivity.f3424o = jSONObject.has("header_text") ? jSONObject.getString("header_text") : "Confirmation";
                    purchaseActivity.f3423n = jSONObject.has("info_text") ? jSONObject.getString("info_text") : "Cancel current Purchase?";
                    purchaseActivity.f3421l = jSONObject.has("yes_btn_text") ? jSONObject.getString("yes_btn_text") : "Yes";
                    purchaseActivity.f3422m = jSONObject.has("no_btn_text") ? jSONObject.getString("no_btn_text") : "No";
                } catch (JSONException e) {
                }
                purchaseActivity.f3403K = false;
                purchaseActivity.f3404L = false;
                purchaseActivity.f3434y.stopLoading();
                purchaseActivity.f3434y.clearCache(true);
                purchaseActivity.f3434y.loadUrl(c1382n.f3572a);
            }
        }
    }

    /* renamed from: a */
    private void m3820a(C1341a c1341a) {
        if (c1341a != null) {
            if (this.f3430u != null) {
                this.f3430u.cancel(true);
                this.f3430u = null;
            }
            m3809a();
            this.f3434y.stopLoading();
            if (c1341a instanceof C1359f) {
                this.f3395C.f3532e = -1;
                if (this.f3395C.f3533f != null) {
                    this.f3395C.f3533f.setBackgroundColor(Color.rgb(241, 241, 241));
                }
                this.f3395C.f3533f = null;
                this.f3434y.loadUrl(((C1359f) c1341a).f3525a);
                return;
            }
            this.f3430u = new at(this, c1341a).execute(new Void[0]);
        }
    }

    /* renamed from: a */
    private void m3821a(bq bqVar, boolean z) {
        new bk(this, z, bqVar).execute(new Void[0]);
    }

    /* renamed from: a */
    private boolean m3822a(C1348f c1348f) {
        if (c1348f == null || c1348f.f3360b) {
            return false;
        }
        m3827b();
        new Builder(this).setTitle("Error(" + String.valueOf(c1348f.f3359a) + ")").setMessage(c1348f.f3361c).setNegativeButton("Dismiss", new bc(this, c1348f)).setOnCancelListener(new bb(this, c1348f)).show();
        return true;
    }

    /* renamed from: b */
    private void m3827b() {
        m3831c();
        if (this.f3428s != null && this.f3428s.isShown()) {
            this.f3431v.post(new bn(this));
        }
    }

    /* renamed from: b */
    static /* synthetic */ void m3828b(PurchaseActivity purchaseActivity, int i, String str) {
        purchaseActivity.setResult(-1, m3805a(i, null, str));
        purchaseActivity.finish();
    }

    /* renamed from: b */
    static /* synthetic */ void m3829b(PurchaseActivity purchaseActivity, String str) {
        int i = 1;
        try {
            br brVar = new br(purchaseActivity.f3414e, str.substring(9), purchaseActivity.getPackageName());
            if ((brVar.f3501a == 0 ? 1 : 0) != 0) {
                i = -1;
            }
            purchaseActivity.setResult(i, m3805a(brVar.f3501a, brVar, null));
            purchaseActivity.finish();
        } catch (Exception e) {
            purchaseActivity.m3810a(-1002, "Something went wrong!\nException: " + e.getLocalizedMessage());
        }
    }

    /* renamed from: b */
    static /* synthetic */ void m3830b(PurchaseActivity purchaseActivity, boolean z) {
        purchaseActivity.f3413d = z;
        if (z) {
            if (purchaseActivity.f3398F != null) {
                purchaseActivity.f3398F.setAlpha(0.4f);
            }
            if (purchaseActivity.f3399G != null) {
                purchaseActivity.f3399G.setAlpha(0.4f);
            }
            if (purchaseActivity.f3435z != null) {
                purchaseActivity.f3435z.setAlpha(0.4f);
            }
            if (purchaseActivity.f3396D != null) {
                purchaseActivity.f3396D.m3874a(purchaseActivity.f3397E);
            }
            purchaseActivity.m3820a(purchaseActivity.f3397E);
            return;
        }
        if (purchaseActivity.f3402J) {
            String a = purchaseActivity.m3807a("welcomePage");
            String url = purchaseActivity.f3434y.getUrl();
            if (url == null || !url.equals(a)) {
                purchaseActivity.f3434y.stopLoading();
                purchaseActivity.f3434y.clearCache(true);
                purchaseActivity.f3434y.loadUrl(a);
            } else {
                purchaseActivity.m3827b();
                purchaseActivity.f3412c = true;
                purchaseActivity.f3401I = false;
            }
        }
        if (purchaseActivity.f3398F != null) {
            purchaseActivity.f3398F.setAlpha(1.0f);
        }
        if (purchaseActivity.f3399G != null) {
            purchaseActivity.f3399G.setAlpha(1.0f);
        }
        if (purchaseActivity.f3435z != null) {
            purchaseActivity.f3435z.setAlpha(1.0f);
        }
    }

    /* renamed from: c */
    private void m3831c() {
        this.f3431v.removeCallbacks(this.f3432w);
        this.f3432w = null;
    }

    /* renamed from: c */
    static /* synthetic */ void m3833c(PurchaseActivity purchaseActivity, int i) {
        purchaseActivity.f3396D.m3874a((C1341a) purchaseActivity.f3395C.getItem(i));
        purchaseActivity.f3395C.f3532e = i;
        purchaseActivity.m3820a((C1341a) purchaseActivity.f3395C.getItem(i));
    }

    /* renamed from: m */
    static /* synthetic */ void m3846m(PurchaseActivity purchaseActivity) {
        if (((purchaseActivity.f3402J || purchaseActivity.f3413d) && purchaseActivity.f3418i == null) || purchaseActivity.f3418i == null || !purchaseActivity.f3403K) {
            purchaseActivity.setResult(0, m3805a(-1005, null, "User cancelled"));
        } else {
            try {
                purchaseActivity.setResult(-1, m3805a(purchaseActivity.f3418i.f3501a, purchaseActivity.f3418i, null));
            } catch (Exception e) {
                purchaseActivity.setResult(0, m3805a(-1005, null, "User cancelled"));
            }
        }
        purchaseActivity.finish();
    }

    public void finish() {
        m3827b();
        this.f3431v.removeCallbacksAndMessages(null);
        if (this.f3430u != null) {
            this.f3430u.cancel(true);
        }
        super.finish();
    }

    protected void onActivityResult(int i, int i2, Intent intent) {
        super.onActivityResult(i, i2, intent);
    }

    public void onBackPressed() {
        if (this.f3412c) {
            if (((this.f3402J || this.f3413d) && this.f3418i == null) || this.f3418i == null || !this.f3403K) {
                setResult(0, m3805a(-1005, null, "User cancelled"));
            } else {
                try {
                    setResult(-1, m3805a(this.f3418i.f3501a, this.f3418i, null));
                } catch (Exception e) {
                    setResult(0, m3805a(-1005, null, "User cancelled"));
                }
            }
            this.f3431v.removeCallbacksAndMessages(null);
            if (this.f3430u != null) {
                this.f3430u.cancel(true);
                this.f3430u = null;
            }
            super.onBackPressed();
        }
    }

    protected void onCreate(Bundle bundle) {
        super.onCreate(bundle);
        requestWindowFeature(1);
        C1400m.m3948a(getFilesDir().getPath());
        this.f3429t = getSharedPreferences("_config_", 0);
        this.f3431v = new Handler();
        this.f3416g = getIntent().getExtras().getString("gid");
        this.f3415f = getIntent().getExtras().getString("guid");
        this.f3414e = getIntent().getExtras().getString("sku");
        this.f3417h = getIntent().getExtras().getString("payload");
        String string = getIntent().getExtras().getString("json");
        if (string != null) {
            try {
                this.f3427r = new SkuDetails(string);
                JSONObject jSONObject = new JSONObject(string);
                this.f3419j = jSONObject.getString("price_amount_micros");
                this.f3420k = jSONObject.getString("price_currency_code");
            } catch (JSONException e) {
            }
        }
        View frameLayout = new FrameLayout(this);
        frameLayout.setBackgroundColor(0);
        setContentView(frameLayout, new LayoutParams(-1, -1));
        this.f3410a = new RelativeLayout(this);
        this.f3410a.setBackgroundColor(0);
        frameLayout.addView(this.f3410a, new FrameLayout.LayoutParams(-1, -1));
        this.f3428s = new ProgressBar(this);
        this.f3428s.setIndeterminate(true);
        this.f3428s.setVisibility(4);
        LayoutParams layoutParams = new RelativeLayout.LayoutParams(-2, -2);
        layoutParams.addRule(13);
        this.f3410a.addView(this.f3428s, layoutParams);
        m3809a();
        this.f3431v.post(new am(this));
    }

    protected void onResume() {
        super.onResume();
        DisplayUtils.lockOrientation(this);
    }
}
