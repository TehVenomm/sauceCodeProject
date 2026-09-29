.class final Lcom/helpshift/CoreInternal$4;
.super Ljava/lang/Object;
.source "CoreInternal.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/CoreInternal;->registerDeviceToken(Landroid/content/Context;Ljava/lang/String;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x8
    name = null
.end annotation


# instance fields
.field final synthetic val$context:Landroid/content/Context;

.field final synthetic val$deviceToken:Ljava/lang/String;


# direct methods
.method constructor <init>(Ljava/lang/String;Landroid/content/Context;)V
    .locals 0

    .line 126
    iput-object p1, p0, Lcom/helpshift/CoreInternal$4;->val$deviceToken:Ljava/lang/String;

    iput-object p2, p0, Lcom/helpshift/CoreInternal$4;->val$context:Landroid/content/Context;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 3

    const-string v0, "Helpshift_CoreInternal"

    .line 129
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Registering push token : "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v2, p0, Lcom/helpshift/CoreInternal$4;->val$deviceToken:Ljava/lang/String;

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;)V

    .line 130
    sget-object v0, Lcom/helpshift/CoreInternal;->apiProvider:Lcom/helpshift/Core$ApiProvider;

    iget-object v1, p0, Lcom/helpshift/CoreInternal$4;->val$context:Landroid/content/Context;

    iget-object v2, p0, Lcom/helpshift/CoreInternal$4;->val$deviceToken:Ljava/lang/String;

    invoke-interface {v0, v1, v2}, Lcom/helpshift/Core$ApiProvider;->_registerDeviceToken(Landroid/content/Context;Ljava/lang/String;)V

    return-void
.end method
