.class Lcom/zopim/android/sdk/api/r;
.super Ljava/lang/Object;

# interfaces
.implements Lcom/zopim/android/sdk/api/HttpRequest$ProgressListener;


# instance fields
.field a:I

.field final synthetic b:Lcom/zopim/android/sdk/api/p;


# direct methods
.method constructor <init>(Lcom/zopim/android/sdk/api/p;)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/api/r;->b:Lcom/zopim/android/sdk/api/p;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const/4 p1, 0x0

    iput p1, p0, Lcom/zopim/android/sdk/api/r;->a:I

    return-void
.end method


# virtual methods
.method public onProgressUpdate(I)V
    .locals 3

    iget v0, p0, Lcom/zopim/android/sdk/api/r;->a:I

    if-le p1, v0, :cond_0

    iput p1, p0, Lcom/zopim/android/sdk/api/r;->a:I

    iget-object v0, p0, Lcom/zopim/android/sdk/api/r;->b:Lcom/zopim/android/sdk/api/p;

    const/4 v1, 0x1

    new-array v1, v1, [Ljava/lang/Integer;

    const/4 v2, 0x0

    invoke-static {p1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p1

    aput-object p1, v1, v2

    invoke-static {v0, v1}, Lcom/zopim/android/sdk/api/p;->a(Lcom/zopim/android/sdk/api/p;[Ljava/lang/Object;)V

    :cond_0
    return-void
.end method
