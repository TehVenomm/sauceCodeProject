.class public Lnet/gogame/gopay/sdk/iab/PurchaseActivity;
.super Landroid/app/Activity;


# instance fields
.field private A:Lnet/gogame/gopay/sdk/d;

.field private B:Lnet/gogame/gopay/sdk/iab/b;

.field private C:Lnet/gogame/gopay/sdk/iab/i;

.field private D:Lnet/gogame/gopay/sdk/iab/bs;

.field private E:Lnet/gogame/gopay/sdk/iab/a;

.field private F:Landroid/widget/Spinner;

.field private G:Landroid/widget/Spinner;

.field private H:Lnet/gogame/gopay/sdk/iab/g;

.field private I:Z

.field private J:Z

.field private K:Z

.field private L:Z

.field private M:I

.field private N:I

.field private O:I

.field private P:I

.field private Q:I

.field a:Landroid/widget/RelativeLayout;

.field b:Lnet/gogame/gopay/sdk/support/c;

.field private c:Z

.field private d:Z

.field private e:Ljava/lang/String;

.field private f:Ljava/lang/String;

.field private g:Ljava/lang/String;

.field private h:Ljava/lang/String;

.field private i:Lnet/gogame/gopay/sdk/iab/br;

.field private j:Ljava/lang/String;

.field private k:Ljava/lang/String;

.field private l:Ljava/lang/String;

.field private m:Ljava/lang/String;

.field private n:Ljava/lang/String;

.field private o:Ljava/lang/String;

.field private p:Ljava/util/Map;

.field private q:Lorg/onepf/oms/appstore/googleUtils/SkuDetails;

.field private r:Lorg/onepf/oms/appstore/googleUtils/SkuDetails;

.field private s:Landroid/widget/ProgressBar;

.field private t:Landroid/content/SharedPreferences;

.field private u:Landroid/os/AsyncTask;

.field private v:Landroid/os/Handler;

.field private w:Ljava/lang/Runnable;

.field private x:Ljava/lang/Runnable;

.field private y:Landroid/webkit/WebView;

.field private z:Landroid/widget/Button;


# direct methods
.method public constructor <init>()V
    .locals 2

    invoke-direct {p0}, Landroid/app/Activity;-><init>()V

    const/4 v0, 0x0

    iput-boolean v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->c:Z

    iput-boolean v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->d:Z

    new-instance v1, Lnet/gogame/gopay/sdk/iab/l;

    invoke-direct {v1, p0}, Lnet/gogame/gopay/sdk/iab/l;-><init>(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)V

    iput-object v1, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->x:Ljava/lang/Runnable;

    const/4 v1, 0x0

    iput-object v1, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->E:Lnet/gogame/gopay/sdk/iab/a;

    new-instance v1, Lnet/gogame/gopay/sdk/iab/g;

    invoke-direct {v1}, Lnet/gogame/gopay/sdk/iab/g;-><init>()V

    iput-object v1, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->H:Lnet/gogame/gopay/sdk/iab/g;

    iput-boolean v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->I:Z

    iput-boolean v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->K:Z

    iput-boolean v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->L:Z

    iput v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->M:I

    const/4 v0, 0x3

    iput v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->N:I

    return-void
.end method

.method static synthetic A(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)V
    .locals 0

    invoke-direct {p0}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->a()V

    return-void
.end method

.method static synthetic B(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Lnet/gogame/gopay/sdk/iab/g;
    .locals 0

    iget-object p0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->H:Lnet/gogame/gopay/sdk/iab/g;

    return-object p0
.end method

.method static synthetic C(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Landroid/webkit/WebView;
    .locals 0

    iget-object p0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->y:Landroid/webkit/WebView;

    return-object p0
.end method

.method static synthetic D(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Lnet/gogame/gopay/sdk/iab/br;
    .locals 1

    const/4 v0, 0x0

    iput-object v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->i:Lnet/gogame/gopay/sdk/iab/br;

    return-object v0
.end method

.method static synthetic E(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)V
    .locals 0

    invoke-direct {p0}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->b()V

    return-void
.end method

.method static synthetic F(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Lnet/gogame/gopay/sdk/iab/br;
    .locals 0

    iget-object p0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->i:Lnet/gogame/gopay/sdk/iab/br;

    return-object p0
.end method

.method static synthetic G(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Ljava/lang/Runnable;
    .locals 0

    iget-object p0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->x:Ljava/lang/Runnable;

    return-object p0
.end method

.method static synthetic H(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)I
    .locals 0

    iget p0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->O:I

    return p0
.end method

.method static synthetic I(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Lnet/gogame/gopay/sdk/d;
    .locals 0

    iget-object p0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->A:Lnet/gogame/gopay/sdk/d;

    return-object p0
.end method

.method static synthetic J(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Lorg/onepf/oms/appstore/googleUtils/SkuDetails;
    .locals 0

    iget-object p0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->r:Lorg/onepf/oms/appstore/googleUtils/SkuDetails;

    return-object p0
.end method

.method static synthetic K(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Ljava/lang/String;
    .locals 0

    iget-object p0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->j:Ljava/lang/String;

    return-object p0
.end method

.method static synthetic L(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Ljava/lang/String;
    .locals 0

    iget-object p0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->k:Ljava/lang/String;

    return-object p0
.end method

.method static synthetic M(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Ljava/lang/String;
    .locals 0

    iget-object p0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->h:Ljava/lang/String;

    return-object p0
.end method

.method static synthetic N(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Landroid/os/AsyncTask;
    .locals 1

    const/4 v0, 0x0

    iput-object v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->u:Landroid/os/AsyncTask;

    return-object v0
.end method

.method static synthetic a(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;Ljava/lang/String;Lnet/gogame/gopay/sdk/a;Landroid/content/DialogInterface$OnClickListener;)Landroid/app/Dialog;
    .locals 15

    move-object v0, p0

    new-instance v1, Landroid/app/Dialog;

    invoke-direct {v1, p0}, Landroid/app/Dialog;-><init>(Landroid/content/Context;)V

    new-instance v2, Lnet/gogame/gopay/sdk/iab/bf;

    invoke-direct {v2, p0}, Lnet/gogame/gopay/sdk/iab/bf;-><init>(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)V

    invoke-virtual {v1, v2}, Landroid/app/Dialog;->setOnCancelListener(Landroid/content/DialogInterface$OnCancelListener;)V

    invoke-virtual {v1}, Landroid/app/Dialog;->getWindow()Landroid/view/Window;

    move-result-object v2

    const/4 v3, 0x1

    invoke-virtual {v2, v3}, Landroid/view/Window;->requestFeature(I)Z

    new-instance v2, Landroid/widget/ImageButton;

    invoke-direct {v2, p0}, Landroid/widget/ImageButton;-><init>(Landroid/content/Context;)V

    const/4 v4, 0x0

    invoke-virtual {v2, v4}, Landroid/widget/ImageButton;->setMinimumWidth(I)V

    invoke-virtual {v2, v4}, Landroid/widget/ImageButton;->setMinimumHeight(I)V

    invoke-virtual {v2, v4}, Landroid/widget/ImageButton;->setBackgroundColor(I)V

    invoke-virtual {v2, v4, v4, v4, v4}, Landroid/widget/ImageButton;->setPadding(IIII)V

    new-instance v5, Lnet/gogame/gopay/sdk/iab/bg;

    invoke-direct {v5, p0, v1}, Lnet/gogame/gopay/sdk/iab/bg;-><init>(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;Landroid/app/Dialog;)V

    invoke-virtual {v2, v5}, Landroid/widget/ImageButton;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    invoke-static {}, Lnet/gogame/gopay/sdk/support/m;->g()Landroid/graphics/Bitmap;

    move-result-object v5

    invoke-direct {p0, v5}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->a(Landroid/graphics/Bitmap;)Landroid/graphics/drawable/BitmapDrawable;

    move-result-object v5

    invoke-virtual {v2, v5}, Landroid/widget/ImageButton;->setImageDrawable(Landroid/graphics/drawable/Drawable;)V

    new-instance v5, Landroid/widget/RelativeLayout$LayoutParams;

    const/4 v6, -0x2

    invoke-direct {v5, v6, v6}, Landroid/widget/RelativeLayout$LayoutParams;-><init>(II)V

    const/high16 v7, 0x40e00000    # 7.0f

    invoke-static {p0, v7}, Lnet/gogame/gopay/sdk/support/DisplayUtils;->pxFromDp(Landroid/content/Context;F)I

    move-result v8

    invoke-virtual {v5, v4, v4, v8, v4}, Landroid/widget/RelativeLayout$LayoutParams;->setMargins(IIII)V

    const/16 v8, 0xb

    invoke-virtual {v5, v8}, Landroid/widget/RelativeLayout$LayoutParams;->addRule(I)V

    const/16 v8, 0xf

    invoke-virtual {v5, v8}, Landroid/widget/RelativeLayout$LayoutParams;->addRule(I)V

    new-instance v9, Landroid/widget/TextView;

    invoke-direct {v9, p0}, Landroid/widget/TextView;-><init>(Landroid/content/Context;)V

    invoke-virtual {v9, v4}, Landroid/widget/TextView;->setBackgroundColor(I)V

    invoke-static {p0, v7}, Lnet/gogame/gopay/sdk/support/DisplayUtils;->pxFromDp(Landroid/content/Context;F)I

    move-result v7

    invoke-virtual {v9, v7, v4, v4, v4}, Landroid/widget/TextView;->setPadding(IIII)V

    move-object/from16 v7, p1

    invoke-virtual {v9, v7}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    const/4 v7, 0x0

    invoke-virtual {v9, v7, v3}, Landroid/widget/TextView;->setTypeface(Landroid/graphics/Typeface;I)V

    const/high16 v7, 0x41900000    # 18.0f

    invoke-virtual {v9, v7}, Landroid/widget/TextView;->setTextSize(F)V

    const/4 v7, -0x1

    invoke-virtual {v9, v7}, Landroid/widget/TextView;->setTextColor(I)V

    invoke-virtual {v9}, Landroid/widget/TextView;->setSingleLine()V

    sget-object v10, Landroid/text/TextUtils$TruncateAt;->END:Landroid/text/TextUtils$TruncateAt;

    invoke-virtual {v9, v10}, Landroid/widget/TextView;->setEllipsize(Landroid/text/TextUtils$TruncateAt;)V

    new-instance v10, Landroid/widget/RelativeLayout$LayoutParams;

    invoke-direct {v10, v6, v6}, Landroid/widget/RelativeLayout$LayoutParams;-><init>(II)V

    invoke-virtual {v10, v8}, Landroid/widget/RelativeLayout$LayoutParams;->addRule(I)V

    const/16 v6, 0x9

    invoke-virtual {v10, v6}, Landroid/widget/RelativeLayout$LayoutParams;->addRule(I)V

    new-instance v6, Landroid/widget/RelativeLayout;

    invoke-direct {v6, p0}, Landroid/widget/RelativeLayout;-><init>(Landroid/content/Context;)V

    new-instance v8, Landroid/graphics/drawable/GradientDrawable;

    invoke-direct {v8}, Landroid/graphics/drawable/GradientDrawable;-><init>()V

    const/16 v11, 0x8

    new-array v11, v11, [F

    const/high16 v12, 0x41200000    # 10.0f

    invoke-static {p0, v12}, Lnet/gogame/gopay/sdk/support/DisplayUtils;->pxFromDp(Landroid/content/Context;F)I

    move-result v13

    int-to-float v13, v13

    aput v13, v11, v4

    invoke-static {p0, v12}, Lnet/gogame/gopay/sdk/support/DisplayUtils;->pxFromDp(Landroid/content/Context;F)I

    move-result v13

    int-to-float v13, v13

    aput v13, v11, v3

    invoke-static {p0, v12}, Lnet/gogame/gopay/sdk/support/DisplayUtils;->pxFromDp(Landroid/content/Context;F)I

    move-result v13

    int-to-float v13, v13

    const/4 v14, 0x2

    aput v13, v11, v14

    invoke-static {p0, v12}, Lnet/gogame/gopay/sdk/support/DisplayUtils;->pxFromDp(Landroid/content/Context;F)I

    move-result v13

    int-to-float v13, v13

    const/4 v14, 0x3

    aput v13, v11, v14

    const/4 v13, 0x0

    const/4 v14, 0x4

    aput v13, v11, v14

    const/4 v14, 0x5

    aput v13, v11, v14

    const/4 v14, 0x6

    aput v13, v11, v14

    const/4 v14, 0x7

    aput v13, v11, v14

    invoke-virtual {v8, v11}, Landroid/graphics/drawable/GradientDrawable;->setCornerRadii([F)V

    const/16 v11, 0x5c

    const/16 v13, 0xb0

    const/16 v14, 0x3b

    invoke-static {v11, v13, v14}, Landroid/graphics/Color;->rgb(III)I

    move-result v11

    invoke-virtual {v8, v11}, Landroid/graphics/drawable/GradientDrawable;->setColor(I)V

    sget v11, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v13, 0x10

    if-lt v11, v13, :cond_0

    invoke-virtual {v6, v8}, Landroid/widget/RelativeLayout;->setBackground(Landroid/graphics/drawable/Drawable;)V

    goto :goto_0

    :cond_0
    invoke-virtual {v6, v8}, Landroid/widget/RelativeLayout;->setBackgroundDrawable(Landroid/graphics/drawable/Drawable;)V

    :goto_0
    invoke-virtual {v6, v9, v10}, Landroid/widget/RelativeLayout;->addView(Landroid/view/View;Landroid/view/ViewGroup$LayoutParams;)V

    invoke-virtual {v6, v2, v5}, Landroid/widget/RelativeLayout;->addView(Landroid/view/View;Landroid/view/ViewGroup$LayoutParams;)V

    new-instance v2, Landroid/widget/ListView;

    invoke-direct {v2, p0}, Landroid/widget/ListView;-><init>(Landroid/content/Context;)V

    invoke-virtual {v2, v4}, Landroid/widget/ListView;->setBackgroundColor(I)V

    move-object/from16 v4, p2

    invoke-virtual {v2, v4}, Landroid/widget/ListView;->setAdapter(Landroid/widget/ListAdapter;)V

    iget-object v5, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->C:Lnet/gogame/gopay/sdk/iab/i;

    iget v5, v5, Lnet/gogame/gopay/sdk/iab/i;->e:I

    invoke-virtual {v2, v5}, Landroid/widget/ListView;->setSelection(I)V

    new-instance v5, Lnet/gogame/gopay/sdk/iab/bh;

    move-object/from16 v8, p3

    invoke-direct {v5, p0, v8, v1}, Lnet/gogame/gopay/sdk/iab/bh;-><init>(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;Landroid/content/DialogInterface$OnClickListener;Landroid/app/Dialog;)V

    invoke-virtual {v2, v5}, Landroid/widget/ListView;->setOnItemClickListener(Landroid/widget/AdapterView$OnItemClickListener;)V

    new-instance v5, Landroid/widget/LinearLayout;

    invoke-direct {v5, p0}, Landroid/widget/LinearLayout;-><init>(Landroid/content/Context;)V

    invoke-virtual {v5, v3}, Landroid/widget/LinearLayout;->setOrientation(I)V

    new-instance v3, Landroid/widget/LinearLayout$LayoutParams;

    const/high16 v8, 0x42200000    # 40.0f

    invoke-static {p0, v8}, Lnet/gogame/gopay/sdk/support/DisplayUtils;->pxFromDp(Landroid/content/Context;F)I

    move-result v8

    invoke-direct {v3, v7, v8}, Landroid/widget/LinearLayout$LayoutParams;-><init>(II)V

    new-instance v8, Landroid/widget/LinearLayout$LayoutParams;

    invoke-direct {v8, v7, v7}, Landroid/widget/LinearLayout$LayoutParams;-><init>(II)V

    invoke-virtual {v5, v6, v3}, Landroid/widget/LinearLayout;->addView(Landroid/view/View;Landroid/view/ViewGroup$LayoutParams;)V

    invoke-virtual {v5, v2, v8}, Landroid/widget/LinearLayout;->addView(Landroid/view/View;Landroid/view/ViewGroup$LayoutParams;)V

    invoke-virtual {v1, v5}, Landroid/app/Dialog;->setContentView(Landroid/view/View;)V

    new-instance v2, Landroid/graphics/drawable/GradientDrawable;

    invoke-direct {v2}, Landroid/graphics/drawable/GradientDrawable;-><init>()V

    invoke-static {p0, v12}, Lnet/gogame/gopay/sdk/support/DisplayUtils;->pxFromDp(Landroid/content/Context;F)I

    move-result v3

    int-to-float v3, v3

    invoke-virtual {v2, v3}, Landroid/graphics/drawable/GradientDrawable;->setCornerRadius(F)V

    invoke-virtual {v2, v7}, Landroid/graphics/drawable/GradientDrawable;->setColor(I)V

    sget v3, Landroid/os/Build$VERSION;->SDK_INT:I

    if-lt v3, v13, :cond_1

    invoke-virtual {v5, v2}, Landroid/widget/LinearLayout;->setBackground(Landroid/graphics/drawable/Drawable;)V

    goto :goto_1

    :cond_1
    invoke-virtual {v5, v2}, Landroid/widget/LinearLayout;->setBackgroundDrawable(Landroid/graphics/drawable/Drawable;)V

    :goto_1
    invoke-virtual {v1}, Landroid/app/Dialog;->getWindow()Landroid/view/Window;

    move-result-object v3

    invoke-virtual {v3, v2}, Landroid/view/Window;->setBackgroundDrawable(Landroid/graphics/drawable/Drawable;)V

    invoke-virtual {v1}, Landroid/app/Dialog;->getWindow()Landroid/view/Window;

    move-result-object v2

    invoke-virtual {v2}, Landroid/view/Window;->getAttributes()Landroid/view/WindowManager$LayoutParams;

    move-result-object v2

    invoke-static {p0}, Lnet/gogame/gopay/sdk/support/DisplayUtils;->getScreenSize(Landroid/app/Activity;)Landroid/graphics/Point;

    move-result-object v3

    iget v3, v3, Landroid/graphics/Point;->y:I

    int-to-double v5, v3

    const-wide/high16 v7, 0x3ff8000000000000L    # 1.5

    invoke-static {v5, v6}, Ljava/lang/Double;->isNaN(D)Z

    div-double/2addr v5, v7

    double-to-int v3, v5

    const/high16 v5, 0x42700000    # 60.0f

    invoke-static {p0, v5}, Lnet/gogame/gopay/sdk/support/DisplayUtils;->pxFromDp(Landroid/content/Context;F)I

    move-result v0

    invoke-virtual/range {p2 .. p2}, Lnet/gogame/gopay/sdk/a;->getCount()I

    move-result v4

    mul-int v0, v0, v4

    if-lt v0, v3, :cond_2

    iput v3, v2, Landroid/view/WindowManager$LayoutParams;->height:I

    :cond_2
    return-object v1
.end method

.method private static a(ILnet/gogame/gopay/sdk/iab/br;Ljava/lang/String;)Landroid/content/Intent;
    .locals 2

    new-instance v0, Landroid/content/Intent;

    invoke-direct {v0}, Landroid/content/Intent;-><init>()V

    const-string v1, "RESPONSE_CODE"

    invoke-virtual {v0, v1, p0}, Landroid/content/Intent;->putExtra(Ljava/lang/String;I)Landroid/content/Intent;

    if-eqz p1, :cond_0

    const-string p0, "INAPP_PURCHASE_DATA"

    invoke-virtual {p1}, Lnet/gogame/gopay/sdk/iab/br;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, p0, v1}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Ljava/lang/String;)Landroid/content/Intent;

    const-string p0, "INAPP_DATA_SIGNATURE"

    iget-object p1, p1, Lnet/gogame/gopay/sdk/iab/br;->b:Ljava/lang/String;

    invoke-virtual {v0, p0, p1}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Ljava/lang/String;)Landroid/content/Intent;

    :cond_0
    if-eqz p2, :cond_1

    const-string p0, "MESSAGE"

    invoke-virtual {v0, p0, p2}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Ljava/lang/String;)Landroid/content/Intent;

    :cond_1
    return-object v0
.end method

.method private a(Landroid/graphics/Bitmap;)Landroid/graphics/drawable/BitmapDrawable;
    .locals 5

    if-nez p1, :cond_0

    const/4 p1, 0x0

    return-object p1

    :cond_0
    new-instance v0, Landroid/graphics/drawable/BitmapDrawable;

    invoke-virtual {p0}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->getResources()Landroid/content/res/Resources;

    move-result-object v1

    invoke-virtual {p1}, Landroid/graphics/Bitmap;->getWidth()I

    move-result v2

    int-to-float v2, v2

    const/high16 v3, 0x3f000000    # 0.5f

    mul-float v2, v2, v3

    invoke-static {p0, v2}, Lnet/gogame/gopay/sdk/support/DisplayUtils;->pxFromDp(Landroid/content/Context;F)I

    move-result v2

    invoke-virtual {p1}, Landroid/graphics/Bitmap;->getHeight()I

    move-result v4

    int-to-float v4, v4

    mul-float v4, v4, v3

    invoke-static {p0, v4}, Lnet/gogame/gopay/sdk/support/DisplayUtils;->pxFromDp(Landroid/content/Context;F)I

    move-result v3

    const/4 v4, 0x0

    invoke-static {p1, v2, v3, v4}, Landroid/graphics/Bitmap;->createScaledBitmap(Landroid/graphics/Bitmap;IIZ)Landroid/graphics/Bitmap;

    move-result-object p1

    invoke-direct {v0, v1, p1}, Landroid/graphics/drawable/BitmapDrawable;-><init>(Landroid/content/res/Resources;Landroid/graphics/Bitmap;)V

    return-object v0
.end method

.method private a(Ljava/lang/String;)Ljava/lang/String;
    .locals 1

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->p:Ljava/util/Map;

    invoke-interface {v0, p1}, Ljava/util/Map;->containsKey(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_0

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->p:Ljava/util/Map;

    invoke-interface {v0, p1}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Ljava/lang/String;

    return-object p1

    :cond_0
    const/4 p1, 0x0

    return-object p1
.end method

.method static synthetic a(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;Ljava/lang/String;)Ljava/lang/String;
    .locals 0

    invoke-direct {p0, p1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->a(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p0

    return-object p0
.end method

.method private a()V
    .locals 4

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->s:Landroid/widget/ProgressBar;

    if-eqz v0, :cond_1

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->s:Landroid/widget/ProgressBar;

    invoke-virtual {v0}, Landroid/widget/ProgressBar;->isShown()Z

    move-result v0

    if-eqz v0, :cond_0

    goto :goto_0

    :cond_0
    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->v:Landroid/os/Handler;

    new-instance v1, Lnet/gogame/gopay/sdk/iab/bm;

    invoke-direct {v1, p0}, Lnet/gogame/gopay/sdk/iab/bm;-><init>(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)V

    invoke-virtual {v0, v1}, Landroid/os/Handler;->post(Ljava/lang/Runnable;)Z

    return-void

    :cond_1
    :goto_0
    invoke-direct {p0}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->c()V

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->v:Landroid/os/Handler;

    new-instance v1, Lnet/gogame/gopay/sdk/iab/ax;

    invoke-direct {v1, p0}, Lnet/gogame/gopay/sdk/iab/ax;-><init>(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)V

    iput-object v1, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->w:Ljava/lang/Runnable;

    const-wide/16 v2, 0x2710

    invoke-virtual {v0, v1, v2, v3}, Landroid/os/Handler;->postDelayed(Ljava/lang/Runnable;J)Z

    return-void
.end method

.method private a(ILjava/lang/String;)V
    .locals 2

    const/4 v0, 0x1

    iput-boolean v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->c:Z

    invoke-direct {p0}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->b()V

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->v:Landroid/os/Handler;

    new-instance v1, Lnet/gogame/gopay/sdk/iab/ay;

    invoke-direct {v1, p0, p1, p2}, Lnet/gogame/gopay/sdk/iab/ay;-><init>(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;ILjava/lang/String;)V

    invoke-virtual {v0, v1}, Landroid/os/Handler;->post(Ljava/lang/Runnable;)Z

    return-void
.end method

.method static synthetic a(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)V
    .locals 1

    new-instance v0, Lnet/gogame/gopay/sdk/iab/bd;

    invoke-direct {v0, p0}, Lnet/gogame/gopay/sdk/iab/bd;-><init>(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)V

    const/4 p0, 0x0

    new-array p0, p0, [Ljava/lang/Void;

    invoke-virtual {v0, p0}, Lnet/gogame/gopay/sdk/iab/bd;->execute([Ljava/lang/Object;)Landroid/os/AsyncTask;

    return-void
.end method

.method static synthetic a(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;I)V
    .locals 2

    iput p1, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->P:I

    const/4 v0, 0x0

    iput-boolean v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->I:Z

    iget-object v1, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->F:Landroid/widget/Spinner;

    invoke-virtual {v1, v0}, Landroid/widget/Spinner;->setEnabled(Z)V

    iget-object v1, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->G:Landroid/widget/Spinner;

    invoke-virtual {v1, v0}, Landroid/widget/Spinner;->setEnabled(Z)V

    iget-object v1, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->z:Landroid/widget/Button;

    if-eqz v1, :cond_0

    iget-object v1, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->z:Landroid/widget/Button;

    invoke-virtual {v1, v0}, Landroid/widget/Button;->setEnabled(Z)V

    :cond_0
    invoke-direct {p0}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->a()V

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->A:Lnet/gogame/gopay/sdk/d;

    invoke-virtual {v0, p1}, Lnet/gogame/gopay/sdk/d;->getItem(I)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Lnet/gogame/gopay/sdk/Country;

    invoke-virtual {p1}, Lnet/gogame/gopay/sdk/Country;->getCode()Ljava/lang/String;

    move-result-object p1

    invoke-static {p1}, Lnet/gogame/gopay/sdk/j;->a(Ljava/lang/String;)V

    new-instance p1, Lnet/gogame/gopay/sdk/iab/as;

    invoke-direct {p1, p0}, Lnet/gogame/gopay/sdk/iab/as;-><init>(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)V

    const/4 v0, 0x1

    invoke-direct {p0, p1, v0}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->a(Lnet/gogame/gopay/sdk/iab/bq;Z)V

    return-void
.end method

.method static synthetic a(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;ILjava/lang/String;)V
    .locals 0

    invoke-direct {p0, p1, p2}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->a(ILjava/lang/String;)V

    return-void
.end method

.method static synthetic a(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;Landroid/content/DialogInterface$OnClickListener;Landroid/content/DialogInterface$OnClickListener;)V
    .locals 2

    new-instance v0, Landroid/app/AlertDialog$Builder;

    invoke-direct {v0, p0}, Landroid/app/AlertDialog$Builder;-><init>(Landroid/content/Context;)V

    iget-object v1, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->o:Ljava/lang/String;

    invoke-virtual {v0, v1}, Landroid/app/AlertDialog$Builder;->setTitle(Ljava/lang/CharSequence;)Landroid/app/AlertDialog$Builder;

    move-result-object v0

    iget-object v1, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->n:Ljava/lang/String;

    invoke-virtual {v0, v1}, Landroid/app/AlertDialog$Builder;->setMessage(Ljava/lang/CharSequence;)Landroid/app/AlertDialog$Builder;

    move-result-object v0

    iget-object v1, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->l:Ljava/lang/String;

    invoke-virtual {v0, v1, p1}, Landroid/app/AlertDialog$Builder;->setPositiveButton(Ljava/lang/CharSequence;Landroid/content/DialogInterface$OnClickListener;)Landroid/app/AlertDialog$Builder;

    move-result-object p1

    iget-object p0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->m:Ljava/lang/String;

    invoke-virtual {p1, p0, p2}, Landroid/app/AlertDialog$Builder;->setNegativeButton(Ljava/lang/CharSequence;Landroid/content/DialogInterface$OnClickListener;)Landroid/app/AlertDialog$Builder;

    move-result-object p0

    const/4 p1, 0x0

    invoke-virtual {p0, p1}, Landroid/app/AlertDialog$Builder;->setCancelable(Z)Landroid/app/AlertDialog$Builder;

    move-result-object p0

    invoke-virtual {p0}, Landroid/app/AlertDialog$Builder;->show()Landroid/app/AlertDialog;

    return-void
.end method

.method static synthetic a(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;Ljava/lang/String;Ljava/util/List;)V
    .locals 5

    iget-boolean v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->I:Z

    if-nez v0, :cond_9

    const/4 v0, 0x1

    iput-boolean v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->I:Z

    iget-object v1, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->C:Lnet/gogame/gopay/sdk/iab/i;

    invoke-virtual {v1, p1, p2}, Lnet/gogame/gopay/sdk/iab/i;->a(Ljava/lang/String;Ljava/util/List;)V

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->C:Lnet/gogame/gopay/sdk/iab/i;

    const/4 p2, 0x0

    iput-object p2, p1, Lnet/gogame/gopay/sdk/iab/i;->f:Landroid/view/View;

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->D:Lnet/gogame/gopay/sdk/iab/bs;

    const/4 v1, 0x0

    if-eqz p1, :cond_4

    invoke-static {p0}, Lnet/gogame/gopay/sdk/support/DisplayUtils;->getScreenSize(Landroid/app/Activity;)Landroid/graphics/Point;

    move-result-object p1

    iget-object v2, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->C:Lnet/gogame/gopay/sdk/iab/i;

    invoke-virtual {v2}, Lnet/gogame/gopay/sdk/iab/i;->getCount()I

    move-result v2

    const/high16 v3, 0x42a00000    # 80.0f

    invoke-static {p0, v3}, Lnet/gogame/gopay/sdk/support/DisplayUtils;->pxFromDp(Landroid/content/Context;F)I

    move-result v4

    iget p1, p1, Landroid/graphics/Point;->x:I

    div-int/2addr p1, v4

    sub-int p1, v2, p1

    add-int/2addr p1, v0

    if-eqz v2, :cond_4

    if-lez p1, :cond_0

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->z:Landroid/widget/Button;

    invoke-virtual {p1, v1}, Landroid/widget/Button;->setVisibility(I)V

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->z:Landroid/widget/Button;

    invoke-virtual {p1}, Landroid/widget/Button;->getLeft()I

    move-result p1

    int-to-float p1, p1

    invoke-static {p0, v3}, Lnet/gogame/gopay/sdk/support/DisplayUtils;->pxFromDp(Landroid/content/Context;F)I

    move-result v2

    int-to-float v2, v2

    div-float/2addr p1, v2

    float-to-double v2, p1

    invoke-static {v2, v3}, Ljava/lang/Math;->floor(D)D

    move-result-wide v2

    double-to-int p1, v2

    add-int/lit8 v2, p1, -0x1

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->b:Lnet/gogame/gopay/sdk/support/c;

    invoke-virtual {p1}, Lnet/gogame/gopay/sdk/support/c;->getLayoutParams()Landroid/view/ViewGroup$LayoutParams;

    move-result-object p1

    check-cast p1, Landroid/widget/LinearLayout$LayoutParams;

    iget-object v3, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->z:Landroid/widget/Button;

    invoke-virtual {v3}, Landroid/widget/Button;->getLeft()I

    move-result v3

    :goto_0
    iput v3, p1, Landroid/widget/LinearLayout$LayoutParams;->width:I

    iget-object v3, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->b:Lnet/gogame/gopay/sdk/support/c;

    invoke-virtual {v3, p1}, Lnet/gogame/gopay/sdk/support/c;->setLayoutParams(Landroid/view/ViewGroup$LayoutParams;)V

    goto :goto_1

    :cond_0
    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->z:Landroid/widget/Button;

    const/16 v3, 0x8

    invoke-virtual {p1, v3}, Landroid/widget/Button;->setVisibility(I)V

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->b:Lnet/gogame/gopay/sdk/support/c;

    invoke-virtual {p1}, Lnet/gogame/gopay/sdk/support/c;->getLayoutParams()Landroid/view/ViewGroup$LayoutParams;

    move-result-object p1

    check-cast p1, Landroid/widget/LinearLayout$LayoutParams;

    const/4 v3, -0x1

    goto :goto_0

    :goto_1
    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->D:Lnet/gogame/gopay/sdk/iab/bs;

    iget-object p1, p1, Lnet/gogame/gopay/sdk/iab/bs;->c:Ljava/util/List;

    if-nez p1, :cond_1

    new-instance p1, Ljava/util/ArrayList;

    invoke-direct {p1}, Ljava/util/ArrayList;-><init>()V

    :cond_1
    invoke-interface {p1}, Ljava/util/List;->clear()V

    const/4 v3, 0x0

    :goto_2
    if-ge v3, v2, :cond_2

    new-instance v4, Ljava/lang/Integer;

    invoke-direct {v4, v3}, Ljava/lang/Integer;-><init>(I)V

    invoke-interface {p1, v4}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    add-int/lit8 v3, v3, 0x1

    goto :goto_2

    :cond_2
    iget-object v2, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->D:Lnet/gogame/gopay/sdk/iab/bs;

    iput v1, v2, Lnet/gogame/gopay/sdk/iab/bs;->e:I

    iput-object p2, v2, Lnet/gogame/gopay/sdk/iab/bs;->f:Lnet/gogame/gopay/sdk/iab/a;

    iget-boolean p2, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->J:Z

    if-eqz p2, :cond_3

    iget-object p2, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->E:Lnet/gogame/gopay/sdk/iab/a;

    if-eqz p2, :cond_3

    iget-object p2, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->D:Lnet/gogame/gopay/sdk/iab/bs;

    iget-object v2, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->E:Lnet/gogame/gopay/sdk/iab/a;

    goto :goto_3

    :cond_3
    iget-object p2, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->D:Lnet/gogame/gopay/sdk/iab/bs;

    iget-object v2, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->C:Lnet/gogame/gopay/sdk/iab/i;

    invoke-virtual {v2, v1}, Lnet/gogame/gopay/sdk/iab/i;->getItem(I)Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lnet/gogame/gopay/sdk/iab/a;

    :goto_3
    invoke-virtual {p2, v2}, Lnet/gogame/gopay/sdk/iab/bs;->a(Lnet/gogame/gopay/sdk/iab/a;)V

    iget-object p2, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->D:Lnet/gogame/gopay/sdk/iab/bs;

    iget-object v2, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->C:Lnet/gogame/gopay/sdk/iab/i;

    invoke-virtual {v2}, Lnet/gogame/gopay/sdk/iab/i;->a()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {p2, v2, p1}, Lnet/gogame/gopay/sdk/iab/bs;->a(Ljava/lang/String;Ljava/util/List;)V

    :cond_4
    iget-boolean p1, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->J:Z

    if-eqz p1, :cond_7

    const-string p1, "welcomePage"

    invoke-direct {p0, p1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->a(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    iget-object p2, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->y:Landroid/webkit/WebView;

    invoke-virtual {p2}, Landroid/webkit/WebView;->getUrl()Ljava/lang/String;

    move-result-object p2

    if-eqz p2, :cond_6

    invoke-virtual {p2, p1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p2

    if-nez p2, :cond_5

    goto :goto_4

    :cond_5
    invoke-direct {p0}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->b()V

    iput-boolean v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->c:Z

    iput-boolean v1, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->I:Z

    return-void

    :cond_6
    :goto_4
    iget-object p2, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->y:Landroid/webkit/WebView;

    invoke-virtual {p2}, Landroid/webkit/WebView;->stopLoading()V

    iget-object p2, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->y:Landroid/webkit/WebView;

    invoke-virtual {p2, v0}, Landroid/webkit/WebView;->clearCache(Z)V

    iget-object p0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->y:Landroid/webkit/WebView;

    invoke-virtual {p0, p1}, Landroid/webkit/WebView;->loadUrl(Ljava/lang/String;)V

    return-void

    :cond_7
    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->C:Lnet/gogame/gopay/sdk/iab/i;

    iput v1, p1, Lnet/gogame/gopay/sdk/iab/i;->e:I

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->D:Lnet/gogame/gopay/sdk/iab/bs;

    if-eqz p1, :cond_8

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->D:Lnet/gogame/gopay/sdk/iab/bs;

    iget-object p1, p1, Lnet/gogame/gopay/sdk/iab/bs;->f:Lnet/gogame/gopay/sdk/iab/a;

    goto :goto_5

    :cond_8
    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->C:Lnet/gogame/gopay/sdk/iab/i;

    invoke-virtual {p1, v1}, Lnet/gogame/gopay/sdk/iab/i;->getItem(I)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Lnet/gogame/gopay/sdk/iab/a;

    :goto_5
    invoke-direct {p0, p1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->a(Lnet/gogame/gopay/sdk/iab/a;)V

    :cond_9
    return-void
.end method

.method static synthetic a(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;Lnet/gogame/gopay/sdk/g;Z)V
    .locals 22

    move-object/from16 v0, p0

    move-object/from16 v1, p1

    iget-object v3, v1, Lnet/gogame/gopay/sdk/g;->d:Ljava/util/Map;

    iput-object v3, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->p:Ljava/util/Map;

    const-string v3, "infoPage"

    invoke-direct {v0, v3}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->a(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    if-eqz v3, :cond_0

    new-instance v3, Lnet/gogame/gopay/sdk/iab/f;

    const-string v4, "Info"

    const-string v5, "infoPage"

    invoke-direct {v0, v5}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->a(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v5

    invoke-direct {v3, v4, v5}, Lnet/gogame/gopay/sdk/iab/f;-><init>(Ljava/lang/String;Ljava/lang/String;)V

    iput-object v3, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->E:Lnet/gogame/gopay/sdk/iab/a;

    :cond_0
    iget-object v3, v1, Lnet/gogame/gopay/sdk/g;->b:Lorg/onepf/oms/appstore/googleUtils/SkuDetails;

    iput-object v3, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->q:Lorg/onepf/oms/appstore/googleUtils/SkuDetails;

    iget-object v3, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->E:Lnet/gogame/gopay/sdk/iab/a;

    const/4 v4, 0x1

    const/4 v5, 0x0

    if-eqz v3, :cond_1

    iget-object v3, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->E:Lnet/gogame/gopay/sdk/iab/a;

    check-cast v3, Lnet/gogame/gopay/sdk/iab/f;

    iget-boolean v3, v3, Lnet/gogame/gopay/sdk/iab/f;->b:Z

    if-eqz v3, :cond_1

    const/4 v3, 0x1

    goto :goto_0

    :cond_1
    const/4 v3, 0x0

    :goto_0
    const/high16 v6, 0x42200000    # 40.0f

    if-eqz p2, :cond_2

    const/high16 v7, 0x42200000    # 40.0f

    goto :goto_1

    :cond_2
    const/high16 v7, 0x42f00000    # 120.0f

    :goto_1
    invoke-static {v0, v7}, Lnet/gogame/gopay/sdk/support/DisplayUtils;->pxFromDp(Landroid/content/Context;F)I

    move-result v7

    if-eqz v3, :cond_3

    invoke-static {v0, v6}, Lnet/gogame/gopay/sdk/support/DisplayUtils;->pxFromDp(Landroid/content/Context;F)I

    move-result v8

    goto :goto_2

    :cond_3
    const/4 v8, 0x0

    :goto_2
    invoke-static/range {p0 .. p0}, Lnet/gogame/gopay/sdk/support/DisplayUtils;->getScreenSize(Landroid/app/Activity;)Landroid/graphics/Point;

    move-result-object v9

    iget v9, v9, Landroid/graphics/Point;->x:I

    sub-int/2addr v9, v7

    sub-int/2addr v9, v8

    new-instance v10, Landroid/widget/LinearLayout;

    invoke-direct {v10, v0}, Landroid/widget/LinearLayout;-><init>(Landroid/content/Context;)V

    const v11, -0x333334

    invoke-virtual {v10, v11}, Landroid/widget/LinearLayout;->setBackgroundColor(I)V

    invoke-virtual {v10, v4}, Landroid/widget/LinearLayout;->setOrientation(I)V

    new-instance v11, Landroid/widget/RelativeLayout$LayoutParams;

    const/4 v12, -0x1

    invoke-direct {v11, v12, v12}, Landroid/widget/RelativeLayout$LayoutParams;-><init>(II)V

    const/16 v13, 0xd

    invoke-virtual {v11, v13}, Landroid/widget/RelativeLayout$LayoutParams;->addRule(I)V

    iget-object v13, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->a:Landroid/widget/RelativeLayout;

    invoke-virtual {v13, v10, v11}, Landroid/widget/RelativeLayout;->addView(Landroid/view/View;Landroid/view/ViewGroup$LayoutParams;)V

    new-instance v11, Landroid/widget/LinearLayout;

    invoke-direct {v11, v0}, Landroid/widget/LinearLayout;-><init>(Landroid/content/Context;)V

    invoke-virtual {v11, v12}, Landroid/widget/LinearLayout;->setBackgroundColor(I)V

    invoke-virtual {v11, v5}, Landroid/widget/LinearLayout;->setOrientation(I)V

    const/4 v13, 0x3

    invoke-virtual {v11, v13}, Landroid/widget/LinearLayout;->setHorizontalGravity(I)V

    const/16 v13, 0x11

    invoke-virtual {v11, v13}, Landroid/widget/LinearLayout;->setVerticalGravity(I)V

    new-instance v13, Landroid/widget/LinearLayout$LayoutParams;

    invoke-static {v0, v6}, Lnet/gogame/gopay/sdk/support/DisplayUtils;->pxFromDp(Landroid/content/Context;F)I

    move-result v14

    invoke-direct {v13, v12, v14}, Landroid/widget/LinearLayout$LayoutParams;-><init>(II)V

    invoke-virtual {v10, v11, v13}, Landroid/widget/LinearLayout;->addView(Landroid/view/View;Landroid/view/ViewGroup$LayoutParams;)V

    new-instance v13, Landroid/graphics/drawable/GradientDrawable;

    invoke-direct {v13}, Landroid/graphics/drawable/GradientDrawable;-><init>()V

    const v14, -0x333334

    invoke-virtual {v13, v4, v14}, Landroid/graphics/drawable/GradientDrawable;->setStroke(II)V

    invoke-virtual {v13, v12}, Landroid/graphics/drawable/GradientDrawable;->setColor(I)V

    const/4 v14, 0x2

    invoke-virtual {v13, v14}, Landroid/graphics/drawable/GradientDrawable;->setGradientType(I)V

    new-array v15, v4, [Landroid/graphics/drawable/Drawable;

    aput-object v13, v15, v5

    new-instance v13, Landroid/graphics/drawable/LayerDrawable;

    invoke-direct {v13, v15}, Landroid/graphics/drawable/LayerDrawable;-><init>([Landroid/graphics/drawable/Drawable;)V

    const/16 v17, 0x0

    const/16 v18, -0x2

    const/16 v19, -0x2

    const/16 v20, -0x2

    const/16 v21, 0x0

    move-object/from16 v16, v13

    invoke-virtual/range {v16 .. v21}, Landroid/graphics/drawable/LayerDrawable;->setLayerInset(IIIII)V

    sget v15, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v6, 0x10

    if-lt v15, v6, :cond_4

    invoke-virtual {v11, v13}, Landroid/widget/LinearLayout;->setBackground(Landroid/graphics/drawable/Drawable;)V

    goto :goto_3

    :cond_4
    invoke-virtual {v11, v13}, Landroid/widget/LinearLayout;->setBackgroundDrawable(Landroid/graphics/drawable/Drawable;)V

    :goto_3
    new-instance v13, Landroid/widget/ImageButton;

    invoke-direct {v13, v0}, Landroid/widget/ImageButton;-><init>(Landroid/content/Context;)V

    invoke-virtual {v13, v5}, Landroid/widget/ImageButton;->setMinimumWidth(I)V

    invoke-virtual {v13, v5}, Landroid/widget/ImageButton;->setMinimumHeight(I)V

    const/16 v15, 0x3b

    const/16 v6, 0xb0

    const/16 v14, 0x5c

    invoke-static {v14, v6, v15}, Landroid/graphics/Color;->rgb(III)I

    move-result v4

    invoke-virtual {v13, v4}, Landroid/widget/ImageButton;->setBackgroundColor(I)V

    invoke-virtual {v13, v5, v5, v5, v5}, Landroid/widget/ImageButton;->setPadding(IIII)V

    new-instance v4, Lnet/gogame/gopay/sdk/iab/bo;

    invoke-direct {v4, v0}, Lnet/gogame/gopay/sdk/iab/bo;-><init>(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)V

    invoke-virtual {v13, v4}, Landroid/widget/ImageButton;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    invoke-static {}, Lnet/gogame/gopay/sdk/support/m;->g()Landroid/graphics/Bitmap;

    move-result-object v4

    invoke-direct {v0, v4}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->a(Landroid/graphics/Bitmap;)Landroid/graphics/drawable/BitmapDrawable;

    move-result-object v4

    invoke-virtual {v13, v4}, Landroid/widget/ImageButton;->setImageDrawable(Landroid/graphics/drawable/Drawable;)V

    new-instance v4, Landroid/widget/LinearLayout$LayoutParams;

    invoke-direct {v4, v7, v12}, Landroid/widget/LinearLayout$LayoutParams;-><init>(II)V

    invoke-virtual {v11, v13, v4}, Landroid/widget/LinearLayout;->addView(Landroid/view/View;Landroid/view/ViewGroup$LayoutParams;)V

    new-instance v4, Landroid/widget/LinearLayout;

    invoke-direct {v4, v0}, Landroid/widget/LinearLayout;-><init>(Landroid/content/Context;)V

    const/high16 v7, 0x40800000    # 4.0f

    invoke-virtual {v4, v7}, Landroid/widget/LinearLayout;->setWeightSum(F)V

    new-instance v13, Landroid/widget/LinearLayout$LayoutParams;

    invoke-direct {v13, v9, v12}, Landroid/widget/LinearLayout$LayoutParams;-><init>(II)V

    invoke-virtual {v11, v4, v13}, Landroid/widget/LinearLayout;->addView(Landroid/view/View;Landroid/view/ViewGroup$LayoutParams;)V

    new-instance v9, Lnet/gogame/gopay/sdk/d;

    invoke-direct {v9, v0}, Lnet/gogame/gopay/sdk/d;-><init>(Landroid/content/Context;)V

    iput-object v9, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->A:Lnet/gogame/gopay/sdk/d;

    iget-object v9, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->A:Lnet/gogame/gopay/sdk/d;

    const-string v13, "country"

    invoke-direct {v0, v13}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->a(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v13

    iget-object v7, v1, Lnet/gogame/gopay/sdk/g;->e:Ljava/util/List;

    invoke-virtual {v9, v13, v7}, Lnet/gogame/gopay/sdk/d;->a(Ljava/lang/String;Ljava/util/List;)V

    const/4 v7, 0x0

    :goto_4
    iget-object v9, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->A:Lnet/gogame/gopay/sdk/d;

    invoke-virtual {v9}, Lnet/gogame/gopay/sdk/d;->getCount()I

    move-result v9

    if-ge v7, v9, :cond_6

    iget-object v9, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->A:Lnet/gogame/gopay/sdk/d;

    invoke-virtual {v9, v7}, Lnet/gogame/gopay/sdk/d;->getItem(I)Ljava/lang/Object;

    move-result-object v9

    check-cast v9, Lnet/gogame/gopay/sdk/Country;

    invoke-virtual {v9}, Lnet/gogame/gopay/sdk/Country;->getCode()Ljava/lang/String;

    move-result-object v9

    invoke-static {}, Lnet/gogame/gopay/sdk/j;->a()Ljava/lang/String;

    move-result-object v13

    invoke-virtual {v9, v13}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v9

    if-eqz v9, :cond_5

    goto :goto_5

    :cond_5
    add-int/lit8 v7, v7, 0x1

    goto :goto_4

    :cond_6
    const/4 v7, 0x0

    :goto_5
    new-instance v9, Landroid/widget/LinearLayout$LayoutParams;

    invoke-direct {v9, v5, v12}, Landroid/widget/LinearLayout$LayoutParams;-><init>(II)V

    const/high16 v13, 0x40000000    # 2.0f

    iput v13, v9, Landroid/widget/LinearLayout$LayoutParams;->weight:F

    const/high16 v6, 0x40a00000    # 5.0f

    invoke-static {v0, v6}, Lnet/gogame/gopay/sdk/support/DisplayUtils;->pxFromDp(Landroid/content/Context;F)I

    move-result v6

    const/4 v14, 0x0

    invoke-static {v0, v14}, Lnet/gogame/gopay/sdk/support/DisplayUtils;->pxFromDp(Landroid/content/Context;F)I

    move-result v15

    invoke-static {v0, v14}, Lnet/gogame/gopay/sdk/support/DisplayUtils;->pxFromDp(Landroid/content/Context;F)I

    move-result v13

    invoke-static {v0, v14}, Lnet/gogame/gopay/sdk/support/DisplayUtils;->pxFromDp(Landroid/content/Context;F)I

    move-result v12

    invoke-virtual {v9, v6, v15, v13, v12}, Landroid/widget/LinearLayout$LayoutParams;->setMargins(IIII)V

    new-instance v6, Landroid/widget/Spinner;

    const/4 v12, 0x1

    invoke-direct {v6, v0, v12}, Landroid/widget/Spinner;-><init>(Landroid/content/Context;I)V

    iput-object v6, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->F:Landroid/widget/Spinner;

    iget-object v6, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->F:Landroid/widget/Spinner;

    invoke-virtual {v6, v5}, Landroid/widget/Spinner;->setBackgroundColor(I)V

    iget-object v6, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->F:Landroid/widget/Spinner;

    iget-object v12, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->A:Lnet/gogame/gopay/sdk/d;

    invoke-virtual {v6, v12}, Landroid/widget/Spinner;->setAdapter(Landroid/widget/SpinnerAdapter;)V

    iget-object v6, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->F:Landroid/widget/Spinner;

    invoke-virtual {v6, v7}, Landroid/widget/Spinner;->setSelection(I)V

    iget-object v6, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->F:Landroid/widget/Spinner;

    invoke-virtual {v6, v5, v5, v5, v5}, Landroid/widget/Spinner;->setPadding(IIII)V

    iget-object v6, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->F:Landroid/widget/Spinner;

    iget-object v12, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->A:Lnet/gogame/gopay/sdk/d;

    invoke-virtual {v12}, Lnet/gogame/gopay/sdk/d;->getCount()I

    move-result v12

    const/4 v13, 0x1

    if-le v12, v13, :cond_7

    const/4 v12, 0x1

    goto :goto_6

    :cond_7
    const/4 v12, 0x0

    :goto_6
    invoke-virtual {v6, v12}, Landroid/widget/Spinner;->setEnabled(Z)V

    iget-object v6, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->F:Landroid/widget/Spinner;

    new-instance v12, Lnet/gogame/gopay/sdk/iab/bp;

    invoke-direct {v12, v0}, Lnet/gogame/gopay/sdk/iab/bp;-><init>(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)V

    invoke-virtual {v6, v12}, Landroid/widget/Spinner;->setOnTouchListener(Landroid/view/View$OnTouchListener;)V

    iput v7, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->P:I

    sget v6, Landroid/os/Build$VERSION;->SDK_INT:I

    iget-object v6, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->F:Landroid/widget/Spinner;

    new-instance v7, Lnet/gogame/gopay/sdk/iab/m;

    invoke-direct {v7, v0}, Lnet/gogame/gopay/sdk/iab/m;-><init>(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)V

    invoke-virtual {v6, v7}, Landroid/widget/Spinner;->setOnItemSelectedListener(Landroid/widget/AdapterView$OnItemSelectedListener;)V

    iget-object v6, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->F:Landroid/widget/Spinner;

    invoke-virtual {v4, v6, v9}, Landroid/widget/LinearLayout;->addView(Landroid/view/View;Landroid/view/ViewGroup$LayoutParams;)V

    new-instance v6, Lnet/gogame/gopay/sdk/iab/b;

    invoke-direct {v6, v0}, Lnet/gogame/gopay/sdk/iab/b;-><init>(Landroid/content/Context;)V

    iput-object v6, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->B:Lnet/gogame/gopay/sdk/iab/b;

    iget-object v6, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->B:Lnet/gogame/gopay/sdk/iab/b;

    const-string v7, "paymentType"

    invoke-direct {v0, v7}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->a(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v7

    iget-object v1, v1, Lnet/gogame/gopay/sdk/g;->c:Ljava/util/List;

    invoke-virtual {v6, v7, v1}, Lnet/gogame/gopay/sdk/iab/b;->a(Ljava/lang/String;Ljava/util/List;)V

    new-instance v1, Landroid/widget/ImageView;

    invoke-direct {v1, v0}, Landroid/widget/ImageView;-><init>(Landroid/content/Context;)V

    invoke-static {}, Lnet/gogame/gopay/sdk/support/m;->f()Landroid/graphics/Bitmap;

    move-result-object v6

    invoke-virtual {v1, v6}, Landroid/widget/ImageView;->setImageBitmap(Landroid/graphics/Bitmap;)V

    invoke-virtual {v1, v5, v5, v5, v5}, Landroid/widget/ImageView;->setPadding(IIII)V

    new-instance v6, Landroid/widget/LinearLayout$LayoutParams;

    const/high16 v7, 0x41100000    # 9.0f

    invoke-static {v0, v7}, Lnet/gogame/gopay/sdk/support/DisplayUtils;->pxFromDp(Landroid/content/Context;F)I

    move-result v7

    const/4 v9, -0x1

    invoke-direct {v6, v7, v9}, Landroid/widget/LinearLayout$LayoutParams;-><init>(II)V

    iput v14, v6, Landroid/widget/LinearLayout$LayoutParams;->weight:F

    invoke-virtual {v4, v1, v6}, Landroid/widget/LinearLayout;->addView(Landroid/view/View;Landroid/view/ViewGroup$LayoutParams;)V

    new-instance v1, Landroid/widget/LinearLayout$LayoutParams;

    invoke-direct {v1, v5, v9}, Landroid/widget/LinearLayout$LayoutParams;-><init>(II)V

    const/high16 v6, 0x40000000    # 2.0f

    iput v6, v1, Landroid/widget/LinearLayout$LayoutParams;->weight:F

    invoke-static {v0, v14}, Lnet/gogame/gopay/sdk/support/DisplayUtils;->pxFromDp(Landroid/content/Context;F)I

    move-result v6

    invoke-static {v0, v14}, Lnet/gogame/gopay/sdk/support/DisplayUtils;->pxFromDp(Landroid/content/Context;F)I

    move-result v7

    invoke-static {v0, v14}, Lnet/gogame/gopay/sdk/support/DisplayUtils;->pxFromDp(Landroid/content/Context;F)I

    move-result v9

    invoke-static {v0, v14}, Lnet/gogame/gopay/sdk/support/DisplayUtils;->pxFromDp(Landroid/content/Context;F)I

    move-result v12

    invoke-virtual {v1, v6, v7, v9, v12}, Landroid/widget/LinearLayout$LayoutParams;->setMargins(IIII)V

    iput v5, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->Q:I

    new-instance v6, Landroid/widget/Spinner;

    const/4 v7, 0x1

    invoke-direct {v6, v0, v7}, Landroid/widget/Spinner;-><init>(Landroid/content/Context;I)V

    iput-object v6, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->G:Landroid/widget/Spinner;

    iget-object v6, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->G:Landroid/widget/Spinner;

    invoke-virtual {v6, v5}, Landroid/widget/Spinner;->setBackgroundColor(I)V

    iget-object v6, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->G:Landroid/widget/Spinner;

    iget-object v7, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->B:Lnet/gogame/gopay/sdk/iab/b;

    invoke-virtual {v6, v7}, Landroid/widget/Spinner;->setAdapter(Landroid/widget/SpinnerAdapter;)V

    iget-object v6, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->G:Landroid/widget/Spinner;

    invoke-virtual {v6, v5}, Landroid/widget/Spinner;->setSelection(I)V

    iget-object v6, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->G:Landroid/widget/Spinner;

    invoke-virtual {v6, v5, v5, v5, v5}, Landroid/widget/Spinner;->setPadding(IIII)V

    iget-object v6, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->G:Landroid/widget/Spinner;

    iget-object v7, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->B:Lnet/gogame/gopay/sdk/iab/b;

    invoke-virtual {v7}, Lnet/gogame/gopay/sdk/iab/b;->getCount()I

    move-result v7

    const/4 v9, 0x1

    if-le v7, v9, :cond_8

    const/4 v7, 0x1

    goto :goto_7

    :cond_8
    const/4 v7, 0x0

    :goto_7
    invoke-virtual {v6, v7}, Landroid/widget/Spinner;->setEnabled(Z)V

    sget v6, Landroid/os/Build$VERSION;->SDK_INT:I

    iget-object v6, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->G:Landroid/widget/Spinner;

    new-instance v7, Lnet/gogame/gopay/sdk/iab/p;

    invoke-direct {v7, v0}, Lnet/gogame/gopay/sdk/iab/p;-><init>(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)V

    invoke-virtual {v6, v7}, Landroid/widget/Spinner;->setOnTouchListener(Landroid/view/View$OnTouchListener;)V

    iget-object v6, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->G:Landroid/widget/Spinner;

    new-instance v7, Lnet/gogame/gopay/sdk/iab/q;

    invoke-direct {v7, v0}, Lnet/gogame/gopay/sdk/iab/q;-><init>(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)V

    invoke-virtual {v6, v7}, Landroid/widget/Spinner;->setOnItemSelectedListener(Landroid/widget/AdapterView$OnItemSelectedListener;)V

    iget-object v6, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->G:Landroid/widget/Spinner;

    invoke-virtual {v4, v6, v1}, Landroid/widget/LinearLayout;->addView(Landroid/view/View;Landroid/view/ViewGroup$LayoutParams;)V

    if-eqz v3, :cond_9

    new-instance v1, Landroid/widget/ImageButton;

    invoke-direct {v1, v0}, Landroid/widget/ImageButton;-><init>(Landroid/content/Context;)V

    invoke-virtual {v1, v5}, Landroid/widget/ImageButton;->setMinimumWidth(I)V

    invoke-virtual {v1, v5}, Landroid/widget/ImageButton;->setMinimumHeight(I)V

    const/16 v3, 0x5c

    const/16 v4, 0xb0

    const/16 v6, 0x3b

    invoke-static {v3, v4, v6}, Landroid/graphics/Color;->rgb(III)I

    move-result v7

    invoke-virtual {v1, v7}, Landroid/widget/ImageButton;->setBackgroundColor(I)V

    invoke-virtual {v1, v5, v5, v5, v5}, Landroid/widget/ImageButton;->setPadding(IIII)V

    invoke-static {}, Lnet/gogame/gopay/sdk/support/m;->h()Landroid/graphics/Bitmap;

    move-result-object v3

    invoke-direct {v0, v3}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->a(Landroid/graphics/Bitmap;)Landroid/graphics/drawable/BitmapDrawable;

    move-result-object v3

    invoke-virtual {v1, v3}, Landroid/widget/ImageButton;->setImageDrawable(Landroid/graphics/drawable/Drawable;)V

    new-instance v3, Lnet/gogame/gopay/sdk/iab/u;

    invoke-direct {v3, v0}, Lnet/gogame/gopay/sdk/iab/u;-><init>(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)V

    invoke-virtual {v1, v3}, Landroid/widget/ImageButton;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    new-instance v3, Landroid/widget/LinearLayout$LayoutParams;

    const/4 v4, -0x1

    invoke-direct {v3, v8, v4}, Landroid/widget/LinearLayout$LayoutParams;-><init>(II)V

    invoke-virtual {v11, v1, v3}, Landroid/widget/LinearLayout;->addView(Landroid/view/View;Landroid/view/ViewGroup$LayoutParams;)V

    :cond_9
    new-instance v1, Lnet/gogame/gopay/sdk/iab/i;

    invoke-direct {v1, v0}, Lnet/gogame/gopay/sdk/iab/i;-><init>(Landroid/content/Context;)V

    iput-object v1, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->C:Lnet/gogame/gopay/sdk/iab/i;

    const/16 v1, 0xf1

    if-eqz p2, :cond_c

    new-instance v2, Landroid/widget/RelativeLayout;

    invoke-direct {v2, v0}, Landroid/widget/RelativeLayout;-><init>(Landroid/content/Context;)V

    invoke-static {v1, v1, v1}, Landroid/graphics/Color;->rgb(III)I

    move-result v3

    invoke-virtual {v2, v3}, Landroid/widget/RelativeLayout;->setBackgroundColor(I)V

    new-instance v3, Landroid/widget/LinearLayout$LayoutParams;

    const/4 v4, -0x2

    const/4 v6, -0x1

    invoke-direct {v3, v6, v4}, Landroid/widget/LinearLayout$LayoutParams;-><init>(II)V

    invoke-virtual {v10, v2, v3}, Landroid/widget/LinearLayout;->addView(Landroid/view/View;Landroid/view/ViewGroup$LayoutParams;)V

    const/high16 v3, 0x40000000    # 2.0f

    invoke-static {v0, v3}, Lnet/gogame/gopay/sdk/support/DisplayUtils;->pxFromDp(Landroid/content/Context;F)I

    move-result v3

    new-instance v6, Landroid/graphics/drawable/GradientDrawable;

    invoke-direct {v6}, Landroid/graphics/drawable/GradientDrawable;-><init>()V

    const/16 v7, 0xd5

    invoke-static {v7, v7, v7}, Landroid/graphics/Color;->rgb(III)I

    move-result v7

    invoke-virtual {v6, v3, v7}, Landroid/graphics/drawable/GradientDrawable;->setStroke(II)V

    invoke-static {v1, v1, v1}, Landroid/graphics/Color;->rgb(III)I

    move-result v1

    invoke-virtual {v6, v1}, Landroid/graphics/drawable/GradientDrawable;->setColor(I)V

    const/4 v1, 0x2

    invoke-virtual {v6, v1}, Landroid/graphics/drawable/GradientDrawable;->setGradientType(I)V

    const/4 v1, 0x1

    new-array v7, v1, [Landroid/graphics/drawable/Drawable;

    aput-object v6, v7, v5

    new-instance v1, Landroid/graphics/drawable/LayerDrawable;

    invoke-direct {v1, v7}, Landroid/graphics/drawable/LayerDrawable;-><init>([Landroid/graphics/drawable/Drawable;)V

    const/4 v12, 0x0

    neg-int v15, v3

    const/16 v16, 0x0

    move-object v11, v1

    move v13, v15

    move v14, v15

    invoke-virtual/range {v11 .. v16}, Landroid/graphics/drawable/LayerDrawable;->setLayerInset(IIIII)V

    sget v3, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v6, 0x10

    if-lt v3, v6, :cond_a

    invoke-virtual {v2, v1}, Landroid/widget/RelativeLayout;->setBackground(Landroid/graphics/drawable/Drawable;)V

    goto :goto_8

    :cond_a
    invoke-virtual {v2, v1}, Landroid/widget/RelativeLayout;->setBackgroundDrawable(Landroid/graphics/drawable/Drawable;)V

    :goto_8
    new-instance v1, Landroid/widget/RelativeLayout$LayoutParams;

    const/high16 v3, 0x428c0000    # 70.0f

    invoke-static {v0, v3}, Lnet/gogame/gopay/sdk/support/DisplayUtils;->pxFromDp(Landroid/content/Context;F)I

    move-result v3

    const/high16 v6, 0x42200000    # 40.0f

    invoke-static {v0, v6}, Lnet/gogame/gopay/sdk/support/DisplayUtils;->pxFromDp(Landroid/content/Context;F)I

    move-result v6

    invoke-direct {v1, v3, v6}, Landroid/widget/RelativeLayout$LayoutParams;-><init>(II)V

    const/16 v3, 0xb

    invoke-virtual {v1, v3}, Landroid/widget/RelativeLayout$LayoutParams;->addRule(I)V

    const/16 v3, 0xf

    invoke-virtual {v1, v3}, Landroid/widget/RelativeLayout$LayoutParams;->addRule(I)V

    new-instance v3, Landroid/widget/Button;

    invoke-direct {v3, v0}, Landroid/widget/Button;-><init>(Landroid/content/Context;)V

    iput-object v3, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->z:Landroid/widget/Button;

    iget-object v3, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->z:Landroid/widget/Button;

    const-string v6, "MORE"

    invoke-virtual {v3, v6}, Landroid/widget/Button;->setText(Ljava/lang/CharSequence;)V

    iget-object v3, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->z:Landroid/widget/Button;

    const/high16 v6, 0x41800000    # 16.0f

    invoke-virtual {v3, v6}, Landroid/widget/Button;->setTextSize(F)V

    iget-object v3, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->z:Landroid/widget/Button;

    invoke-virtual {v3, v5}, Landroid/widget/Button;->setMinimumWidth(I)V

    iget-object v3, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->z:Landroid/widget/Button;

    invoke-virtual {v3, v5}, Landroid/widget/Button;->setMinWidth(I)V

    iget-object v3, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->z:Landroid/widget/Button;

    const/high16 v6, 0x428c0000    # 70.0f

    invoke-static {v0, v6}, Lnet/gogame/gopay/sdk/support/DisplayUtils;->pxFromDp(Landroid/content/Context;F)I

    move-result v6

    invoke-virtual {v3, v6}, Landroid/widget/Button;->setMaxWidth(I)V

    iget-object v3, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->z:Landroid/widget/Button;

    sget-object v6, Landroid/graphics/Typeface;->DEFAULT_BOLD:Landroid/graphics/Typeface;

    invoke-virtual {v3, v6}, Landroid/widget/Button;->setTypeface(Landroid/graphics/Typeface;)V

    iget-object v3, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->z:Landroid/widget/Button;

    const/16 v6, 0x5c

    const/16 v7, 0xb0

    const/16 v8, 0x3b

    invoke-static {v6, v7, v8}, Landroid/graphics/Color;->rgb(III)I

    move-result v6

    invoke-virtual {v3, v6}, Landroid/widget/Button;->setTextColor(I)V

    invoke-static {}, Lnet/gogame/gopay/sdk/support/m;->d()Landroid/graphics/Bitmap;

    move-result-object v3

    if-eqz v3, :cond_b

    iget-object v6, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->z:Landroid/widget/Button;

    const/4 v7, 0x0

    const/4 v8, 0x0

    invoke-direct {v0, v3}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->a(Landroid/graphics/Bitmap;)Landroid/graphics/drawable/BitmapDrawable;

    move-result-object v3

    const/4 v9, 0x0

    invoke-virtual {v6, v7, v8, v3, v9}, Landroid/widget/Button;->setCompoundDrawablesWithIntrinsicBounds(Landroid/graphics/drawable/Drawable;Landroid/graphics/drawable/Drawable;Landroid/graphics/drawable/Drawable;Landroid/graphics/drawable/Drawable;)V

    :cond_b
    iget-object v3, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->z:Landroid/widget/Button;

    const/high16 v6, 0x40800000    # 4.0f

    invoke-static {v0, v6}, Lnet/gogame/gopay/sdk/support/DisplayUtils;->pxFromDp(Landroid/content/Context;F)I

    move-result v7

    invoke-virtual {v3, v7}, Landroid/widget/Button;->setCompoundDrawablePadding(I)V

    iget-object v3, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->z:Landroid/widget/Button;

    invoke-virtual {v3, v5}, Landroid/widget/Button;->setBackgroundColor(I)V

    iget-object v3, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->z:Landroid/widget/Button;

    invoke-static {v0, v6}, Lnet/gogame/gopay/sdk/support/DisplayUtils;->pxFromDp(Landroid/content/Context;F)I

    move-result v6

    invoke-virtual {v3, v5, v5, v6, v5}, Landroid/widget/Button;->setPadding(IIII)V

    iget-object v3, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->z:Landroid/widget/Button;

    new-instance v6, Lnet/gogame/gopay/sdk/iab/x;

    invoke-direct {v6, v0}, Lnet/gogame/gopay/sdk/iab/x;-><init>(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)V

    invoke-virtual {v3, v6}, Landroid/widget/Button;->setOnTouchListener(Landroid/view/View$OnTouchListener;)V

    iget-object v3, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->z:Landroid/widget/Button;

    new-instance v6, Lnet/gogame/gopay/sdk/iab/y;

    invoke-direct {v6, v0}, Lnet/gogame/gopay/sdk/iab/y;-><init>(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)V

    invoke-virtual {v3, v6}, Landroid/widget/Button;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    iget-object v3, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->z:Landroid/widget/Button;

    invoke-virtual {v2, v3, v1}, Landroid/widget/RelativeLayout;->addView(Landroid/view/View;Landroid/view/ViewGroup$LayoutParams;)V

    new-instance v1, Landroid/widget/LinearLayout;

    invoke-direct {v1, v0}, Landroid/widget/LinearLayout;-><init>(Landroid/content/Context;)V

    invoke-virtual {v1, v5}, Landroid/widget/LinearLayout;->setBackgroundColor(I)V

    invoke-virtual {v1, v5}, Landroid/widget/LinearLayout;->setOrientation(I)V

    new-instance v3, Landroid/widget/RelativeLayout$LayoutParams;

    invoke-direct {v3, v4, v4}, Landroid/widget/RelativeLayout$LayoutParams;-><init>(II)V

    const/16 v4, 0x9

    invoke-virtual {v3, v4}, Landroid/widget/RelativeLayout$LayoutParams;->addRule(I)V

    const/16 v4, 0xf

    invoke-virtual {v3, v4}, Landroid/widget/RelativeLayout$LayoutParams;->addRule(I)V

    invoke-virtual {v2, v1, v3}, Landroid/widget/RelativeLayout;->addView(Landroid/view/View;Landroid/view/ViewGroup$LayoutParams;)V

    new-instance v2, Lnet/gogame/gopay/sdk/support/c;

    invoke-direct {v2, v0}, Lnet/gogame/gopay/sdk/support/c;-><init>(Landroid/content/Context;)V

    iput-object v2, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->b:Lnet/gogame/gopay/sdk/support/c;

    iget-object v2, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->b:Lnet/gogame/gopay/sdk/support/c;

    invoke-virtual {v2, v5}, Lnet/gogame/gopay/sdk/support/c;->setBackgroundColor(I)V

    iget-object v2, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->b:Lnet/gogame/gopay/sdk/support/c;

    invoke-virtual {v2, v5}, Lnet/gogame/gopay/sdk/support/c;->setScrollingEnabled(Z)V

    iget-object v2, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->b:Lnet/gogame/gopay/sdk/support/c;

    invoke-virtual {v2, v5}, Lnet/gogame/gopay/sdk/support/c;->setHorizontalFadingEdgeEnabled(Z)V

    iget-object v2, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->b:Lnet/gogame/gopay/sdk/support/c;

    new-instance v3, Lnet/gogame/gopay/sdk/iab/bs;

    iget-object v4, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->C:Lnet/gogame/gopay/sdk/iab/i;

    invoke-direct {v3, v0, v4}, Lnet/gogame/gopay/sdk/iab/bs;-><init>(Landroid/content/Context;Lnet/gogame/gopay/sdk/iab/i;)V

    iput-object v3, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->D:Lnet/gogame/gopay/sdk/iab/bs;

    invoke-virtual {v2, v3}, Lnet/gogame/gopay/sdk/support/c;->setAdapter(Landroid/widget/ListAdapter;)V

    iget-object v2, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->b:Lnet/gogame/gopay/sdk/support/c;

    new-instance v3, Lnet/gogame/gopay/sdk/iab/ac;

    invoke-direct {v3, v0}, Lnet/gogame/gopay/sdk/iab/ac;-><init>(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)V

    invoke-virtual {v2, v3}, Lnet/gogame/gopay/sdk/support/c;->setOnItemClickListener(Landroid/widget/AdapterView$OnItemClickListener;)V

    iget-object v2, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->b:Lnet/gogame/gopay/sdk/support/c;

    new-instance v3, Landroid/widget/LinearLayout$LayoutParams;

    const/high16 v4, 0x42700000    # 60.0f

    invoke-static {v0, v4}, Lnet/gogame/gopay/sdk/support/DisplayUtils;->pxFromDp(Landroid/content/Context;F)I

    move-result v4

    const/4 v6, -0x1

    invoke-direct {v3, v6, v4}, Landroid/widget/LinearLayout$LayoutParams;-><init>(II)V

    invoke-virtual {v1, v2, v3}, Landroid/widget/LinearLayout;->addView(Landroid/view/View;Landroid/view/ViewGroup$LayoutParams;)V

    new-instance v1, Landroid/widget/LinearLayout;

    invoke-direct {v1, v0}, Landroid/widget/LinearLayout;-><init>(Landroid/content/Context;)V

    const/16 v2, 0x64

    const/16 v3, 0x64

    const/16 v4, 0x64

    invoke-static {v2, v3, v4}, Landroid/graphics/Color;->rgb(III)I

    move-result v2

    invoke-virtual {v1, v2}, Landroid/widget/LinearLayout;->setBackgroundColor(I)V

    const/4 v2, 0x1

    invoke-virtual {v1, v2}, Landroid/widget/LinearLayout;->setOrientation(I)V

    new-instance v2, Landroid/widget/LinearLayout$LayoutParams;

    invoke-direct {v2, v6, v6}, Landroid/widget/LinearLayout$LayoutParams;-><init>(II)V

    invoke-virtual {v10, v1, v2}, Landroid/widget/LinearLayout;->addView(Landroid/view/View;Landroid/view/ViewGroup$LayoutParams;)V

    goto/16 :goto_a

    :cond_c
    const/4 v6, -0x1

    iget-object v2, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->C:Lnet/gogame/gopay/sdk/iab/i;

    const/4 v3, 0x2

    iput v3, v2, Lnet/gogame/gopay/sdk/iab/i;->d:I

    new-instance v2, Landroid/widget/LinearLayout;

    invoke-direct {v2, v0}, Landroid/widget/LinearLayout;-><init>(Landroid/content/Context;)V

    invoke-virtual {v2, v5}, Landroid/widget/LinearLayout;->setBackgroundColor(I)V

    invoke-virtual {v2, v5}, Landroid/widget/LinearLayout;->setOrientation(I)V

    new-instance v3, Landroid/widget/LinearLayout$LayoutParams;

    invoke-direct {v3, v6, v6}, Landroid/widget/LinearLayout$LayoutParams;-><init>(II)V

    invoke-virtual {v10, v2, v3}, Landroid/widget/LinearLayout;->addView(Landroid/view/View;Landroid/view/ViewGroup$LayoutParams;)V

    new-instance v3, Landroid/graphics/drawable/GradientDrawable;

    invoke-direct {v3}, Landroid/graphics/drawable/GradientDrawable;-><init>()V

    const/16 v4, 0xe6

    const/16 v6, 0xe6

    const/16 v7, 0xe6

    invoke-static {v4, v6, v7}, Landroid/graphics/Color;->rgb(III)I

    move-result v4

    const/4 v6, 0x1

    invoke-virtual {v3, v6, v4}, Landroid/graphics/drawable/GradientDrawable;->setStroke(II)V

    invoke-static {v1, v1, v1}, Landroid/graphics/Color;->rgb(III)I

    move-result v1

    invoke-virtual {v3, v1}, Landroid/graphics/drawable/GradientDrawable;->setColor(I)V

    const/4 v1, 0x2

    invoke-virtual {v3, v1}, Landroid/graphics/drawable/GradientDrawable;->setGradientType(I)V

    new-array v1, v6, [Landroid/graphics/drawable/Drawable;

    aput-object v3, v1, v5

    new-instance v3, Landroid/graphics/drawable/LayerDrawable;

    invoke-direct {v3, v1}, Landroid/graphics/drawable/LayerDrawable;-><init>([Landroid/graphics/drawable/Drawable;)V

    const/4 v8, 0x0

    const/4 v9, -0x2

    const/4 v10, -0x2

    const/4 v11, 0x0

    const/4 v12, -0x2

    move-object v7, v3

    invoke-virtual/range {v7 .. v12}, Landroid/graphics/drawable/LayerDrawable;->setLayerInset(IIIII)V

    new-instance v1, Landroid/widget/ListView;

    invoke-direct {v1, v0}, Landroid/widget/ListView;-><init>(Landroid/content/Context;)V

    sget v4, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v6, 0x10

    if-lt v4, v6, :cond_d

    invoke-virtual {v1, v3}, Landroid/widget/ListView;->setBackground(Landroid/graphics/drawable/Drawable;)V

    goto :goto_9

    :cond_d
    invoke-virtual {v1, v3}, Landroid/widget/ListView;->setBackgroundDrawable(Landroid/graphics/drawable/Drawable;)V

    :goto_9
    iget-object v3, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->C:Lnet/gogame/gopay/sdk/iab/i;

    invoke-virtual {v1, v3}, Landroid/widget/ListView;->setAdapter(Landroid/widget/ListAdapter;)V

    new-instance v3, Lnet/gogame/gopay/sdk/iab/af;

    invoke-direct {v3, v0}, Lnet/gogame/gopay/sdk/iab/af;-><init>(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)V

    invoke-virtual {v1, v3}, Landroid/widget/ListView;->setOnTouchListener(Landroid/view/View$OnTouchListener;)V

    new-instance v3, Lnet/gogame/gopay/sdk/iab/ag;

    invoke-direct {v3, v0}, Lnet/gogame/gopay/sdk/iab/ag;-><init>(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)V

    invoke-virtual {v1, v3}, Landroid/widget/ListView;->setOnItemClickListener(Landroid/widget/AdapterView$OnItemClickListener;)V

    new-instance v3, Lnet/gogame/gopay/sdk/iab/aj;

    invoke-direct {v3, v0}, Lnet/gogame/gopay/sdk/iab/aj;-><init>(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)V

    invoke-virtual {v1, v3}, Landroid/widget/ListView;->setOnItemLongClickListener(Landroid/widget/AdapterView$OnItemLongClickListener;)V

    new-instance v3, Landroid/widget/LinearLayout$LayoutParams;

    const/high16 v4, 0x42f00000    # 120.0f

    invoke-static {v0, v4}, Lnet/gogame/gopay/sdk/support/DisplayUtils;->pxFromDp(Landroid/content/Context;F)I

    move-result v4

    const/4 v6, -0x1

    invoke-direct {v3, v4, v6}, Landroid/widget/LinearLayout$LayoutParams;-><init>(II)V

    invoke-virtual {v2, v1, v3}, Landroid/widget/LinearLayout;->addView(Landroid/view/View;Landroid/view/ViewGroup$LayoutParams;)V

    move-object v1, v2

    :goto_a
    iget-object v2, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->H:Lnet/gogame/gopay/sdk/iab/g;

    new-instance v3, Lnet/gogame/gopay/sdk/iab/an;

    invoke-direct {v3, v0}, Lnet/gogame/gopay/sdk/iab/an;-><init>(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)V

    invoke-virtual {v2, v3}, Lnet/gogame/gopay/sdk/iab/g;->setCallback(Lnet/gogame/gopay/sdk/iab/h;)V

    new-instance v2, Landroid/webkit/WebView;

    invoke-direct {v2, v0}, Landroid/webkit/WebView;-><init>(Landroid/content/Context;)V

    iput-object v2, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->y:Landroid/webkit/WebView;

    iget-object v2, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->y:Landroid/webkit/WebView;

    iget-object v3, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->H:Lnet/gogame/gopay/sdk/iab/g;

    const-string v4, "app"

    invoke-virtual {v2, v3, v4}, Landroid/webkit/WebView;->addJavascriptInterface(Ljava/lang/Object;Ljava/lang/String;)V

    iget-object v2, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->y:Landroid/webkit/WebView;

    const/4 v3, 0x1

    invoke-virtual {v2, v3}, Landroid/webkit/WebView;->setHorizontalScrollBarEnabled(Z)V

    iget-object v2, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->y:Landroid/webkit/WebView;

    invoke-virtual {v2, v3}, Landroid/webkit/WebView;->setVerticalScrollBarEnabled(Z)V

    iget-object v2, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->y:Landroid/webkit/WebView;

    invoke-virtual {v2, v5}, Landroid/webkit/WebView;->setHapticFeedbackEnabled(Z)V

    iget-object v2, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->y:Landroid/webkit/WebView;

    new-instance v4, Lnet/gogame/gopay/sdk/iab/ap;

    invoke-direct {v4, v0}, Lnet/gogame/gopay/sdk/iab/ap;-><init>(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)V

    invoke-virtual {v2, v4}, Landroid/webkit/WebView;->setOnLongClickListener(Landroid/view/View$OnLongClickListener;)V

    iget-object v2, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->y:Landroid/webkit/WebView;

    invoke-virtual {v2, v5}, Landroid/webkit/WebView;->setLongClickable(Z)V

    iget-object v2, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->y:Landroid/webkit/WebView;

    invoke-virtual {v2}, Landroid/webkit/WebView;->getSettings()Landroid/webkit/WebSettings;

    move-result-object v2

    invoke-virtual {v2, v3}, Landroid/webkit/WebSettings;->setAllowContentAccess(Z)V

    iget-object v2, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->y:Landroid/webkit/WebView;

    invoke-virtual {v2}, Landroid/webkit/WebView;->getSettings()Landroid/webkit/WebSettings;

    move-result-object v2

    invoke-virtual {v2, v3}, Landroid/webkit/WebSettings;->setJavaScriptCanOpenWindowsAutomatically(Z)V

    iget-object v2, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->y:Landroid/webkit/WebView;

    invoke-virtual {v2}, Landroid/webkit/WebView;->getSettings()Landroid/webkit/WebSettings;

    move-result-object v2

    invoke-virtual {v2, v3}, Landroid/webkit/WebSettings;->setJavaScriptEnabled(Z)V

    iget-object v2, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->y:Landroid/webkit/WebView;

    invoke-virtual {v2}, Landroid/webkit/WebView;->getSettings()Landroid/webkit/WebSettings;

    move-result-object v2

    invoke-virtual {v2, v3}, Landroid/webkit/WebSettings;->setSupportZoom(Z)V

    iget-object v2, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->y:Landroid/webkit/WebView;

    invoke-virtual {v2}, Landroid/webkit/WebView;->getSettings()Landroid/webkit/WebSettings;

    move-result-object v2

    invoke-virtual {v2, v3}, Landroid/webkit/WebSettings;->setDomStorageEnabled(Z)V

    iget-object v2, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->y:Landroid/webkit/WebView;

    invoke-virtual {v2}, Landroid/webkit/WebView;->getSettings()Landroid/webkit/WebSettings;

    move-result-object v2

    invoke-virtual {v2, v3}, Landroid/webkit/WebSettings;->setUseWideViewPort(Z)V

    iget-object v2, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->y:Landroid/webkit/WebView;

    invoke-virtual {v2}, Landroid/webkit/WebView;->getSettings()Landroid/webkit/WebSettings;

    move-result-object v2

    invoke-virtual {v2, v3}, Landroid/webkit/WebSettings;->setLoadWithOverviewMode(Z)V

    iget-object v2, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->y:Landroid/webkit/WebView;

    invoke-virtual {v2}, Landroid/webkit/WebView;->getSettings()Landroid/webkit/WebSettings;

    move-result-object v2

    invoke-virtual {v2, v5}, Landroid/webkit/WebSettings;->setAppCacheEnabled(Z)V

    iget-object v2, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->y:Landroid/webkit/WebView;

    invoke-virtual {v2}, Landroid/webkit/WebView;->getSettings()Landroid/webkit/WebSettings;

    move-result-object v2

    const/4 v3, 0x2

    invoke-virtual {v2, v3}, Landroid/webkit/WebSettings;->setCacheMode(I)V

    iget-object v2, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->y:Landroid/webkit/WebView;

    new-instance v3, Landroid/webkit/WebChromeClient;

    invoke-direct {v3}, Landroid/webkit/WebChromeClient;-><init>()V

    invoke-virtual {v2, v3}, Landroid/webkit/WebView;->setWebChromeClient(Landroid/webkit/WebChromeClient;)V

    iget-object v2, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->y:Landroid/webkit/WebView;

    new-instance v3, Lnet/gogame/gopay/sdk/iab/aq;

    invoke-direct {v3, v0}, Lnet/gogame/gopay/sdk/iab/aq;-><init>(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)V

    invoke-virtual {v2, v3}, Landroid/webkit/WebView;->setWebViewClient(Landroid/webkit/WebViewClient;)V

    iget-object v2, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->y:Landroid/webkit/WebView;

    new-instance v3, Lnet/gogame/gopay/sdk/iab/ar;

    invoke-direct {v3, v0}, Lnet/gogame/gopay/sdk/iab/ar;-><init>(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)V

    invoke-virtual {v2, v3}, Landroid/webkit/WebView;->setOnTouchListener(Landroid/view/View$OnTouchListener;)V

    iget-object v2, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->y:Landroid/webkit/WebView;

    new-instance v3, Landroid/widget/LinearLayout$LayoutParams;

    const/4 v4, -0x1

    invoke-direct {v3, v4, v4}, Landroid/widget/LinearLayout$LayoutParams;-><init>(II)V

    invoke-virtual {v1, v2, v3}, Landroid/widget/LinearLayout;->addView(Landroid/view/View;Landroid/view/ViewGroup$LayoutParams;)V

    iget-object v1, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->s:Landroid/widget/ProgressBar;

    if-eqz v1, :cond_e

    iget-object v1, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->s:Landroid/widget/ProgressBar;

    invoke-virtual {v1}, Landroid/widget/ProgressBar;->bringToFront()V

    :cond_e
    const/4 v1, 0x1

    iput-boolean v1, v0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->J:Z

    return-void
.end method

.method static synthetic a(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;Lnet/gogame/gopay/sdk/iab/a;)V
    .locals 0

    invoke-direct {p0, p1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->a(Lnet/gogame/gopay/sdk/iab/a;)V

    return-void
.end method

.method static synthetic a(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;Lnet/gogame/gopay/sdk/iab/bq;)V
    .locals 1

    const/4 v0, 0x0

    invoke-direct {p0, p1, v0}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->a(Lnet/gogame/gopay/sdk/iab/bq;Z)V

    return-void
.end method

.method static synthetic a(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;Lnet/gogame/gopay/sdk/n;)V
    .locals 12

    iget-boolean v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->J:Z

    if-nez v0, :cond_c

    iget-boolean v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->d:Z

    if-eqz v0, :cond_0

    goto/16 :goto_a

    :cond_0
    if-eqz p1, :cond_1

    iget-object v0, p1, Lnet/gogame/gopay/sdk/n;->c:Lnet/gogame/gopay/sdk/f;

    invoke-direct {p0, v0}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->a(Lnet/gogame/gopay/sdk/f;)Z

    move-result v0

    if-nez v0, :cond_c

    :cond_1
    iget-object v0, p1, Lnet/gogame/gopay/sdk/n;->b:Lorg/json/JSONObject;

    const/4 v1, 0x1

    :try_start_0
    new-instance v11, Lnet/gogame/gopay/sdk/iab/br;

    iget-object v3, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->e:Ljava/lang/String;

    invoke-virtual {p0}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->getPackageName()Ljava/lang/String;

    move-result-object v4

    const-string v2, "order_id"

    invoke-virtual {v0, v2}, Lorg/json/JSONObject;->has(Ljava/lang/String;)Z

    move-result v2

    if-eqz v2, :cond_2

    const-string v2, "order_id"

    invoke-virtual {v0, v2}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    goto :goto_0

    :cond_2
    const-string v2, "xxx"

    :goto_0
    move-object v5, v2

    const-string v2, "order_id"

    invoke-virtual {v0, v2}, Lorg/json/JSONObject;->has(Ljava/lang/String;)Z

    move-result v2

    if-eqz v2, :cond_3

    const-string v2, "order_id"

    invoke-virtual {v0, v2}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    goto :goto_1

    :cond_3
    const-string v2, "xxx"

    :goto_1
    move-object v6, v2

    const-string v2, "timestamp"

    invoke-virtual {v0, v2}, Lorg/json/JSONObject;->has(Ljava/lang/String;)Z

    move-result v2

    if-eqz v2, :cond_4

    const-string v2, "timestamp"

    invoke-virtual {v0, v2}, Lorg/json/JSONObject;->getLong(Ljava/lang/String;)J

    move-result-wide v7

    goto :goto_2

    :cond_4
    const-wide/16 v7, 0x0

    :goto_2
    const-string v2, "payload"

    invoke-virtual {v0, v2}, Lorg/json/JSONObject;->has(Ljava/lang/String;)Z

    move-result v2

    if-eqz v2, :cond_5

    const-string v2, "payload"

    invoke-virtual {v0, v2}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    goto :goto_3

    :cond_5
    iget-object v2, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->h:Ljava/lang/String;

    :goto_3
    move-object v9, v2

    const-string v2, "gp_status"

    invoke-virtual {v0, v2}, Lorg/json/JSONObject;->has(Ljava/lang/String;)Z

    move-result v2

    if-eqz v2, :cond_6

    const-string v2, "gp_status"

    invoke-virtual {v0, v2}, Lorg/json/JSONObject;->getInt(Ljava/lang/String;)I

    move-result v2

    move v10, v2

    goto :goto_4

    :cond_6
    const/4 v10, 0x1

    :goto_4
    move-object v2, v11

    invoke-direct/range {v2 .. v10}, Lnet/gogame/gopay/sdk/iab/br;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;I)V

    iput-object v11, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->i:Lnet/gogame/gopay/sdk/iab/br;

    const-string v2, "timer"

    invoke-virtual {v0, v2}, Lorg/json/JSONObject;->has(Ljava/lang/String;)Z

    move-result v2

    if-eqz v2, :cond_7

    const-string v2, "timer"

    invoke-virtual {v0, v2}, Lorg/json/JSONObject;->getInt(Ljava/lang/String;)I

    move-result v2

    goto :goto_5

    :cond_7
    const/4 v2, 0x5

    :goto_5
    mul-int/lit16 v2, v2, 0x3e8

    iput v2, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->O:I

    const-string v2, "header_text"

    invoke-virtual {v0, v2}, Lorg/json/JSONObject;->has(Ljava/lang/String;)Z

    move-result v2

    if-eqz v2, :cond_8

    const-string v2, "header_text"

    invoke-virtual {v0, v2}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    goto :goto_6

    :cond_8
    const-string v2, "Confirmation"

    :goto_6
    iput-object v2, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->o:Ljava/lang/String;

    const-string v2, "info_text"

    invoke-virtual {v0, v2}, Lorg/json/JSONObject;->has(Ljava/lang/String;)Z

    move-result v2

    if-eqz v2, :cond_9

    const-string v2, "info_text"

    invoke-virtual {v0, v2}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    goto :goto_7

    :cond_9
    const-string v2, "Cancel current Purchase?"

    :goto_7
    iput-object v2, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->n:Ljava/lang/String;

    const-string v2, "yes_btn_text"

    invoke-virtual {v0, v2}, Lorg/json/JSONObject;->has(Ljava/lang/String;)Z

    move-result v2

    if-eqz v2, :cond_a

    const-string v2, "yes_btn_text"

    invoke-virtual {v0, v2}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    goto :goto_8

    :cond_a
    const-string v2, "Yes"

    :goto_8
    iput-object v2, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->l:Ljava/lang/String;

    const-string v2, "no_btn_text"

    invoke-virtual {v0, v2}, Lorg/json/JSONObject;->has(Ljava/lang/String;)Z

    move-result v2

    if-eqz v2, :cond_b

    const-string v2, "no_btn_text"

    invoke-virtual {v0, v2}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    goto :goto_9

    :cond_b
    const-string v0, "No"

    :goto_9
    iput-object v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->m:Ljava/lang/String;
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    :catch_0
    const/4 v0, 0x0

    iput-boolean v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->K:Z

    iput-boolean v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->L:Z

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->y:Landroid/webkit/WebView;

    invoke-virtual {v0}, Landroid/webkit/WebView;->stopLoading()V

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->y:Landroid/webkit/WebView;

    invoke-virtual {v0, v1}, Landroid/webkit/WebView;->clearCache(Z)V

    iget-object p0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->y:Landroid/webkit/WebView;

    iget-object p1, p1, Lnet/gogame/gopay/sdk/n;->a:Ljava/lang/String;

    invoke-virtual {p0, p1}, Landroid/webkit/WebView;->loadUrl(Ljava/lang/String;)V

    :cond_c
    :goto_a
    return-void
.end method

.method private a(Lnet/gogame/gopay/sdk/iab/a;)V
    .locals 3

    if-nez p1, :cond_0

    return-void

    :cond_0
    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->u:Landroid/os/AsyncTask;

    const/4 v1, 0x0

    if-eqz v0, :cond_1

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->u:Landroid/os/AsyncTask;

    const/4 v2, 0x1

    invoke-virtual {v0, v2}, Landroid/os/AsyncTask;->cancel(Z)Z

    iput-object v1, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->u:Landroid/os/AsyncTask;

    :cond_1
    invoke-direct {p0}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->a()V

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->y:Landroid/webkit/WebView;

    invoke-virtual {v0}, Landroid/webkit/WebView;->stopLoading()V

    instance-of v0, p1, Lnet/gogame/gopay/sdk/iab/f;

    if-eqz v0, :cond_3

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->C:Lnet/gogame/gopay/sdk/iab/i;

    const/4 v2, -0x1

    iput v2, v0, Lnet/gogame/gopay/sdk/iab/i;->e:I

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->C:Lnet/gogame/gopay/sdk/iab/i;

    iget-object v0, v0, Lnet/gogame/gopay/sdk/iab/i;->f:Landroid/view/View;

    if-eqz v0, :cond_2

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->C:Lnet/gogame/gopay/sdk/iab/i;

    iget-object v0, v0, Lnet/gogame/gopay/sdk/iab/i;->f:Landroid/view/View;

    const/16 v2, 0xf1

    invoke-static {v2, v2, v2}, Landroid/graphics/Color;->rgb(III)I

    move-result v2

    invoke-virtual {v0, v2}, Landroid/view/View;->setBackgroundColor(I)V

    :cond_2
    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->C:Lnet/gogame/gopay/sdk/iab/i;

    iput-object v1, v0, Lnet/gogame/gopay/sdk/iab/i;->f:Landroid/view/View;

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->y:Landroid/webkit/WebView;

    check-cast p1, Lnet/gogame/gopay/sdk/iab/f;

    iget-object p1, p1, Lnet/gogame/gopay/sdk/iab/f;->a:Ljava/lang/String;

    invoke-virtual {v0, p1}, Landroid/webkit/WebView;->loadUrl(Ljava/lang/String;)V

    return-void

    :cond_3
    new-instance v0, Lnet/gogame/gopay/sdk/iab/at;

    invoke-direct {v0, p0, p1}, Lnet/gogame/gopay/sdk/iab/at;-><init>(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;Lnet/gogame/gopay/sdk/iab/a;)V

    const/4 p1, 0x0

    new-array p1, p1, [Ljava/lang/Void;

    invoke-virtual {v0, p1}, Lnet/gogame/gopay/sdk/iab/at;->execute([Ljava/lang/Object;)Landroid/os/AsyncTask;

    move-result-object p1

    iput-object p1, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->u:Landroid/os/AsyncTask;

    return-void
.end method

.method private a(Lnet/gogame/gopay/sdk/iab/bq;Z)V
    .locals 1

    new-instance v0, Lnet/gogame/gopay/sdk/iab/bk;

    invoke-direct {v0, p0, p2, p1}, Lnet/gogame/gopay/sdk/iab/bk;-><init>(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;ZLnet/gogame/gopay/sdk/iab/bq;)V

    const/4 p1, 0x0

    new-array p1, p1, [Ljava/lang/Void;

    invoke-virtual {v0, p1}, Lnet/gogame/gopay/sdk/iab/bk;->execute([Ljava/lang/Object;)Landroid/os/AsyncTask;

    return-void
.end method

.method private a(Lnet/gogame/gopay/sdk/f;)Z
    .locals 3

    if-eqz p1, :cond_0

    iget-boolean v0, p1, Lnet/gogame/gopay/sdk/f;->b:Z

    if-nez v0, :cond_0

    invoke-direct {p0}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->b()V

    new-instance v0, Landroid/app/AlertDialog$Builder;

    invoke-direct {v0, p0}, Landroid/app/AlertDialog$Builder;-><init>(Landroid/content/Context;)V

    new-instance v1, Ljava/lang/StringBuilder;

    const-string v2, "Error("

    invoke-direct {v1, v2}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    iget v2, p1, Lnet/gogame/gopay/sdk/f;->a:I

    invoke-static {v2}, Ljava/lang/String;->valueOf(I)Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, ")"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Landroid/app/AlertDialog$Builder;->setTitle(Ljava/lang/CharSequence;)Landroid/app/AlertDialog$Builder;

    move-result-object v0

    iget-object v1, p1, Lnet/gogame/gopay/sdk/f;->c:Ljava/lang/String;

    invoke-virtual {v0, v1}, Landroid/app/AlertDialog$Builder;->setMessage(Ljava/lang/CharSequence;)Landroid/app/AlertDialog$Builder;

    move-result-object v0

    const-string v1, "Dismiss"

    new-instance v2, Lnet/gogame/gopay/sdk/iab/bc;

    invoke-direct {v2, p0, p1}, Lnet/gogame/gopay/sdk/iab/bc;-><init>(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;Lnet/gogame/gopay/sdk/f;)V

    invoke-virtual {v0, v1, v2}, Landroid/app/AlertDialog$Builder;->setNegativeButton(Ljava/lang/CharSequence;Landroid/content/DialogInterface$OnClickListener;)Landroid/app/AlertDialog$Builder;

    move-result-object v0

    new-instance v1, Lnet/gogame/gopay/sdk/iab/bb;

    invoke-direct {v1, p0, p1}, Lnet/gogame/gopay/sdk/iab/bb;-><init>(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;Lnet/gogame/gopay/sdk/f;)V

    invoke-virtual {v0, v1}, Landroid/app/AlertDialog$Builder;->setOnCancelListener(Landroid/content/DialogInterface$OnCancelListener;)Landroid/app/AlertDialog$Builder;

    move-result-object p1

    invoke-virtual {p1}, Landroid/app/AlertDialog$Builder;->show()Landroid/app/AlertDialog;

    const/4 p1, 0x1

    return p1

    :cond_0
    const/4 p1, 0x0

    return p1
.end method

.method static synthetic a(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;Lnet/gogame/gopay/sdk/f;)Z
    .locals 0

    invoke-direct {p0, p1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->a(Lnet/gogame/gopay/sdk/f;)Z

    move-result p0

    return p0
.end method

.method static synthetic a(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;Z)Z
    .locals 0

    iput-boolean p1, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->K:Z

    return p1
.end method

.method static synthetic b(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;I)I
    .locals 0

    iput p1, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->Q:I

    return p1
.end method

.method static synthetic b(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Landroid/content/SharedPreferences;
    .locals 0

    iget-object p0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->t:Landroid/content/SharedPreferences;

    return-object p0
.end method

.method private b()V
    .locals 2

    invoke-direct {p0}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->c()V

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->s:Landroid/widget/ProgressBar;

    if-eqz v0, :cond_1

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->s:Landroid/widget/ProgressBar;

    invoke-virtual {v0}, Landroid/widget/ProgressBar;->isShown()Z

    move-result v0

    if-nez v0, :cond_0

    goto :goto_0

    :cond_0
    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->v:Landroid/os/Handler;

    new-instance v1, Lnet/gogame/gopay/sdk/iab/bn;

    invoke-direct {v1, p0}, Lnet/gogame/gopay/sdk/iab/bn;-><init>(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)V

    invoke-virtual {v0, v1}, Landroid/os/Handler;->post(Ljava/lang/Runnable;)Z

    :cond_1
    :goto_0
    return-void
.end method

.method static synthetic b(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;ILjava/lang/String;)V
    .locals 1

    const/4 v0, 0x0

    invoke-static {p1, v0, p2}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->a(ILnet/gogame/gopay/sdk/iab/br;Ljava/lang/String;)Landroid/content/Intent;

    move-result-object p1

    const/4 p2, -0x1

    invoke-virtual {p0, p2, p1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->setResult(ILandroid/content/Intent;)V

    invoke-virtual {p0}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->finish()V

    return-void
.end method

.method static synthetic b(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;Ljava/lang/String;)V
    .locals 3

    const/16 v0, 0x9

    invoke-virtual {p1, v0}, Ljava/lang/String;->substring(I)Ljava/lang/String;

    move-result-object p1

    :try_start_0
    new-instance v0, Lnet/gogame/gopay/sdk/iab/br;

    iget-object v1, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->e:Ljava/lang/String;

    invoke-virtual {p0}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->getPackageName()Ljava/lang/String;

    move-result-object v2

    invoke-direct {v0, v1, p1, v2}, Lnet/gogame/gopay/sdk/iab/br;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    iget p1, v0, Lnet/gogame/gopay/sdk/iab/br;->a:I

    const/4 v1, 0x1

    if-nez p1, :cond_0

    const/4 p1, 0x1

    goto :goto_0

    :cond_0
    const/4 p1, 0x0

    :goto_0
    if-eqz p1, :cond_1

    const/4 v1, -0x1

    :cond_1
    iget p1, v0, Lnet/gogame/gopay/sdk/iab/br;->a:I

    const/4 v2, 0x0

    invoke-static {p1, v0, v2}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->a(ILnet/gogame/gopay/sdk/iab/br;Ljava/lang/String;)Landroid/content/Intent;

    move-result-object p1

    invoke-virtual {p0, v1, p1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->setResult(ILandroid/content/Intent;)V

    invoke-virtual {p0}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->finish()V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    return-void

    :catch_0
    move-exception p1

    const/16 v0, -0x3ea

    new-instance v1, Ljava/lang/StringBuilder;

    const-string v2, "Something went wrong!\nException: "

    invoke-direct {v1, v2}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    invoke-virtual {p1}, Ljava/lang/Exception;->getLocalizedMessage()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-direct {p0, v0, p1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->a(ILjava/lang/String;)V

    return-void
.end method

.method static synthetic b(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;Z)V
    .locals 2

    iput-boolean p1, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->d:Z

    if-eqz p1, :cond_4

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->F:Landroid/widget/Spinner;

    const v0, 0x3ecccccd    # 0.4f

    if-eqz p1, :cond_0

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->F:Landroid/widget/Spinner;

    invoke-virtual {p1, v0}, Landroid/widget/Spinner;->setAlpha(F)V

    :cond_0
    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->G:Landroid/widget/Spinner;

    if-eqz p1, :cond_1

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->G:Landroid/widget/Spinner;

    invoke-virtual {p1, v0}, Landroid/widget/Spinner;->setAlpha(F)V

    :cond_1
    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->z:Landroid/widget/Button;

    if-eqz p1, :cond_2

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->z:Landroid/widget/Button;

    invoke-virtual {p1, v0}, Landroid/widget/Button;->setAlpha(F)V

    :cond_2
    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->D:Lnet/gogame/gopay/sdk/iab/bs;

    if-eqz p1, :cond_3

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->D:Lnet/gogame/gopay/sdk/iab/bs;

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->E:Lnet/gogame/gopay/sdk/iab/a;

    invoke-virtual {p1, v0}, Lnet/gogame/gopay/sdk/iab/bs;->a(Lnet/gogame/gopay/sdk/iab/a;)V

    :cond_3
    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->E:Lnet/gogame/gopay/sdk/iab/a;

    invoke-direct {p0, p1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->a(Lnet/gogame/gopay/sdk/iab/a;)V

    return-void

    :cond_4
    iget-boolean p1, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->J:Z

    if-eqz p1, :cond_7

    const-string p1, "welcomePage"

    invoke-direct {p0, p1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->a(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->y:Landroid/webkit/WebView;

    invoke-virtual {v0}, Landroid/webkit/WebView;->getUrl()Ljava/lang/String;

    move-result-object v0

    const/4 v1, 0x1

    if-eqz v0, :cond_6

    invoke-virtual {v0, p1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-nez v0, :cond_5

    goto :goto_0

    :cond_5
    invoke-direct {p0}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->b()V

    iput-boolean v1, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->c:Z

    const/4 p1, 0x0

    iput-boolean p1, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->I:Z

    goto :goto_1

    :cond_6
    :goto_0
    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->y:Landroid/webkit/WebView;

    invoke-virtual {v0}, Landroid/webkit/WebView;->stopLoading()V

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->y:Landroid/webkit/WebView;

    invoke-virtual {v0, v1}, Landroid/webkit/WebView;->clearCache(Z)V

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->y:Landroid/webkit/WebView;

    invoke-virtual {v0, p1}, Landroid/webkit/WebView;->loadUrl(Ljava/lang/String;)V

    :cond_7
    :goto_1
    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->F:Landroid/widget/Spinner;

    const/high16 v0, 0x3f800000    # 1.0f

    if-eqz p1, :cond_8

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->F:Landroid/widget/Spinner;

    invoke-virtual {p1, v0}, Landroid/widget/Spinner;->setAlpha(F)V

    :cond_8
    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->G:Landroid/widget/Spinner;

    if-eqz p1, :cond_9

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->G:Landroid/widget/Spinner;

    invoke-virtual {p1, v0}, Landroid/widget/Spinner;->setAlpha(F)V

    :cond_9
    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->z:Landroid/widget/Button;

    if-eqz p1, :cond_a

    iget-object p0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->z:Landroid/widget/Button;

    invoke-virtual {p0, v0}, Landroid/widget/Button;->setAlpha(F)V

    :cond_a
    return-void
.end method

.method private c()V
    .locals 2

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->v:Landroid/os/Handler;

    iget-object v1, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->w:Ljava/lang/Runnable;

    invoke-virtual {v0, v1}, Landroid/os/Handler;->removeCallbacks(Ljava/lang/Runnable;)V

    const/4 v0, 0x0

    iput-object v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->w:Ljava/lang/Runnable;

    return-void
.end method

.method static synthetic c(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)V
    .locals 2

    new-instance v0, Lnet/gogame/gopay/sdk/iab/bi;

    invoke-direct {v0, p0}, Lnet/gogame/gopay/sdk/iab/bi;-><init>(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)V

    const/4 v1, 0x0

    invoke-direct {p0, v0, v1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->a(Lnet/gogame/gopay/sdk/iab/bq;Z)V

    return-void
.end method

.method static synthetic c(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;I)V
    .locals 2

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->D:Lnet/gogame/gopay/sdk/iab/bs;

    iget-object v1, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->C:Lnet/gogame/gopay/sdk/iab/i;

    invoke-virtual {v1, p1}, Lnet/gogame/gopay/sdk/iab/i;->getItem(I)Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lnet/gogame/gopay/sdk/iab/a;

    invoke-virtual {v0, v1}, Lnet/gogame/gopay/sdk/iab/bs;->a(Lnet/gogame/gopay/sdk/iab/a;)V

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->C:Lnet/gogame/gopay/sdk/iab/i;

    iput p1, v0, Lnet/gogame/gopay/sdk/iab/i;->e:I

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->C:Lnet/gogame/gopay/sdk/iab/i;

    invoke-virtual {v0, p1}, Lnet/gogame/gopay/sdk/iab/i;->getItem(I)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Lnet/gogame/gopay/sdk/iab/a;

    invoke-direct {p0, p1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->a(Lnet/gogame/gopay/sdk/iab/a;)V

    return-void
.end method

.method static synthetic c(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;Z)Z
    .locals 0

    iput-boolean p1, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->L:Z

    return p1
.end method

.method static synthetic d(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Ljava/lang/String;
    .locals 0

    iget-object p0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->g:Ljava/lang/String;

    return-object p0
.end method

.method static synthetic d(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;Z)Z
    .locals 0

    iput-boolean p1, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->J:Z

    return p1
.end method

.method static synthetic e(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Ljava/lang/String;
    .locals 0

    iget-object p0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->f:Ljava/lang/String;

    return-object p0
.end method

.method static synthetic e(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;Z)Z
    .locals 0

    iput-boolean p1, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->c:Z

    return p1
.end method

.method static synthetic f(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Ljava/lang/String;
    .locals 0

    iget-object p0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->e:Ljava/lang/String;

    return-object p0
.end method

.method static synthetic g(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)I
    .locals 0

    iget p0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->M:I

    return p0
.end method

.method static synthetic h(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)I
    .locals 0

    iget p0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->N:I

    return p0
.end method

.method static synthetic i(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)I
    .locals 2

    iget v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->M:I

    add-int/lit8 v1, v0, 0x1

    iput v1, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->M:I

    return v0
.end method

.method static synthetic j(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)I
    .locals 1

    const/4 v0, 0x0

    iput v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->M:I

    return v0
.end method

.method static synthetic k(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Landroid/os/Handler;
    .locals 0

    iget-object p0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->v:Landroid/os/Handler;

    return-object p0
.end method

.method static synthetic l(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Landroid/widget/ProgressBar;
    .locals 0

    iget-object p0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->s:Landroid/widget/ProgressBar;

    return-object p0
.end method

.method static synthetic m(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)V
    .locals 6

    iget-boolean v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->J:Z

    const/16 v1, -0x3ed

    const/4 v2, 0x0

    const/4 v3, 0x0

    if-nez v0, :cond_0

    iget-boolean v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->d:Z

    if-eqz v0, :cond_1

    :cond_0
    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->i:Lnet/gogame/gopay/sdk/iab/br;

    if-eqz v0, :cond_3

    :cond_1
    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->i:Lnet/gogame/gopay/sdk/iab/br;

    if-eqz v0, :cond_3

    iget-boolean v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->K:Z

    if-nez v0, :cond_2

    goto :goto_0

    :cond_2
    const/4 v0, -0x1

    :try_start_0
    iget-object v4, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->i:Lnet/gogame/gopay/sdk/iab/br;

    iget v4, v4, Lnet/gogame/gopay/sdk/iab/br;->a:I

    iget-object v5, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->i:Lnet/gogame/gopay/sdk/iab/br;

    invoke-static {v4, v5, v3}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->a(ILnet/gogame/gopay/sdk/iab/br;Ljava/lang/String;)Landroid/content/Intent;

    move-result-object v4

    invoke-virtual {p0, v0, v4}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->setResult(ILandroid/content/Intent;)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_1

    :catch_0
    :cond_3
    :goto_0
    const-string v0, "User cancelled"

    invoke-static {v1, v3, v0}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->a(ILnet/gogame/gopay/sdk/iab/br;Ljava/lang/String;)Landroid/content/Intent;

    move-result-object v0

    invoke-virtual {p0, v2, v0}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->setResult(ILandroid/content/Intent;)V

    :goto_1
    invoke-virtual {p0}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->finish()V

    return-void
.end method

.method static synthetic n(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Z
    .locals 1

    const/4 v0, 0x0

    iput-boolean v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->I:Z

    return v0
.end method

.method static synthetic o(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Z
    .locals 0

    iget-boolean p0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->d:Z

    return p0
.end method

.method static synthetic p(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Landroid/widget/Spinner;
    .locals 0

    iget-object p0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->G:Landroid/widget/Spinner;

    return-object p0
.end method

.method static synthetic q(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Lnet/gogame/gopay/sdk/iab/b;
    .locals 0

    iget-object p0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->B:Lnet/gogame/gopay/sdk/iab/b;

    return-object p0
.end method

.method static synthetic r(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)I
    .locals 0

    iget p0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->P:I

    return p0
.end method

.method static synthetic s(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Z
    .locals 0

    iget-boolean p0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->K:Z

    return p0
.end method

.method static synthetic t(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Z
    .locals 0

    iget-boolean p0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->L:Z

    return p0
.end method

.method static synthetic u(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Z
    .locals 0

    iget-boolean p0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->J:Z

    return p0
.end method

.method static synthetic v(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Landroid/widget/Spinner;
    .locals 0

    iget-object p0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->F:Landroid/widget/Spinner;

    return-object p0
.end method

.method static synthetic w(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)I
    .locals 0

    iget p0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->Q:I

    return p0
.end method

.method static synthetic x(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Landroid/widget/Button;
    .locals 0

    iget-object p0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->z:Landroid/widget/Button;

    return-object p0
.end method

.method static synthetic y(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Lnet/gogame/gopay/sdk/iab/i;
    .locals 0

    iget-object p0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->C:Lnet/gogame/gopay/sdk/iab/i;

    return-object p0
.end method

.method static synthetic z(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Lnet/gogame/gopay/sdk/iab/bs;
    .locals 0

    iget-object p0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->D:Lnet/gogame/gopay/sdk/iab/bs;

    return-object p0
.end method


# virtual methods
.method public finish()V
    .locals 2

    invoke-direct {p0}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->b()V

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->v:Landroid/os/Handler;

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Landroid/os/Handler;->removeCallbacksAndMessages(Ljava/lang/Object;)V

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->u:Landroid/os/AsyncTask;

    if-eqz v0, :cond_0

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->u:Landroid/os/AsyncTask;

    const/4 v1, 0x1

    invoke-virtual {v0, v1}, Landroid/os/AsyncTask;->cancel(Z)Z

    :cond_0
    invoke-super {p0}, Landroid/app/Activity;->finish()V

    return-void
.end method

.method protected onActivityResult(IILandroid/content/Intent;)V
    .locals 0

    invoke-super {p0, p1, p2, p3}, Landroid/app/Activity;->onActivityResult(IILandroid/content/Intent;)V

    return-void
.end method

.method public onBackPressed()V
    .locals 6

    iget-boolean v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->c:Z

    if-nez v0, :cond_0

    return-void

    :cond_0
    iget-boolean v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->J:Z

    const/16 v1, -0x3ed

    const/4 v2, 0x0

    const/4 v3, 0x0

    if-nez v0, :cond_1

    iget-boolean v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->d:Z

    if-eqz v0, :cond_2

    :cond_1
    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->i:Lnet/gogame/gopay/sdk/iab/br;

    if-eqz v0, :cond_4

    :cond_2
    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->i:Lnet/gogame/gopay/sdk/iab/br;

    if-eqz v0, :cond_4

    iget-boolean v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->K:Z

    if-nez v0, :cond_3

    goto :goto_0

    :cond_3
    const/4 v0, -0x1

    :try_start_0
    iget-object v4, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->i:Lnet/gogame/gopay/sdk/iab/br;

    iget v4, v4, Lnet/gogame/gopay/sdk/iab/br;->a:I

    iget-object v5, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->i:Lnet/gogame/gopay/sdk/iab/br;

    invoke-static {v4, v5, v3}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->a(ILnet/gogame/gopay/sdk/iab/br;Ljava/lang/String;)Landroid/content/Intent;

    move-result-object v4

    invoke-virtual {p0, v0, v4}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->setResult(ILandroid/content/Intent;)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_1

    :catch_0
    :cond_4
    :goto_0
    const-string v0, "User cancelled"

    invoke-static {v1, v3, v0}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->a(ILnet/gogame/gopay/sdk/iab/br;Ljava/lang/String;)Landroid/content/Intent;

    move-result-object v0

    invoke-virtual {p0, v2, v0}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->setResult(ILandroid/content/Intent;)V

    :goto_1
    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->v:Landroid/os/Handler;

    invoke-virtual {v0, v3}, Landroid/os/Handler;->removeCallbacksAndMessages(Ljava/lang/Object;)V

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->u:Landroid/os/AsyncTask;

    if-eqz v0, :cond_5

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->u:Landroid/os/AsyncTask;

    const/4 v1, 0x1

    invoke-virtual {v0, v1}, Landroid/os/AsyncTask;->cancel(Z)Z

    iput-object v3, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->u:Landroid/os/AsyncTask;

    :cond_5
    invoke-super {p0}, Landroid/app/Activity;->onBackPressed()V

    return-void
.end method

.method protected onCreate(Landroid/os/Bundle;)V
    .locals 4

    invoke-super {p0, p1}, Landroid/app/Activity;->onCreate(Landroid/os/Bundle;)V

    const/4 p1, 0x1

    invoke-virtual {p0, p1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->requestWindowFeature(I)Z

    invoke-virtual {p0}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->getFilesDir()Ljava/io/File;

    move-result-object v0

    invoke-virtual {v0}, Ljava/io/File;->getPath()Ljava/lang/String;

    move-result-object v0

    invoke-static {v0}, Lnet/gogame/gopay/sdk/support/m;->a(Ljava/lang/String;)V

    const-string v0, "_config_"

    const/4 v1, 0x0

    invoke-virtual {p0, v0, v1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->getSharedPreferences(Ljava/lang/String;I)Landroid/content/SharedPreferences;

    move-result-object v0

    iput-object v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->t:Landroid/content/SharedPreferences;

    new-instance v0, Landroid/os/Handler;

    invoke-direct {v0}, Landroid/os/Handler;-><init>()V

    iput-object v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->v:Landroid/os/Handler;

    invoke-virtual {p0}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->getIntent()Landroid/content/Intent;

    move-result-object v0

    invoke-virtual {v0}, Landroid/content/Intent;->getExtras()Landroid/os/Bundle;

    move-result-object v0

    const-string v2, "gid"

    invoke-virtual {v0, v2}, Landroid/os/Bundle;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    iput-object v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->g:Ljava/lang/String;

    invoke-virtual {p0}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->getIntent()Landroid/content/Intent;

    move-result-object v0

    invoke-virtual {v0}, Landroid/content/Intent;->getExtras()Landroid/os/Bundle;

    move-result-object v0

    const-string v2, "guid"

    invoke-virtual {v0, v2}, Landroid/os/Bundle;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    iput-object v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->f:Ljava/lang/String;

    invoke-virtual {p0}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->getIntent()Landroid/content/Intent;

    move-result-object v0

    invoke-virtual {v0}, Landroid/content/Intent;->getExtras()Landroid/os/Bundle;

    move-result-object v0

    const-string v2, "sku"

    invoke-virtual {v0, v2}, Landroid/os/Bundle;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    iput-object v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->e:Ljava/lang/String;

    invoke-virtual {p0}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->getIntent()Landroid/content/Intent;

    move-result-object v0

    invoke-virtual {v0}, Landroid/content/Intent;->getExtras()Landroid/os/Bundle;

    move-result-object v0

    const-string v2, "payload"

    invoke-virtual {v0, v2}, Landroid/os/Bundle;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    iput-object v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->h:Ljava/lang/String;

    invoke-virtual {p0}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->getIntent()Landroid/content/Intent;

    move-result-object v0

    invoke-virtual {v0}, Landroid/content/Intent;->getExtras()Landroid/os/Bundle;

    move-result-object v0

    const-string v2, "json"

    invoke-virtual {v0, v2}, Landroid/os/Bundle;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    if-eqz v0, :cond_0

    :try_start_0
    new-instance v2, Lorg/onepf/oms/appstore/googleUtils/SkuDetails;

    invoke-direct {v2, v0}, Lorg/onepf/oms/appstore/googleUtils/SkuDetails;-><init>(Ljava/lang/String;)V

    iput-object v2, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->r:Lorg/onepf/oms/appstore/googleUtils/SkuDetails;

    new-instance v2, Lorg/json/JSONObject;

    invoke-direct {v2, v0}, Lorg/json/JSONObject;-><init>(Ljava/lang/String;)V

    const-string v0, "price_amount_micros"

    invoke-virtual {v2, v0}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    iput-object v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->j:Ljava/lang/String;

    const-string v0, "price_currency_code"

    invoke-virtual {v2, v0}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    iput-object v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->k:Ljava/lang/String;
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    :catch_0
    :cond_0
    new-instance v0, Landroid/widget/FrameLayout;

    invoke-direct {v0, p0}, Landroid/widget/FrameLayout;-><init>(Landroid/content/Context;)V

    invoke-virtual {v0, v1}, Landroid/widget/FrameLayout;->setBackgroundColor(I)V

    new-instance v2, Landroid/view/ViewGroup$LayoutParams;

    const/4 v3, -0x1

    invoke-direct {v2, v3, v3}, Landroid/view/ViewGroup$LayoutParams;-><init>(II)V

    invoke-virtual {p0, v0, v2}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->setContentView(Landroid/view/View;Landroid/view/ViewGroup$LayoutParams;)V

    new-instance v2, Landroid/widget/RelativeLayout;

    invoke-direct {v2, p0}, Landroid/widget/RelativeLayout;-><init>(Landroid/content/Context;)V

    iput-object v2, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->a:Landroid/widget/RelativeLayout;

    iget-object v2, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->a:Landroid/widget/RelativeLayout;

    invoke-virtual {v2, v1}, Landroid/widget/RelativeLayout;->setBackgroundColor(I)V

    iget-object v1, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->a:Landroid/widget/RelativeLayout;

    new-instance v2, Landroid/widget/FrameLayout$LayoutParams;

    invoke-direct {v2, v3, v3}, Landroid/widget/FrameLayout$LayoutParams;-><init>(II)V

    invoke-virtual {v0, v1, v2}, Landroid/widget/FrameLayout;->addView(Landroid/view/View;Landroid/view/ViewGroup$LayoutParams;)V

    new-instance v0, Landroid/widget/ProgressBar;

    invoke-direct {v0, p0}, Landroid/widget/ProgressBar;-><init>(Landroid/content/Context;)V

    iput-object v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->s:Landroid/widget/ProgressBar;

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->s:Landroid/widget/ProgressBar;

    invoke-virtual {v0, p1}, Landroid/widget/ProgressBar;->setIndeterminate(Z)V

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->s:Landroid/widget/ProgressBar;

    const/4 v0, 0x4

    invoke-virtual {p1, v0}, Landroid/widget/ProgressBar;->setVisibility(I)V

    new-instance p1, Landroid/widget/RelativeLayout$LayoutParams;

    const/4 v0, -0x2

    invoke-direct {p1, v0, v0}, Landroid/widget/RelativeLayout$LayoutParams;-><init>(II)V

    const/16 v0, 0xd

    invoke-virtual {p1, v0}, Landroid/widget/RelativeLayout$LayoutParams;->addRule(I)V

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->a:Landroid/widget/RelativeLayout;

    iget-object v1, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->s:Landroid/widget/ProgressBar;

    invoke-virtual {v0, v1, p1}, Landroid/widget/RelativeLayout;->addView(Landroid/view/View;Landroid/view/ViewGroup$LayoutParams;)V

    invoke-direct {p0}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->a()V

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->v:Landroid/os/Handler;

    new-instance v0, Lnet/gogame/gopay/sdk/iab/am;

    invoke-direct {v0, p0}, Lnet/gogame/gopay/sdk/iab/am;-><init>(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)V

    invoke-virtual {p1, v0}, Landroid/os/Handler;->post(Ljava/lang/Runnable;)Z

    return-void
.end method

.method protected onResume()V
    .locals 0

    invoke-super {p0}, Landroid/app/Activity;->onResume()V

    invoke-static {p0}, Lnet/gogame/gopay/sdk/support/DisplayUtils;->lockOrientation(Landroid/app/Activity;)V

    return-void
.end method
