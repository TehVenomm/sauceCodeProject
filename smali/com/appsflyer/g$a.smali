.class final Lcom/appsflyer/g$a;
.super Ljava/lang/Object;
.source ""


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/appsflyer/g;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x18
    name = "a"
.end annotation


# instance fields
.field private final ˋ:Ljava/lang/String;

.field private final ˏ:Ljava/lang/String;

.field private final ॱ:Ljava/lang/String;


# direct methods
.method constructor <init>()V
    .locals 0

    .line 3016
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method constructor <init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    .locals 0
    .param p1    # Ljava/lang/String;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .param p2    # Ljava/lang/String;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param
    .param p3    # Ljava/lang/String;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param

    .line 113
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 114
    iput-object p1, p0, Lcom/appsflyer/g$a;->ˋ:Ljava/lang/String;

    .line 115
    iput-object p2, p0, Lcom/appsflyer/g$a;->ॱ:Ljava/lang/String;

    .line 116
    iput-object p3, p0, Lcom/appsflyer/g$a;->ˏ:Ljava/lang/String;

    return-void
.end method

.method static ˎ(Landroid/content/Context;Ljava/lang/String;)Z
    .locals 2

    .line 3035
    invoke-static {p0, p1}, Landroidx/core/content/ContextCompat;->checkSelfPermission(Landroid/content/Context;Ljava/lang/String;)I

    move-result p0

    .line 3037
    new-instance v0, Ljava/lang/StringBuilder;

    const-string v1, "is Permission Available: "

    invoke-direct {v0, v1}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    invoke-virtual {v0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p1, "; res: "

    invoke-virtual {v0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, p0}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-static {p1}, Lcom/appsflyer/AFLogger;->afRDLog(Ljava/lang/String;)V

    if-nez p0, :cond_0

    const/4 p0, 0x1

    return p0

    :cond_0
    const/4 p0, 0x0

    return p0
.end method


# virtual methods
.method final ˊ()Ljava/lang/String;
    .locals 1
    .annotation build Landroidx/annotation/Nullable;
    .end annotation

    .line 125
    iget-object v0, p0, Lcom/appsflyer/g$a;->ॱ:Ljava/lang/String;

    return-object v0
.end method

.method final ˎ()Ljava/lang/String;
    .locals 1
    .annotation build Landroidx/annotation/Nullable;
    .end annotation

    .line 130
    iget-object v0, p0, Lcom/appsflyer/g$a;->ˏ:Ljava/lang/String;

    return-object v0
.end method

.method final ॱ()Ljava/lang/String;
    .locals 1

    .line 120
    iget-object v0, p0, Lcom/appsflyer/g$a;->ˋ:Ljava/lang/String;

    return-object v0
.end method
