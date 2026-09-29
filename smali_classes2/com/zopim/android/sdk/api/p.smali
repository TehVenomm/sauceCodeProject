.class final Lcom/zopim/android/sdk/api/p;
.super Landroid/os/AsyncTask;


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "Landroid/os/AsyncTask<",
        "Landroid/util/Pair<",
        "Ljava/io/File;",
        "Ljava/net/URL;",
        ">;",
        "Ljava/lang/Integer;",
        "Ljava/lang/Void;",
        ">;"
    }
.end annotation


# static fields
.field private static final c:Ljava/lang/String; = "p"


# instance fields
.field a:Lcom/zopim/android/sdk/api/HttpRequest$ProgressListener;

.field b:Lcom/zopim/android/sdk/api/u;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Lcom/zopim/android/sdk/api/u<",
            "Ljava/lang/Void;",
            ">;"
        }
    .end annotation
.end field

.field private d:Lcom/zopim/android/sdk/api/ErrorResponse;

.field private e:Z


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

.method static synthetic a(Lcom/zopim/android/sdk/api/p;Lcom/zopim/android/sdk/api/ErrorResponse;)Lcom/zopim/android/sdk/api/ErrorResponse;
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/api/p;->d:Lcom/zopim/android/sdk/api/ErrorResponse;

    return-object p1
.end method

.method static synthetic a()Ljava/lang/String;
    .locals 1

    sget-object v0, Lcom/zopim/android/sdk/api/p;->c:Ljava/lang/String;

    return-object v0
.end method

.method static synthetic a(Lcom/zopim/android/sdk/api/p;[Ljava/lang/Object;)V
    .locals 0

    invoke-virtual {p0, p1}, Lcom/zopim/android/sdk/api/p;->publishProgress([Ljava/lang/Object;)V

    return-void
.end method

.method static synthetic a(Lcom/zopim/android/sdk/api/p;Z)Z
    .locals 0

    iput-boolean p1, p0, Lcom/zopim/android/sdk/api/p;->e:Z

    return p1
.end method


# virtual methods
.method protected varargs a([Landroid/util/Pair;)Ljava/lang/Void;
    .locals 4
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "([",
            "Landroid/util/Pair<",
            "Ljava/io/File;",
            "Ljava/net/URL;",
            ">;)",
            "Ljava/lang/Void;"
        }
    .end annotation

    const/4 v0, 0x0

    if-eqz p1, :cond_1

    array-length v1, p1

    if-nez v1, :cond_0

    goto :goto_0

    :cond_0
    const/4 v1, 0x0

    aget-object v2, p1, v1

    iget-object v2, v2, Landroid/util/Pair;->first:Ljava/lang/Object;

    check-cast v2, Ljava/io/File;

    aget-object p1, p1, v1

    iget-object p1, p1, Landroid/util/Pair;->second:Ljava/lang/Object;

    check-cast p1, Ljava/net/URL;

    new-instance v1, Lcom/zopim/android/sdk/api/s;

    invoke-direct {v1}, Lcom/zopim/android/sdk/api/s;-><init>()V

    new-instance v3, Lcom/zopim/android/sdk/api/q;

    invoke-direct {v3, p0}, Lcom/zopim/android/sdk/api/q;-><init>(Lcom/zopim/android/sdk/api/p;)V

    invoke-virtual {v1, v3}, Lcom/zopim/android/sdk/api/s;->a(Lcom/zopim/android/sdk/api/u;)V

    new-instance v3, Lcom/zopim/android/sdk/api/r;

    invoke-direct {v3, p0}, Lcom/zopim/android/sdk/api/r;-><init>(Lcom/zopim/android/sdk/api/p;)V

    invoke-virtual {v1, v3}, Lcom/zopim/android/sdk/api/s;->a(Lcom/zopim/android/sdk/api/HttpRequest$ProgressListener;)V

    invoke-virtual {v1, v2, p1}, Lcom/zopim/android/sdk/api/s;->a(Ljava/io/File;Ljava/net/URL;)V

    return-object v0

    :cond_1
    :goto_0
    sget-object p1, Lcom/zopim/android/sdk/api/p;->c:Ljava/lang/String;

    const-string v1, "File - URL pair validation failed. Will not start file upload."

    invoke-static {p1, v1}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    return-object v0
.end method

.method public a(Lcom/zopim/android/sdk/api/u;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/zopim/android/sdk/api/u<",
            "Ljava/lang/Void;",
            ">;)V"
        }
    .end annotation

    iput-object p1, p0, Lcom/zopim/android/sdk/api/p;->b:Lcom/zopim/android/sdk/api/u;

    return-void
.end method

.method protected a(Ljava/lang/Void;)V
    .locals 1

    invoke-super {p0, p1}, Landroid/os/AsyncTask;->onPostExecute(Ljava/lang/Object;)V

    iget-object p1, p0, Lcom/zopim/android/sdk/api/p;->b:Lcom/zopim/android/sdk/api/u;

    if-eqz p1, :cond_1

    iget-object p1, p0, Lcom/zopim/android/sdk/api/p;->d:Lcom/zopim/android/sdk/api/ErrorResponse;

    if-eqz p1, :cond_0

    iget-object p1, p0, Lcom/zopim/android/sdk/api/p;->b:Lcom/zopim/android/sdk/api/u;

    iget-object v0, p0, Lcom/zopim/android/sdk/api/p;->d:Lcom/zopim/android/sdk/api/ErrorResponse;

    invoke-virtual {p1, v0}, Lcom/zopim/android/sdk/api/u;->b(Lcom/zopim/android/sdk/api/ErrorResponse;)V

    goto :goto_0

    :cond_0
    iget-boolean p1, p0, Lcom/zopim/android/sdk/api/p;->e:Z

    if-eqz p1, :cond_1

    iget-object p1, p0, Lcom/zopim/android/sdk/api/p;->b:Lcom/zopim/android/sdk/api/u;

    const/4 v0, 0x0

    invoke-virtual {p1, v0}, Lcom/zopim/android/sdk/api/u;->b(Ljava/lang/Object;)V

    :cond_1
    :goto_0
    return-void
.end method

.method protected varargs a([Ljava/lang/Integer;)V
    .locals 2

    invoke-super {p0, p1}, Landroid/os/AsyncTask;->onProgressUpdate([Ljava/lang/Object;)V

    iget-object v0, p0, Lcom/zopim/android/sdk/api/p;->a:Lcom/zopim/android/sdk/api/HttpRequest$ProgressListener;

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/zopim/android/sdk/api/p;->a:Lcom/zopim/android/sdk/api/HttpRequest$ProgressListener;

    const/4 v1, 0x0

    aget-object p1, p1, v1

    invoke-virtual {p1}, Ljava/lang/Integer;->intValue()I

    move-result p1

    invoke-interface {v0, p1}, Lcom/zopim/android/sdk/api/HttpRequest$ProgressListener;->onProgressUpdate(I)V

    :cond_0
    return-void
.end method

.method protected synthetic doInBackground([Ljava/lang/Object;)Ljava/lang/Object;
    .locals 0

    check-cast p1, [Landroid/util/Pair;

    invoke-virtual {p0, p1}, Lcom/zopim/android/sdk/api/p;->a([Landroid/util/Pair;)Ljava/lang/Void;

    move-result-object p1

    return-object p1
.end method

.method protected synthetic onPostExecute(Ljava/lang/Object;)V
    .locals 0

    check-cast p1, Ljava/lang/Void;

    invoke-virtual {p0, p1}, Lcom/zopim/android/sdk/api/p;->a(Ljava/lang/Void;)V

    return-void
.end method

.method protected synthetic onProgressUpdate([Ljava/lang/Object;)V
    .locals 0

    check-cast p1, [Ljava/lang/Integer;

    invoke-virtual {p0, p1}, Lcom/zopim/android/sdk/api/p;->a([Ljava/lang/Integer;)V

    return-void
.end method
