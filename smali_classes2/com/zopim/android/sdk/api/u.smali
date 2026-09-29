.class abstract Lcom/zopim/android/sdk/api/u;
.super Ljava/lang/Object;


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "<T:",
        "Ljava/lang/Object;",
        ">",
        "Ljava/lang/Object;"
    }
.end annotation


# instance fields
.field private a:Z


# direct methods
.method constructor <init>()V
    .locals 1

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const/4 v0, 0x1

    iput-boolean v0, p0, Lcom/zopim/android/sdk/api/u;->a:Z

    return-void
.end method


# virtual methods
.method public abstract a(Lcom/zopim/android/sdk/api/ErrorResponse;)V
.end method

.method public abstract a(Ljava/lang/Object;)V
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(TT;)V"
        }
    .end annotation
.end method

.method b(Lcom/zopim/android/sdk/api/ErrorResponse;)V
    .locals 1

    iget-boolean v0, p0, Lcom/zopim/android/sdk/api/u;->a:Z

    if-eqz v0, :cond_0

    invoke-virtual {p0, p1}, Lcom/zopim/android/sdk/api/u;->a(Lcom/zopim/android/sdk/api/ErrorResponse;)V

    :cond_0
    return-void
.end method

.method b(Ljava/lang/Object;)V
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(TT;)V"
        }
    .end annotation

    iget-boolean v0, p0, Lcom/zopim/android/sdk/api/u;->a:Z

    if-eqz v0, :cond_0

    invoke-virtual {p0, p1}, Lcom/zopim/android/sdk/api/u;->a(Ljava/lang/Object;)V

    :cond_0
    return-void
.end method
