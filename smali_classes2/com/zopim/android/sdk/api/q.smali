.class Lcom/zopim/android/sdk/api/q;
.super Lcom/zopim/android/sdk/api/u;


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "Lcom/zopim/android/sdk/api/u<",
        "Ljava/lang/Void;",
        ">;"
    }
.end annotation


# instance fields
.field final synthetic a:Lcom/zopim/android/sdk/api/p;


# direct methods
.method constructor <init>(Lcom/zopim/android/sdk/api/p;)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/api/q;->a:Lcom/zopim/android/sdk/api/p;

    invoke-direct {p0}, Lcom/zopim/android/sdk/api/u;-><init>()V

    return-void
.end method


# virtual methods
.method public a(Lcom/zopim/android/sdk/api/ErrorResponse;)V
    .locals 3

    invoke-static {}, Lcom/zopim/android/sdk/api/p;->a()Ljava/lang/String;

    move-result-object v0

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Error occurred. Reason: "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    iget-object v0, p0, Lcom/zopim/android/sdk/api/q;->a:Lcom/zopim/android/sdk/api/p;

    invoke-static {v0, p1}, Lcom/zopim/android/sdk/api/p;->a(Lcom/zopim/android/sdk/api/p;Lcom/zopim/android/sdk/api/ErrorResponse;)Lcom/zopim/android/sdk/api/ErrorResponse;

    return-void
.end method

.method public bridge synthetic a(Ljava/lang/Object;)V
    .locals 0

    check-cast p1, Ljava/lang/Void;

    invoke-virtual {p0, p1}, Lcom/zopim/android/sdk/api/q;->a(Ljava/lang/Void;)V

    return-void
.end method

.method public a(Ljava/lang/Void;)V
    .locals 1

    iget-object p1, p0, Lcom/zopim/android/sdk/api/q;->a:Lcom/zopim/android/sdk/api/p;

    const/4 v0, 0x1

    invoke-static {p1, v0}, Lcom/zopim/android/sdk/api/p;->a(Lcom/zopim/android/sdk/api/p;Z)Z

    return-void
.end method
