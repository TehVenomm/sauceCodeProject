.class Lcom/helpshift/common/platform/AndroidPlatform$1$1;
.super Lcom/helpshift/common/domain/F;
.source "AndroidPlatform.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/common/platform/AndroidPlatform$1;->thread(Lcom/helpshift/common/domain/F;)Lcom/helpshift/common/domain/F;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$1:Lcom/helpshift/common/platform/AndroidPlatform$1;

.field final synthetic val$f:Lcom/helpshift/common/domain/F;


# direct methods
.method constructor <init>(Lcom/helpshift/common/platform/AndroidPlatform$1;Lcom/helpshift/common/domain/F;)V
    .locals 0

    .line 253
    iput-object p1, p0, Lcom/helpshift/common/platform/AndroidPlatform$1$1;->this$1:Lcom/helpshift/common/platform/AndroidPlatform$1;

    iput-object p2, p0, Lcom/helpshift/common/platform/AndroidPlatform$1$1;->val$f:Lcom/helpshift/common/domain/F;

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 2

    .line 256
    new-instance v0, Landroid/os/Handler;

    invoke-static {}, Landroid/os/Looper;->getMainLooper()Landroid/os/Looper;

    move-result-object v1

    invoke-direct {v0, v1}, Landroid/os/Handler;-><init>(Landroid/os/Looper;)V

    new-instance v1, Lcom/helpshift/common/platform/AndroidPlatform$1$1$1;

    invoke-direct {v1, p0}, Lcom/helpshift/common/platform/AndroidPlatform$1$1$1;-><init>(Lcom/helpshift/common/platform/AndroidPlatform$1$1;)V

    invoke-virtual {v0, v1}, Landroid/os/Handler;->post(Ljava/lang/Runnable;)Z

    return-void
.end method
