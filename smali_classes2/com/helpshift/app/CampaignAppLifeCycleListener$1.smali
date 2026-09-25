.class Lcom/helpshift/app/CampaignAppLifeCycleListener$1;
.super Ljava/lang/Object;
.source "CampaignAppLifeCycleListener.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/app/CampaignAppLifeCycleListener;->addLifecycleListener(Lcom/helpshift/app/LifecycleListener;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/app/CampaignAppLifeCycleListener;

.field final synthetic val$listener:Lcom/helpshift/app/LifecycleListener;


# direct methods
.method constructor <init>(Lcom/helpshift/app/CampaignAppLifeCycleListener;Lcom/helpshift/app/LifecycleListener;)V
    .locals 0

    .line 28
    iput-object p1, p0, Lcom/helpshift/app/CampaignAppLifeCycleListener$1;->this$0:Lcom/helpshift/app/CampaignAppLifeCycleListener;

    iput-object p2, p0, Lcom/helpshift/app/CampaignAppLifeCycleListener$1;->val$listener:Lcom/helpshift/app/LifecycleListener;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 1

    .line 31
    invoke-static {}, Lcom/helpshift/applifecycle/HSAppLifeCycleController;->getInstance()Lcom/helpshift/applifecycle/HSAppLifeCycleController;

    move-result-object v0

    invoke-virtual {v0}, Lcom/helpshift/applifecycle/HSAppLifeCycleController;->isAppInForeground()Z

    move-result v0

    if-eqz v0, :cond_0

    .line 32
    iget-object v0, p0, Lcom/helpshift/app/CampaignAppLifeCycleListener$1;->val$listener:Lcom/helpshift/app/LifecycleListener;

    invoke-interface {v0}, Lcom/helpshift/app/LifecycleListener;->onForeground()V

    :cond_0
    return-void
.end method
