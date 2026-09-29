.class public Lnet/gogame/gopay/sdk/StoreActivity;
.super Landroid/app/Activity;


# instance fields
.field private a:Landroid/os/Handler;

.field private b:Landroid/app/ProgressDialog;

.field private c:Lnet/gogame/gopay/sdk/m;

.field private d:Lnet/gogame/gopay/sdk/d;

.field private e:Lnet/gogame/gopay/sdk/w;


# direct methods
.method public constructor <init>()V
    .locals 0

    invoke-direct {p0}, Landroid/app/Activity;-><init>()V

    return-void
.end method

.method static synthetic a(Lnet/gogame/gopay/sdk/StoreActivity;Landroid/app/ProgressDialog;)Landroid/app/ProgressDialog;
    .locals 0

    iput-object p1, p0, Lnet/gogame/gopay/sdk/StoreActivity;->b:Landroid/app/ProgressDialog;

    return-object p1
.end method

.method static synthetic a(Lnet/gogame/gopay/sdk/StoreActivity;)Lnet/gogame/gopay/sdk/d;
    .locals 0

    iget-object p0, p0, Lnet/gogame/gopay/sdk/StoreActivity;->d:Lnet/gogame/gopay/sdk/d;

    return-object p0
.end method

.method private a()V
    .locals 2

    new-instance v0, Lnet/gogame/gopay/sdk/r;

    invoke-direct {v0, p0}, Lnet/gogame/gopay/sdk/r;-><init>(Lnet/gogame/gopay/sdk/StoreActivity;)V

    const/4 v1, 0x0

    new-array v1, v1, [Ljava/lang/Void;

    invoke-virtual {v0, v1}, Lnet/gogame/gopay/sdk/r;->execute([Ljava/lang/Object;)Landroid/os/AsyncTask;

    return-void
.end method

.method private b()V
    .locals 2

    iget-object v0, p0, Lnet/gogame/gopay/sdk/StoreActivity;->a:Landroid/os/Handler;

    new-instance v1, Lnet/gogame/gopay/sdk/t;

    invoke-direct {v1, p0}, Lnet/gogame/gopay/sdk/t;-><init>(Lnet/gogame/gopay/sdk/StoreActivity;)V

    invoke-virtual {v0, v1}, Landroid/os/Handler;->post(Ljava/lang/Runnable;)Z

    return-void
.end method

.method static synthetic b(Lnet/gogame/gopay/sdk/StoreActivity;)V
    .locals 0

    invoke-direct {p0}, Lnet/gogame/gopay/sdk/StoreActivity;->a()V

    return-void
.end method

.method static synthetic c(Lnet/gogame/gopay/sdk/StoreActivity;)Lnet/gogame/gopay/sdk/w;
    .locals 0

    iget-object p0, p0, Lnet/gogame/gopay/sdk/StoreActivity;->e:Lnet/gogame/gopay/sdk/w;

    return-object p0
.end method

.method static synthetic d(Lnet/gogame/gopay/sdk/StoreActivity;)V
    .locals 3

    iget-object v0, p0, Lnet/gogame/gopay/sdk/StoreActivity;->a:Landroid/os/Handler;

    new-instance v1, Lnet/gogame/gopay/sdk/s;

    invoke-direct {v1, p0}, Lnet/gogame/gopay/sdk/s;-><init>(Lnet/gogame/gopay/sdk/StoreActivity;)V

    invoke-virtual {v0, v1}, Landroid/os/Handler;->post(Ljava/lang/Runnable;)Z

    :try_start_0
    iget-object v0, p0, Lnet/gogame/gopay/sdk/StoreActivity;->c:Lnet/gogame/gopay/sdk/m;

    iget-object v0, v0, Lnet/gogame/gopay/sdk/m;->a:Ljava/lang/String;

    iget-object v1, p0, Lnet/gogame/gopay/sdk/StoreActivity;->c:Lnet/gogame/gopay/sdk/m;

    iget-object v1, v1, Lnet/gogame/gopay/sdk/m;->b:Ljava/lang/String;

    invoke-static {}, Lnet/gogame/gopay/sdk/j;->a()Ljava/lang/String;

    move-result-object v2

    invoke-static {v0, v1, v2}, Lnet/gogame/gopay/sdk/j;->a(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Lnet/gogame/gopay/sdk/h;

    move-result-object v0

    iget-object v1, v0, Lnet/gogame/gopay/sdk/h;->a:Ljava/lang/String;

    invoke-static {v1}, Lnet/gogame/gopay/sdk/j;->a(Ljava/lang/String;)V

    iget-object v1, p0, Lnet/gogame/gopay/sdk/StoreActivity;->a:Landroid/os/Handler;

    new-instance v2, Lnet/gogame/gopay/sdk/q;

    invoke-direct {v2, p0, v0}, Lnet/gogame/gopay/sdk/q;-><init>(Lnet/gogame/gopay/sdk/StoreActivity;Lnet/gogame/gopay/sdk/h;)V

    invoke-virtual {v1, v2}, Landroid/os/Handler;->post(Ljava/lang/Runnable;)Z
    :try_end_0
    .catch Lorg/onepf/oms/appstore/googleUtils/IabException; {:try_start_0 .. :try_end_0} :catch_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    invoke-direct {p0}, Lnet/gogame/gopay/sdk/StoreActivity;->b()V

    return-void

    :catchall_0
    move-exception v0

    invoke-direct {p0}, Lnet/gogame/gopay/sdk/StoreActivity;->b()V

    throw v0

    :catch_0
    invoke-direct {p0}, Lnet/gogame/gopay/sdk/StoreActivity;->b()V

    return-void
.end method

.method static synthetic e(Lnet/gogame/gopay/sdk/StoreActivity;)Landroid/app/ProgressDialog;
    .locals 0

    iget-object p0, p0, Lnet/gogame/gopay/sdk/StoreActivity;->b:Landroid/app/ProgressDialog;

    return-object p0
.end method


# virtual methods
.method protected onCreate(Landroid/os/Bundle;)V
    .locals 6

    invoke-super {p0, p1}, Landroid/app/Activity;->onCreate(Landroid/os/Bundle;)V

    invoke-virtual {p0}, Lnet/gogame/gopay/sdk/StoreActivity;->getFilesDir()Ljava/io/File;

    move-result-object p1

    invoke-virtual {p1}, Ljava/io/File;->getPath()Ljava/lang/String;

    move-result-object p1

    invoke-static {p1}, Lnet/gogame/gopay/sdk/support/m;->a(Ljava/lang/String;)V

    const/4 p1, 0x1

    invoke-virtual {p0, p1}, Lnet/gogame/gopay/sdk/StoreActivity;->requestWindowFeature(I)Z

    const-string v0, "store_title"

    invoke-static {v0}, Lnet/gogame/gopay/sdk/support/s;->a(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p0, v0}, Lnet/gogame/gopay/sdk/StoreActivity;->setTitle(Ljava/lang/CharSequence;)V

    new-instance v0, Landroid/os/Handler;

    invoke-direct {v0}, Landroid/os/Handler;-><init>()V

    iput-object v0, p0, Lnet/gogame/gopay/sdk/StoreActivity;->a:Landroid/os/Handler;

    invoke-virtual {p0}, Lnet/gogame/gopay/sdk/StoreActivity;->getIntent()Landroid/content/Intent;

    move-result-object v0

    invoke-virtual {v0}, Landroid/content/Intent;->getExtras()Landroid/os/Bundle;

    move-result-object v0

    const-string v1, "appId"

    invoke-virtual {v0, v1}, Landroid/os/Bundle;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    const/4 v1, 0x0

    if-eqz v0, :cond_0

    invoke-virtual {p0}, Lnet/gogame/gopay/sdk/StoreActivity;->getIntent()Landroid/content/Intent;

    move-result-object v0

    invoke-virtual {v0}, Landroid/content/Intent;->getExtras()Landroid/os/Bundle;

    move-result-object v0

    const-string v2, "appId"

    invoke-virtual {v0, v2}, Landroid/os/Bundle;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    goto :goto_0

    :cond_0
    move-object v0, v1

    :goto_0
    invoke-virtual {p0}, Lnet/gogame/gopay/sdk/StoreActivity;->getIntent()Landroid/content/Intent;

    move-result-object v2

    invoke-virtual {v2}, Landroid/content/Intent;->getExtras()Landroid/os/Bundle;

    move-result-object v2

    const-string v3, "guid"

    invoke-virtual {v2, v3}, Landroid/os/Bundle;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    if-eqz v2, :cond_1

    invoke-virtual {p0}, Lnet/gogame/gopay/sdk/StoreActivity;->getIntent()Landroid/content/Intent;

    move-result-object v1

    invoke-virtual {v1}, Landroid/content/Intent;->getExtras()Landroid/os/Bundle;

    move-result-object v1

    const-string v2, "guid"

    invoke-virtual {v1, v2}, Landroid/os/Bundle;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    :cond_1
    new-instance v2, Lnet/gogame/gopay/sdk/m;

    invoke-direct {v2, v0, v1}, Lnet/gogame/gopay/sdk/m;-><init>(Ljava/lang/String;Ljava/lang/String;)V

    iput-object v2, p0, Lnet/gogame/gopay/sdk/StoreActivity;->c:Lnet/gogame/gopay/sdk/m;

    new-instance v0, Landroid/widget/LinearLayout;

    invoke-direct {v0, p0}, Landroid/widget/LinearLayout;-><init>(Landroid/content/Context;)V

    invoke-virtual {v0, p1}, Landroid/widget/LinearLayout;->setOrientation(I)V

    new-instance v1, Landroid/widget/RelativeLayout;

    invoke-direct {v1, p0}, Landroid/widget/RelativeLayout;-><init>(Landroid/content/Context;)V

    const/high16 v2, 0x40a00000    # 5.0f

    invoke-static {p0, v2}, Lnet/gogame/gopay/sdk/support/DisplayUtils;->pxFromDp(Landroid/content/Context;F)I

    move-result v3

    const/4 v4, 0x0

    invoke-static {p0, v4}, Lnet/gogame/gopay/sdk/support/DisplayUtils;->pxFromDp(Landroid/content/Context;F)I

    move-result v5

    invoke-static {p0, v2}, Lnet/gogame/gopay/sdk/support/DisplayUtils;->pxFromDp(Landroid/content/Context;F)I

    move-result v2

    invoke-static {p0, v4}, Lnet/gogame/gopay/sdk/support/DisplayUtils;->pxFromDp(Landroid/content/Context;F)I

    move-result v4

    invoke-virtual {v1, v3, v5, v2, v4}, Landroid/widget/RelativeLayout;->setPadding(IIII)V

    const/16 v2, 0xe

    invoke-virtual {v1, v2}, Landroid/widget/RelativeLayout;->setGravity(I)V

    const-string v2, "#87b856"

    invoke-static {v2}, Landroid/graphics/Color;->parseColor(Ljava/lang/String;)I

    move-result v2

    invoke-virtual {v1, v2}, Landroid/widget/RelativeLayout;->setBackgroundColor(I)V

    invoke-virtual {v0, v1}, Landroid/widget/LinearLayout;->addView(Landroid/view/View;)V

    new-instance v2, Landroid/widget/TextView;

    invoke-direct {v2, p0}, Landroid/widget/TextView;-><init>(Landroid/content/Context;)V

    sget-object v3, Landroid/graphics/Typeface;->DEFAULT_BOLD:Landroid/graphics/Typeface;

    invoke-virtual {v2, v3}, Landroid/widget/TextView;->setTypeface(Landroid/graphics/Typeface;)V

    const/4 v3, 0x3

    const/high16 v4, 0x41200000    # 10.0f

    invoke-virtual {v2, v3, v4}, Landroid/widget/TextView;->setTextSize(IF)V

    const/4 v3, -0x1

    invoke-virtual {v2, v3}, Landroid/widget/TextView;->setTextColor(I)V

    new-instance v3, Landroid/widget/RelativeLayout$LayoutParams;

    const/4 v4, -0x2

    invoke-direct {v3, v4, v4}, Landroid/widget/RelativeLayout$LayoutParams;-><init>(II)V

    const/16 v5, 0xf

    invoke-virtual {v3, v5}, Landroid/widget/RelativeLayout$LayoutParams;->addRule(I)V

    invoke-virtual {v2, v3}, Landroid/widget/TextView;->setLayoutParams(Landroid/view/ViewGroup$LayoutParams;)V

    const-string v3, "store_title"

    invoke-static {v3}, Lnet/gogame/gopay/sdk/support/s;->a(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v2, v3}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    invoke-virtual {v1, v2}, Landroid/widget/RelativeLayout;->addView(Landroid/view/View;)V

    new-instance v2, Lnet/gogame/gopay/sdk/d;

    invoke-direct {v2, p0, p1}, Lnet/gogame/gopay/sdk/d;-><init>(Landroid/content/Context;Z)V

    iput-object v2, p0, Lnet/gogame/gopay/sdk/StoreActivity;->d:Lnet/gogame/gopay/sdk/d;

    new-instance v2, Landroid/widget/Spinner;

    invoke-direct {v2, p0}, Landroid/widget/Spinner;-><init>(Landroid/content/Context;)V

    const/4 v3, 0x0

    invoke-virtual {v2, v3}, Landroid/widget/Spinner;->setBackgroundColor(I)V

    iget-object v5, p0, Lnet/gogame/gopay/sdk/StoreActivity;->d:Lnet/gogame/gopay/sdk/d;

    invoke-virtual {v2, v5}, Landroid/widget/Spinner;->setAdapter(Landroid/widget/SpinnerAdapter;)V

    invoke-virtual {v2, v3, v3, v3, v3}, Landroid/widget/Spinner;->setPadding(IIII)V

    new-instance v3, Lnet/gogame/gopay/sdk/o;

    invoke-direct {v3, p0}, Lnet/gogame/gopay/sdk/o;-><init>(Lnet/gogame/gopay/sdk/StoreActivity;)V

    invoke-virtual {v2, v3}, Landroid/widget/Spinner;->setOnItemSelectedListener(Landroid/widget/AdapterView$OnItemSelectedListener;)V

    new-instance v3, Landroid/widget/RelativeLayout$LayoutParams;

    invoke-direct {v3, v4, v4}, Landroid/widget/RelativeLayout$LayoutParams;-><init>(II)V

    const/16 v4, 0xb

    invoke-virtual {v3, v4}, Landroid/widget/RelativeLayout$LayoutParams;->addRule(I)V

    invoke-virtual {v2, v3}, Landroid/widget/Spinner;->setLayoutParams(Landroid/view/ViewGroup$LayoutParams;)V

    invoke-virtual {v1, v2}, Landroid/widget/RelativeLayout;->addView(Landroid/view/View;)V

    invoke-static {}, Lnet/gogame/gopay/sdk/i;->a()Z

    move-result v1

    if-eqz v1, :cond_2

    invoke-virtual {v0, p1}, Landroid/widget/LinearLayout;->setLongClickable(Z)V

    new-instance p1, Lnet/gogame/gopay/sdk/p;

    invoke-direct {p1, p0}, Lnet/gogame/gopay/sdk/p;-><init>(Lnet/gogame/gopay/sdk/StoreActivity;)V

    invoke-virtual {v0, p1}, Landroid/widget/LinearLayout;->setOnLongClickListener(Landroid/view/View$OnLongClickListener;)V

    :cond_2
    new-instance p1, Lnet/gogame/gopay/sdk/w;

    iget-object v1, p0, Lnet/gogame/gopay/sdk/StoreActivity;->c:Lnet/gogame/gopay/sdk/m;

    invoke-direct {p1, p0, v1}, Lnet/gogame/gopay/sdk/w;-><init>(Landroid/app/Activity;Lnet/gogame/gopay/sdk/m;)V

    iput-object p1, p0, Lnet/gogame/gopay/sdk/StoreActivity;->e:Lnet/gogame/gopay/sdk/w;

    iget-object p1, p0, Lnet/gogame/gopay/sdk/StoreActivity;->e:Lnet/gogame/gopay/sdk/w;

    invoke-virtual {v0, p1}, Landroid/widget/LinearLayout;->addView(Landroid/view/View;)V

    invoke-virtual {p0, v0}, Lnet/gogame/gopay/sdk/StoreActivity;->setContentView(Landroid/view/View;)V

    return-void
.end method

.method protected onResume()V
    .locals 0

    invoke-super {p0}, Landroid/app/Activity;->onResume()V

    invoke-direct {p0}, Lnet/gogame/gopay/sdk/StoreActivity;->a()V

    return-void
.end method

.method protected onStop()V
    .locals 1

    invoke-super {p0}, Landroid/app/Activity;->onStop()V

    iget-object v0, p0, Lnet/gogame/gopay/sdk/StoreActivity;->b:Landroid/app/ProgressDialog;

    invoke-virtual {v0}, Landroid/app/ProgressDialog;->dismiss()V

    return-void
.end method
