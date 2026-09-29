.class Lcom/helpshift/app/CampaignAppLifeCycleListener$2;
.super Ljava/lang/Object;
.source "CampaignAppLifeCycleListener.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/app/CampaignAppLifeCycleListener;->onAppForeground(Landroid/content/Context;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/app/CampaignAppLifeCycleListener;


# direct methods
.method constructor <init>(Lcom/helpshift/app/CampaignAppLifeCycleListener;)V
    .locals 0

    .line 50
    iput-object p1, p0, Lcom/helpshift/app/CampaignAppLifeCycleListener$2;->this$0:Lcom/helpshift/app/CampaignAppLifeCycleListener;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 2

    .line 53
    iget-object v0, p0, Lcom/helpshift/app/CampaignAppLifeCycleListener$2;->this$0:Lcom/helpshift/app/CampaignAppLifeCycleListener;

    invoke-static {v0}, Lcom/helpshift/app/CampaignAppLifeCycleListener;->access$000(Lcom/helpshift/app/CampaignAppLifeCycleListener;)Ljava/util/concurrent/LinkedBlockingDeque;

    move-result-object v0

    invoke-virtual {v0}, Ljava/util/concurrent/LinkedBlockingDeque;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/app/LifecycleListener;

    .line 54
    invoke-interface {v1}, Lcom/helpshift/app/LifecycleListener;->onForeground()V

    goto :goto_0

    :cond_0
    return-void
.end method
