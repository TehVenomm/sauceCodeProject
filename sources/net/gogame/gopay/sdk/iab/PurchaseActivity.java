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
import io.fabric.sdk.android.services.settings.SettingsJsonConstants;
import java.util.ArrayList;
import java.util.List;
import java.util.Map;
import net.gogame.gopay.sdk.C1026a;
import net.gogame.gopay.sdk.C1029d;
import net.gogame.gopay.sdk.C1032f;
import net.gogame.gopay.sdk.C1033g;
import net.gogame.gopay.sdk.C1062j;
import net.gogame.gopay.sdk.C1066n;
import net.gogame.gopay.sdk.Country;
import net.gogame.gopay.sdk.support.C1074c;
import net.gogame.gopay.sdk.support.C1084m;
import net.gogame.gopay.sdk.support.DisplayUtils;
import org.json.JSONException;
import org.json.JSONObject;
import org.onepf.oms.appstore.googleUtils.SkuDetails;

public class PurchaseActivity extends Activity {
    /* renamed from: A */
    private C1029d f1005A;
    /* renamed from: B */
    private C1037b f1006B;
    /* renamed from: C */
    private C1045i f1007C;
    /* renamed from: D */
    private bs f1008D;
    /* renamed from: E */
    private C1025a f1009E = null;
    /* renamed from: F */
    private Spinner f1010F;
    /* renamed from: G */
    private Spinner f1011G;
    /* renamed from: H */
    private C1044g f1012H = new C1044g();
    /* renamed from: I */
    private boolean f1013I = false;
    /* renamed from: J */
    private boolean f1014J;
    /* renamed from: K */
    private boolean f1015K = false;
    /* renamed from: L */
    private boolean f1016L = false;
    /* renamed from: M */
    private int f1017M = 0;
    /* renamed from: N */
    private int f1018N = 3;
    /* renamed from: O */
    private int f1019O;
    /* renamed from: P */
    private int f1020P;
    /* renamed from: Q */
    private int f1021Q;
    /* renamed from: a */
    RelativeLayout f1022a;
    /* renamed from: b */
    C1074c f1023b;
    /* renamed from: c */
    private boolean f1024c = false;
    /* renamed from: d */
    private boolean f1025d = false;
    /* renamed from: e */
    private String f1026e;
    /* renamed from: f */
    private String f1027f;
    /* renamed from: g */
    private String f1028g;
    /* renamed from: h */
    private String f1029h;
    /* renamed from: i */
    private br f1030i;
    /* renamed from: j */
    private String f1031j;
    /* renamed from: k */
    private String f1032k;
    /* renamed from: l */
    private String f1033l;
    /* renamed from: m */
    private String f1034m;
    /* renamed from: n */
    private String f1035n;
    /* renamed from: o */
    private String f1036o;
    /* renamed from: p */
    private Map f1037p;
    /* renamed from: q */
    private SkuDetails f1038q;
    /* renamed from: r */
    private SkuDetails f1039r;
    /* renamed from: s */
    private ProgressBar f1040s;
    /* renamed from: t */
    private SharedPreferences f1041t;
    /* renamed from: u */
    private AsyncTask f1042u;
    /* renamed from: v */
    private Handler f1043v;
    /* renamed from: w */
    private Runnable f1044w;
    /* renamed from: x */
    private Runnable f1045x = new C1048l(this);
    /* renamed from: y */
    private WebView f1046y;
    /* renamed from: z */
    private Button f1047z;

    /* renamed from: a */
    static /* synthetic */ Dialog m779a(PurchaseActivity purchaseActivity, String str, C1026a c1026a, OnClickListener onClickListener) {
        Dialog dialog = new Dialog(purchaseActivity);
        dialog.setOnCancelListener(new bf(purchaseActivity));
        dialog.getWindow().requestFeature(1);
        View imageButton = new ImageButton(purchaseActivity);
        imageButton.setMinimumWidth(0);
        imageButton.setMinimumHeight(0);
        imageButton.setBackgroundColor(0);
        imageButton.setPadding(0, 0, 0, 0);
        imageButton.setOnClickListener(new bg(purchaseActivity, dialog));
        imageButton.setImageDrawable(purchaseActivity.m781a(C1084m.m936g()));
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
        imageButton.setAdapter(c1026a);
        imageButton.setSelection(purchaseActivity.f1007C.f1144e);
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
        if (DisplayUtils.pxFromDp(purchaseActivity, 60.0f) * c1026a.getCount() >= i) {
            attributes.height = i;
        }
        return dialog;
    }

    /* renamed from: a */
    private static Intent m780a(int i, br brVar, String str) {
        Intent intent = new Intent();
        intent.putExtra("RESPONSE_CODE", i);
        if (brVar != null) {
            intent.putExtra("INAPP_PURCHASE_DATA", brVar.toString());
            intent.putExtra("INAPP_DATA_SIGNATURE", brVar.f1114b);
        }
        if (str != null) {
            intent.putExtra("MESSAGE", str);
        }
        return intent;
    }

    /* renamed from: a */
    private BitmapDrawable m781a(Bitmap bitmap) {
        return bitmap == null ? null : new BitmapDrawable(getResources(), Bitmap.createScaledBitmap(bitmap, DisplayUtils.pxFromDp(this, ((float) bitmap.getWidth()) * 0.5f), DisplayUtils.pxFromDp(this, ((float) bitmap.getHeight()) * 0.5f), false));
    }

    /* renamed from: a */
    private String m782a(String str) {
        return this.f1037p.containsKey(str) ? (String) this.f1037p.get(str) : null;
    }

    /* renamed from: a */
    private void m784a() {
        if (this.f1040s == null || this.f1040s.isShown()) {
            m806c();
            Handler handler = this.f1043v;
            Runnable axVar = new ax(this);
            this.f1044w = axVar;
            handler.postDelayed(axVar, 10000);
            return;
        }
        this.f1043v.post(new bm(this));
    }

    /* renamed from: a */
    private void m785a(int i, String str) {
        this.f1024c = true;
        m802b();
        this.f1043v.post(new ay(this, i, str));
    }

    /* renamed from: a */
    static /* synthetic */ void m787a(PurchaseActivity purchaseActivity, int i) {
        purchaseActivity.f1020P = i;
        purchaseActivity.f1013I = false;
        purchaseActivity.f1010F.setEnabled(false);
        purchaseActivity.f1011G.setEnabled(false);
        if (purchaseActivity.f1047z != null) {
            purchaseActivity.f1047z.setEnabled(false);
        }
        purchaseActivity.m784a();
        C1062j.m868a(((Country) purchaseActivity.f1005A.getItem(i)).getCode());
        purchaseActivity.m796a(new as(purchaseActivity), true);
    }

    /* renamed from: a */
    static /* synthetic */ void m790a(PurchaseActivity purchaseActivity, String str, List list) {
        if (!purchaseActivity.f1013I) {
            purchaseActivity.f1013I = true;
            purchaseActivity.f1007C.m757a(str, list);
            purchaseActivity.f1007C.f1145f = null;
            if (purchaseActivity.f1008D != null) {
                Point screenSize = DisplayUtils.getScreenSize(purchaseActivity);
                int count = purchaseActivity.f1007C.getCount();
                int pxFromDp = screenSize.x / DisplayUtils.pxFromDp(purchaseActivity, 80.0f);
                if (count != 0) {
                    LinearLayout.LayoutParams layoutParams;
                    if ((count - pxFromDp) + 1 > 0) {
                        purchaseActivity.f1047z.setVisibility(0);
                        count = (int) Math.floor((double) (((float) purchaseActivity.f1047z.getLeft()) / ((float) DisplayUtils.pxFromDp(purchaseActivity, 80.0f))));
                        layoutParams = (LinearLayout.LayoutParams) purchaseActivity.f1023b.getLayoutParams();
                        layoutParams.width = purchaseActivity.f1047z.getLeft();
                        purchaseActivity.f1023b.setLayoutParams(layoutParams);
                        pxFromDp = count - 1;
                    } else {
                        purchaseActivity.f1047z.setVisibility(8);
                        layoutParams = (LinearLayout.LayoutParams) purchaseActivity.f1023b.getLayoutParams();
                        layoutParams.width = -1;
                        purchaseActivity.f1023b.setLayoutParams(layoutParams);
                        pxFromDp = count;
                    }
                    List list2 = purchaseActivity.f1008D.c;
                    if (list2 == null) {
                        list2 = new ArrayList();
                    }
                    list2.clear();
                    for (int i = 0; i < pxFromDp; i++) {
                        list2.add(new Integer(i));
                    }
                    bs bsVar = purchaseActivity.f1008D;
                    bsVar.f1117e = 0;
                    bsVar.f1118f = null;
                    if (!purchaseActivity.f1014J || purchaseActivity.f1009E == null) {
                        purchaseActivity.f1008D.m849a((C1025a) purchaseActivity.f1007C.getItem(0));
                    } else {
                        purchaseActivity.f1008D.m849a(purchaseActivity.f1009E);
                    }
                    purchaseActivity.f1008D.m757a(purchaseActivity.f1007C.m756a(), list2);
                }
            }
            if (purchaseActivity.f1014J) {
                String a = purchaseActivity.m782a("welcomePage");
                String url = purchaseActivity.f1046y.getUrl();
                if (url == null || !url.equals(a)) {
                    purchaseActivity.f1046y.stopLoading();
                    purchaseActivity.f1046y.clearCache(true);
                    purchaseActivity.f1046y.loadUrl(a);
                    return;
                }
                purchaseActivity.m802b();
                purchaseActivity.f1024c = true;
                purchaseActivity.f1013I = false;
                return;
            }
            purchaseActivity.f1007C.f1144e = 0;
            purchaseActivity.m795a(purchaseActivity.f1008D != null ? purchaseActivity.f1008D.f1118f : (C1025a) purchaseActivity.f1007C.getItem(0));
        }
    }

    /* renamed from: a */
    static /* synthetic */ void m791a(PurchaseActivity purchaseActivity, C1033g c1033g, boolean z) {
        LinearLayout linearLayout;
        purchaseActivity.f1037p = c1033g.f977d;
        if (purchaseActivity.m782a("infoPage") != null) {
            purchaseActivity.f1009E = new C1043f("Info", purchaseActivity.m782a("infoPage"));
        }
        purchaseActivity.f1038q = c1033g.f975b;
        Object obj = (purchaseActivity.f1009E == null || !((C1043f) purchaseActivity.f1009E).f1138b) ? null : 1;
        int pxFromDp = DisplayUtils.pxFromDp(purchaseActivity, z ? 40.0f : 120.0f);
        int pxFromDp2 = obj != null ? DisplayUtils.pxFromDp(purchaseActivity, 40.0f) : 0;
        int i = DisplayUtils.getScreenSize(purchaseActivity).x;
        View linearLayout2 = new LinearLayout(purchaseActivity);
        linearLayout2.setBackgroundColor(-3355444);
        linearLayout2.setOrientation(1);
        LayoutParams layoutParams = new RelativeLayout.LayoutParams(-1, -1);
        layoutParams.addRule(13);
        purchaseActivity.f1022a.addView(linearLayout2, layoutParams);
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
        imageButton.setImageDrawable(purchaseActivity.m781a(C1084m.m936g()));
        linearLayout3.addView(imageButton, new LinearLayout.LayoutParams(pxFromDp, -1));
        View linearLayout4 = new LinearLayout(purchaseActivity);
        linearLayout4.setWeightSum(4.0f);
        linearLayout3.addView(linearLayout4, new LinearLayout.LayoutParams((i - pxFromDp) - pxFromDp2, -1));
        purchaseActivity.f1005A = new C1029d(purchaseActivity);
        purchaseActivity.f1005A.m757a(purchaseActivity.m782a("country"), c1033g.f978e);
        int i2 = 0;
        while (i2 < purchaseActivity.f1005A.getCount()) {
            if (((Country) purchaseActivity.f1005A.getItem(i2)).getCode().equals(C1062j.m858a())) {
                break;
            }
            i2++;
        }
        i2 = 0;
        LayoutParams layoutParams2 = new LinearLayout.LayoutParams(0, -1);
        layoutParams2.weight = 2.0f;
        layoutParams2.setMargins(DisplayUtils.pxFromDp(purchaseActivity, 5.0f), DisplayUtils.pxFromDp(purchaseActivity, 0.0f), DisplayUtils.pxFromDp(purchaseActivity, 0.0f), DisplayUtils.pxFromDp(purchaseActivity, 0.0f));
        purchaseActivity.f1010F = new Spinner(purchaseActivity, 1);
        purchaseActivity.f1010F.setBackgroundColor(0);
        purchaseActivity.f1010F.setAdapter(purchaseActivity.f1005A);
        purchaseActivity.f1010F.setSelection(i2);
        purchaseActivity.f1010F.setPadding(0, 0, 0, 0);
        purchaseActivity.f1010F.setEnabled(purchaseActivity.f1005A.getCount() > 1);
        purchaseActivity.f1010F.setOnTouchListener(new bp(purchaseActivity));
        purchaseActivity.f1020P = i2;
        int i3 = VERSION.SDK_INT;
        purchaseActivity.f1010F.setOnItemSelectedListener(new C1049m(purchaseActivity));
        linearLayout4.addView(purchaseActivity.f1010F, layoutParams2);
        purchaseActivity.f1006B = new C1037b(purchaseActivity);
        purchaseActivity.f1006B.m757a(purchaseActivity.m782a("paymentType"), c1033g.f976c);
        imageButton = new ImageView(purchaseActivity);
        imageButton.setImageBitmap(C1084m.m935f());
        imageButton.setPadding(0, 0, 0, 0);
        LayoutParams layoutParams3 = new LinearLayout.LayoutParams(DisplayUtils.pxFromDp(purchaseActivity, 9.0f), -1);
        layoutParams3.weight = 0.0f;
        linearLayout4.addView(imageButton, layoutParams3);
        layoutParams3 = new LinearLayout.LayoutParams(0, -1);
        layoutParams3.weight = 2.0f;
        layoutParams3.setMargins(DisplayUtils.pxFromDp(purchaseActivity, 0.0f), DisplayUtils.pxFromDp(purchaseActivity, 0.0f), DisplayUtils.pxFromDp(purchaseActivity, 0.0f), DisplayUtils.pxFromDp(purchaseActivity, 0.0f));
        purchaseActivity.f1021Q = 0;
        purchaseActivity.f1011G = new Spinner(purchaseActivity, 1);
        purchaseActivity.f1011G.setBackgroundColor(0);
        purchaseActivity.f1011G.setAdapter(purchaseActivity.f1006B);
        purchaseActivity.f1011G.setSelection(0);
        purchaseActivity.f1011G.setPadding(0, 0, 0, 0);
        purchaseActivity.f1011G.setEnabled(purchaseActivity.f1006B.getCount() > 1);
        i3 = VERSION.SDK_INT;
        purchaseActivity.f1011G.setOnTouchListener(new C1052p(purchaseActivity));
        purchaseActivity.f1011G.setOnItemSelectedListener(new C1053q(purchaseActivity));
        linearLayout4.addView(purchaseActivity.f1011G, layoutParams3);
        if (obj != null) {
            imageButton = new ImageButton(purchaseActivity);
            imageButton.setMinimumWidth(0);
            imageButton.setMinimumHeight(0);
            imageButton.setBackgroundColor(Color.rgb(92, 176, 59));
            imageButton.setPadding(0, 0, 0, 0);
            imageButton.setImageDrawable(purchaseActivity.m781a(C1084m.m937h()));
            imageButton.setOnClickListener(new C1056u(purchaseActivity));
            linearLayout3.addView(imageButton, new LinearLayout.LayoutParams(pxFromDp2, -1));
        }
        purchaseActivity.f1007C = new C1045i(purchaseActivity);
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
            purchaseActivity.f1047z = new Button(purchaseActivity);
            purchaseActivity.f1047z.setText("MORE");
            purchaseActivity.f1047z.setTextSize(16.0f);
            purchaseActivity.f1047z.setMinimumWidth(0);
            purchaseActivity.f1047z.setMinWidth(0);
            purchaseActivity.f1047z.setMaxWidth(DisplayUtils.pxFromDp(purchaseActivity, 70.0f));
            purchaseActivity.f1047z.setTypeface(Typeface.DEFAULT_BOLD);
            purchaseActivity.f1047z.setTextColor(Color.rgb(92, 176, 59));
            Bitmap d = C1084m.m933d();
            if (d != null) {
                purchaseActivity.f1047z.setCompoundDrawablesWithIntrinsicBounds(null, null, purchaseActivity.m781a(d), null);
            }
            purchaseActivity.f1047z.setCompoundDrawablePadding(DisplayUtils.pxFromDp(purchaseActivity, 4.0f));
            purchaseActivity.f1047z.setBackgroundColor(0);
            purchaseActivity.f1047z.setPadding(0, 0, DisplayUtils.pxFromDp(purchaseActivity, 4.0f), 0);
            purchaseActivity.f1047z.setOnTouchListener(new C1059x(purchaseActivity));
            purchaseActivity.f1047z.setOnClickListener(new C1060y(purchaseActivity));
            relativeLayout.addView(purchaseActivity.f1047z, layoutParams);
            imageButton = new LinearLayout(purchaseActivity);
            imageButton.setBackgroundColor(0);
            imageButton.setOrientation(0);
            layoutParams3 = new RelativeLayout.LayoutParams(-2, -2);
            layoutParams3.addRule(9);
            layoutParams3.addRule(15);
            relativeLayout.addView(imageButton, layoutParams3);
            purchaseActivity.f1023b = new C1074c(purchaseActivity);
            purchaseActivity.f1023b.setBackgroundColor(0);
            purchaseActivity.f1023b.setScrollingEnabled(false);
            purchaseActivity.f1023b.setHorizontalFadingEdgeEnabled(false);
            C1074c c1074c = purchaseActivity.f1023b;
            ListAdapter bsVar = new bs(purchaseActivity, purchaseActivity.f1007C);
            purchaseActivity.f1008D = bsVar;
            c1074c.setAdapter(bsVar);
            purchaseActivity.f1023b.setOnItemClickListener(new ac(purchaseActivity));
            imageButton.addView(purchaseActivity.f1023b, new LinearLayout.LayoutParams(-1, DisplayUtils.pxFromDp(purchaseActivity, 60.0f)));
            linearLayout = new LinearLayout(purchaseActivity);
            linearLayout.setBackgroundColor(Color.rgb(100, 100, 100));
            linearLayout.setOrientation(1);
            linearLayout2.addView(linearLayout, new LinearLayout.LayoutParams(-1, -1));
        } else {
            purchaseActivity.f1007C.f1143d = 2;
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
            listView.setAdapter(purchaseActivity.f1007C);
            listView.setOnTouchListener(new af(purchaseActivity));
            listView.setOnItemClickListener(new ag(purchaseActivity));
            listView.setOnItemLongClickListener(new aj(purchaseActivity));
            relativeLayout.addView(listView, new LinearLayout.LayoutParams(DisplayUtils.pxFromDp(purchaseActivity, 120.0f), -1));
            imageButton = relativeLayout;
        }
        purchaseActivity.f1012H.setCallback(new an(purchaseActivity));
        purchaseActivity.f1046y = new WebView(purchaseActivity);
        purchaseActivity.f1046y.addJavascriptInterface(purchaseActivity.f1012H, SettingsJsonConstants.APP_KEY);
        purchaseActivity.f1046y.setHorizontalScrollBarEnabled(true);
        purchaseActivity.f1046y.setVerticalScrollBarEnabled(true);
        purchaseActivity.f1046y.setHapticFeedbackEnabled(false);
        purchaseActivity.f1046y.setOnLongClickListener(new ap(purchaseActivity));
        purchaseActivity.f1046y.setLongClickable(false);
        purchaseActivity.f1046y.getSettings().setAllowContentAccess(true);
        purchaseActivity.f1046y.getSettings().setJavaScriptCanOpenWindowsAutomatically(true);
        purchaseActivity.f1046y.getSettings().setJavaScriptEnabled(true);
        purchaseActivity.f1046y.getSettings().setSupportZoom(true);
        purchaseActivity.f1046y.getSettings().setDomStorageEnabled(true);
        purchaseActivity.f1046y.getSettings().setUseWideViewPort(true);
        purchaseActivity.f1046y.getSettings().setLoadWithOverviewMode(true);
        purchaseActivity.f1046y.getSettings().setAppCacheEnabled(false);
        purchaseActivity.f1046y.getSettings().setCacheMode(2);
        purchaseActivity.f1046y.setWebChromeClient(new WebChromeClient());
        purchaseActivity.f1046y.setWebViewClient(new aq(purchaseActivity));
        purchaseActivity.f1046y.setOnTouchListener(new ar(purchaseActivity));
        linearLayout.addView(purchaseActivity.f1046y, new LinearLayout.LayoutParams(-1, -1));
        if (purchaseActivity.f1040s != null) {
            purchaseActivity.f1040s.bringToFront();
        }
        purchaseActivity.f1014J = true;
    }

    /* renamed from: a */
    static /* synthetic */ void m794a(PurchaseActivity purchaseActivity, C1066n c1066n) {
        if (!purchaseActivity.f1014J && !purchaseActivity.f1025d) {
            if (c1066n == null || !purchaseActivity.m797a(c1066n.f1186c)) {
                JSONObject jSONObject = c1066n.f1185b;
                try {
                    purchaseActivity.f1030i = new br(purchaseActivity.f1026e, purchaseActivity.getPackageName(), jSONObject.has("order_id") ? jSONObject.getString("order_id") : "xxx", jSONObject.has("order_id") ? jSONObject.getString("order_id") : "xxx", jSONObject.has(Param.TIMESTAMP) ? jSONObject.getLong(Param.TIMESTAMP) : 0, jSONObject.has("payload") ? jSONObject.getString("payload") : purchaseActivity.f1029h, jSONObject.has("gp_status") ? jSONObject.getInt("gp_status") : 1);
                    purchaseActivity.f1019O = (jSONObject.has("timer") ? jSONObject.getInt("timer") : 5) * 1000;
                    purchaseActivity.f1036o = jSONObject.has("header_text") ? jSONObject.getString("header_text") : "Confirmation";
                    purchaseActivity.f1035n = jSONObject.has("info_text") ? jSONObject.getString("info_text") : "Cancel current Purchase?";
                    purchaseActivity.f1033l = jSONObject.has("yes_btn_text") ? jSONObject.getString("yes_btn_text") : "Yes";
                    purchaseActivity.f1034m = jSONObject.has("no_btn_text") ? jSONObject.getString("no_btn_text") : "No";
                } catch (JSONException e) {
                }
                purchaseActivity.f1015K = false;
                purchaseActivity.f1016L = false;
                purchaseActivity.f1046y.stopLoading();
                purchaseActivity.f1046y.clearCache(true);
                purchaseActivity.f1046y.loadUrl(c1066n.f1184a);
            }
        }
    }

    /* renamed from: a */
    private void m795a(C1025a c1025a) {
        if (c1025a != null) {
            if (this.f1042u != null) {
                this.f1042u.cancel(true);
                this.f1042u = null;
            }
            m784a();
            this.f1046y.stopLoading();
            if (c1025a instanceof C1043f) {
                this.f1007C.f1144e = -1;
                if (this.f1007C.f1145f != null) {
                    this.f1007C.f1145f.setBackgroundColor(Color.rgb(241, 241, 241));
                }
                this.f1007C.f1145f = null;
                this.f1046y.loadUrl(((C1043f) c1025a).f1137a);
                return;
            }
            this.f1042u = new at(this, c1025a).execute(new Void[0]);
        }
    }

    /* renamed from: a */
    private void m796a(bq bqVar, boolean z) {
        new bk(this, z, bqVar).execute(new Void[0]);
    }

    /* renamed from: a */
    private boolean m797a(C1032f c1032f) {
        if (c1032f == null || c1032f.f972b) {
            return false;
        }
        m802b();
        new Builder(this).setTitle("Error(" + String.valueOf(c1032f.f971a) + ")").setMessage(c1032f.f973c).setNegativeButton("Dismiss", new bc(this, c1032f)).setOnCancelListener(new bb(this, c1032f)).show();
        return true;
    }

    /* renamed from: b */
    private void m802b() {
        m806c();
        if (this.f1040s != null && this.f1040s.isShown()) {
            this.f1043v.post(new bn(this));
        }
    }

    /* renamed from: b */
    static /* synthetic */ void m803b(PurchaseActivity purchaseActivity, int i, String str) {
        purchaseActivity.setResult(-1, m780a(i, null, str));
        purchaseActivity.finish();
    }

    /* renamed from: b */
    static /* synthetic */ void m804b(PurchaseActivity purchaseActivity, String str) {
        int i = 1;
        try {
            br brVar = new br(purchaseActivity.f1026e, str.substring(9), purchaseActivity.getPackageName());
            if ((brVar.f1113a == 0 ? 1 : 0) != 0) {
                i = -1;
            }
            purchaseActivity.setResult(i, m780a(brVar.f1113a, brVar, null));
            purchaseActivity.finish();
        } catch (Exception e) {
            purchaseActivity.m785a(-1002, "Something went wrong!\nException: " + e.getLocalizedMessage());
        }
    }

    /* renamed from: b */
    static /* synthetic */ void m805b(PurchaseActivity purchaseActivity, boolean z) {
        purchaseActivity.f1025d = z;
        if (z) {
            if (purchaseActivity.f1010F != null) {
                purchaseActivity.f1010F.setAlpha(0.4f);
            }
            if (purchaseActivity.f1011G != null) {
                purchaseActivity.f1011G.setAlpha(0.4f);
            }
            if (purchaseActivity.f1047z != null) {
                purchaseActivity.f1047z.setAlpha(0.4f);
            }
            if (purchaseActivity.f1008D != null) {
                purchaseActivity.f1008D.m849a(purchaseActivity.f1009E);
            }
            purchaseActivity.m795a(purchaseActivity.f1009E);
            return;
        }
        if (purchaseActivity.f1014J) {
            String a = purchaseActivity.m782a("welcomePage");
            String url = purchaseActivity.f1046y.getUrl();
            if (url == null || !url.equals(a)) {
                purchaseActivity.f1046y.stopLoading();
                purchaseActivity.f1046y.clearCache(true);
                purchaseActivity.f1046y.loadUrl(a);
            } else {
                purchaseActivity.m802b();
                purchaseActivity.f1024c = true;
                purchaseActivity.f1013I = false;
            }
        }
        if (purchaseActivity.f1010F != null) {
            purchaseActivity.f1010F.setAlpha(1.0f);
        }
        if (purchaseActivity.f1011G != null) {
            purchaseActivity.f1011G.setAlpha(1.0f);
        }
        if (purchaseActivity.f1047z != null) {
            purchaseActivity.f1047z.setAlpha(1.0f);
        }
    }

    /* renamed from: c */
    private void m806c() {
        this.f1043v.removeCallbacks(this.f1044w);
        this.f1044w = null;
    }

    /* renamed from: c */
    static /* synthetic */ void m808c(PurchaseActivity purchaseActivity, int i) {
        purchaseActivity.f1008D.m849a((C1025a) purchaseActivity.f1007C.getItem(i));
        purchaseActivity.f1007C.f1144e = i;
        purchaseActivity.m795a((C1025a) purchaseActivity.f1007C.getItem(i));
    }

    /* renamed from: m */
    static /* synthetic */ void m821m(PurchaseActivity purchaseActivity) {
        if (((purchaseActivity.f1014J || purchaseActivity.f1025d) && purchaseActivity.f1030i == null) || purchaseActivity.f1030i == null || !purchaseActivity.f1015K) {
            purchaseActivity.setResult(0, m780a(-1005, null, "User cancelled"));
        } else {
            try {
                purchaseActivity.setResult(-1, m780a(purchaseActivity.f1030i.f1113a, purchaseActivity.f1030i, null));
            } catch (Exception e) {
                purchaseActivity.setResult(0, m780a(-1005, null, "User cancelled"));
            }
        }
        purchaseActivity.finish();
    }

    public void finish() {
        m802b();
        this.f1043v.removeCallbacksAndMessages(null);
        if (this.f1042u != null) {
            this.f1042u.cancel(true);
        }
        super.finish();
    }

    protected void onActivityResult(int i, int i2, Intent intent) {
        super.onActivityResult(i, i2, intent);
    }

    public void onBackPressed() {
        if (this.f1024c) {
            if (((this.f1014J || this.f1025d) && this.f1030i == null) || this.f1030i == null || !this.f1015K) {
                setResult(0, m780a(-1005, null, "User cancelled"));
            } else {
                try {
                    setResult(-1, m780a(this.f1030i.f1113a, this.f1030i, null));
                } catch (Exception e) {
                    setResult(0, m780a(-1005, null, "User cancelled"));
                }
            }
            this.f1043v.removeCallbacksAndMessages(null);
            if (this.f1042u != null) {
                this.f1042u.cancel(true);
                this.f1042u = null;
            }
            super.onBackPressed();
        }
    }

    protected void onCreate(Bundle bundle) {
        super.onCreate(bundle);
        requestWindowFeature(1);
        C1084m.m923a(getFilesDir().getPath());
        this.f1041t = getSharedPreferences("_config_", 0);
        this.f1043v = new Handler();
        this.f1028g = getIntent().getExtras().getString("gid");
        this.f1027f = getIntent().getExtras().getString("guid");
        this.f1026e = getIntent().getExtras().getString("sku");
        this.f1029h = getIntent().getExtras().getString("payload");
        String string = getIntent().getExtras().getString("json");
        if (string != null) {
            try {
                this.f1039r = new SkuDetails(string);
                JSONObject jSONObject = new JSONObject(string);
                this.f1031j = jSONObject.getString("price_amount_micros");
                this.f1032k = jSONObject.getString("price_currency_code");
            } catch (JSONException e) {
            }
        }
        View frameLayout = new FrameLayout(this);
        frameLayout.setBackgroundColor(0);
        setContentView(frameLayout, new LayoutParams(-1, -1));
        this.f1022a = new RelativeLayout(this);
        this.f1022a.setBackgroundColor(0);
        frameLayout.addView(this.f1022a, new FrameLayout.LayoutParams(-1, -1));
        this.f1040s = new ProgressBar(this);
        this.f1040s.setIndeterminate(true);
        this.f1040s.setVisibility(4);
        LayoutParams layoutParams = new RelativeLayout.LayoutParams(-2, -2);
        layoutParams.addRule(13);
        this.f1022a.addView(this.f1040s, layoutParams);
        m784a();
        this.f1043v.post(new am(this));
    }

    protected void onResume() {
        super.onResume();
        DisplayUtils.lockOrientation(this);
    }
}
