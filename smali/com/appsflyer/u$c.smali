.class final Lcom/appsflyer/u$c;
.super Landroid/os/AsyncTask;
.source ""


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/appsflyer/u;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x8
    name = "c"
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Landroid/os/AsyncTask<",
        "Ljava/lang/Void;",
        "Ljava/lang/Void;",
        "Ljava/lang/String;",
        ">;"
    }
.end annotation


# instance fields
.field private ˊ:Ljava/lang/String;

.field private final ˋ:Ljava/lang/ref/WeakReference;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/lang/ref/WeakReference<",
            "Landroid/content/Context;",
            ">;"
        }
    .end annotation
.end field


# direct methods
.method constructor <init>(Ljava/lang/ref/WeakReference;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/ref/WeakReference<",
            "Landroid/content/Context;",
            ">;)V"
        }
    .end annotation

    .line 140
    invoke-direct {p0}, Landroid/os/AsyncTask;-><init>()V

    .line 141
    iput-object p1, p0, Lcom/appsflyer/u$c;->ˋ:Ljava/lang/ref/WeakReference;

    return-void
.end method

.method private varargs ˎ()Ljava/lang/String;
    .locals 3

    const/4 v0, 0x0

    .line 154
    :try_start_0
    iget-object v1, p0, Lcom/appsflyer/u$c;->ˊ:Ljava/lang/String;

    if-eqz v1, :cond_0

    .line 155
    iget-object v1, p0, Lcom/appsflyer/u$c;->ˋ:Ljava/lang/ref/WeakReference;

    iget-object v2, p0, Lcom/appsflyer/u$c;->ˊ:Ljava/lang/String;

    invoke-static {v1, v2}, Lcom/appsflyer/u;->ˋ(Ljava/lang/ref/WeakReference;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    move-object v0, v1

    :cond_0
    return-object v0

    :catch_0
    move-exception v1

    const-string v2, "Error registering for uninstall feature"

    .line 159
    invoke-static {v2, v1}, Lcom/appsflyer/AFLogger;->afErrorLog(Ljava/lang/String;Ljava/lang/Throwable;)V

    return-object v0
.end method


# virtual methods
.method protected final synthetic doInBackground([Ljava/lang/Object;)Ljava/lang/Object;
    .locals 0

    .line 136
    invoke-direct {p0}, Lcom/appsflyer/u$c;->ˎ()Ljava/lang/String;

    move-result-object p1

    return-object p1
.end method

.method protected final synthetic onPostExecute(Ljava/lang/Object;)V
    .locals 2

    .line 136
    check-cast p1, Ljava/lang/String;

    .line 3166
    invoke-static {p1}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v0

    if-nez v0, :cond_1

    .line 3168
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v0

    const-string v1, "afUninstallToken"

    invoke-virtual {v0, v1}, Lcom/appsflyer/AppsFlyerProperties;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    .line 3169
    new-instance v1, Lcom/appsflyer/d;

    invoke-direct {v1, p1}, Lcom/appsflyer/d;-><init>(Ljava/lang/String;)V

    if-nez v0, :cond_0

    .line 3171
    iget-object p1, p0, Lcom/appsflyer/u$c;->ˋ:Ljava/lang/ref/WeakReference;

    invoke-virtual {p1}, Ljava/lang/ref/Reference;->get()Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Landroid/content/Context;

    invoke-static {p1, v1}, Lcom/appsflyer/u;->ॱ(Landroid/content/Context;Lcom/appsflyer/d;)V

    return-void

    .line 3173
    :cond_0
    invoke-static {v0}, Lcom/appsflyer/d;->ˊ(Ljava/lang/String;)Lcom/appsflyer/d;

    move-result-object p1

    .line 3176
    invoke-virtual {p1, v1}, Lcom/appsflyer/d;->ˊ(Lcom/appsflyer/d;)Z

    move-result v0

    if-eqz v0, :cond_1

    .line 3178
    iget-object v0, p0, Lcom/appsflyer/u$c;->ˋ:Ljava/lang/ref/WeakReference;

    invoke-virtual {v0}, Ljava/lang/ref/Reference;->get()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroid/content/Context;

    invoke-static {v0, p1}, Lcom/appsflyer/u;->ॱ(Landroid/content/Context;Lcom/appsflyer/d;)V

    :cond_1
    return-void
.end method

.method protected final onPreExecute()V
    .locals 2

    .line 146
    invoke-super {p0}, Landroid/os/AsyncTask;->onPreExecute()V

    .line 147
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v0

    const-string v1, "gcmProjectNumber"

    invoke-virtual {v0, v1}, Lcom/appsflyer/AppsFlyerProperties;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    iput-object v0, p0, Lcom/appsflyer/u$c;->ˊ:Ljava/lang/String;

    return-void
.end method
