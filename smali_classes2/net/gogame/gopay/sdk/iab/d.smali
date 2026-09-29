.class final Lnet/gogame/gopay/sdk/iab/d;
.super Landroid/os/AsyncTask;


# instance fields
.field final synthetic a:Lnet/gogame/gopay/sdk/iab/GoPayInAppBillingService;


# direct methods
.method constructor <init>(Lnet/gogame/gopay/sdk/iab/GoPayInAppBillingService;)V
    .locals 0

    iput-object p1, p0, Lnet/gogame/gopay/sdk/iab/d;->a:Lnet/gogame/gopay/sdk/iab/GoPayInAppBillingService;

    invoke-direct {p0}, Landroid/os/AsyncTask;-><init>()V

    return-void
.end method


# virtual methods
.method protected final synthetic doInBackground([Ljava/lang/Object;)Ljava/lang/Object;
    .locals 1

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/d;->a:Lnet/gogame/gopay/sdk/iab/GoPayInAppBillingService;

    invoke-static {p1}, Lnet/gogame/gopay/sdk/iab/GoPayInAppBillingService;->a(Lnet/gogame/gopay/sdk/iab/GoPayInAppBillingService;)Ljava/lang/String;

    move-result-object p1

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/d;->a:Lnet/gogame/gopay/sdk/iab/GoPayInAppBillingService;

    invoke-static {v0}, Lnet/gogame/gopay/sdk/iab/GoPayInAppBillingService;->b(Lnet/gogame/gopay/sdk/iab/GoPayInAppBillingService;)Ljava/lang/String;

    move-result-object v0

    invoke-static {p1, v0}, Lnet/gogame/gopay/sdk/j;->a(Ljava/lang/String;Ljava/lang/String;)V

    const/4 p1, 0x0

    return-object p1
.end method
