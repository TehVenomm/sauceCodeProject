.class Lcom/zopim/android/sdk/api/o;
.super Lcom/zopim/android/sdk/api/u;


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "Lcom/zopim/android/sdk/api/u<",
        "Ljava/io/File;",
        ">;"
    }
.end annotation


# instance fields
.field final synthetic a:Lcom/zopim/android/sdk/api/n;


# direct methods
.method constructor <init>(Lcom/zopim/android/sdk/api/n;)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/api/o;->a:Lcom/zopim/android/sdk/api/n;

    invoke-direct {p0}, Lcom/zopim/android/sdk/api/u;-><init>()V

    return-void
.end method


# virtual methods
.method public a(Lcom/zopim/android/sdk/api/ErrorResponse;)V
    .locals 3

    invoke-static {}, Lcom/zopim/android/sdk/api/n;->a()Ljava/lang/String;

    move-result-object v0

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Error occurred. Reason: "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-interface {p1}, Lcom/zopim/android/sdk/api/ErrorResponse;->a()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    iget-object v0, p0, Lcom/zopim/android/sdk/api/o;->a:Lcom/zopim/android/sdk/api/n;

    invoke-static {v0, p1}, Lcom/zopim/android/sdk/api/n;->a(Lcom/zopim/android/sdk/api/n;Lcom/zopim/android/sdk/api/ErrorResponse;)Lcom/zopim/android/sdk/api/ErrorResponse;

    return-void
.end method

.method public a(Ljava/io/File;)V
    .locals 1

    iget-object v0, p0, Lcom/zopim/android/sdk/api/o;->a:Lcom/zopim/android/sdk/api/n;

    invoke-static {v0, p1}, Lcom/zopim/android/sdk/api/n;->a(Lcom/zopim/android/sdk/api/n;Ljava/io/File;)Ljava/io/File;

    return-void
.end method

.method public bridge synthetic a(Ljava/lang/Object;)V
    .locals 0

    check-cast p1, Ljava/io/File;

    invoke-virtual {p0, p1}, Lcom/zopim/android/sdk/api/o;->a(Ljava/io/File;)V

    return-void
.end method
