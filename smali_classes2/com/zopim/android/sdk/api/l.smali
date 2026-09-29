.class Lcom/zopim/android/sdk/api/l;
.super Ljava/lang/Object;

# interfaces
.implements Lcom/zopim/android/sdk/api/ErrorResponse;


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/zopim/android/sdk/api/l$a;
    }
.end annotation


# instance fields
.field private a:Lcom/zopim/android/sdk/api/ErrorResponse$Kind;

.field private b:Ljava/lang/String;

.field private c:I

.field private d:Ljava/lang/String;

.field private e:Ljava/lang/String;

.field private f:Ljava/lang/String;


# direct methods
.method private constructor <init>()V
    .locals 0

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method private constructor <init>(Lcom/zopim/android/sdk/api/l$a;)V
    .locals 1

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    invoke-static {p1}, Lcom/zopim/android/sdk/api/l$a;->a(Lcom/zopim/android/sdk/api/l$a;)Lcom/zopim/android/sdk/api/ErrorResponse$Kind;

    move-result-object v0

    iput-object v0, p0, Lcom/zopim/android/sdk/api/l;->a:Lcom/zopim/android/sdk/api/ErrorResponse$Kind;

    invoke-static {p1}, Lcom/zopim/android/sdk/api/l$a;->b(Lcom/zopim/android/sdk/api/l$a;)Ljava/lang/String;

    move-result-object v0

    iput-object v0, p0, Lcom/zopim/android/sdk/api/l;->b:Ljava/lang/String;

    invoke-static {p1}, Lcom/zopim/android/sdk/api/l$a;->c(Lcom/zopim/android/sdk/api/l$a;)I

    move-result v0

    iput v0, p0, Lcom/zopim/android/sdk/api/l;->c:I

    invoke-static {p1}, Lcom/zopim/android/sdk/api/l$a;->d(Lcom/zopim/android/sdk/api/l$a;)Ljava/lang/String;

    move-result-object v0

    iput-object v0, p0, Lcom/zopim/android/sdk/api/l;->d:Ljava/lang/String;

    invoke-static {p1}, Lcom/zopim/android/sdk/api/l$a;->e(Lcom/zopim/android/sdk/api/l$a;)Ljava/lang/String;

    move-result-object v0

    iput-object v0, p0, Lcom/zopim/android/sdk/api/l;->e:Ljava/lang/String;

    invoke-static {p1}, Lcom/zopim/android/sdk/api/l$a;->f(Lcom/zopim/android/sdk/api/l$a;)Ljava/lang/String;

    move-result-object p1

    iput-object p1, p0, Lcom/zopim/android/sdk/api/l;->f:Ljava/lang/String;

    return-void
.end method

.method synthetic constructor <init>(Lcom/zopim/android/sdk/api/l$a;Lcom/zopim/android/sdk/api/m;)V
    .locals 0

    invoke-direct {p0, p1}, Lcom/zopim/android/sdk/api/l;-><init>(Lcom/zopim/android/sdk/api/l$a;)V

    return-void
.end method


# virtual methods
.method public a()Ljava/lang/String;
    .locals 1

    const/4 v0, 0x0

    return-object v0
.end method

.method public toString()Ljava/lang/String;
    .locals 2

    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "kind:"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/zopim/android/sdk/api/l;->a:Lcom/zopim/android/sdk/api/ErrorResponse$Kind;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    const-string v1, " reason:"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/zopim/android/sdk/api/l;->b:Ljava/lang/String;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " status:"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget v1, p0, Lcom/zopim/android/sdk/api/l;->c:I

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const-string v1, " response:"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/zopim/android/sdk/api/l;->e:Ljava/lang/String;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " url:"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/zopim/android/sdk/api/l;->d:Ljava/lang/String;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method
