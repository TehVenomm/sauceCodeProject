.class Lcom/helpshift/common/platform/AndroidPlatform$1;
.super Ljava/lang/Object;
.source "AndroidPlatform.java"

# interfaces
.implements Lcom/helpshift/common/domain/Threader;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/common/platform/AndroidPlatform;->getUIThreader()Lcom/helpshift/common/domain/Threader;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/common/platform/AndroidPlatform;


# direct methods
.method constructor <init>(Lcom/helpshift/common/platform/AndroidPlatform;)V
    .locals 0

    .line 250
    iput-object p1, p0, Lcom/helpshift/common/platform/AndroidPlatform$1;->this$0:Lcom/helpshift/common/platform/AndroidPlatform;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public thread(Lcom/helpshift/common/domain/F;)Lcom/helpshift/common/domain/F;
    .locals 1

    .line 253
    new-instance v0, Lcom/helpshift/common/platform/AndroidPlatform$1$1;

    invoke-direct {v0, p0, p1}, Lcom/helpshift/common/platform/AndroidPlatform$1$1;-><init>(Lcom/helpshift/common/platform/AndroidPlatform$1;Lcom/helpshift/common/domain/F;)V

    return-object v0
.end method
