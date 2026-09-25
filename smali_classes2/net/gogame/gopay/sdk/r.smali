.class final Lnet/gogame/gopay/sdk/r;
.super Landroid/os/AsyncTask;


# instance fields
.field final synthetic a:Lnet/gogame/gopay/sdk/StoreActivity;


# direct methods
.method constructor <init>(Lnet/gogame/gopay/sdk/StoreActivity;)V
    .locals 0

    iput-object p1, p0, Lnet/gogame/gopay/sdk/r;->a:Lnet/gogame/gopay/sdk/StoreActivity;

    invoke-direct {p0}, Landroid/os/AsyncTask;-><init>()V

    return-void
.end method


# virtual methods
.method protected final synthetic doInBackground([Ljava/lang/Object;)Ljava/lang/Object;
    .locals 0

    iget-object p1, p0, Lnet/gogame/gopay/sdk/r;->a:Lnet/gogame/gopay/sdk/StoreActivity;

    invoke-static {p1}, Lnet/gogame/gopay/sdk/StoreActivity;->d(Lnet/gogame/gopay/sdk/StoreActivity;)V

    const/4 p1, 0x0

    return-object p1
.end method
