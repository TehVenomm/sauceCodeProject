.class final Lcom/zopim/android/sdk/api/n;
.super Landroid/os/AsyncTask;


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "Landroid/os/AsyncTask<",
        "Landroid/util/Pair<",
        "Ljava/net/URL;",
        "Ljava/io/File;",
        ">;",
        "Ljava/lang/Void;",
        "Ljava/io/File;",
        ">;"
    }
.end annotation


# static fields
.field private static final b:Ljava/lang/String; = "n"


# instance fields
.field a:Lcom/zopim/android/sdk/api/u;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Lcom/zopim/android/sdk/api/u<",
            "Ljava/io/File;",
            ">;"
        }
    .end annotation
.end field

.field private c:Ljava/io/File;

.field private d:Lcom/zopim/android/sdk/api/ErrorResponse;


# direct methods
.method static constructor <clinit>()V
    .locals 0

    return-void
.end method

.method constructor <init>()V
    .locals 0

    invoke-direct {p0}, Landroid/os/AsyncTask;-><init>()V

    return-void
.end method

.method static synthetic a(Lcom/zopim/android/sdk/api/n;Lcom/zopim/android/sdk/api/ErrorResponse;)Lcom/zopim/android/sdk/api/ErrorResponse;
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/api/n;->d:Lcom/zopim/android/sdk/api/ErrorResponse;

    return-object p1
.end method

.method static synthetic a(Lcom/zopim/android/sdk/api/n;Ljava/io/File;)Ljava/io/File;
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/api/n;->c:Ljava/io/File;

    return-object p1
.end method

.method static synthetic a()Ljava/lang/String;
    .locals 1

    sget-object v0, Lcom/zopim/android/sdk/api/n;->b:Ljava/lang/String;

    return-object v0
.end method


# virtual methods
.method protected varargs a([Landroid/util/Pair;)Ljava/io/File;
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "([",
            "Landroid/util/Pair<",
            "Ljava/net/URL;",
            "Ljava/io/File;",
            ">;)",
            "Ljava/io/File;"
        }
    .end annotation

    if-eqz p1, :cond_1

    array-length v0, p1

    if-nez v0, :cond_0

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    aget-object v1, p1, v0

    iget-object v1, v1, Landroid/util/Pair;->first:Ljava/lang/Object;

    check-cast v1, Ljava/net/URL;

    aget-object p1, p1, v0

    iget-object p1, p1, Landroid/util/Pair;->second:Ljava/lang/Object;

    check-cast p1, Ljava/io/File;

    new-instance v0, Lcom/zopim/android/sdk/api/j;

    invoke-direct {v0}, Lcom/zopim/android/sdk/api/j;-><init>()V

    new-instance v2, Lcom/zopim/android/sdk/api/o;

    invoke-direct {v2, p0}, Lcom/zopim/android/sdk/api/o;-><init>(Lcom/zopim/android/sdk/api/n;)V

    invoke-virtual {v0, v2}, Lcom/zopim/android/sdk/api/j;->a(Lcom/zopim/android/sdk/api/u;)V

    invoke-virtual {v0, v1, p1}, Lcom/zopim/android/sdk/api/j;->a(Ljava/net/URL;Ljava/io/File;)V

    iget-object p1, p0, Lcom/zopim/android/sdk/api/n;->c:Ljava/io/File;

    return-object p1

    :cond_1
    :goto_0
    sget-object p1, Lcom/zopim/android/sdk/api/n;->b:Ljava/lang/String;

    const-string v0, "File - URL pair validation failed. Will not start file upload."

    invoke-static {p1, v0}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    const/4 p1, 0x0

    return-object p1
.end method

.method public a(Lcom/zopim/android/sdk/api/u;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/zopim/android/sdk/api/u<",
            "Ljava/io/File;",
            ">;)V"
        }
    .end annotation

    iput-object p1, p0, Lcom/zopim/android/sdk/api/n;->a:Lcom/zopim/android/sdk/api/u;

    return-void
.end method

.method protected a(Ljava/io/File;)V
    .locals 1

    invoke-super {p0, p1}, Landroid/os/AsyncTask;->onPostExecute(Ljava/lang/Object;)V

    iget-object v0, p0, Lcom/zopim/android/sdk/api/n;->a:Lcom/zopim/android/sdk/api/u;

    if-eqz v0, :cond_1

    iget-object v0, p0, Lcom/zopim/android/sdk/api/n;->d:Lcom/zopim/android/sdk/api/ErrorResponse;

    if-eqz v0, :cond_0

    iget-object p1, p0, Lcom/zopim/android/sdk/api/n;->a:Lcom/zopim/android/sdk/api/u;

    iget-object v0, p0, Lcom/zopim/android/sdk/api/n;->d:Lcom/zopim/android/sdk/api/ErrorResponse;

    invoke-virtual {p1, v0}, Lcom/zopim/android/sdk/api/u;->a(Lcom/zopim/android/sdk/api/ErrorResponse;)V

    goto :goto_0

    :cond_0
    if-eqz p1, :cond_1

    iget-object v0, p0, Lcom/zopim/android/sdk/api/n;->a:Lcom/zopim/android/sdk/api/u;

    invoke-virtual {v0, p1}, Lcom/zopim/android/sdk/api/u;->a(Ljava/lang/Object;)V

    :cond_1
    :goto_0
    return-void
.end method

.method protected synthetic doInBackground([Ljava/lang/Object;)Ljava/lang/Object;
    .locals 0

    check-cast p1, [Landroid/util/Pair;

    invoke-virtual {p0, p1}, Lcom/zopim/android/sdk/api/n;->a([Landroid/util/Pair;)Ljava/io/File;

    move-result-object p1

    return-object p1
.end method

.method protected synthetic onPostExecute(Ljava/lang/Object;)V
    .locals 0

    check-cast p1, Ljava/io/File;

    invoke-virtual {p0, p1}, Lcom/zopim/android/sdk/api/n;->a(Ljava/io/File;)V

    return-void
.end method
