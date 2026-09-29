.class public Lcom/zopim/android/sdk/model/ChatLog$Option;
.super Ljava/lang/Object;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/zopim/android/sdk/model/ChatLog;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x9
    name = "Option"
.end annotation


# instance fields
.field private label:Ljava/lang/String;

.field private selected:Z


# direct methods
.method private constructor <init>()V
    .locals 0

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public constructor <init>(Ljava/lang/String;)V
    .locals 2

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    if-nez p1, :cond_0

    invoke-static {}, Lcom/zopim/android/sdk/model/ChatLog;->access$000()Ljava/lang/String;

    move-result-object v0

    const-string v1, "Option label not assigned"

    invoke-static {v0, v1}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    const-string v0, ""

    iput-object v0, p0, Lcom/zopim/android/sdk/model/ChatLog$Option;->label:Ljava/lang/String;

    :cond_0
    iput-object p1, p0, Lcom/zopim/android/sdk/model/ChatLog$Option;->label:Ljava/lang/String;

    const/4 p1, 0x0

    iput-boolean p1, p0, Lcom/zopim/android/sdk/model/ChatLog$Option;->selected:Z

    return-void
.end method


# virtual methods
.method public getLabel()Ljava/lang/String;
    .locals 1
    .annotation build Landroidx/annotation/NonNull;
    .end annotation

    iget-object v0, p0, Lcom/zopim/android/sdk/model/ChatLog$Option;->label:Ljava/lang/String;

    return-object v0
.end method

.method public isSelected()Z
    .locals 1
    .annotation build Landroidx/annotation/NonNull;
    .end annotation

    iget-boolean v0, p0, Lcom/zopim/android/sdk/model/ChatLog$Option;->selected:Z

    return v0
.end method

.method public select()V
    .locals 1

    const/4 v0, 0x1

    iput-boolean v0, p0, Lcom/zopim/android/sdk/model/ChatLog$Option;->selected:Z

    return-void
.end method
