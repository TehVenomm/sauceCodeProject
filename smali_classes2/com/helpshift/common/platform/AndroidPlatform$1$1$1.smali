.class Lcom/helpshift/common/platform/AndroidPlatform$1$1$1;
.super Ljava/lang/Object;
.source "AndroidPlatform.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/common/platform/AndroidPlatform$1$1;->f()V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$2:Lcom/helpshift/common/platform/AndroidPlatform$1$1;


# direct methods
.method constructor <init>(Lcom/helpshift/common/platform/AndroidPlatform$1$1;)V
    .locals 0

    .line 256
    iput-object p1, p0, Lcom/helpshift/common/platform/AndroidPlatform$1$1$1;->this$2:Lcom/helpshift/common/platform/AndroidPlatform$1$1;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 1

    .line 259
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidPlatform$1$1$1;->this$2:Lcom/helpshift/common/platform/AndroidPlatform$1$1;

    iget-object v0, v0, Lcom/helpshift/common/platform/AndroidPlatform$1$1;->val$f:Lcom/helpshift/common/domain/F;

    invoke-virtual {v0}, Lcom/helpshift/common/domain/F;->f()V

    return-void
.end method
